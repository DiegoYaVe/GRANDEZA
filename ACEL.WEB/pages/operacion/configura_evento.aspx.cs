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
using System.IO;

namespace ACEL.WEB.pages.operacion
{
    public partial class configura_eventos : System.Web.UI.Page
    {
        #region --> Atributos <--------------------------
        private DataTable dtUsuario;
        private DataTable dtBusqueda;
        private DataTable dtAsistenciaevento;
        private static string excelFilePath = string.Empty;
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
        public DataTable dtAsistenciaevento_VS
        {
            get
            {
                if (ViewState["dtAsistenciaevento"] != null)
                {

                    return (DataTable)ViewState["dtAsistenciaevento"];

                }

                else
                {

                    return dtAsistenciaevento;

                }
            }
            set { ViewState["dtAsistenciaevento"] = value; }
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
            ltrPantallaSecundaria.Text = "Configura Eventos";
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
            CargaClientes();
            //if (dtUsuario_VS.Rows[0]["Perfil"].ToString() == "ADMIN" && long.Parse(dtUsuario_VS.Rows[0]["Acceso"].ToString()) <= 2)
            //{

                //CargaCombosClientes();
                PresentaGridBusqueda();
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
            TipoAmbiente("BUSQUEDA");
            //SessionTimeoutLiteral.Text = "<script type='text/javascript'>sessionTimeout = " + (Session.Timeout * 60) + ";</script>";


            
        }
        private void TipoAmbiente(string pTipo)
        {
            switch (pTipo)
            {
                
                case "EVENTOS":
                    panEventosActivos.Visible = true;
                    panRegistroEvento.Visible = false;
                    btnConsulta.Visible = false;
                    break;
                case "ASISTENCIA":
                    panEventosActivos.Visible = false;
                    panRegistroEvento.Visible = true;
                    btnConsulta.Visible = true;
                    break;
            }
            //if (dtUsuario_VS.Rows[0]["Perfil"].ToString() == "ADMIN" && long.Parse(dtUsuario_VS.Rows[0]["Acceso"].ToString()) <= 2)
            //{
            //    //btnAlta.Visible = true;
            //}
            //else
            //{
            //    //btnAlta.Visible = false;
            //}
        }
        private void CrearTablaBusqueda()
        {
            dtBusqueda = new DataTable();
            dtBusqueda.Columns.Add("id");
            dtBusqueda.Columns.Add("FechaEvento");
            dtBusqueda.Columns.Add("Nombre");
            dtBusqueda.Columns.Add("Descripcion");
            dtBusqueda.Columns.Add("TipoRegistro");
            dtBusqueda.Columns.Add("Status");

            dtBusqueda_VS = dtBusqueda;
        }
        private void CrearTablaAsistencia()
        {
            dtAsistenciaevento = new DataTable();
            dtAsistenciaevento.Columns.Add("idEvento");
            dtAsistenciaevento.Columns.Add("Cliente");
            dtAsistenciaevento.Columns.Add("TipoCliente");
            dtAsistenciaevento.Columns.Add("Certificados");
            dtAsistenciaevento.Columns.Add("Status");

            dtAsistenciaevento_VS = dtAsistenciaevento;
        }
        private void LlenarAsistencias(List<ENT.ACEL_CUENTA_INVERSIONISTAS> pelRG = null)
        {
            dtAsistenciaevento.Clear();
            DataTable dt = dtAsistenciaevento;
            foreach (ENT.ACEL_CUENTA_INVERSIONISTAS eiAlmacen in pelRG)
            {
                
                DataRow drFila = dt.NewRow();
                drFila["idEvento"] = eiAlmacen.idEvento;
                drFila["Cliente"] = eiAlmacen.NomComercial;
                drFila["TipoCliente"] = eiAlmacen.TipoInversionista;
                try { drFila["Certificados"] = eiAlmacen.CantidadCertificados.Value.ToString(); }
                catch { drFila["Certificados"] = ""; }
                drFila["Status"] = eiAlmacen.Cliente;
                dt.Rows.Add(drFila);
            }

            dtAsistenciaevento_VS = dt;
        }
        private List<ENT.ACEL_CUENTA_INVERSIONISTAS> BusquedaAsistencias(long pidEvento)
        {
            
            List<ACEL_CUENTA_INVERSIONISTAS> elCURU = new List<ACEL_CUENTA_INVERSIONISTAS>();
            elCURU = new boACEL_CUENTA_INVERSIONISTAS().BuscarEvento(1, 1, pidEvento);

            return elCURU;
        }
        private List<ENT.ACEL_CUENTA_EVENTOS> Busqueda()
        {
            bool mNomConf = false;

            //if (txtNomBus.Text != "")
            //    mNomConf = true;
            List<ACEL_CUENTA_EVENTOS> elCURU = new List<ACEL_CUENTA_EVENTOS>();
                elCURU = new boACEL_CUENTA_EVENTOS().Buscar(1, 1);

            return elCURU;
        }
        private void LlenarBusqueda(List<ENT.ACEL_CUENTA_EVENTOS> pelRG = null)
        {
            dtBusqueda.Clear();
            DataTable dt = dtBusqueda;
            foreach (ENT.ACEL_CUENTA_EVENTOS eiAlmacen in pelRG)
            {
                DataRow drFila = dt.NewRow();
                drFila["id"] = eiAlmacen.idEvento;
                drFila["FechaEvento"] = eiAlmacen.FechaEvento.Value.ToShortDateString();
                drFila["Nombre"] = eiAlmacen.NombreEvento;
                drFila["Descripcion"] = eiAlmacen.Descripcion;
                drFila["TipoRegistro"] = eiAlmacen.TipoEvento;
                drFila["Status"] = eiAlmacen.StatusEvento;
                dt.Rows.Add(drFila);
            }

            dtBusqueda_VS = dt;
        }
        private void PresentaGridBusqueda()
        {
            dtBusqueda = new DataTable();
            CrearTablaBusqueda();
            List<ENT.ACEL_CUENTA_EVENTOS> elAltaCRM2 = new List<ENT.ACEL_CUENTA_EVENTOS>();
            elAltaCRM2 = Busqueda();
            LlenarBusqueda(elAltaCRM2);
            rptEventosAct.DataSource = dtBusqueda_VS;
            rptEventosAct.DataBind();
        }
        private void LimpiaDatos()
        {
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            txtNom.Text = "";
            txtFecha1.Text = currentDateTime.ToShortDateString(); 
            cmbStatusEvento.SelectedIndex = 0;
            txtDesc.Text = "";
            cmbTipoEvento.SelectedIndex = 0;
            hfidRegistro.Value = "0";
            cmbCliente.SelectedIndex = 0;
            //cmbInversionista.SelectedIndex = 0;
            foreach (RepeaterItem item in rptEscalas.Items)
            {
                TextBox txtValorN1 = (TextBox)item.FindControl("txtValorN1");
                TextBox txtAnticipo1 = (TextBox)item.FindControl("txtAnticipo1");
                TextBox txtValorN2 = (TextBox)item.FindControl("txtValorN2");
                TextBox txtAnticipo2 = (TextBox)item.FindControl("txtAnticipo2");
                TextBox txtEnganche2 = (TextBox)item.FindControl("txtEnganche2");
                TextBox txtPlazo2 = (TextBox)item.FindControl("txtPlazo2");
                TextBox txtValorN2_2 = (TextBox)item.FindControl("txtValorN2_2");
                TextBox txtAnticipo2_2 = (TextBox)item.FindControl("txtAnticipo2_2");
                TextBox txtValorN3 = (TextBox)item.FindControl("txtValorN3");
                TextBox txtAnticipo3 = (TextBox)item.FindControl("txtAnticipo3");
                TextBox txtEnganche3 = (TextBox)item.FindControl("txtEnganche3");
                TextBox txtPlazo3 = (TextBox)item.FindControl("txtPlazo3");
                TextBox txtValorN3_1 = (TextBox)item.FindControl("txtValorN3_1");
                TextBox txtAnticipo3_1 = (TextBox)item.FindControl("txtAnticipo3_1");
                TextBox txtValorN4 = (TextBox)item.FindControl("txtValorN4");
                TextBox txtAnticipo4 = (TextBox)item.FindControl("txtAnticipo4");
                TextBox txtEnganche4 = (TextBox)item.FindControl("txtEnganche4");
                TextBox txtPlazo4 = (TextBox)item.FindControl("txtPlazo4");
                TextBox txtValorN4_1 = (TextBox)item.FindControl("txtValorN4_1");
                TextBox txtAnticipo4_1 = (TextBox)item.FindControl("txtAnticipo4_1");
                HiddenField hfTipoInversionista = (HiddenField)item.FindControl("hfTipoInversionista");
                txtValorN1.Text = "0.00";
                txtAnticipo1.Text = "0.00";
                txtValorN2.Text = "0.00";
                txtAnticipo2.Text = "0.00";
                txtEnganche2.Text = "0.00";
                txtPlazo2.Text = "0.00";
                txtValorN2_2.Text = "0.00";
                txtAnticipo2_2.Text = "0.00";
                txtValorN3.Text = "0.00";
                txtAnticipo3.Text = "0.00";
                txtEnganche3.Text = "0.00";
                txtPlazo3.Text = "0.00";
                txtValorN3_1.Text = "0.00";
                txtAnticipo3_1.Text = "0.00";
                txtValorN4.Text = "0.00";
                txtAnticipo4.Text = "0.00";
                txtEnganche4.Text = "0.00";
                txtPlazo4.Text = "0.00";
                txtValorN4_1.Text = "0.00";
                txtAnticipo4_1.Text = "0.00";
                hfTipoInversionista.Value = "0";
            }

        }
        private void CargaAsistenciaEvento(long pidEvento)
        {
            List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> eiGeneral = new List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS>();
            ACEL_CUENTA_EVENTOS eiEvento = new ACEL_CUENTA_EVENTOS();
            eiEvento = new boACEL_CUENTA_EVENTOS().Buscarid(1, 1, pidEvento);
            if (eiEvento == null)
            {
                MuestraMensaje("Error al cargar el EVENTO, contacte a sistemas");
                return;
            }
            
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            txtNom.Text = eiEvento.NombreEvento;
            txtFecha1.Text = eiEvento.FechaEvento.Value.ToShortDateString();
            cmbStatusEvento.SelectedItem.Text = eiEvento.StatusEvento;
            txtDesc.Text = eiEvento.Descripcion;
            cmbTipoEvento.SelectedItem.Text = eiEvento.TipoEvento;
            hfidRegistro.Value = eiEvento.idEvento.ToString();
            cmbCliente.SelectedValue = eiEvento.TipoCliente;
            CargaInversionista(long.Parse(cmbCliente.SelectedValue));

            foreach (RepeaterItem item in rptEscalas.Items)
            {
                TextBox txtValorN1 = (TextBox)item.FindControl("txtValorN1");
                TextBox txtAnticipo1 = (TextBox)item.FindControl("txtAnticipo1");
                TextBox txtValorN2 = (TextBox)item.FindControl("txtValorN2");
                TextBox txtAnticipo2 = (TextBox)item.FindControl("txtAnticipo2");
                TextBox txtEnganche2 = (TextBox)item.FindControl("txtEnganche2");
                TextBox txtPlazo2 = (TextBox)item.FindControl("txtPlazo2");
                TextBox txtValorN2_2 = (TextBox)item.FindControl("txtValorN2_2");
                TextBox txtAnticipo2_2 = (TextBox)item.FindControl("txtAnticipo2_2");
                TextBox txtValorN3 = (TextBox)item.FindControl("txtValorN3");
                TextBox txtAnticipo3 = (TextBox)item.FindControl("txtAnticipo3");
                TextBox txtEnganche3 = (TextBox)item.FindControl("txtEnganche3");
                TextBox txtPlazo3 = (TextBox)item.FindControl("txtPlazo3");
                TextBox txtValorN3_1 = (TextBox)item.FindControl("txtValorN3_1");
                TextBox txtAnticipo3_1 = (TextBox)item.FindControl("txtAnticipo3_1");
                TextBox txtValorN4 = (TextBox)item.FindControl("txtValorN4");
                TextBox txtAnticipo4 = (TextBox)item.FindControl("txtAnticipo4");
                TextBox txtEnganche4 = (TextBox)item.FindControl("txtEnganche4");
                TextBox txtPlazo4 = (TextBox)item.FindControl("txtPlazo4");
                TextBox txtValorN4_1 = (TextBox)item.FindControl("txtValorN4_1");
                TextBox txtAnticipo4_1 = (TextBox)item.FindControl("txtAnticipo4_1");

                TextBox txt10MesValorNominal = (TextBox)item.FindControl("txt10MesValorNominal");
                TextBox txt10MesAnticipo = (TextBox)item.FindControl("txt10MesAnticipo");
                TextBox txt10MesEnganche = (TextBox)item.FindControl("txt10MesEnganche");
                TextBox txt10MesPlazo = (TextBox)item.FindControl("txt10MesPlazo");
                TextBox txt10ContValorNominal = (TextBox)item.FindControl("txt10ContValorNominal");
                TextBox txt10ContAnticipo = (TextBox)item.FindControl("txt10ContAnticipo");

                TextBox txt40MesValorNominal = (TextBox)item.FindControl("txt40MesValorNominal");
                TextBox txt40MesAnticipo = (TextBox)item.FindControl("txt40MesAnticipo");
                TextBox txt40MesEnganche = (TextBox)item.FindControl("txt40MesEnganche");
                TextBox txt40MesPlazo = (TextBox)item.FindControl("txt40MesPlazo");
                TextBox txt40ContValorNominal = (TextBox)item.FindControl("txt40ContValorNominal");
                TextBox txt40ContAnticipo = (TextBox)item.FindControl("txt40ContAnticipo");

                HiddenField hfTipoInversionista = (HiddenField)item.FindControl("hfTipoInversionista");

                eiGeneral = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscarEventoInv(1, 1, eiEvento.idEvento, hfTipoInversionista.Value);

                foreach (ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS eiEscala in eiGeneral)
                {

                    if (eiEscala.TipoEscala == "1-9 CONTADO" )
                    {
                        txtValorN1.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txtAnticipo1.Text = eiEscala.Aticipo.Value.ToString("N2");
                    }
                    if (eiEscala.TipoEscala == "10 MENSUALIDADES")
                    {
                        txt10MesValorNominal.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txt10MesAnticipo.Text = eiEscala.Aticipo.Value.ToString("N2");
                        txt10MesEnganche.Text = eiEscala.EngancheLiquidacion.Value.ToString("N2");
                        txt10MesPlazo.Text = eiEscala.Mensualidad.Value.ToString("N0");
                    }
                    if (eiEscala.TipoEscala == "10 CONTADO")
                    {
                        txt10ContValorNominal.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txt10ContAnticipo.Text = eiEscala.Aticipo.Value.ToString("N2");
                    }
                    if (eiEscala.TipoEscala == "11-39 MENSUALIDADES")
                    {
                        txtValorN2.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txtAnticipo2.Text = eiEscala.Aticipo.Value.ToString("N2");
                        txtEnganche2.Text = eiEscala.EngancheLiquidacion.Value.ToString("N2");
                        txtPlazo2.Text = eiEscala.Mensualidad.Value.ToString("N0");
                    }
                    if (eiEscala.TipoEscala == "11-39 CONTADO")
                    {
                        txtValorN2_2.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txtAnticipo2_2.Text = eiEscala.Aticipo.Value.ToString("N2");
                    }
                    if (eiEscala.TipoEscala == "40 MENSUALIDADES")
                    {
                        txt40MesValorNominal.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txt40MesAnticipo.Text = eiEscala.Aticipo.Value.ToString("N2");
                        txt40MesEnganche.Text = eiEscala.EngancheLiquidacion.Value.ToString("N2");
                        txt40MesPlazo.Text = eiEscala.Mensualidad.Value.ToString("N0");
                    }
                    if (eiEscala.TipoEscala == "40 CONTADO")
                    {
                        txt40ContValorNominal.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txt40ContAnticipo.Text = eiEscala.Aticipo.Value.ToString("N2");
                    }
                    if (eiEscala.TipoEscala == "41-99 MENSUALIDADES")
                    {
                        txtValorN3.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txtAnticipo3.Text = eiEscala.Aticipo.Value.ToString("N2");
                        txtEnganche3.Text = eiEscala.EngancheLiquidacion.Value.ToString("N2");
                        txtPlazo3.Text = eiEscala.Mensualidad.Value.ToString("N0");
                    }
                    if (eiEscala.TipoEscala == "41-99 CONTADO")
                    {
                        txtValorN3_1.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txtAnticipo3_1.Text = eiEscala.Aticipo.Value.ToString("N2");
                    }
                    if (eiEscala.TipoEscala == "+ de 100 MENSUALIDADES")
                    {
                        txtValorN4.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txtAnticipo4.Text = eiEscala.Aticipo.Value.ToString("N2");
                        txtEnganche4.Text = eiEscala.EngancheLiquidacion.Value.ToString("N2");
                        txtPlazo4.Text = eiEscala.Mensualidad.Value.ToString("N0");
                    }
                    if (eiEscala.TipoEscala == "+ de 100 CONTADO")
                    {
                        txtValorN4_1.Text = eiEscala.ValorNominal.Value.ToString("N2");
                        txtAnticipo4_1.Text = eiEscala.Aticipo.Value.ToString("N2");
                    }
                }
            }
                
        }
        private void LimpiaConsulta()
        {
            //txtNomBus.Text = "";
            //txtFecha1.Text = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString();
            //txtFecha2.Text = DateTime.Now.ToShortDateString();
            //cmbTipoBus.SelectedIndex = 0;
        }
        private void ShowToastMessage(string Text)
        {
            // Ejecuta el script para mostrar el toast
            string script = $"showInfoToast('" + Text + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarToast",
                    script, true);
        }
        private void CargaClientes()
        {
            cmbCliente.Items.Clear();
            List<ACEL_CUENTA_INDICES> elIndices = new List<ACEL_CUENTA_INDICES>();
            elIndices = new boACEL_INDICES().BuscarIndice(1, 1, 1);
            foreach (ACEL_CUENTA_INDICES eiIndice in elIndices)
            {
                cmbCliente.Items.Add(new ListItem(eiIndice.Descripcion, eiIndice.idDetalle.ToString()));
            }
        }
        private void CargaInversionista(long pidValorAsociado)
        {
            //cmbInversionista.Items.Clear();
            //List<ACEL_CUENTA_INDICES> elIndices = new List<ACEL_CUENTA_INDICES>();
            //elIndices = new boACEL_INDICES().BuscarValorAsociado(1, 1, pidValorAsociado);
            //rptEscalas.DataSource = elIndices;
            //rptEscalas.DataBind();
        }
        #endregion

        #region --> Eventos <----------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    Session.Timeout = 20; // Reinicia el tiempo de espera de la sesión
                }
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
            LimpiaDatos();
            TipoAmbiente("ASISTENCIA");
            btnEliminar.Visible = false;
            //ltrTipoOperacion.Text = "Alta de Usuario";
            Session.Timeout = 20;
        }
        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            TipoAmbiente("EVENTOS");
            Session.Timeout = 20;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            long pidEvento = 0;
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            ACEL_CUENTA_EVENTOS peiConfigura = new ACEL_CUENTA_EVENTOS();
            if (hfidRegistro.Value == "0")
            {
                peiConfigura.idBranch = 1;
                peiConfigura.idCuenta = 1;
                peiConfigura.NombreEvento = txtNom.Text;
                peiConfigura.Descripcion = txtDesc.Text;
                peiConfigura.StatusEvento = cmbStatusEvento.SelectedValue;
                peiConfigura.FechaEvento = DateTime.Parse(txtFecha1.Text);
                peiConfigura.TipoEvento = cmbTipoEvento.SelectedValue;
                peiConfigura.TipoCliente = cmbCliente.SelectedValue;
                //peiConfigura.TipoInversionista = cmbInversionista.SelectedItem.Text;
                peiConfigura.FechaAlta = currentDateTime;
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                peiConfigura.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                peiConfigura.Status = "ACTIVO";
                pidEvento = new boACEL_CUENTA_EVENTOS().Inserta(peiConfigura);
                if (pidEvento > 0)
                {
                    foreach (RepeaterItem item in rptEscalas.Items)
                    {
                        TextBox txtValorN1 = (TextBox)item.FindControl("txtValorN1");
                        TextBox txtAnticipo1 = (TextBox)item.FindControl("txtAnticipo1");
                        TextBox txtValorN2 = (TextBox)item.FindControl("txtValorN2");
                        TextBox txtAnticipo2 = (TextBox)item.FindControl("txtAnticipo2");
                        TextBox txtEnganche2 = (TextBox)item.FindControl("txtEnganche2");
                        TextBox txtPlazo2 = (TextBox)item.FindControl("txtPlazo2");
                        TextBox txtValorN2_2 = (TextBox)item.FindControl("txtValorN2_2");
                        TextBox txtAnticipo2_2 = (TextBox)item.FindControl("txtAnticipo2_2");
                        TextBox txtValorN3 = (TextBox)item.FindControl("txtValorN3");
                        TextBox txtAnticipo3 = (TextBox)item.FindControl("txtAnticipo3");
                        TextBox txtEnganche3 = (TextBox)item.FindControl("txtEnganche3");
                        TextBox txtPlazo3 = (TextBox)item.FindControl("txtPlazo3");
                        TextBox txtValorN3_1 = (TextBox)item.FindControl("txtValorN3_1");
                        TextBox txtAnticipo3_1 = (TextBox)item.FindControl("txtAnticipo3_1");
                        TextBox txtValorN4 = (TextBox)item.FindControl("txtValorN4");
                        TextBox txtAnticipo4 = (TextBox)item.FindControl("txtAnticipo4");
                        TextBox txtEnganche4 = (TextBox)item.FindControl("txtEnganche4");
                        TextBox txtPlazo4 = (TextBox)item.FindControl("txtPlazo4");
                        TextBox txtValorN4_1 = (TextBox)item.FindControl("txtValorN4_1");
                        TextBox txtAnticipo4_1 = (TextBox)item.FindControl("txtAnticipo4_1");

                        TextBox txt10MesValorNominal = (TextBox)item.FindControl("txt10MesValorNominal");
                        TextBox txt10MesAnticipo = (TextBox)item.FindControl("txt10MesAnticipo");
                        TextBox txt10MesEnganche = (TextBox)item.FindControl("txt10MesEnganche");
                        TextBox txt10MesPlazo = (TextBox)item.FindControl("txt10MesPlazo");
                        TextBox txt10ContValorNominal = (TextBox)item.FindControl("txt10ContValorNominal");
                        TextBox txt10ContAnticipo = (TextBox)item.FindControl("txt10ContAnticipo");

                        TextBox txt40MesValorNominal = (TextBox)item.FindControl("txt40MesValorNominal");
                        TextBox txt40MesAnticipo = (TextBox)item.FindControl("txt40MesAnticipo");
                        TextBox txt40MesEnganche = (TextBox)item.FindControl("txt40MesEnganche");
                        TextBox txt40MesPlazo = (TextBox)item.FindControl("txt40MesPlazo");
                        TextBox txt40ContValorNominal = (TextBox)item.FindControl("txt40ContValorNominal");
                        TextBox txt40ContAnticipo = (TextBox)item.FindControl("txt40ContAnticipo");

                        HiddenField hfTipoInversionista = (HiddenField)item.FindControl("hfTipoInversionista");

                        ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "1-9 CONTADO";
                        eiEscala.TipoPago = "CONTADO";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN1.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo1.Text);
                        eiEscala.EngancheLiquidacion = 0;
                        eiEscala.Mensualidad = 0;
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "10 MENSUALIDADES";
                        eiEscala.TipoPago = "MENSUALIDADES";
                        eiEscala.ValorNominal = decimal.Parse(txt10MesValorNominal.Text);
                        eiEscala.Aticipo = decimal.Parse(txt10MesAnticipo.Text);
                        eiEscala.EngancheLiquidacion = decimal.Parse(txt10MesEnganche.Text);
                        eiEscala.Mensualidad = long.Parse(txt10MesPlazo.Text);
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "10 CONTADO";
                        eiEscala.TipoPago = "CONTADO";
                        eiEscala.ValorNominal = decimal.Parse(txt10ContValorNominal.Text);
                        eiEscala.Aticipo = decimal.Parse(txt10ContAnticipo.Text);
                        eiEscala.EngancheLiquidacion = 0;
                        eiEscala.Mensualidad = 0;
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "11-39 MENSUALIDADES";
                        eiEscala.TipoPago = "MENSUALIDADES";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN2.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo2.Text);
                        eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche2.Text);
                        eiEscala.Mensualidad = long.Parse(txtPlazo2.Text);
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "11-39 CONTADO";
                        eiEscala.TipoPago = "CONTADO";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN2_2.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo2_2.Text);
                        eiEscala.EngancheLiquidacion = 0;
                        eiEscala.Mensualidad = 0;
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "40 MENSUALIDADES";
                        eiEscala.TipoPago = "MENSUALIDADES";
                        eiEscala.ValorNominal = decimal.Parse(txt40MesValorNominal.Text);
                        eiEscala.Aticipo = decimal.Parse(txt40MesAnticipo.Text);
                        eiEscala.EngancheLiquidacion = decimal.Parse(txt40MesEnganche.Text);
                        eiEscala.Mensualidad = long.Parse(txt40MesPlazo.Text);
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "40 CONTADO";
                        eiEscala.TipoPago = "CONTADO";
                        eiEscala.ValorNominal = decimal.Parse(txt40ContValorNominal.Text);
                        eiEscala.Aticipo = decimal.Parse(txt40ContAnticipo.Text);
                        eiEscala.EngancheLiquidacion = 0;
                        eiEscala.Mensualidad = 0;
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "41-99 MENSUALIDADES";
                        eiEscala.TipoPago = "MENSUALIDADES";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN3.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo3.Text);
                        eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche3.Text);
                        eiEscala.Mensualidad = long.Parse(txtPlazo3.Text);
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "41-99 MENSUALIDADES";
                        eiEscala.TipoPago = "MENSUALIDADES";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN3.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo3.Text);
                        eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche3.Text);
                        eiEscala.Mensualidad = long.Parse(txtPlazo3.Text);
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "40-99 CONTADO";
                        eiEscala.TipoPago = "CONTADO";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN3_1.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo3_1.Text);
                        eiEscala.EngancheLiquidacion = 0;
                        eiEscala.Mensualidad = 0;
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "+ de 100 MENSUALIDADES";
                        eiEscala.TipoPago = "MENSUALIDADES";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN4.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo4.Text);
                        eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche4.Text);
                        eiEscala.Mensualidad = long.Parse(txtPlazo4.Text);
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        eiEscala.idBranch = 1;
                        eiEscala.idCuenta = 1;
                        eiEscala.idEvento = pidEvento;
                        eiEscala.TipoEscala = "+ de 100 CONTADO";
                        eiEscala.TipoPago = "CONTADO";
                        eiEscala.ValorNominal = decimal.Parse(txtValorN4_1.Text);
                        eiEscala.Aticipo = decimal.Parse(txtAnticipo4_1.Text);
                        eiEscala.EngancheLiquidacion = 0;
                        eiEscala.Mensualidad = 0;
                        eiEscala.FechaAlta = currentDateTime;
                        eiEscala.FechaMod = currentDateTime;
                        eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        eiEscala.Status = "ACTIVO";
                        eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);
                    }
                    MuestraMensaje("El registro se dió de alta correctamente");
                    LimpiaDatos();
                    TipoAmbiente("EVENTOS");
                    PresentaGridBusqueda();
                }
                //peiConfigura.TipoUsuario = cmbTipo.SelectedValue;
            }
            else
            {
                peiConfigura = new boACEL_CUENTA_EVENTOS().Buscarid(1,1,long.Parse(hfidRegistro.Value));
                pidEvento = peiConfigura.idEvento;
                peiConfigura.NombreEvento = txtNom.Text;
                peiConfigura.Descripcion = txtDesc.Text;
                peiConfigura.StatusEvento = cmbStatusEvento.SelectedValue;
                peiConfigura.FechaEvento = DateTime.Parse(txtFecha1.Text);
                peiConfigura.TipoEvento = cmbTipoEvento.SelectedValue;
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuMod = "";
                peiConfigura.TipoCliente = cmbCliente.SelectedValue;
                if (new boACEL_CUENTA_EVENTOS().Actualiza(peiConfigura))
                {
                    if (pidEvento > 0)
                    {
                        foreach (RepeaterItem item in rptEscalas.Items)
                        {
                            TextBox txtValorN1 = (TextBox)item.FindControl("txtValorN1");
                            TextBox txtAnticipo1 = (TextBox)item.FindControl("txtAnticipo1");
                            TextBox txtValorN2 = (TextBox)item.FindControl("txtValorN2");
                            TextBox txtAnticipo2 = (TextBox)item.FindControl("txtAnticipo2");
                            TextBox txtEnganche2 = (TextBox)item.FindControl("txtEnganche2");
                            TextBox txtPlazo2 = (TextBox)item.FindControl("txtPlazo2");
                            TextBox txtValorN2_2 = (TextBox)item.FindControl("txtValorN2_2");
                            TextBox txtAnticipo2_2 = (TextBox)item.FindControl("txtAnticipo2_2");
                            TextBox txtValorN3 = (TextBox)item.FindControl("txtValorN3");
                            TextBox txtAnticipo3 = (TextBox)item.FindControl("txtAnticipo3");
                            TextBox txtEnganche3 = (TextBox)item.FindControl("txtEnganche3");
                            TextBox txtPlazo3 = (TextBox)item.FindControl("txtPlazo3");
                            TextBox txtValorN3_1 = (TextBox)item.FindControl("txtValorN3_1");
                            TextBox txtAnticipo3_1 = (TextBox)item.FindControl("txtAnticipo3_1");
                            TextBox txtValorN4 = (TextBox)item.FindControl("txtValorN4");
                            TextBox txtAnticipo4 = (TextBox)item.FindControl("txtAnticipo4");
                            TextBox txtEnganche4 = (TextBox)item.FindControl("txtEnganche4");
                            TextBox txtPlazo4 = (TextBox)item.FindControl("txtPlazo4");
                            TextBox txtValorN4_1 = (TextBox)item.FindControl("txtValorN4_1");
                            TextBox txtAnticipo4_1 = (TextBox)item.FindControl("txtAnticipo4_1");

                            TextBox txt10MesValorNominal = (TextBox)item.FindControl("txt10MesValorNominal");
                            TextBox txt10MesAnticipo = (TextBox)item.FindControl("txt10MesAnticipo");
                            TextBox txt10MesEnganche = (TextBox)item.FindControl("txt10MesEnganche");
                            TextBox txt10MesPlazo = (TextBox)item.FindControl("txt10MesPlazo");
                            TextBox txt10ContValorNominal = (TextBox)item.FindControl("txt10ContValorNominal");
                            TextBox txt10ContAnticipo = (TextBox)item.FindControl("txt10ContAnticipo");

                            TextBox txt40MesValorNominal = (TextBox)item.FindControl("txt40MesValorNominal");
                            TextBox txt40MesAnticipo = (TextBox)item.FindControl("txt40MesAnticipo");
                            TextBox txt40MesEnganche = (TextBox)item.FindControl("txt40MesEnganche");
                            TextBox txt40MesPlazo = (TextBox)item.FindControl("txt40MesPlazo");
                            TextBox txt40ContValorNominal = (TextBox)item.FindControl("txt40ContValorNominal");
                            TextBox txt40ContAnticipo = (TextBox)item.FindControl("txt40ContAnticipo");

                            HiddenField hfTipoInversionista = (HiddenField)item.FindControl("hfTipoInversionista");

                            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "1-9 CONTADO");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txtValorN1.Text);
                                eiEscala.Aticipo = decimal.Parse(txtAnticipo1.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "10 MENSUALIDADES");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txt10MesValorNominal.Text);
                                eiEscala.Aticipo = decimal.Parse(txt10MesAnticipo.Text);
                                eiEscala.EngancheLiquidacion = decimal.Parse(txt10MesEnganche.Text);
                                eiEscala.Mensualidad = long.Parse(txt10MesPlazo.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "10 CONTADO");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txt10ContValorNominal.Text);
                                eiEscala.Aticipo = decimal.Parse(txt10ContAnticipo.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "11-39 MENSUALIDADES");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txtValorN2.Text);
                                eiEscala.Aticipo = decimal.Parse(txtAnticipo2.Text);
                                eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche2.Text);
                                eiEscala.Mensualidad = long.Parse(txtPlazo2.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "11-39 CONTADO");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txtValorN2_2.Text);
                                eiEscala.Aticipo = decimal.Parse(txtAnticipo2_2.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "40 MENSUALIDADES");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txt40MesValorNominal.Text);
                                eiEscala.Aticipo = decimal.Parse(txt40MesAnticipo.Text);
                                eiEscala.EngancheLiquidacion = decimal.Parse(txt40MesEnganche.Text);
                                eiEscala.Mensualidad = long.Parse(txt40MesPlazo.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "40 CONTADO");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txt40ContValorNominal.Text);
                                eiEscala.Aticipo = decimal.Parse(txt40ContAnticipo.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "41-99 MENSUALIDADES");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txtValorN3.Text);
                                eiEscala.Aticipo = decimal.Parse(txtAnticipo3.Text);
                                eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche3.Text);
                                eiEscala.Mensualidad = long.Parse(txtPlazo3.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "41-99 CONTADO");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txtValorN3_1.Text);
                                eiEscala.Aticipo = decimal.Parse(txtAnticipo3_1.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "+ de 100 MENSUALIDADES");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txtValorN4.Text);
                                eiEscala.Aticipo = decimal.Parse(txtAnticipo4.Text);
                                eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche4.Text);
                                eiEscala.Mensualidad = long.Parse(txtPlazo4.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }

                            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscartTipoInversionista(1, 1, pidEvento, hfTipoInversionista.Value, "+ de 100 CONTADO");
                            if (eiEscala != null)
                            {
                                eiEscala.ValorNominal = decimal.Parse(txtValorN4_1.Text);
                                eiEscala.Aticipo = decimal.Parse(txtAnticipo4_1.Text);
                                eiEscala.FechaMod = currentDateTime;
                                eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                                eiEscala.Status = "ACTIVO";
                                new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Actualiza(eiEscala);
                            }
                        }
                        //foreach (RepeaterItem item in rptEscalas.Items)
                        //{
                        //    TextBox txtValorN1 = (TextBox)item.FindControl("txtValorN1");
                        //    TextBox txtAnticipo1 = (TextBox)item.FindControl("txtAnticipo1");
                        //    TextBox txtValorN2 = (TextBox)item.FindControl("txtValorN2");
                        //    TextBox txtAnticipo2 = (TextBox)item.FindControl("txtAnticipo2");
                        //    TextBox txtEnganche2 = (TextBox)item.FindControl("txtEnganche2");
                        //    TextBox txtPlazo2 = (TextBox)item.FindControl("txtPlazo2");
                        //    TextBox txtValorN2_2 = (TextBox)item.FindControl("txtValorN2_2");
                        //    TextBox txtAnticipo2_2 = (TextBox)item.FindControl("txtAnticipo2_2");
                        //    TextBox txtValorN3 = (TextBox)item.FindControl("txtValorN3");
                        //    TextBox txtAnticipo3 = (TextBox)item.FindControl("txtAnticipo3");
                        //    TextBox txtEnganche3 = (TextBox)item.FindControl("txtEnganche3");
                        //    TextBox txtPlazo3 = (TextBox)item.FindControl("txtPlazo3");
                        //    TextBox txtValorN3_1 = (TextBox)item.FindControl("txtValorN3_1");
                        //    TextBox txtAnticipo3_1 = (TextBox)item.FindControl("txtAnticipo3_1");
                        //    TextBox txtValorN4 = (TextBox)item.FindControl("txtValorN4");
                        //    TextBox txtAnticipo4 = (TextBox)item.FindControl("txtAnticipo4");
                        //    TextBox txtEnganche4 = (TextBox)item.FindControl("txtEnganche4");
                        //    TextBox txtPlazo4 = (TextBox)item.FindControl("txtPlazo4");
                        //    TextBox txtValorN4_1 = (TextBox)item.FindControl("txtValorN4_1");
                        //    TextBox txtAnticipo4_1 = (TextBox)item.FindControl("txtAnticipo4_1");
                        //    HiddenField hfTipoInversionista = (HiddenField)item.FindControl("hfTipoInversionista");

                        //    ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        //    eiEscala.idBranch = 1;
                        //    eiEscala.idCuenta = 1;
                        //    eiEscala.idEvento = pidEvento;
                        //    eiEscala.TipoEscala = "1-9 CONTADO";
                        //    eiEscala.TipoPago = "CONTADO";
                        //    eiEscala.ValorNominal = decimal.Parse(txtValorN1.Text);
                        //    eiEscala.Aticipo = decimal.Parse(txtAnticipo1.Text);
                        //    eiEscala.EngancheLiquidacion = 0;
                        //    eiEscala.Mensualidad = 0;
                        //    eiEscala.FechaAlta = currentDateTime;
                        //    eiEscala.FechaMod = currentDateTime;
                        //    eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.Status = "ACTIVO";
                        //    eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        //    new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        //    eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        //    eiEscala.idBranch = 1;
                        //    eiEscala.idCuenta = 1;
                        //    eiEscala.idEvento = pidEvento;
                        //    eiEscala.TipoEscala = "10-39 MENSUALIDADES";
                        //    eiEscala.TipoPago = "MENSUALIDADES";
                        //    eiEscala.ValorNominal = decimal.Parse(txtValorN2.Text);
                        //    eiEscala.Aticipo = decimal.Parse(txtAnticipo2.Text);
                        //    eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche2.Text);
                        //    eiEscala.Mensualidad = long.Parse(txtPlazo2.Text);
                        //    eiEscala.FechaAlta = currentDateTime;
                        //    eiEscala.FechaMod = currentDateTime;
                        //    eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.Status = "ACTIVO";
                        //    eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        //    new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        //    eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        //    eiEscala.idBranch = 1;
                        //    eiEscala.idCuenta = 1;
                        //    eiEscala.idEvento = pidEvento;
                        //    eiEscala.TipoEscala = "10-39 CONTADO";
                        //    eiEscala.TipoPago = "CONTADO";
                        //    eiEscala.ValorNominal = decimal.Parse(txtValorN2_2.Text);
                        //    eiEscala.Aticipo = decimal.Parse(txtAnticipo2_2.Text);
                        //    eiEscala.EngancheLiquidacion = 0;
                        //    eiEscala.Mensualidad = 0;
                        //    eiEscala.FechaAlta = currentDateTime;
                        //    eiEscala.FechaMod = currentDateTime;
                        //    eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.Status = "ACTIVO";
                        //    eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        //    new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        //    eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        //    eiEscala.idBranch = 1;
                        //    eiEscala.idCuenta = 1;
                        //    eiEscala.idEvento = pidEvento;
                        //    eiEscala.TipoEscala = "40-99 MENSUALIDADES";
                        //    eiEscala.TipoPago = "MENSUALIDADES";
                        //    eiEscala.ValorNominal = decimal.Parse(txtValorN3.Text);
                        //    eiEscala.Aticipo = decimal.Parse(txtAnticipo3.Text);
                        //    eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche3.Text);
                        //    eiEscala.Mensualidad = long.Parse(txtPlazo3.Text);
                        //    eiEscala.FechaAlta = currentDateTime;
                        //    eiEscala.FechaMod = currentDateTime;
                        //    eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.Status = "ACTIVO";
                        //    eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        //    new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        //    eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        //    eiEscala.idBranch = 1;
                        //    eiEscala.idCuenta = 1;
                        //    eiEscala.idEvento = pidEvento;
                        //    eiEscala.TipoEscala = "40-99 CONTADO";
                        //    eiEscala.TipoPago = "CONTADO";
                        //    eiEscala.ValorNominal = decimal.Parse(txtValorN3_1.Text);
                        //    eiEscala.Aticipo = decimal.Parse(txtAnticipo3_1.Text);
                        //    eiEscala.EngancheLiquidacion = 0;
                        //    eiEscala.Mensualidad = 0;
                        //    eiEscala.FechaAlta = currentDateTime;
                        //    eiEscala.FechaMod = currentDateTime;
                        //    eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.Status = "ACTIVO";
                        //    eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        //    new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        //    eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        //    eiEscala.idBranch = 1;
                        //    eiEscala.idCuenta = 1;
                        //    eiEscala.idEvento = pidEvento;
                        //    eiEscala.TipoEscala = "+ de 100 MENSUALIDADES";
                        //    eiEscala.TipoPago = "MENSUALIDADES";
                        //    eiEscala.ValorNominal = decimal.Parse(txtValorN4.Text);
                        //    eiEscala.Aticipo = decimal.Parse(txtAnticipo4.Text);
                        //    eiEscala.EngancheLiquidacion = decimal.Parse(txtEnganche4.Text);
                        //    eiEscala.Mensualidad = long.Parse(txtPlazo4.Text);
                        //    eiEscala.FechaAlta = currentDateTime;
                        //    eiEscala.FechaMod = currentDateTime;
                        //    eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.Status = "ACTIVO";
                        //    eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        //    new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);

                        //    eiEscala = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
                        //    eiEscala.idBranch = 1;
                        //    eiEscala.idCuenta = 1;
                        //    eiEscala.idEvento = pidEvento;
                        //    eiEscala.TipoEscala = "+ de 100 CONTADO";
                        //    eiEscala.TipoPago = "CONTADO";
                        //    eiEscala.ValorNominal = decimal.Parse(txtValorN4_1.Text);
                        //    eiEscala.Aticipo = decimal.Parse(txtAnticipo4_1.Text);
                        //    eiEscala.EngancheLiquidacion = 0;
                        //    eiEscala.Mensualidad = 0;
                        //    eiEscala.FechaAlta = currentDateTime;
                        //    eiEscala.FechaMod = currentDateTime;
                        //    eiEscala.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                        //    eiEscala.Status = "ACTIVO";
                        //    eiEscala.TipoInversionista = hfTipoInversionista.Value;
                        //    new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().Inserta(eiEscala);
                        //}
                        MuestraMensaje("El registro se modificó correctamente");
                        LimpiaDatos();
                        TipoAmbiente("EVENTOS");
                        PresentaGridBusqueda();
                    }
                }
            }
        }
        protected void afuExcelFile_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            //if (afuExcelFile.HasFile)
            //{
            //    // Guardar temporalmente el archivo en el servidor
            //    string folderPath = Server.MapPath("~/Uploads/");
            //    if (!Directory.Exists(folderPath))
            //    {
            //        Directory.CreateDirectory(folderPath);
            //    }

            //    excelFilePath = Path.Combine(folderPath, Path.GetFileName(afuExcelFile.FileName));
            //    afuExcelFile.SaveAs(excelFilePath);
            //}
        }
        protected void btnProcessExcel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(excelFilePath))
            {
                DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
                FileInfo fileInfo = new FileInfo(excelFilePath);

                // Establece el contexto de la licencia antes de procesar el archivo
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Para uso no comercial

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    // Seleccionamos la primera hoja del archivo de Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // Obtenemos el número total de filas y columnas
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    // Leemos la primera fila (encabezados)
                    var headerMapping = new Dictionary<string, int>();

                    for (int col = 1; col <= colCount; col++)
                    {
                        string header = worksheet.Cells[1, col].Text; // Encabezado en la fila 1
                        if (!string.IsNullOrEmpty(header))
                        {
                            headerMapping[header] = col; // Mapear encabezado a su índice de columna
                        }
                    }

                    // Verificamos que los encabezados que necesitamos existan
                    if (headerMapping.ContainsKey("NOMBRE") && headerMapping.ContainsKey("EVENTO"))
                    {
                        // Recorremos las filas y columnas, empezando desde la fila 2 (omitimos la primera fila que es el encabezado)
                        for (int row = 2; row <= rowCount; row++)
                        {
                            // Accedemos a los valores de las columnas según su encabezado
                            string CODIGO = worksheet.Cells[row, headerMapping["#"]].Text;
                            string NOMBRE = worksheet.Cells[row, headerMapping["NOMBRE"]].Text;
                            string MONTOPAGO = worksheet.Cells[row, headerMapping["MONTO PAGO"]].Text;
                            string FECHAPAGO = worksheet.Cells[row, headerMapping["FECHA PAGO"]].Text;
                            string MESPAGO = worksheet.Cells[row, headerMapping["MES PAGO"]].Text;
                            string EVENTO = worksheet.Cells[row, headerMapping["EVENTO"]].Text;
                            //string mid = worksheet.Cells[row, headerMapping["id"]].Text;
                            //string mNombre = worksheet.Cells[row, headerMapping["Nombre"]].Text;
                            //string mApellidos = worksheet.Cells[row, headerMapping["Apellidos"]].Text;
                            //string mCorreo = worksheet.Cells[row, headerMapping["Correo"]].Text;
                            //string mWhatsApp = worksheet.Cells[row, headerMapping["WhatsApp"]].Text;
                            //string mTipoAlumno = worksheet.Cells[row, headerMapping["TipoAlumno"]].Text;
                            //string mEventoAsistencia = worksheet.Cells[row, headerMapping["EventoAsistencia"]].Text;
                            //string mCertificados = worksheet.Cells[row, headerMapping["Certificados"]].Text;
                            //string mValorNominalCertificado = worksheet.Cells[row, headerMapping["ValorNominalCertificado"]].Text;
                            //string mMontoTotalCertificados = worksheet.Cells[row, headerMapping["MontoTotalCertificados"]].Text;
                            //string mEnganche = worksheet.Cells[row, headerMapping["Enganche"]].Text;
                            //string mAnticipo = worksheet.Cells[row, headerMapping["Anticipo"]].Text;
                            //string mMeses = worksheet.Cells[row, headerMapping["Meses"]].Text;

                            //mEventoAsistencia = new boACEL_CUENTA_EVENTOS().BuscarNom(1, 1, mEventoAsistencia).idEvento.ToString();
                            ACEL_CUENTA_INVERSIONISTAS eiCliente = new ACEL_CUENTA_INVERSIONISTAS();
                            eiCliente = new boACEL_CUENTA_INVERSIONISTAS().BuscarNombre(1, 1, NOMBRE);
                            if (eiCliente != null)
                            {

                                ACEL_CUENTA_INVERSIONISTAS_PAGOS eiPago = new ACEL_CUENTA_INVERSIONISTAS_PAGOS();
                                eiPago.idBranch = 1;
                                eiPago.idCuenta = 1;
                                eiPago.ViaDeposito = "MANUAL";
                                eiPago.FechaPago = DateTime.Parse(FECHAPAGO);
                                eiPago.MontoPago = decimal.Parse(MONTOPAGO);
                                eiPago.ReferenciaPago = MESPAGO;
                                eiPago.FechaAlta = currentDateTime;
                                eiPago.FechaMod = currentDateTime;
                                eiPago.UsuAlta = "ICG";
                                eiPago.UsuMod = "ICG";
                                eiPago.Status = "ACTIVO";
                                long midPago = new boACEL_CUENTA_INVERSIONISTAS_PAGOS().Inserta(eiPago);
                            }
                        }
                        MuestraMensaje("Los datos se procesaron corectamente");

                    }
                    else
                    {
                        Response.Write("El archivo Excel no contiene los encabezados esperados.");
                    }
                }

                // Opcional: Eliminar el archivo después de procesarlo
                if (System.IO.File.Exists(excelFilePath))
                {
                    System.IO.File.Delete(excelFilePath);
                }
            }
            else
            {
                Response.Write("No se ha cargado ningún archivo.");
            }
        }
        protected void btnConsultar_Click1(object sender, EventArgs e)
        {
            PresentaGridBusqueda();
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
        protected void rptEventosAct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Session.Timeout = 20;
            HiddenField hf = (HiddenField)e.Item.FindControl("hfidEv");
            List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> peiConfigura = new List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS>();
            peiConfigura = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscarEvento(1, 1, long.Parse(hf.Value));
            if (e.CommandName == "registro")
            {
                LimpiaDatos();
                hfidRegistro.Value = hf.Value;
                TipoAmbiente("ASISTENCIA");
                ltrPantallaSecundaria.Text = "Configura EVENTO";
                CargaAsistenciaEvento(long.Parse(hf.Value));
                btnEliminar.Visible = true;
                //PresentaGridAsistencia(long.Parse(hf.Value));
                //ShowToastMessage("Hola bienvenido a la plataforma de ISra");
            }
        }
        #endregion
        protected void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaInversionista(long.Parse(cmbCliente.SelectedValue));
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (hfidRegistro.Value != "0")
            {
                ACEL_CUENTA_EVENTOS eiEvento = new ACEL_CUENTA_EVENTOS();
                eiEvento = new boACEL_CUENTA_EVENTOS().Buscarid(1, 1, long.Parse(hfidRegistro.Value));
                if (eiEvento == null)
                {
                    MuestraMensaje("Error al eliminar el EVENTO, contacte a sistemas");
                    return;
                }

                if (new boACEL_CUENTA_EVENTOS().Baja(eiEvento))
                {
                    MuestraMensaje("El evento se dió de baja correctamente");
                    TipoAmbiente("EVENTOS");
                    PresentaGridBusqueda();
                }
                else
                {
                    MuestraMensaje("No se pudo dar de baja el evento por que ya tiene inversionistas que ocupan las escalas de este evento");
                }
                Session.Timeout = 20;
            }
        }
        protected void rptEscalas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtPrecio = (TextBox)e.Item.FindControl("txtPrecio");
                TextBox txtAnticipo1 = (TextBox)e.Item.FindControl("txtAnticipo1");
                TextBox txtValorN2 = (TextBox)e.Item.FindControl("txtValorN2");
                TextBox txtAnticipo2 = (TextBox)e.Item.FindControl("txtAnticipo2");
                TextBox txtValorN2_2 = (TextBox)e.Item.FindControl("txtValorN2_2");
                TextBox txtAnticipo2_2 = (TextBox)e.Item.FindControl("txtAnticipo2_2");
                TextBox txtValorN3 = (TextBox)e.Item.FindControl("txtValorN3");
                TextBox txtAnticipo3 = (TextBox)e.Item.FindControl("txtAnticipo3");
                TextBox txtValorN3_1 = (TextBox)e.Item.FindControl("txtValorN3_1");
                TextBox txtAnticipo3_1 = (TextBox)e.Item.FindControl("txtAnticipo3_1");
                TextBox txtValorN4 = (TextBox)e.Item.FindControl("txtValorN4");
                TextBox txtAnticipo4 = (TextBox)e.Item.FindControl("txtAnticipo4");
                TextBox txtValorN4_1 = (TextBox)e.Item.FindControl("txtValorN4_1");
                TextBox txtAnticipo4_1 = (TextBox)e.Item.FindControl("txtAnticipo4_1");
                if (txtPrecio != null)
                {
                    txtPrecio.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtAnticipo1.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtValorN2.Attributes.Add("onblur", "formatPrecioBundle(this.id);");

                    txtAnticipo2.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtValorN2_2.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtAnticipo2_2.Attributes.Add("onblur", "formatPrecioBundle(this.id);");

                    txtValorN3.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtAnticipo3.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtValorN3_1.Attributes.Add("onblur", "formatPrecioBundle(this.id);");

                    txtAnticipo3_1.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtValorN4.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtAnticipo4.Attributes.Add("onblur", "formatPrecioBundle(this.id);");

                    txtPrecio.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                    txtAnticipo1.Attributes.Add("onblur", "formatPrecioBundle(this.id);");
                }
            }
        }
    }
}