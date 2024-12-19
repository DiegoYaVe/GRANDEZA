using System;
using System.Linq;
using ACEL.ENT; // Assuming entity models are here
using ACEL.DAL; // Assuming data access layers and context are here

namespace ACEL.BO.ai
{
    public class aiACEL_CUENTA_INVERSIONISTAS
    {
        #region ----> Atributos <----------------------------
        private dcACELDataContext adcCANIRAC;
        #endregion

        #region --> Constructor <----------------------------
        public aiACEL_CUENTA_INVERSIONISTAS(dcACELDataContext padcCANIRAC)
        {
            adcCANIRAC = padcCANIRAC;
        }
        #endregion

        #region --> Métodos para ACEL_CUENTA_INVERSIONISTAS <-----------------
        public long InsertarInversionista(ACEL_CUENTA_INVERSIONISTAS peiGeneral)
        {
            return adcCANIRAC.sp_ACEL_CUENTA_INVERSIONISTAS(dalOperacion.Inserta,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idInversionista,
                   peiGeneral.CveInversionista,
                   peiGeneral.ApellidoPat,
                   peiGeneral.ApellidoMat,
                   peiGeneral.NomComercial,
                   peiGeneral.RazonSocial,
                   peiGeneral.TipoInversionista,
                   peiGeneral.Contacto,
                   peiGeneral.TelContacto,
                   peiGeneral.CorreoContacto,
                   peiGeneral.Imagen,
                   peiGeneral.Cliente,
                   peiGeneral.Usuario,
                   peiGeneral.Pass,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.CondicionesPago,
                   peiGeneral.NumMensualidades,
                   peiGeneral.TipoMoneda,
                   peiGeneral.RFC,
                   peiGeneral.CantidadCertificados,
                   peiGeneral.NombreEvento,
                   peiGeneral.idEvento,
                   peiGeneral.idEscala
                   ).FirstOrDefault().idInversionista.Value; // Assuming the SP returns the primary key
        }

        public void ActualizarInversionista(ACEL_CUENTA_INVERSIONISTAS peiGeneral)
        {
            adcCANIRAC.sp_ACEL_CUENTA_INVERSIONISTAS(dalOperacion.Actualiza,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idInversionista,
                   peiGeneral.CveInversionista,
                   peiGeneral.ApellidoPat,
                   peiGeneral.ApellidoMat,
                   peiGeneral.NomComercial,
                   peiGeneral.RazonSocial,
                   peiGeneral.TipoInversionista,
                   peiGeneral.Contacto,
                   peiGeneral.TelContacto,
                   peiGeneral.CorreoContacto,
                   peiGeneral.Imagen,
                   peiGeneral.Cliente,
                   peiGeneral.Usuario,
                   peiGeneral.Pass,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.CondicionesPago,
                   peiGeneral.NumMensualidades,
                   peiGeneral.TipoMoneda,
                   peiGeneral.RFC,
                   peiGeneral.CantidadCertificados,
                   peiGeneral.NombreEvento,
                   peiGeneral.idEvento,
                   peiGeneral.idEscala);
        }

        public void BajaInversionista(ACEL_CUENTA_INVERSIONISTAS peiGeneral)
        {
            adcCANIRAC.sp_ACEL_CUENTA_INVERSIONISTAS(dalOperacion.Baja,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idInversionista,
                   peiGeneral.CveInversionista,
                   peiGeneral.ApellidoPat,
                   peiGeneral.ApellidoMat,
                   peiGeneral.NomComercial,
                   peiGeneral.RazonSocial,
                   peiGeneral.TipoInversionista,
                   peiGeneral.Contacto,
                   peiGeneral.TelContacto,
                   peiGeneral.CorreoContacto,
                   peiGeneral.Imagen,
                   peiGeneral.Cliente,
                   peiGeneral.Usuario,
                   peiGeneral.Pass,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.CondicionesPago,
                   peiGeneral.NumMensualidades,
                   peiGeneral.TipoMoneda,
                   peiGeneral.RFC,
                   peiGeneral.CantidadCertificados,
                   peiGeneral.NombreEvento,
                   peiGeneral.idEvento,
                   peiGeneral.idEscala);
        }
        #endregion
    }
}
