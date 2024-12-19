using System;
using System.Linq;
using ACEL.ENT; // Assuming entity models are here
using ACEL.DAL; // Assuming data access layers and context are here

namespace ACEL.BO.ai
{
    public class aiACEL_CUENTA_INVERSIONISTAS_PAGOS
    {
        #region ----> Atributos <----------------------------
        private dcACELDataContext adcCANIRAC;
        #endregion

        #region --> Constructor <----------------------------
        public aiACEL_CUENTA_INVERSIONISTAS_PAGOS(dcACELDataContext padcCANIRAC)
        {
            adcCANIRAC = padcCANIRAC;
        }
        #endregion

        #region --> Métodos para ACEL_CUENTA_INVERSIONISTAS_PAGOS <-----------
        public long InsertarPago(ACEL_CUENTA_INVERSIONISTAS_PAGOS peiGeneral)
        {
            return adcCANIRAC.sp_ACEL_CUENTA_INVERSIONISTAS_PAGOS(dalOperacion.Inserta,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idInversionista,
                   peiGeneral.idPago,
                   peiGeneral.CveInversionista,
                   peiGeneral.MontoPago,
                   peiGeneral.FechaPago,
                   peiGeneral.ViaDeposito,
                   peiGeneral.ReferenciaPago,
                   peiGeneral.TipoPago,
                   peiGeneral.Descripcion,
                   peiGeneral.FechaAlta,
                   peiGeneral.UsuAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuMod,
                   peiGeneral.Status
                   ).FirstOrDefault().idPago.Value; // Assuming the SP returns the primary key
        }

        public void ActualizarPago(ACEL_CUENTA_INVERSIONISTAS_PAGOS peiGeneral)
        {
            adcCANIRAC.sp_ACEL_CUENTA_INVERSIONISTAS_PAGOS(dalOperacion.Actualiza,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idInversionista,
                   peiGeneral.idPago,
                   peiGeneral.CveInversionista,
                   peiGeneral.MontoPago,
                   peiGeneral.FechaPago,
                   peiGeneral.ViaDeposito,
                   peiGeneral.ReferenciaPago,
                   peiGeneral.TipoPago,
                   peiGeneral.Descripcion,
                   peiGeneral.FechaAlta,
                   peiGeneral.UsuAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuMod,
                   peiGeneral.Status);
        }

        public void BajaPago(ACEL_CUENTA_INVERSIONISTAS_PAGOS peiGeneral)
        {
            adcCANIRAC.sp_ACEL_CUENTA_INVERSIONISTAS_PAGOS(dalOperacion.Baja,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idInversionista,
                   peiGeneral.idPago,
                   peiGeneral.CveInversionista,
                   peiGeneral.MontoPago,
                   peiGeneral.FechaPago,
                   peiGeneral.ViaDeposito,
                   peiGeneral.ReferenciaPago,
                   peiGeneral.TipoPago,
                   peiGeneral.Descripcion,
                   peiGeneral.FechaAlta,
                   peiGeneral.UsuAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuMod,
                   peiGeneral.Status);
        }
        #endregion
    }
}