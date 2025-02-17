using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe;
using ACEL.ENT;
using OfficeOpenXml;
using System.Data;
using System.Text;
using System.Web.Security;
using ACEL.BO.bo;
using System.Web.Script.Serialization;
using static Microsoft.IO.RecyclableMemoryStreamManager;
using System.Configuration;

namespace ACEL.WEB.pages.operacion
{
    public partial class clientes : System.Web.UI.Page//cambio 1
    {
        #region --> Atributos <--------------------------
        private DataTable dtUsuario;
        private DataTable dtBusqueda;
        private DataTable dtPagos;
        #endregion

        #region --> ViewState <--------------------------
        public DataTable dtUsuario_VS
        {
            get
            {
                if (ViewState["dtUsuario"] != null)
                {

                    return (DataTable)ViewState["dtUsuario"];

                }

                else
                {

                    return dtUsuario;

                }
            }
            set { ViewState["dtUsuario"] = value; }
        }
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
        public DataTable dtPagos_VS
        {
            get
            {
                if (ViewState["dtPagos"] != null)
                {

                    return (DataTable)ViewState["dtPagos"];

                }

                else
                {

                    return dtPagos;

                }
            }
            set { ViewState["dtPagos"] = value; }
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
        private void CrearEntidadUsuario()
        {
            dtUsuario = new DataTable();
            dtUsuario.Columns.Add("idUsuario");
            dtUsuario.Columns.Add("CveUsuario");
            dtUsuario.Columns.Add("NombreUsuario");
            dtUsuario.Columns.Add("Apellidos");
            dtUsuario.Columns.Add("Perfil");
            dtUsuario.Columns.Add("Acceso");
            dtUsuario.Columns.Add("Puesto");
            dtUsuario_VS = dtUsuario;

            DataTable dt = dtUsuario_VS;
            DataRow drFila = dt.NewRow();

            ACEL_CUENTA_USUARIOS1 eiUsuario = new ACEL_CUENTA_USUARIOS1();

            try { eiUsuario = new boACEL_USUARIOS().Buscarid(1, 1, long.Parse(HttpContext.Current.User.Identity.Name)); }
            catch
            {
                MuestraMensaje("Usuario invalido, debe iniciar sesion nuevamente");
                FormsAuthentication.SignOut();
                Session.Abandon();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
            if (eiUsuario == null)
            {
                MuestraMensaje("Usuario invalido, debe iniciar sesion nuevamente");
                FormsAuthentication.SignOut();
                Session.Abandon();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
            drFila["idUsuario"] = long.Parse(HttpContext.Current.User.Identity.Name);
            drFila["CveUsuario"] = eiUsuario.Cve;
            drFila["NombreUsuario"] = eiUsuario.NomComercial;
            drFila["Apellidos"] = eiUsuario.Apellidos;
            drFila["Perfil"] = eiUsuario.TipoUsuario;
            drFila["Acceso"] = eiUsuario.NivelAcceso;
            drFila["Puesto"] = eiUsuario.Puesto;
            dt.Rows.Add(drFila);
            dtUsuario_VS = dt;

            ltrNomUsuario.Text = dtUsuario_VS.Rows[0]["NombreUsuario"].ToString();
            ltrPuestoUsuario.Text = dtUsuario_VS.Rows[0]["Puesto"].ToString();
            ltrNomUsuario2.Text = ltrNomUsuario.Text;
            //ltrPantallaActiva.Text = "CONFIGURACIONES";
            ltrPantallaSecundaria.Text = "Inversionistas";

        }
        private void Inicio()
        {
            CrearEntidadUsuario();
            //ltrNombre.Text = dtUsuario_VS.Rows[0]["NombreUsuario"].ToString();
            //ltrPuesto.Text = dtUsuario_VS.Rows[0]["Puesto"].ToString();
            //ltrNomUsuario2.Text = ltrNombre.Text;
            //ltrPantallaActiva.Text = "CONFIGURACIONES";
            //ltrPantallaSecundaria.Text = "Usuarios";
            LimpiaDatos();
            LimpiaConsulta();
            //if (dtUsuario_VS.Rows[0]["Perfil"].ToString() == "ADMIN" && long.Parse(dtUsuario_VS.Rows[0]["Acceso"].ToString()) <= 2)
            //{

            //CargaCombosClientes();
            //PresentaGridBusqueda(true);
            //btnAlta.Visible = true;
            //btnGuardar.Visible = true;
            //txtUsuario.Visible = true;
            //lblUsuario.Visible = false;
            //panDatosSirscom.Visible = true;
            //}
            //else
            //{
            //PresentaGridBusqueda(false);
            //btnAlta.Visible = false;
            //btnConsulta.Visible = false;
            //btnGuardar.Visible = true;
            //txtUsuario.Visible = false;
            //lblUsuario.Visible = true;
            //panDatosSirscom.Visible = false;

            //}
            //TipoAmbiente("BUSQUEDA");
            //CargaComboClientes();
            //CargaComboEventos();
            //SessionTimeoutLiteral.Text = "<script type='text/javascript'>sessionTimeout = " + (Session.Timeout * 60) + ";</script>";
        }
        private void TipoAmbiente(string pTipo)
        {
            switch (pTipo)
            {
                case "BUSQUEDA":
                    panConsulta.Style["display"] = "block"; // ✅ Se muestra el panel de búsqueda
                    panDatos.Style["display"] = "none"; // ❌ Se oculta el de datos
                    //btnConsulta.Style["display"] = "none";
                    //btnAlta.Style["display"] = "block";
                    break;

                case "DATOS":
                    panConsulta.Style["display"] = "none"; // ❌ Se oculta el panel de búsqueda
                    panDatos.Style["display"] = "block"; // ✅ Se muestra el panel de datos
                    //btnConsulta.Style["display"] = "block";
                    //btnAlta.Style["display"] = "none";
                    break;
            }
        }

        private void CrearTablaBusqueda()
        {
            dtBusqueda = new DataTable();
            dtBusqueda.Columns.Add("id");
            dtBusqueda.Columns.Add("NomComercial");
            dtBusqueda.Columns.Add("TipoCliente");
            dtBusqueda.Columns.Add("Correo");
            dtBusqueda.Columns.Add("Evento");
            dtBusqueda.Columns.Add("Certificados");
            dtBusqueda.Columns.Add("Status");

            dtBusqueda_VS = dtBusqueda;
        }
        private void CreaTablaPagos()
        {
            dtPagos = new DataTable();
            dtPagos.Columns.Add("Descripcion");
            dtPagos.Columns.Add("Fecha");
            dtPagos.Columns.Add("Monto");
            dtPagos.Columns.Add("Status");

            dtPagos_VS = dtPagos;
        }
        private void LlenarTablaPagos(long pidInversionista, ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiEscala)
        {
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            dtPagos.Clear();
            DataTable dt = dtPagos;
            ACEL_CUENTA_INVERSIONISTAS eiInv = new ACEL_CUENTA_INVERSIONISTAS();
            eiInv = new boACEL_CUENTA_INVERSIONISTAS().Buscarid(1, 1, pidInversionista);
            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
            eiEscala = peiEscala;
            decimal Total = (eiEscala.ValorNominal.Value * eiInv.CantidadCertificados.Value);
            decimal Anticipo = eiEscala.Aticipo.Value;
            decimal Enganche;
            Enganche = Total * decimal.Parse("0." + eiEscala.EngancheLiquidacion.Value.ToString("N0"));
            DataRow drFila;
            drFila = dt.NewRow();
            drFila["Descripcion"] = "Anticipo";
            drFila["Fecha"] = currentDateTime.AddDays(3).ToShortDateString();
            drFila["Monto"] = Anticipo.ToString("N2");
            drFila["Status"] = "PENDIENTE";
            dt.Rows.Add(drFila);

            if (eiEscala.TipoPago == "MENSUALIDADES")
            {
                drFila = dt.NewRow();
                drFila["Descripcion"] = "Enganche";
                drFila["Fecha"] = currentDateTime.AddDays(10).ToShortDateString();
                drFila["Monto"] = Enganche.ToString("N2");
                drFila["Status"] = "PENDIENTE";
                dt.Rows.Add(drFila);
            }
            else
            {
                drFila = dt.NewRow();
                drFila["Descripcion"] = "Liquidación";
                drFila["Fecha"] = currentDateTime.AddDays(10).ToShortDateString();
                drFila["Monto"] = Enganche.ToString("N2");
                drFila["Status"] = "PENDIENTE";
                dt.Rows.Add(drFila);
            }


            for (int i = 0; i < eiEscala.Mensualidad.Value; i++)
            {
                currentDateTime = currentDateTime.AddMonths(1);
                drFila = dt.NewRow();
                drFila["Descripcion"] = "Pago mensualidad " + (i + 1).ToString() + " de " + eiEscala.Mensualidad.Value.ToString("N0");
                drFila["Fecha"] = currentDateTime.ToShortDateString();
                drFila["Monto"] = ((Total - Enganche) / eiEscala.Mensualidad.Value).ToString("N2");
                drFila["Status"] = "PENDIENTE";

                dt.Rows.Add(drFila);
            }

            dtPagos_VS = dt;
        }
        private List<ENT.ACEL_CUENTA_INVERSIONISTAS> Busqueda(bool pAdmin = false)
        {
            bool mNomConf = false;

            //if (txtNomBus.Text != "")
            //    mNomConf = true;
            List<ACEL_CUENTA_INVERSIONISTAS> elCURU = new List<ACEL_CUENTA_INVERSIONISTAS>();
            if (pAdmin)
            {
                elCURU = new boACEL_CUENTA_INVERSIONISTAS().Buscar(1, 1);

            }


            //if (mNomConf)
            //{
            //    elCURU = (from tp in elCURU
            //              where tp.NombreEvento.Contains(txtNomBus.Text)
            //              select tp).ToList();
            //}

            return elCURU;
        }
        private void LlenarBusqueda(List<ENT.ACEL_CUENTA_INVERSIONISTAS> pelRG = null)
        {
            dtBusqueda.Clear();
            DataTable dt = dtBusqueda;
            foreach (ENT.ACEL_CUENTA_INVERSIONISTAS eiAlmacen in pelRG)
            {
                DataRow drFila = dt.NewRow();
                drFila["id"] = eiAlmacen.idInversionista;
                drFila["NomComercial"] = eiAlmacen.NomComercial;

                if (eiAlmacen.Cliente == "1")
                {
                    switch (eiAlmacen.TipoInversionista)
                    {
                        case "1":
                            drFila["TipoCliente"] = "SERES DE RIQUEZA";
                            break;
                        case "2":
                            drFila["TipoCliente"] = "INCUBADORA";
                            break;
                    }
                }
                else
                {
                    switch (eiAlmacen.TipoInversionista)
                    {
                        case "1":
                            drFila["TipoCliente"] = "ACCIONISTA";
                            break;
                        case "2":
                            drFila["TipoCliente"] = "PROPIETARIO";
                            break;
                        case "3":
                            drFila["TipoCliente"] = "CO PROPIETARIO";
                            break;
                    }
                }

                drFila["Correo"] = eiAlmacen.CorreoContacto;
                drFila["Evento"] = eiAlmacen.NombreEvento;
                drFila["Certificados"] = eiAlmacen.CantidadCertificados;
                drFila["Status"] = eiAlmacen.Status;
                dt.Rows.Add(drFila);
            }

            dtBusqueda_VS = dt;
        }
        private String PresentaGridBusqueda(bool pAdmin)
        {
            dtBusqueda = new DataTable();
            CrearTablaBusqueda();
            List<ENT.ACEL_CUENTA_INVERSIONISTAS> elAltaCRM2 = new List<ENT.ACEL_CUENTA_INVERSIONISTAS>();
            elAltaCRM2 = Busqueda(pAdmin);
            LlenarBusqueda(elAltaCRM2);
            //HiddenFieldData.Value = DataTableToJson(dtBusqueda_VS);
            String dataJson = DataTableToJson(dtBusqueda_VS);
            //rptConfiguraciones.DataSource = dtBusqueda_VS;
            //rptConfiguraciones.DataBind();
            return dataJson;
        }
        private string DataTableToJson(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                foreach (DataColumn column in dt.Columns)
                {
                    item[column.ColumnName] = row[column].ToString();
                }
                lista.Add(item);
            }

            return serializer.Serialize(lista);
        }
        private void LimpiaDatos()
        {
            
        }
        private void CargaCliente(long pidUsuario)
        {
            /*
            ACEL_CUENTA_INVERSIONISTAS eiCliente = new ACEL_CUENTA_INVERSIONISTAS();
            eiCliente = new boACEL_CUENTA_INVERSIONISTAS().Buscarid(1, 1, pidUsuario);
            if (eiCliente == null)
            {
                MuestraMensaje("Error al cargar el usuario, contacte a sistemas");
                return;
            }
            txtNom.Text = eiCliente.NomComercial;
            txtCorreo.Text = eiCliente.CorreoContacto;
            txtTelefono.Text = eiCliente.TelContacto;
            try { cmbCliente.SelectedValue = eiCliente.Cliente; }
            catch { cmbCliente.SelectedValue = "1"; }
            try { cmbInversionista.SelectedValue = eiCliente.TipoInversionista; }
            catch { cmbInversionista.SelectedValue = "1"; }
            try { cmbTipoPago.SelectedValue = eiCliente.CondicionesPago; }
            catch { cmbTipoPago.SelectedValue = "MENSUALIDADES"; }
            try { cmbEventos.SelectedValue = eiCliente.idEvento.ToString(); }
            catch { cmbEventos.SelectedIndex = 0; }
            hfidRegistro.Value = eiCliente.idInversionista.ToString();
            //txtCertificado.Text = eiCliente.CantidadCertificados.ToString();
            long certificados = 0;
            //long certificados = long.Parse(txtCertificado.Text);
            string tipoPago = cmbTipoPago.SelectedItem.Text;
            string pTipoEscala;

            if (certificados >= 1 && certificados <= 9)
            {
                pTipoEscala = "1-9 CONTADO";
            }
            else if (certificados == 10)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "10 CONTADO";
                }
                else
                {
                    pTipoEscala = "10 MENSUALIDADES";
                }
            }
            else if (certificados >= 11 && certificados <= 39)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "11-39 CONTADO";
                }
                else
                {
                    pTipoEscala = "11-39 MENSUALIDADES";
                }
            }
            else if (certificados == 40)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "40 CONTADO";
                }
                else
                {
                    pTipoEscala = "40 MENSUALIDADES";
                }
            }
            else if (certificados >= 41 && certificados <= 99)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "41-99 CONTADO";
                }
                else
                {
                    pTipoEscala = "41-99 MENSUALIDADES";
                }
            }
            else if (certificados >= 100)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "+ de 100 CONTADO";
                }
                else
                {
                    pTipoEscala = "+ de 100 MENSUALIDADES";
                }
            }
            else
                pTipoEscala = "";



            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, eiCliente.idEvento.Value, cmbInversionista.SelectedItem.Text, pTipoEscala, eiCliente.CondicionesPago);
            CargaTotalPagos(eiEscala, eiCliente.CantidadCertificados.Value);
            CreaTablaPagos();
            LlenarTablaPagos(pidUsuario, eiEscala);
            rptPagos.DataSource = dtPagos_VS;
            rptPagos.DataBind();
            */
        }
        private void CargaComboClientes()
        {
            
        }
        private void CargaInversionista(long pidValorAsociado)
        {
           
        }
        private void CargaComboEventos()
        {
            
        }
        private void LimpiaConsulta()
        {
            //txtNomBus.Text = "";
            //txtFecha1.Text = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString();
            //txtFecha2.Text = DateTime.Now.ToShortDateString();
            //cmbTipoBus.SelectedIndex = 0;
        }
        private void CargaTotalPagos(ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiCert, long pCertificados)
        {
            //decimal Total = (peiCert.ValorNominal.Value * pCertificados);
            //decimal Anticipo = peiCert.Aticipo.Value;
            //decimal Enganche;
            //if (peiCert.TipoPago == "MENSUALIDADES")
            //    Enganche = Total * decimal.Parse("0." + peiCert.EngancheLiquidacion.Value.ToString("N0"));
            //else
            //    Enganche = Total - Anticipo;

            //ltrTotal.Text = Total.ToString("N2");
            //ltrAnticipo.Text = Anticipo.ToString("N2");
            //ltrEnganche.Text = Enganche.ToString("N2");
            //if (peiCert.TipoPago == "MENSUALIDADES")
            //{
            //    try
            //    {
            //        ltrPagos.Text = peiCert.Mensualidad.Value.ToString("N0") + " PAGOS DE $" +
            //          ((Total - Enganche) / peiCert.Mensualidad.Value).ToString("N2");
            //    }
            //    catch { ltrPagos.Text = "0.00"; }
            //}

            //else
            //    ltrPagos.Text = "1 PAGO DE $" +
            //        Enganche.ToString("N2");
        }
        #endregion

        #region --> Eventos <----------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Inicio();
                //if (dtUsuario_VS.Rows[0]["Perfil"].ToString() == "ADMIN")
                //{
                //    panMenuCompleto.Visible = true;
                //    panMenuUsuario.Visible = false;
                //}
                //else
                //{
                //    panMenuCompleto.Visible = false;
                //    panMenuUsuario.Visible = true;
                //    PresentaRPTMenu();
                //}
                //panConsulta.Style["display"] = "block";
                //panDatos.Style["display"] = "none";
            }
        }
        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
            return;
        }
        protected void btnAlta_Click(object sender, EventArgs e)
        {
            //LimpiaDatos();
            //TipoAmbiente("DATOS");
            //ltrTipoOperacion.Text = "Alta de Usuario";
            //Session.Timeout = 20;
        }
        /*
        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            TipoAmbiente("BUSQUEDA");
            Session.Timeout = 20;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OcultarDetallesConsulta", "OcultarDetallesConsulta();", true);
        }
        */
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        protected void rptConfiguraciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Session.Timeout = 20;
            HiddenField hf = (HiddenField)e.Item.FindControl("hfid");

            if (e.CommandName == "ver")
            {
                LimpiaDatos();
                hfidRegistro.Value = hf.Value;

                // ✅ Enviar el ID a la función de JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarDatos",
                    "mostrarDatos(" + hf.Value + ");", true);
            }
        }
        protected void btnConsultar_Click1(object sender, EventArgs e)
        {
            if (dtUsuario_VS.Rows[0]["Perfil"].ToString() == "ADMIN" && long.Parse(dtUsuario_VS.Rows[0]["Acceso"].ToString()) <= 2)
            {
                //CargaCombosClientes();
                //PresentaGridBusqueda(true);
                //btnAlta.Visible = true;
                //btnGuardar.Visible = true;
                //txtUsuario.Visible = true;
                //lblUsuario.Visible = false;
                //panDatosSirscom.Visible = true;
            }
            else
            {
                //PresentaGridBusqueda(false);
                //btnAlta.Visible = false;
                //btnConsulta.Visible = false;
                //btnGuardar.Visible = true;
                //txtUsuario.Visible = false;
                //lblUsuario.Visible = true;
                //panDatosSirscom.Visible = false;
            }
            Session.Timeout = 20;
            System.Threading.Thread.Sleep(2000);
        }
        protected void btnLimpiar_Click1(object sender, EventArgs e)
        {
            LimpiaConsulta();
            Session.Timeout = 20;
        }
        protected void lnkbSalir_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
            return;
        }
        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //string menuItem = DataBinder.Eval(e.Item.DataItem, "Modulo").ToString(); // Asegúrate de que el tipo de datos es correcto
                //var rptPaginas = (Repeater)e.Item.FindControl("rptPaginas");

                //CrearTablaMenuPantalla();
                //List<SIRSCOM_CUENTA_PANTALLAS_ACCESOS_USUARIO> elPantallas = new List<SIRSCOM_CUENTA_PANTALLAS_ACCESOS_USUARIO>();
                //elPantallas = new boSIRSCOM_CUENTA_PANTALLAS_ACCESOS_USUARIO().BuscarPantallasModuloUsuario(1, 1, long.Parse(HttpContext.Current.User.Identity.Name), menuItem);
                //LlenarTablaMenuPantalla(elPantallas);
                //rptPaginas.DataSource = dtMenuPantalla_VS;
                //rptPaginas.DataBind();
            }
        }
        #endregion

        protected void cmbInversionista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CargaInversionista(long.Parse(cmbCliente.SelectedValue));
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static string FiltrarInversionistas(string criterio, string orden)
        {
            clientes clienteInstance = new clientes();
            String dataJson = clienteInstance.PresentaGridBusqueda(true);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> inversionistas = serializer.Deserialize<List<Dictionary<string, object>>>(dataJson);

            // Aplicar filtro de Evento
            if (!string.IsNullOrEmpty(criterio))
            {
                inversionistas = inversionistas.Where(x => x["Evento"].ToString().Contains(criterio)).ToList();
            }

            // Aplicar ordenación
            if (orden == "asc")
            {
                inversionistas = inversionistas.OrderBy(x => x["NomComercial"].ToString()).ToList();
            }
            else if (orden == "desc")
            {
                inversionistas = inversionistas.OrderByDescending(x => x["NomComercial"].ToString()).ToList();
            }

            return serializer.Serialize(inversionistas);
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static bool GuardarInversionista(string pidRegistro, string pNom, string pCliente, string pCorreo, string pTelefono, string pCertificado, string pidInversionista, string pInversionista, string pidEventos, string pEventos, string pTipoPago, string pCveUsuario)
        {
            bool flagStatus = false;
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            ACEL_CUENTA_INVERSIONISTAS peiConfigura = new ACEL_CUENTA_INVERSIONISTAS();

            long certificados = long.Parse(pCertificado);

            string tipoPago = pTipoPago;
            string pTipoEscala;

            if (certificados >= 1 && certificados <= 9)
            {
                pTipoEscala = "1-9 CONTADO";
            }
            else if (certificados == 10)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "10 CONTADO";
                }
                else
                {
                    pTipoEscala = "10 MENSUALIDADES";
                }
            }
            else if (certificados >= 11 && certificados <= 39)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "11-39 CONTADO";
                }
                else
                {
                    pTipoEscala = "11-39 MENSUALIDADES";
                }
            }
            else if (certificados == 40)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "40 CONTADO";
                }
                else
                {
                    pTipoEscala = "40 MENSUALIDADES";
                }
            }
            else if (certificados >= 41 && certificados <= 99)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "41-99 CONTADO";
                }
                else
                {
                    pTipoEscala = "41-99 MENSUALIDADES";
                }
            }
            else if (certificados >= 100)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "+ de 100 CONTADO";
                }
                else
                {
                    pTipoEscala = "+ de 100 MENSUALIDADES";
                }
            }
            else
                pTipoEscala = "";
            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscarEscalaPago(1, 1, long.Parse(pidEventos),
                pTipoEscala, pInversionista);
            if (eiEscala == null)
            {
                return false; //Error al buscar la escala
            }

            if (pidRegistro == "0")
            {
                peiConfigura.idBranch = 1;
                peiConfigura.idCuenta = 1;
                peiConfigura.NomComercial = pNom;
                peiConfigura.Cliente = pCliente;
                peiConfigura.CorreoContacto = pCorreo;
                peiConfigura.TelContacto = pTelefono;
                peiConfigura.CantidadCertificados = long.Parse(pCertificado);
                peiConfigura.TipoInversionista = pidInversionista;
                peiConfigura.idEvento = long.Parse(pidEventos);
                peiConfigura.NombreEvento = pEventos;
                peiConfigura.CondicionesPago = pTipoPago;
                peiConfigura.idEscala = eiEscala.idEscala;
                peiConfigura.FechaAlta = currentDateTime;
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuAlta = pCveUsuario;
                peiConfigura.UsuMod = pCveUsuario;
                peiConfigura.Status = "ACTIVO";
                long pidEvento = new boACEL_CUENTA_INVERSIONISTAS().Inserta(peiConfigura);
                if (pidEvento > 0)
                {
                    flagStatus = true; //El registro se dió de alta correctamente
                }
                //peiConfigura.TipoUsuario = cmbTipo.SelectedValue;
            }
            else
            {
                peiConfigura = new boACEL_CUENTA_INVERSIONISTAS().Buscarid(1, 1, long.Parse(pidRegistro));
                peiConfigura.NomComercial = pNom;
                peiConfigura.Cliente = pCliente;
                peiConfigura.CorreoContacto = pCorreo;
                peiConfigura.TelContacto = pTelefono;
                peiConfigura.CantidadCertificados = long.Parse(pCertificado);
                peiConfigura.TipoInversionista = pidInversionista;
                peiConfigura.idEvento = long.Parse(pidEventos);
                peiConfigura.idEscala = eiEscala.idEscala;
                peiConfigura.NombreEvento = pEventos;
                peiConfigura.CondicionesPago = pTipoPago;
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuMod = pCveUsuario; //ID DEL USUARIO QUE EDITA
                if (new boACEL_CUENTA_INVERSIONISTAS().Actualiza(peiConfigura))
                {
                    flagStatus = true; //El registro se modificó correctamente
                }
            }

            return flagStatus;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static string ObtenerInversionista(int idInversionista)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                ACEL_CUENTA_INVERSIONISTAS inversionista = new boACEL_CUENTA_INVERSIONISTAS().Buscarid(1, 1, idInversionista);

                if (inversionista == null)
                    return serializer.Serialize(new { error = "Inversionista no encontrado" });

                // Obtener descripción de Tipo de Inversionista
                string tipoInversionistaTexto = ObtenerDescripcionTipoInversionista(inversionista.Cliente, inversionista.TipoInversionista);

                // Determinar escala
                string tipoEscala = DeterminarTipoEscala(inversionista.CantidadCertificados.Value, inversionista.CondicionesPago);
                ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS escala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS()
                    .BuscartTipoInversionista(1, 1, inversionista.idEvento.Value, tipoInversionistaTexto, tipoEscala, inversionista.CondicionesPago);

                if (escala == null)
                    return serializer.Serialize(new { error = "No se encontró la escala para este inversionista." });

                // Calcular valores del Estado de Cuenta
                decimal total = escala.ValorNominal.Value * inversionista.CantidadCertificados.Value;
                decimal anticipo = escala.Aticipo.Value;
                decimal enganche = escala.TipoPago == "MENSUALIDADES"
                    ? total * decimal.Parse("0." + escala.EngancheLiquidacion.Value.ToString("N0"))
                    : total - anticipo;

                List<Dictionary<string, object>> pagos = GenerarPagos(idInversionista, escala);

                var datos = new
                {
                    id = inversionista.idInversionista,
                    nombre = inversionista.NomComercial,
                    correo = inversionista.CorreoContacto,
                    telefono = inversionista.TelContacto,
                    evento = inversionista.idEvento,
                    certificados = inversionista.CantidadCertificados.Value.ToString("N0"),
                    tipo = inversionista.TipoInversionista,
                    tipoTexto = tipoInversionistaTexto,
                    total = total.ToString("N2"),
                    anticipo = anticipo.ToString("N2"),
                    enganche = enganche.ToString("N2"),
                    pagos = pagos,
                    cliente = inversionista.Cliente,
                    tipoPago = inversionista.CondicionesPago,
                };

                Console.WriteLine("Estado de cuenta generado correctamente:", datos); // DEBUG

                return serializer.Serialize(datos);
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { error = ex.Message });
            }
        }
        private static string ObtenerDescripcionTipoInversionista(string cliente, string tipoInversionista)
        {
            List<ACEL_CUENTA_INDICES> elIndices = new boACEL_INDICES().BuscarValorAsociado(1, 1, long.Parse(cliente));

            ACEL_CUENTA_INDICES tipo = elIndices.FirstOrDefault(x => x.idDetalle.ToString() == tipoInversionista);

            return tipo != null ? tipo.Descripcion : "Desconocido";
        }


        // Método auxiliar para determinar la escala de inversión
        private static string DeterminarTipoEscala(long certificados, string tipoPago)
        {
            if (certificados >= 1 && certificados <= 9) return "1-9 CONTADO";
            if (certificados == 10) return tipoPago == "CONTADO" ? "10 CONTADO" : "10 MENSUALIDADES";
            if (certificados >= 11 && certificados <= 39) return tipoPago == "CONTADO" ? "11-39 CONTADO" : "11-39 MENSUALIDADES";
            if (certificados == 40) return tipoPago == "CONTADO" ? "40 CONTADO" : "40 MENSUALIDADES";
            if (certificados >= 41 && certificados <= 99) return tipoPago == "CONTADO" ? "41-99 CONTADO" : "41-99 MENSUALIDADES";
            if (certificados >= 100) return tipoPago == "CONTADO" ? "+ de 100 CONTADO" : "+ de 100 MENSUALIDADES";
            return "";
        }

        // Método auxiliar para generar pagos del inversionista
        private static List<Dictionary<string, object>> GenerarPagos(long pidInversionista, ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS escala)
        {
            DateTime currentDateTime = DateTime.Now;
            List<Dictionary<string, object>> pagos = new List<Dictionary<string, object>>();

            decimal total = (escala.ValorNominal.Value * 10);
            decimal anticipo = escala.Aticipo.Value;
            decimal enganche = total * decimal.Parse("0." + escala.EngancheLiquidacion.Value.ToString("N0"));

            pagos.Add(new Dictionary<string, object>
            { 
        { "Descripcion", "Anticipo" },
        { "Fecha", currentDateTime.AddDays(3).ToShortDateString() },
        { "Monto", anticipo.ToString("N2") },
        { "Status", "PENDIENTE" }
            });

            if (escala.TipoPago == "MENSUALIDADES")
            {
                pagos.Add(new Dictionary<string, object>
        {
            { "Descripcion", "Enganche" },
            { "Fecha", currentDateTime.AddDays(10).ToShortDateString() },
            { "Monto", enganche.ToString("N2") },
            { "Status", "PENDIENTE" }
        });
            }
            else
            {
                pagos.Add(new Dictionary<string, object>
        {
            { "Descripcion", "Liquidación" },
            { "Fecha", currentDateTime.AddDays(10).ToShortDateString() },
            { "Monto", enganche.ToString("N2") },
            { "Status", "PENDIENTE" }
        });
            }

            return pagos;

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static List<ListItem> llenarInversionistas(string pCliente)
        {

            List<ACEL_CUENTA_INDICES> elIndices = new List<ACEL_CUENTA_INDICES>();
            elIndices = new boACEL_INDICES().BuscarValorAsociado(1, 1, long.Parse(pCliente));
            List<ListItem> inversionistas = new List<ListItem>();
            foreach (ACEL_CUENTA_INDICES eiCont in elIndices)
            {
                inversionistas.Add(new ListItem(eiCont.Descripcion, eiCont.idDetalle.ToString()));
            }
            return inversionistas;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static List<ListItem> llenarClientes()
        {

            List<ACEL_CUENTA_INDICES> elIndices = new List<ACEL_CUENTA_INDICES>();
            elIndices = new boACEL_INDICES().BuscarIndice(1, 1, 1);
            List<ListItem> clientes = new List<ListItem>();
            foreach (ACEL_CUENTA_INDICES eiCont in elIndices)
            {
                clientes.Add(new ListItem(eiCont.Descripcion, eiCont.idDetalle.ToString()));
            }
            return clientes;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static List<ListItem> llenarEvento()
        {
            List<ACEL_CUENTA_EVENTOS> elEventos = new List<ACEL_CUENTA_EVENTOS>();
            elEventos = new boACEL_CUENTA_EVENTOS().Buscar(1, 1);
            List<ListItem> eventos = new List<ListItem>();
            foreach (ACEL_CUENTA_EVENTOS eiEvento in elEventos)
            {
                eventos.Add(new ListItem(eiEvento.NombreEvento, eiEvento.idEvento.ToString()));
            }
            return eventos;
        }

    }


}