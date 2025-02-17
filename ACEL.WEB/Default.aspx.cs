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
using System.IO;
using System.Text.RegularExpressions;

namespace ACEL.WEB
{
    public partial class Default : System.Web.UI.Page//Diego Prueba Merge
    {
        private static string excelFilePath = string.Empty;

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
                //LoadPayments();
            }

        }
        private void LoadPayments(bool Btn = false)
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
            decimal mMonto = 0;
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
                            paymentList.Add(new
                            {
                                Amount = payment.Amount / 100.0, // Stripe works with cents
                                Currency = payment.Currency.ToUpper(),
                                Status = payment.Status,
                                Created = DateTime.Parse(payment.Created.ToString()).ToShortDateString(),
                                Referencia = charges.Data[0].BillingDetails.Name,
                                Correo = charges.Data[0].BillingDetails.Email,
                                CardID = charges.Data[0].PaymentMethod,
                                Id = charges.Data[0].Id,
                                StatusGrandeza = "SIN APLICAR"
                            });
                        }
                        else //si encontro al inversionista le aplica su pago y lo refleja en su estado de cuenta
                        {
                            if (Btn)
                            { 
                                
                            }
                            paymentList.Add(new
                            {
                                Amount = payment.Amount / 100.0, // Stripe works with cents
                                Currency = payment.Currency.ToUpper(),
                                Status = payment.Status,
                                Created = DateTime.Parse(payment.Created.ToString()).ToShortDateString(),
                                Referencia = charges.Data[0].BillingDetails.Name,
                                Correo = charges.Data[0].BillingDetails.Email,
                                CardID = charges.Data[0].PaymentMethod,
                                Id = charges.Data[0].Id,
                                StatusGrandeza = eiInversionista.NomComercial
                            });
                        }
                        mMonto = mMonto + decimal.Parse((payment.Amount / 100.0).ToString());
                    }
                    
                }

            }

            ltrTotalPagos.Text = "Pendientes de aplicar: " + paymentList.Count.ToString();
            ltrMonto.Text = mMonto.ToString("N2");
            if (mMonto > 0)
                btnAplicarSTRIPE.Visible = true;
            else
                btnAplicarSTRIPE.Visible = false;
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
            LoadPayments();
            MuestraMensaje("Pagos actualizados");
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

        protected void afuExcelFile_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            if (afuExcelFile.HasFile)
            {
                // Guardar temporalmente el archivo en el servidor
                string folderPath = Server.MapPath("~/temp/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                excelFilePath = Path.Combine(folderPath, Path.GetFileName(afuExcelFile.FileName));
                afuExcelFile.SaveAs(excelFilePath);
            }
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
                    if (headerMapping.ContainsKey("Fecha Movimiento") && headerMapping.ContainsKey("Descripción"))
                    {
                        decimal mMonto = 0;
                        var paymentList = new List<object>();

                        // Recorremos las filas y columnas, empezando desde la fila 2 (omitimos la primera fila que es el encabezado)
                        for (int row = 2; row <= rowCount; row++)
                        {
                            // Verificar si las celdas clave están vacías
                            if (string.IsNullOrWhiteSpace(worksheet.Cells[row, headerMapping["Fecha Movimiento"]].Text) &&
                                string.IsNullOrWhiteSpace(worksheet.Cells[row, headerMapping["Abonos"]].Text))
                            {
                                // Saltar filas vacías
                                continue;
                            }

                            // Accedemos a los valores de las columnas según su encabezado
                            string CODIGO = worksheet.Cells[row, headerMapping["#"]].Text;
                            string FechaPago = worksheet.Cells[row, headerMapping["Fecha Movimiento"]].Text;
                            string HoraPago = worksheet.Cells[row, headerMapping["Hora"]].Text;
                            string Recibo = worksheet.Cells[row, headerMapping["Recibo"]].Text;
                            string Descripcion = worksheet.Cells[row, headerMapping["Descripción"]].Text;
                            string Cargos = worksheet.Cells[row, headerMapping["Cargos"]].Text;
                            string Abonos = worksheet.Cells[row, headerMapping["Abonos"]].Text;

                            string abonosLimpio = Abonos.Replace("$", "").Trim();

                            //mEventoAsistencia = new boACEL_CUENTA_EVENTOS().BuscarNom(1, 1, mEventoAsistencia).idEvento.ToString();
                            ACEL_CUENTA_INVERSIONISTAS eiCliente = new ACEL_CUENTA_INVERSIONISTAS();
                            string texto = Descripcion;
                            string nombreOrdenante = "";
                            // Expresión regular para capturar el nombre del ordenante
                            string patron = @"Ordenante:\s([A-Z\s]+)\sCuenta Ordenante:";
                            Match match = Regex.Match(texto, patron);

                            if (match.Success)
                            {
                                nombreOrdenante = match.Groups[1].Value.Trim();
                            }
                            eiCliente = new boACEL_CUENTA_INVERSIONISTAS().BuscarNombre(1, 1, nombreOrdenante.ToUpper());
                            
                            if (eiCliente != null)
                            {
                                ACEL_CUENTA_INVERSIONISTAS_PAGOS eiPago = new ACEL_CUENTA_INVERSIONISTAS_PAGOS();
                                eiPago.idBranch = 1;
                                eiPago.idCuenta = 1;
                                eiPago.idInversionista = eiCliente.idInversionista;
                                eiPago.ViaDeposito = "BAJIO";
                                try { eiPago.FechaPago = DateTime.Parse(FechaPago); }
                                catch { eiPago.FechaPago = null; }
                                try { eiPago.MontoPago = decimal.Parse(abonosLimpio); }
                                catch { eiPago.MontoPago = 0; }
                                eiPago.ReferenciaPago = Descripcion;
                                eiPago.FechaAlta = currentDateTime;
                                eiPago.FechaMod = currentDateTime;
                                eiPago.UsuAlta = "ICG";
                                eiPago.UsuMod = "ICG";
                                eiPago.Status = "ACTIVO";
                                long midPago = new boACEL_CUENTA_INVERSIONISTAS_PAGOS().Inserta(eiPago);
                            }
                            else
                            { 

                            }
                            try
                            {
                                paymentList.Add(new
                                {
                                    Amount = abonosLimpio, // Stripe works with cents
                                    Currency = Recibo,
                                    Status = "Success",
                                    CreatedBajio = DateTime.Parse(FechaPago).ToShortDateString(),
                                    ReferenciaBajio = nombreOrdenante,
                                    CorreoBajio = "",
                                    CardIDBajio = CODIGO,
                                    IDBajio = Recibo,
                                    StatusBajio = "SIN APLICAR"
                                });
                            }
                            catch { 
                                
                            }

                            try { mMonto = mMonto + decimal.Parse((abonosLimpio).ToString()); }
                            catch { mMonto = mMonto + 0; }
                        }
                        MuestraMensaje("Los datos se procesaron corectamente");
                        ltrBajio.Text = "Pendientes de aplicar: " + paymentList.Count.ToString();
                        ltrMontoBajio.Text = mMonto.ToString("N2");
                        if (mMonto > 0)
                            btnAplicarBAJIO.Visible = true;
                        else
                            btnAplicarBAJIO.Visible = false;
                        rptBajio.DataSource = paymentList;
                        rptBajio.DataBind();
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

        protected void btnAplicarSTRIPE_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnAplicarBAJIO_Click(object sender, EventArgs e)
        {

        }
    }
}