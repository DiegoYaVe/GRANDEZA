using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACEL.BO.bo;
using ACEL.ENT;

namespace ACEL.WEB
{
    public partial class pruebaeventos : System.Web.UI.Page
    {
        private readonly boACEL_CUENTA_EVENTOS _boEventos = new boACEL_CUENTA_EVENTOS();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEventos();
            }
        }

        private void CargarEventos()
        {
            var eventos = _boEventos.Buscar();
            EventosGrid.DataSource = eventos;
            EventosGrid.DataBind();
        }

        protected void AltaButton_Click(object sender, EventArgs e)
        {
            MostrarFormulario(true);
            LimpiarFormulario();
        }

        protected void SortButton_Click(object sender, EventArgs e)
        {
            var eventos = _boEventos.Buscar();
            string sortingField = SortingField.SelectedValue;

            switch (sortingField)
            {
                case "NombreEvento":
                    eventos.Sort((x, y) => string.Compare(x.NombreEvento, y.NombreEvento, StringComparison.OrdinalIgnoreCase));
                    break;
                case "Descripcion":
                    eventos.Sort((x, y) => string.Compare(x.Descripcion, y.Descripcion, StringComparison.OrdinalIgnoreCase));
                    break;
                case "StatusEvento":
                    eventos.Sort((x, y) => string.Compare(x.StatusEvento, y.StatusEvento, StringComparison.OrdinalIgnoreCase));
                    break;
                case "FechaAlta":
                    eventos.Sort((x, y) => DateTime.Compare(x.FechaAlta ?? DateTime.MinValue, y.FechaAlta ?? DateTime.MinValue));
                    break;
            }

            EventosGrid.DataSource = eventos;
            EventosGrid.DataBind();
        }

        protected void EventosGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                long idEvento = Convert.ToInt64(e.CommandArgument);

                var evento = _boEventos.Buscarid(1, 1, idEvento); // Usa los valores de idBranch e idCuenta según corresponda

                if (evento != null)
                {
                    idEvento = evento.idEvento;
                    nombreEvento.Text = evento.NombreEvento;
                    descripcion.Text = evento.Descripcion;
                    statusEvento.SelectedValue = evento.StatusEvento;
                    fechaAlta.Text = evento.FechaAlta?.ToString("yyyy-MM-dd");
                    fechaMod.Text = evento.FechaMod?.ToString("yyyy-MM-dd");

                    MostrarFormulario(true);
                }
            }
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            var evento = new ACEL_CUENTA_EVENTOS
            {
                idBranch = 1, // Ajusta según tu contexto
                idCuenta = 1, // Ajusta según tu contexto
                idEvento = string.IsNullOrEmpty(idEvento.Text) ? 0 : Convert.ToInt64(idEvento.Text),
                NombreEvento = nombreEvento.Text,
                Descripcion = descripcion.Text,
                StatusEvento = statusEvento.SelectedValue,
                FechaAlta = string.IsNullOrEmpty(fechaAlta.Text) ? (DateTime?)null : Convert.ToDateTime(fechaAlta.Text),
                FechaMod = DateTime.Now,
                UsuAlta = "currentUser", // Reemplaza por el usuario actual
                UsuMod = "currentUser",  // Reemplaza por el usuario actual
                Status = "ACTIVO"
            };

            if (evento.idEvento == 0)
            {
                _boEventos.Inserta(evento);
            }
            else
            {
                _boEventos.Actualiza(evento);
            }

            CargarEventos();
            MostrarFormulario(false);
        }

        protected void RegresarButton_Click(object sender, EventArgs e)
        {
            MostrarFormulario(false);
        }

        private void MostrarFormulario(bool mostrar)
        {
            formSection.Style["display"] = mostrar ? "block" : "none";
            tableSection.Style["display"] = mostrar ? "none" : "block";
            AltaButton.Visible = !mostrar;
        }

        private void LimpiarFormulario()
        {
            idEvento.Text = string.Empty;
            nombreEvento.Text = string.Empty;
            descripcion.Text = string.Empty;
            statusEvento.SelectedValue = "Activo";
            fechaAlta.Text = string.Empty;
            fechaMod.Text = string.Empty;
        }



    }
}