using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ACEL.BO;
using ACEL.BO.bo;
using ACEL.ENT;

namespace ACEL.WEB.pages.tesoreria.recibos.impresion
{
    public partial class recibo_pago : System.Web.UI.Page
    {
        #region --> Atributos <--------------------------
        private DataTable dtBusqueda;
        private DataTable dtPartidas;
        private DataTable dtPartidasBundle;
        private DataTable dtPartidasBundle1;
        //private bool isCve1Visible = false;
        //private bool isClaveVisible = false;
        //private bool isNumParteVisible = false;
        private bool isCampoEditable1 = false;
        private bool isCampoEntrega = false;
        //private bool isUnidad = false;
        #endregion

        #region --> ViewState <--------------------------
        public DataTable dtBusqueda_VS
        {
            get
            {
                if (ViewState["dtBusqueda"] != null)
                {

                    return (DataTable)ViewState["dtBusqueda"];

                }

                else
                {

                    return dtBusqueda;

                }
            }
            set { ViewState["dtBusqueda"] = value; }
        }
        public DataTable dtPartidas_VS
        {
            get
            {
                if (ViewState["dtPartidas"] != null)
                {

                    return (DataTable)ViewState["dtPartidas"];

                }

                else
                {

                    return dtPartidas;

                }
            }
            set { ViewState["dtPartidas"] = value; }
        }
        public DataTable dtPartidasBundle_VS
        {
            get
            {
                if (ViewState["dtPartidasBundle"] != null)
                {

                    return (DataTable)ViewState["dtPartidasBundle"];

                }

                else
                {

                    return dtPartidasBundle;

                }
            }
            set { ViewState["dtPartidasBundle"] = value; }
        }
        public DataTable dtPartidasBundle1_VS
        {
            get
            {
                if (ViewState["dtPartidasBundle1"] != null)
                {

                    return (DataTable)ViewState["dtPartidasBundle1"];

                }

                else
                {

                    return dtPartidasBundle1;

                }
            }
            set { ViewState["dtPartidasBundle1"] = value; }
        }
        #endregion

        #region --> Metodos <----------------------------
        private void MuestraMensaje(string pMensaje)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(pMensaje.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }
        private void CrearTablaBusqueda()
        {

            dtBusqueda.Columns.Add("Cantidad");
            dtBusqueda.Columns.Add("Item");
            dtBusqueda.Columns.Add("Descripcion");
            dtBusqueda.Columns.Add("Precio");
            dtBusqueda.Columns.Add("Importe");

            dtBusqueda_VS = dtBusqueda;
        }
        private void CrearTablaPartidas()
        {
            dtPartidas = new DataTable();
            dtPartidas.Columns.Add("id");
            dtPartidas.Columns.Add("Cantidad");
            dtPartidas.Columns.Add("Descripcion");
            dtPartidas.Columns.Add("ClaveInterna");
            dtPartidas.Columns.Add("ClaveExterna");
            dtPartidas.Columns.Add("Modelo");
            dtPartidas.Columns.Add("Precio");
            dtPartidas.Columns.Add("Importe");
            dtPartidas.Columns.Add("Imagen1");
            dtPartidas.Columns.Add("ArchivoAdj");
            dtPartidas.Columns.Add("Editable");
            dtPartidas.Columns.Add("Unidad");
            dtPartidas.Columns.Add("Clave");
            dtPartidas.Columns.Add("Entrega");
            dtPartidas.Columns.Add("Orientacion");

            dtPartidas_VS = dtPartidas;
        }
        private void CrearTablaBundle()
        {
            dtPartidasBundle = new DataTable();
            dtPartidasBundle.Columns.Add("id");
            dtPartidasBundle.Columns.Add("Cantidad");
            dtPartidasBundle.Columns.Add("Descripcion");
            dtPartidasBundle.Columns.Add("Precio");

            dtPartidasBundle_VS = dtPartidasBundle;
        }
        private void CrearTablaBundle1()
        {
            dtPartidasBundle1 = new DataTable();
            dtPartidasBundle1.Columns.Add("idPartida");
            dtPartidasBundle1.Columns.Add("id");
            dtPartidasBundle1.Columns.Add("Cantidad");
            dtPartidasBundle1.Columns.Add("Descripcion");
            dtPartidasBundle1.Columns.Add("Precio");

            dtPartidasBundle1_VS = dtPartidasBundle1;
        }
        private void LlenarTablaPartida(bool pAlta = false, ACEL_CUENTA_INVERSIONISTAS_PAGOS peiPart = null)
        {
            DataTable dt = dtPartidas_VS;
            DataRow drFila = dt.NewRow();
            drFila["id"] = peiPart.idPago.ToString();
            drFila["Cantidad"] = 1;
            drFila["Descripcion"] = peiPart.TipoPago; 
            drFila["Importe"] = peiPart.MontoPago.Value.ToString("N2");
            //try { drFila["Imagen1"] = pImagen1; }
            //catch { drFila["Imagen1"] = ""; }
            dt.Rows.Add(drFila);
            dtPartidas_VS = dt;
        }
       
        private void PresentaGridBusqueda(bool pLink, long pidCotizacion, long pidInversionista)
        {
            ACEL_CUENTA_INVERSIONISTAS_PAGOS eiCot = new ACEL_CUENTA_INVERSIONISTAS_PAGOS();
            eiCot = new boACEL_CUENTA_INVERSIONISTAS_PAGOS().Buscarid(1,1,pidInversionista, pidCotizacion);

            lblCliente.Text = eiCot.ACEL_CUENTA_INVERSIONISTAS.NomComercial;
            lblCertificados.Text = eiCot.ACEL_CUENTA_INVERSIONISTAS.CantidadCertificados.Value.ToString();
            lblFecha.Text = eiCot.FechaPago.Value.Day.ToString() + " de " +
                MonthName(eiCot.FechaPago.Value.Month) + " del " + eiCot.FechaPago.Value.Year.ToString();
            
            lblTotal.Text = eiCot.MontoPago.Value.ToString("N2");
            //TimeSpan difFechas = eiCot.Vigencia.Value - eiCot.FechaCotizacion.Value;
            
            lblVendedor2.Text = "GRANDEZA VERACRUZANA";
            lblCotizacion.Text = eiCot.idPago.ToString();
            
            
            decimal cantidad = eiCot.MontoPago.Value;
            int centavos = (int)((cantidad - Math.Floor(cantidad)) * 100);
            string centavosFormateados = $"{centavos:00}/100 MN";
            lblCentavos.Text = " " + centavosFormateados;

            ltrTipoPago.Text = eiCot.TipoPago;
            ltrMontoPago.Text = eiCot.MontoPago.Value.ToString("N2");
            
            this.Page.Title = "PAGO_" + lblCliente.Text + " + eiCot.idPago";
            ClientScript.RegisterStartupScript(this.GetType(), "ConvertNumberToText",
                    $"window.onload = function() {{ var numeroConvertido = NumeroALetras({eiCot.MontoPago.Value}); document.getElementById('{lblNumeroEnLetras.ClientID}').innerText = numeroConvertido; }};", true);
        }
        
        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }
        private int GetColumnIndexByHeaderText(GridView grid, string headerText)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText == headerText)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region --> Eventos <----------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long pidInversionista = long.Parse(Session["pInversionista"].ToString());
                long pidPago = long.Parse(Session["pPago"].ToString());
                PresentaGridBusqueda(false, pidPago, pidInversionista);
            }
        }

        protected void rptConfiguraciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        #endregion
    }
}