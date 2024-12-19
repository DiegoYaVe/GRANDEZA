using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web;
using ACEL.ENT;
using System.Data;
using ACEL.BO.bo;

namespace ACEL.UTL.Mail
{
    public class AtributosEmail
    {
        private string aDominio;
        private int aPort;
        private string aFrom;
        private string aTo;
        private NetworkCredential aCredenciales;

        public AtributosEmail()
        {
            aCredenciales = new NetworkCredential();
            aPort = 25;
            aDominio = string.Empty;
        }

        public string gFrom
        {
            get { return aFrom; }
            set { aFrom = value; }
        }

        public string gTo
        {
            get { return aTo; }
            set { aTo = value; }
        }

        public string gDominio
        {
            get { return aDominio; }
            set { aDominio = value; }
        }

        public int gPort
        {
            get { return aPort; }
            set { aPort = value; }
        }

        public NetworkCredential gCredenciales
        {
            get { return aCredenciales; }
            set { aCredenciales = value; }
        }
    }
    public class utlMail
    {

        #region --> Metodos <------------------




        //public bool EnvioMailCURU(ACEL_CUENTA_AFILIACIONES peiAfilia, string pCorreo, string pPathHTML, string pAdjunto)
        //{
        //    bool mEnvioCorrecto = false;
        //    try
        //    {
        //        SmtpClient sctEnvio = new SmtpClient();
        //        sctEnvio.Credentials = new System.Net.NetworkCredential("no-reply@sistema.ACELVeracruz.org", "noreply2020@");
        //        //sctEnvio.Credentials = new System.Net.NetworkCredential("info@alfredoculebro.com", "info_2016_ACEL");
        //        sctEnvio.Host = "mail5005.site4now.net";
        //        //sctEnvio.Host = "mail.alfredoculebro.com";
        //        sctEnvio.Port = 8889;
        //        //sctEnvio.Port = 2525;
        //        sctEnvio.EnableSsl = false;
        //        string mBody = System.IO.File.ReadAllText(pPathHTML);
        //        mBody = mBody.Replace("#Nombre#", peiAfilia.NomComercial);

        //        MailMessage mmsMensaje = new MailMessage();//"info@universidadculebro.com", pPara, "Gracias por registrarte al N1", mBody);
        //        mmsMensaje.From = new MailAddress("no-reply@sistema.ACELVeracruz.org", "AFILIACION ACEL Veracruz");
        //        mmsMensaje.IsBodyHtml = true;
        //        mmsMensaje.To.Add(pCorreo);
        //        mmsMensaje.Subject = "Bienvenido a ACEL Veracruz";
        //        mmsMensaje.Body = mBody;
        //        mmsMensaje.Priority = MailPriority.Normal;
        //        mmsMensaje.BodyEncoding = System.Text.Encoding.UTF8;
        //        mmsMensaje.SubjectEncoding = System.Text.Encoding.Default;
        //        //mmsMensaje.Attachments.Add((new Attachment(pAdjunto)));
        //        sctEnvio.Send(mmsMensaje);
        //        sctEnvio.Dispose();
        //        mEnvioCorrecto = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mEnvioCorrecto = false;
        //    }

        //    return mEnvioCorrecto;
        //}

        //public bool EnvioMailRecibo(string pCorreo, ACEL_CUENTA_COTIZACION peiCot, string pPathHTML)
        //{
        //    bool mEnvioCorrecto = false;
        //    try
        //    {
        //        ACEL_CUENTA_COTIZACION eiCot = new ACEL_CUENTA_COTIZACION();
        //        eiCot = peiCot;
        //        SmtpClient sctEnvio = new SmtpClient();
        //        sctEnvio.Credentials = new System.Net.NetworkCredential(eiCot.ACEL_CUENTA_USUARIOS1.Correo, eiCot.ACEL_CUENTA_USUARIOS1.PassCorreo);
        //        //sctEnvio.Credentials = new System.Net.NetworkCredential("info@alfredoculebro.com", "info_2016_ACEL");
        //        sctEnvio.Host = "smtp.gmail.com";
        //        //sctEnvio.Host = "mail.alfredoculebro.com";
        //        sctEnvio.Port = 587;
        //        //sctEnvio.Port = 2525;
        //        sctEnvio.EnableSsl = true;

        //        decimal mMontoTotal = eiCot.Total.Value;

        //        string mBody = System.IO.File.ReadAllText(pPathHTML);
        //        mBody = mBody.Replace("#Nombre#", eiCot.NomCotizacion.ToUpper());
        //        mBody = mBody.Replace("#NombreVendedor#", eiCot.ACEL_CUENTA_USUARIOS1.NomComercial.ToUpper());
        //        mBody = mBody.Replace("#SubTotal#", peiCot.SubTotal.Value.ToString("N2"));
        //        mBody = mBody.Replace("#Iva#", peiCot.Iva.Value.ToString("N2"));
        //        mBody = mBody.Replace("#MontoTotal#", mMontoTotal.ToString("N2"));
        //        mBody = mBody.Replace("#pidCotizacion#", peiCot.idCotizacion.ToString());
        //        mBody = mBody.Replace("#Vigencia#", peiCot.Vigencia.Value.ToShortDateString());
        //        mBody = mBody.Replace("#Correo#", eiCot.ACEL_CUENTA_USUARIOS1.Correo);

        //        StringBuilder articulosHtml = new StringBuilder();
        //        foreach (var articulo in eiCot.ACEL_CUENTA_COTIZACION_PARTIDAS)
        //        {
        //            articulosHtml.Append("<tr>");
        //            articulosHtml.AppendFormat("<td style='border-right:1px solid #dddddd;border-bottom:1px solid #dddddd;text-align:left;padding:7px'>{0}</td>", articulo.Descricpion);
        //            articulosHtml.AppendFormat("<td style='border-right:1px solid #dddddd;border-bottom:1px solid #dddddd;text-align:center;padding:7px'>{0}</td>", articulo.Cantidad.Value.ToString("N0"));
        //            articulosHtml.Append("</tr>");
        //        }
        //        mBody = mBody.Replace("#ArticulosCotizacion#", articulosHtml.ToString());

        //        MailMessage mmsMensaje = new MailMessage();//"info@universidadculebro.com", pPara, "Gracias por registrarte al N1", mBody);
        //        mmsMensaje.From = new MailAddress(eiCot.ACEL_CUENTA_USUARIOS1.Correo, "COTIZACIÓN ACEL");
        //        mmsMensaje.IsBodyHtml = true;
        //        mmsMensaje.To.Add(pCorreo);
        //        mmsMensaje.Subject = eiCot.ACEL_CUENTA_USUARIOS1.NomComercial + " le ha enviado una cotización";
        //        mmsMensaje.Body = mBody;
        //        mmsMensaje.Priority = MailPriority.Normal;
        //        mmsMensaje.BodyEncoding = System.Text.Encoding.UTF8;
        //        mmsMensaje.SubjectEncoding = System.Text.Encoding.Default;
        //        //mmsMensaje.Attachments.Add((new Attachment(pAdjunto)));
        //        sctEnvio.Send(mmsMensaje);
        //        sctEnvio.Dispose();
        //        mEnvioCorrecto = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mEnvioCorrecto = false;
        //    }

        //    return mEnvioCorrecto;
        //}
        #endregion
    }
}
