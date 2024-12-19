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

namespace ACEL.WEB.pages.operacion
{
    public partial class clientes : System.Web.UI.Page
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
            CargaComboClientes();
            CargaComboEventos();
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

                if (eiAlmacen.TipoInversionista == "1")
                {
                    switch (eiAlmacen.Cliente)
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
                    switch (eiAlmacen.Cliente)
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
            txtNom.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cmbCliente.SelectedIndex = 0;
            cmbInversionista.SelectedIndex = 0;
            cmbTipoPago.SelectedIndex = 0;
            cmbEventos.SelectedIndex = 0;
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
            txtCertificados.Text = eiCliente.CantidadCertificados.ToString();

            long certificados = long.Parse(txtCertificados.Text);
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
        }
        private void CargaComboClientes()
        {
            cmbCliente.Items.Clear();
            List<ACEL_CUENTA_INDICES> elIndices = new List<ACEL_CUENTA_INDICES>();
            elIndices = new boACEL_INDICES().BuscarIndice(1, 1, 1);
            foreach (ACEL_CUENTA_INDICES eiIndice in elIndices)
            {
                cmbCliente.Items.Add(new ListItem(eiIndice.Descripcion, eiIndice.idDetalle.ToString()));
            }
            CargaInversionista(long.Parse(cmbCliente.SelectedValue));
        }
        private void CargaInversionista(long pidValorAsociado)
        {
            cmbInversionista.Items.Clear();
            List<ACEL_CUENTA_INDICES> elIndices = new List<ACEL_CUENTA_INDICES>();
            elIndices = new boACEL_INDICES().BuscarValorAsociado(1, 1, pidValorAsociado);
            foreach (ACEL_CUENTA_INDICES eiIndice in elIndices)
            {
                cmbInversionista.Items.Add(new ListItem(eiIndice.Descripcion, eiIndice.idDetalle.ToString()));
            }
        }
        private void CargaComboEventos()
        {
            cmbEventos.Items.Clear();
            List<ACEL_CUENTA_EVENTOS> elEventos = new List<ACEL_CUENTA_EVENTOS>();
            elEventos = new boACEL_CUENTA_EVENTOS().Buscar(1, 1);
            foreach (ACEL_CUENTA_EVENTOS eiEvento in elEventos)
            {
                cmbEventos.Items.Add(new ListItem(eiEvento.NombreEvento, eiEvento.idEvento.ToString()));
            }
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
            decimal Total = (peiCert.ValorNominal.Value * pCertificados);
            decimal Anticipo = peiCert.Aticipo.Value;
            decimal Enganche;
            if (peiCert.TipoPago == "MENSUALIDADES")
                Enganche = Total * decimal.Parse("0." + peiCert.EngancheLiquidacion.Value.ToString("N0"));
            else
                Enganche = Total - Anticipo;

            ltrTotal.Text = Total.ToString("N2");
            ltrAnticipo.Text = Anticipo.ToString("N2");
            ltrEnganche.Text = Enganche.ToString("N2");
            if (peiCert.TipoPago == "MENSUALIDADES")
            {
                try
                {
                    ltrPagos.Text = peiCert.Mensualidad.Value.ToString("N0") + " PAGOS DE $" +
                      ((Total - Enganche) / peiCert.Mensualidad.Value).ToString("N2");
                }
                catch { ltrPagos.Text = "0.00"; }
            }
                
            else
                ltrPagos.Text = "1 PAGO DE $" +
                    Enganche.ToString("N2");
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

            long certificados = long.Parse(txtCertificados.Text);
            string tipoPago = cmbTipoPago.SelectedItem.Text;
            string pTipoEscala;

            if (certificados >= 1 && certificados <= 9)
            {
                pTipoEscala = "1-9 CONTADO";
            }
            else if (certificados >= 10 && certificados <= 39)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "10-39 CONTADO";
                }
                else
                {
                    pTipoEscala = "10-39 MENSUALIDADES";
                }
            }
            else if (certificados >= 40 && certificados <= 99)
            {
                if (tipoPago == "CONTADO")
                {
                    pTipoEscala = "40-99 CONTADO";
                }
                else
                {
                    pTipoEscala = "40-99 MENSUALIDADES";
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
            eiEscala = new boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS().BuscarEscalaPago(1, 1, long.Parse(cmbEventos.SelectedValue),
                pTipoEscala, cmbInversionista.SelectedItem.Text);
            if (eiEscala == null)
            {
                MuestraMensaje("Error al buscar la escala");
                return;
            }

            if (hfidRegistro.Value == "0")
            {
                peiConfigura.idBranch = 1;
                peiConfigura.idCuenta = 1;
                peiConfigura.NomComercial = txtNom.Text;
                peiConfigura.Cliente = cmbCliente.SelectedValue;
                peiConfigura.CorreoContacto = txtCorreo.Text;
                peiConfigura.TelContacto = txtTelefono.Text;
                peiConfigura.CantidadCertificados = long.Parse(txtCertificados.Text);
                peiConfigura.TipoInversionista = cmbInversionista.SelectedValue;
                peiConfigura.idEvento = long.Parse(cmbEventos.SelectedValue);
                peiConfigura.NombreEvento = cmbEventos.SelectedItem.Text;
                peiConfigura.CondicionesPago = cmbTipoPago.SelectedValue;
                peiConfigura.idEscala = eiEscala.idEscala;
                peiConfigura.FechaAlta = currentDateTime;
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuAlta = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                peiConfigura.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                peiConfigura.Status = "ACTIVO";
                long pidEvento = new boACEL_CUENTA_INVERSIONISTAS().Inserta(peiConfigura);
                if (pidEvento > 0)
                {
                    hfidRegistro.Value = pidEvento.ToString();
                    MuestraMensaje("El registro se dió de alta correctamente");
                }
                //peiConfigura.TipoUsuario = cmbTipo.SelectedValue;
            }
            else
            {
                peiConfigura = new boACEL_CUENTA_INVERSIONISTAS().Buscarid(1,1,long.Parse(hfidRegistro.Value));
                peiConfigura.NomComercial = txtNom.Text;
                peiConfigura.Cliente = cmbCliente.SelectedValue;
                peiConfigura.CorreoContacto = txtCorreo.Text;
                peiConfigura.TelContacto = txtTelefono.Text;
                peiConfigura.CantidadCertificados = long.Parse(txtCertificados.Text);
                peiConfigura.TipoInversionista = cmbInversionista.SelectedValue;
                peiConfigura.idEvento = long.Parse(cmbEventos.SelectedValue);
                peiConfigura.idEscala = eiEscala.idEscala;
                peiConfigura.NombreEvento = cmbEventos.SelectedItem.Text;
                peiConfigura.CondicionesPago = cmbTipoPago.SelectedValue;
                peiConfigura.FechaMod = currentDateTime;
                peiConfigura.UsuMod = dtUsuario_VS.Rows[0]["CveUsuario"].ToString();
                if (new boACEL_CUENTA_INVERSIONISTAS().Actualiza(peiConfigura))
                {
                    MuestraMensaje("El registro se modificó correctamente");
                }
            }
            PresentaGridBusqueda(true);
            CargaTotalPagos(eiEscala, certificados);
            //TipoAmbiente("BUSQUEDA");
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
                CargaCliente(long.Parse(hf.Value));
            }
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

        protected void cmbInversionista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaInversionista(long.Parse(cmbCliente.SelectedValue));
        }
    }
}