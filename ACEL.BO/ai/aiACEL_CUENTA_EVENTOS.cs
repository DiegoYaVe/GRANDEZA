using System;
using System.Linq;
using ACEL.ENT;
using ACEL.DAL;

namespace ACEL.BO.ai
{
    public class aiACEL_CUENTA_EVENTOS
    {
        #region ----> Atributos <----------------------------
        private dcACELDataContext adcACEL;
        #endregion

        #region --> Constructor <----------------------------
        public aiACEL_CUENTA_EVENTOS(dcACELDataContext padcACEL)
        {
            adcACEL = padcACEL;
        }
        #endregion

        #region --> Metodos <--------------------------------
        public long Insertar(ACEL_CUENTA_EVENTOS peiGeneral)
        {
            return adcACEL.sp_ACEL_CUENTA_EVENTOS(dalOperacion.Inserta,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idEvento,
                   peiGeneral.NombreEvento,
                   peiGeneral.Descripcion,
                   peiGeneral.StatusEvento,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.FechaEvento,
                   peiGeneral.TipoEvento,
                   peiGeneral.TipoCliente,
                   peiGeneral.TipoInversionista
                   ).FirstOrDefault().Column1.Value;
        }

        public void Actualizar(ACEL_CUENTA_EVENTOS peiGeneral)
        {
            adcACEL.sp_ACEL_CUENTA_EVENTOS(dalOperacion.Actualiza,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idEvento,
                   peiGeneral.NombreEvento,
                   peiGeneral.Descripcion,
                   peiGeneral.StatusEvento,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.FechaEvento,
                   peiGeneral.TipoEvento,
                   peiGeneral.TipoCliente,
                   peiGeneral.TipoInversionista);
        }

        public void Baja(ACEL_CUENTA_EVENTOS peiGeneral)
        {
            adcACEL.sp_ACEL_CUENTA_EVENTOS(dalOperacion.Baja,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idEvento,
                   peiGeneral.NombreEvento,
                   peiGeneral.Descripcion,
                   peiGeneral.StatusEvento,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.FechaEvento,
                   peiGeneral.TipoEvento,
                   peiGeneral.TipoCliente,
                   peiGeneral.TipoInversionista);
        }
        #endregion
    }
}
