using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACEL.BO.bo;
using ACEL.ENT;

namespace ACEL.WEB
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            ACEL_CUENTA_USUARIOS1 eiUsuario = new ACEL_CUENTA_USUARIOS1();
            eiUsuario = new boACEL_USUARIOS().BuscarCvePass(1, 1, username.Text, pass.Text);
            if (eiUsuario == null)
            {
                MuestraMensaje("El usuario no existe");
                username.Text = "";
                username.Focus();
                return;
            }
            if (eiUsuario.Pass != pass.Text)
            {
                MuestraMensaje("El password es incorrecto por favor vuelva a " +
                "intentarlo.");
                pass.Text = "";
                pass.Focus();
                return;
            }


            Session["NombreUsuario"] = eiUsuario.Nombre + " " + eiUsuario.Apellidos;
            FormsAuthentication.RedirectFromLoginPage(eiUsuario.idUsuario.ToString(), true);
        }

        private void MuestraMensaje(string pMensaje)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(pMensaje.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }
    }
}