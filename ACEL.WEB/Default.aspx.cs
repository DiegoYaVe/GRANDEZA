using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe;
using ACEL.ENT;
using OfficeOpenXml;
using System.Web.Security;
using System.Text;
using System.Data;
using ACEL.BO.bo;

namespace ACEL.WEB
{
    public partial class Default : System.Web.UI.Page//Diego Prueba Merge
    {
        private DataTable dtUsuario;
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

            //if (eiUsuario.TipoUsuario == "CLIENTE")
            //{
            //    Response.Redirect("../../default_cliente.aspx");
            //}
            //else if (eiUsuario.TipoUsuario == "REPARTO")
            //{
            //    Response.Redirect("../../default_reparto.aspx");
            //}

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
            ltrNombre1.Text = dtUsuario_VS.Rows[0]["NombreUsuario"].ToString();
            ltrPuestoUsuario.Text = dtUsuario_VS.Rows[0]["Puesto"].ToString();
        }
        private void MuestraMensaje(string pMensaje)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(pMensaje.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CrearEntidadUsuario();
                LoadPayments();
            }

        }
        private void LoadPayments()
        {
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            StripeConfiguration.ApiKey = "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";

            var service = new PaymentIntentService();
            var options = new PaymentIntentListOptions
            {
                Limit = 100,
                Created = new DateRangeOptions
                {
                    GreaterThanOrEqual = currentDateTime.Date.AddDays(-1),
                    LessThan = currentDateTime.Date
                }
            };

            var payments = service.List(options);
            var paymentList = new List<object>();
            var paymentListNo = new List<object>();

            var chargeService = new ChargeService();

            foreach (var payment in payments)
            {
                if (payment.Status == "succeeded")// verifica que el pago este procesado y en la cuenta
                {
                    var chargeListOptions = new ChargeListOptions
                    {
                        PaymentIntent = payment.Id // Utiliza el PaymentIntentId para buscar cargos
                    };

                    var charges = chargeService.List(chargeListOptions);

                    bool mVerificaReferencia = new boACEL_CUENTA_INVERSIONISTAS_PAGOS().BuscaReferencia(1, 1, charges.Data[0].Id);

                    if (!mVerificaReferencia) // si no se encuentra la referencia en la base, el pago se guarda en la base
                    {
                        ACEL_CUENTA_INVERSIONISTAS eiInversionista = new ACEL_CUENTA_INVERSIONISTAS();
                        eiInversionista = new boACEL_CUENTA_INVERSIONISTAS().BuscarNombreCorreo(1, 1,
                            charges.Data[0].BillingDetails.Name.ToUpper(),
                            charges.Data[0].BillingDetails.Email.ToLower()); // busca al inversionista por nombre y correo
                        if (eiInversionista == null) // si no encontro el inversionista agrega el pago a una lista de pagos por asignar
                        {
                            paymentListNo.Add(new
                            {
                                Amount = payment.Amount / 100.0, // Stripe works with cents
                                Currency = payment.Currency.ToUpper(),
                                Status = payment.Status,
                                Created = DateTime.Parse(payment.Created.ToString()).ToShortDateString(),
                                Referencia = charges.Data[0].BillingDetails.Name,
                                Correo = charges.Data[0].BillingDetails.Email,
                                CardID = charges.Data[0].PaymentMethod,
                                Id = charges.Data[0].Id
                            });
                        }
                        else // si, si encontro al inversionista le aplica su pago y lo refleja en su estado de cuenta
                        {
                            paymentList.Add(new
                            {
                                Amount = payment.Amount / 100.0, // Stripe works with cents
                                Currency = payment.Currency.ToUpper(),
                                Status = payment.Status,
                                Created = DateTime.Parse(payment.Created.ToString()).ToShortDateString(),
                                Referencia = charges.Data[0].BillingDetails.Name,
                                Correo = charges.Data[0].BillingDetails.Email,
                                CardID = charges.Data[0].PaymentMethod,
                                Id = charges.Data[0].Id
                            });
                        }
                    }
                    
                }

            }

            ltrTotalPagos.Text = "Pagos de STRIPE: " + paymentList.Count.ToString();
            rptPagosSTRIPE.DataSource = paymentList;
            rptPagosSTRIPE.DataBind();
        }

        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
            return;
        }

        protected void btnRealizarPagosStripe_Click(object sender, EventArgs e)
        {
            try
            {
                // Aquí coloca la lógica para realizar los pagos
                MuestraMensaje("Pago realizado con éxito.");
            }
            catch (Exception ex)
            {
                MuestraMensaje("Error al realizar el pago: " + ex.Message);
            }
        }

        protected void btnRealizarPagosBajio_Click(object sender, EventArgs e)
        {
            MuestraMensaje("Pago realizado con éxito.");

            try
            {
                // Aquí coloca la lógica para realizar los pagos
                MuestraMensaje("Pago realizado con éxito.");
            }
            catch (Exception ex)
            {
                MuestraMensaje("Error al realizar el pago: " + ex.Message);
            }
        }


    }
}