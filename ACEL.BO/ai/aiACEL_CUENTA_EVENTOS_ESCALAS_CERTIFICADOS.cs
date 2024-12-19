using System;
using System.Linq;
using ACEL.ENT;
using ACEL.DAL;

namespace ACEL.BO.ai
{
    public class aiACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
    {
        #region ----> Atributos <----------------------------
        private dcACELDataContext adcACEL;
        #endregion

        #region --> Constructor <----------------------------
        public aiACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS(dcACELDataContext padcACEL)
        {
            adcACEL = padcACEL;
        }
        #endregion

        #region --> Metodos <--------------------------------
        public long Insertar(ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiGeneral)
        {
            return adcACEL.sp_ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS(dalOperacion.Inserta,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idEvento,
                   peiGeneral.idEscala,
                   peiGeneral.TipoEscala,
                   peiGeneral.TipoPago,
                   peiGeneral.ValorNominal,
                   peiGeneral.Aticipo,
                   peiGeneral.EngancheLiquidacion,
                   peiGeneral.Mensualidad,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.TipoInversionista
                   ).FirstOrDefault().Column1.Value;
        }

        public void Actualizar(ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiGeneral)
        {
            adcACEL.sp_ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS(dalOperacion.Actualiza,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idEvento,
                   peiGeneral.idEscala,
                   peiGeneral.TipoEscala,
                   peiGeneral.TipoPago,
                   peiGeneral.ValorNominal,
                   peiGeneral.Aticipo,
                   peiGeneral.EngancheLiquidacion,
                   peiGeneral.Mensualidad,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.TipoInversionista);
        }

        public void Baja(ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiGeneral)
        {
            adcACEL.sp_ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS(dalOperacion.Baja,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idEvento,
                   peiGeneral.idEscala,
                   peiGeneral.TipoEscala,
                   peiGeneral.TipoPago,
                   peiGeneral.ValorNominal,
                   peiGeneral.Aticipo,
                   peiGeneral.EngancheLiquidacion,
                   peiGeneral.Mensualidad,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.TipoInversionista);
        }
        #endregion
    }
}
