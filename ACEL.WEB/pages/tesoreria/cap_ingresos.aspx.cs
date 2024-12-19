using ACEL.BO.bo;
using ACEL.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACEL.WEB.pages.tesoreria
{
    public partial class cap_ingresos : System.Web.UI.Page
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
            //ltrPantallaSecundaria.Text = "Clientes";

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
            PresentaGridBusqueda(true);
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
                case "BUSQUEDA":
                    panConsulta.Visible = true;
                    panDatos.Visible = false;
                    btnConsulta.Visible = false;
                    btnAlta.Visible = true;
                    break;
                case "DATOS":
                    panConsulta.Visible = false;
                    panDatos.Visible = true;
                    btnConsulta.Visible = true;
                    btnAlta.Visible = false;
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
            dtBusqueda.Columns.Add("NomComercial");
            dtBusqueda.Columns.Add("TipoCliente");
            dtBusqueda.Columns.Add("Correo");
            dtBusqueda.Columns.Add("Evento");
            dtBusqueda.Columns.Add("Certificados");
            dtBusqueda.Columns.Add("Status");

            dtBusqueda_VS = dtBusqueda;
        }
        private void CrearTablaPagos()
        {
            dtPagos = new DataTable();
            dtPagos.Columns.Add("idCliente");
            dtPagos.Columns.Add("idPago");
            dtPagos.Columns.Add("MontoPago");
            dtPagos.Columns.Add("FechaPago");
            dtPagos.Columns.Add("MetodoPago");
            dtPagos.Columns.Add("ReferenciaPago");
            dtPagos.Columns.Add("Evento");

            dtPagos_VS = dtPagos;
        }
        private void LlenarPagos(List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> elPagos = null)
        {
            dtPagos.Clear();
            DataTable dt = dtPagos;
            foreach (ENT.ACEL_CUENTA_INVERSIONISTAS_PAGOS eiAlmacen in elPagos)
            {
                DataRow drFila = dt.NewRow();
                drFila["idCliente"] = eiAlmacen.idInversionista;
                drFila["idPago"] = eiAlmacen.idPago;
                drFila["MontoPago"] = eiAlmacen.MontoPago.Value.ToString("N2");
                drFila["FechaPago"] = eiAlmacen.FechaPago.Value.ToShortDateString();
                drFila["MetodoPago"] = eiAlmacen.ViaDeposito;
                drFila["ReferenciaPago"] = eiAlmacen.ReferenciaPago;
                drFila["Evento"] = "";
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
        private List<ENT.ACEL_CUENTA_INVERSIONISTAS_PAGOS> BusquedaPagos(long pidCliente)
        {
            bool mNomConf = false;

            //if (txtNomBus.Text != "")
            //    mNomConf = true;
            List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> elCURU = new List<ACEL_CUENTA_INVERSIONISTAS_PAGOS>();
            elCURU = new boACEL_CUENTA_INVERSIONISTAS_PAGOS().Buscar(1, 1, pidCliente);


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
                drFila["TipoCliente"] = eiAlmacen.TipoInversionista;
                drFila["Correo"] = eiAlmacen.CorreoContacto;
                drFila["Evento"] = eiAlmacen.NombreEvento;
                drFila["Certificados"] = eiAlmacen.CantidadCertificados;
                drFila["Status"] = eiAlmacen.Status;
                dt.Rows.Add(drFila);
            }

            dtBusqueda_VS = dt;
        }
        private void PresentaGridBusqueda(bool pAdmin)
        {
            dtBusqueda = new DataTable();
            CrearTablaBusqueda();
            List<ENT.ACEL_CUENTA_INVERSIONISTAS> elAltaCRM2 = new List<ENT.ACEL_CUENTA_INVERSIONISTAS>();
            elAltaCRM2 = Busqueda(pAdmin);
            LlenarBusqueda(elAltaCRM2);
            rptConfiguraciones.DataSource = dtBusqueda_VS;
            rptConfiguraciones.DataBind();
        }
        private void LimpiaDatos()
        {
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            txtNom.Text = "";
            txtCorreo.Text = "";
            txtFecha1.Text = currentDateTime.ToShortDateString();
            cmbStatusEvento.SelectedIndex = 0;
            cmbTipoEvento.SelectedIndex = 0;
            hfidRegistro.Value = "0";
        }
        private void CargaCliente(long pidUsuario)
        {
            ACEL_CUENTA_INVERSIONISTAS eiCliente = new ACEL_CUENTA_INVERSIONISTAS();
            eiCliente = new boACEL_CUENTA_INVERSIONISTAS().Buscarid(1, 1, pidUsuario);
            if (eiCliente == null)
            {
                MuestraMensaje("Error al cargar el usuario, contacte a sistemas");
                return;
            }
            txtNom.Text = eiCliente.NomComercial;

            txtFecha1.Text = eiCliente.FechaAlta.Value.ToShortDateString();
            cmbStatusEvento.SelectedValue = eiCliente.Status;
            cmbTipoEvento.SelectedValue = eiCliente.TipoInversionista;
            txtCorreo.Text = eiCliente.CorreoContacto;
            //imagen

        }
        private void LimpiaConsulta()
        {
            //txtNomBus.Text = "";
            //txtFecha1.Text = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString();
            //txtFecha2.Text = DateTime.Now.ToShortDateString();
            //cmbTipoBus.SelectedIndex = 0;
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
            TipoAmbiente("DATOS");
            //ltrTipoOperacion.Text = "Alta de Usuario";
            Session.Timeout = 20;
        }
        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            TipoAmbiente("BUSQUEDA");
            Session.Timeout = 20;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            ACEL_CUENTA_INVERSIONISTAS peiConfigura = new ACEL_CUENTA_INVERSIONISTAS();
            if (hfidRegistro.Value == "0")
            {
                peiConfigura.idBranch = 1;
                peiConfigura.idCuenta = 1;
                peiConfigura.NomComercial = txtNom.Text;
                peiConfigura.TipoInversionista = cmbTipoEvento.SelectedValue;
                peiConfigura.CorreoContacto = txtCorreo.Text;
                peiConfigura.FechaAlta = DateTime.Parse(txtFecha1.Text);
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuAlta = "";
                peiConfigura.UsuMod = "";
                peiConfigura.Status = cmbStatusEvento.SelectedValue;
                long pidEvento = new boACEL_CUENTA_INVERSIONISTAS().Inserta(peiConfigura);
                if (pidEvento > 0)
                {
                    MuestraMensaje("El registro se dió de alta correctamente");
                }
                //peiConfigura.TipoUsuario = cmbTipo.SelectedValue;
            }
            else
            {
                peiConfigura = new boACEL_CUENTA_INVERSIONISTAS().Buscarid(1, 1, long.Parse(hfidRegistro.Value));
                peiConfigura.NomComercial = txtNom.Text;
                peiConfigura.TipoInversionista = cmbTipoEvento.SelectedValue;
                peiConfigura.CorreoContacto = txtCorreo.Text;
                peiConfigura.FechaAlta = DateTime.Parse(txtFecha1.Text);
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuMod = "";
                peiConfigura.Status = cmbStatusEvento.SelectedValue;
                if (new boACEL_CUENTA_INVERSIONISTAS().Actualiza(peiConfigura))
                {
                    MuestraMensaje("El registro se modificó correctamente");
                }
            }
            PresentaGridBusqueda(true);
            TipoAmbiente("BUSQUEDA");
        }
        protected void rptConfiguraciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Session.Timeout = 20;
            HiddenField hf = (HiddenField)e.Item.FindControl("hfid");

            if (e.CommandName == "ver")
            {
                LimpiaDatos();
                hfidRegistro.Value = hf.Value;
                TipoAmbiente("DATOS");
                //ltrTipoOperacion.Text = "Editando el usuario " + hf.Value;
                CargaCliente(long.Parse(hf.Value));
            }

            //if (e.CommandName == "elimina")
            //{
            //    if (dtUsuario_VS.Rows[0]["Perfil"].ToString() != "ADMIN")
            //    {
            //        MuestraMensaje("No tienes los permisos de eliminar este registro");
            //        //PresentaGridBusqueda(false);
            //        return;
            //    }
            //    if (new boSIRSCOM_USUARIOS().Baja(peiConfigura))
            //    {
            //        PresentaGridBusqueda(true);
            //        TipoAmbiente("BUSQUEDA");
            //        hfidRegistro.Value = "0";
            //        MuestraMensaje("El usuario se dió de baja correctamente");
            //    }
            //    else
            //    {
            //        MuestraMensaje("Error al dar de baja, contacte a sistemas");
            //    }
            //}
        }
        protected void btnConsultar_Click1(object sender, EventArgs e)
        {
            if (dtUsuario_VS.Rows[0]["Perfil"].ToString() == "ADMIN" && long.Parse(dtUsuario_VS.Rows[0]["Acceso"].ToString()) <= 2)
            {
                //CargaCombosClientes();
                PresentaGridBusqueda(true);
                //btnAlta.Visible = true;
                //btnGuardar.Visible = true;
                //txtUsuario.Visible = true;
                //lblUsuario.Visible = false;
                //panDatosSirscom.Visible = true;
            }
            else
            {
                PresentaGridBusqueda(false);
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

        protected void rptConfiguraciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string idCliente = (e.Item.FindControl("hfid") as HiddenField).Value;
                Repeater rptPartidas = e.Item.FindControl("rptPartidas") as Repeater;
                List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> eiCot = new List<ACEL_CUENTA_INVERSIONISTAS_PAGOS>();
                eiCot = new boACEL_CUENTA_INVERSIONISTAS_PAGOS().Buscar(1, 1, long.Parse(idCliente));
                CrearTablaPagos();
                LlenarPagos(eiCot);
                rptPartidas.DataSource = dtPagos_VS;
                rptPartidas.DataBind();
            }
        }
    }
}