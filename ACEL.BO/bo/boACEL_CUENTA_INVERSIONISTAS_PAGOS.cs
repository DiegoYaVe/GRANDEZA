using System;
using System.Collections.Generic;
using System.Linq;
using ACEL.ENT;
using ACEL.DAL;
using ACEL.BO.ai;

namespace ACEL.BO.bo
{
    public class boACEL_CUENTA_INVERSIONISTAS_PAGOS : dalConexion
    {
        #region --> Atributos <---------------------------
        private string aClass = "boACEL_CUENTA_INVERSIONISTAS_PAGOS";
        #endregion

        #region --> Constructor <--------------------------
        public boACEL_CUENTA_INVERSIONISTAS_PAGOS()
        {

        }
        #endregion

        #region --> Métodos <-----------------------------

        public long Inserta(ACEL_CUENTA_INVERSIONISTAS_PAGOS peiACEL_RG)
        {
            long Valido = 0;
            if (TestConnection())
            {
                try
                {
                    Valido = new aiACEL_CUENTA_INVERSIONISTAS_PAGOS(dcACEL).InsertarPago(peiACEL_RG);
                }
                catch (Exception ex)
                {
                    Valido = 0;
                }
            }
            return Valido;
        }

        public bool Actualiza(ACEL_CUENTA_INVERSIONISTAS_PAGOS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_INVERSIONISTAS_PAGOS(dcACEL).ActualizarPago(peiACEL_RG);
                    Valido = true;
                }
                catch (Exception ex)
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public bool Baja(ACEL_CUENTA_INVERSIONISTAS_PAGOS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_INVERSIONISTAS_PAGOS(dcACEL).BajaPago(peiACEL_RG);
                    Valido = true;
                }
                catch
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> Buscar()
        {
            List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> elACEL_RG = new List<ACEL_CUENTA_INVERSIONISTAS_PAGOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS_PAGOS
                                 where spc.Status == "ACTIVO"
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> Buscar(long pidBranch, long pidCuenta, long pidInversionista)
        {
            List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> elACEL_RG = new List<ACEL_CUENTA_INVERSIONISTAS_PAGOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS_PAGOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idInversionista == pidInversionista
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> Buscar(long pidBranch, long pidCuenta, long pidInversionista, DateTime pFechaInicio, DateTime pFechaFin)
        {
            List<ACEL_CUENTA_INVERSIONISTAS_PAGOS> elACEL_RG = new List<ACEL_CUENTA_INVERSIONISTAS_PAGOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS_PAGOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idInversionista == pidInversionista
                                 && (spc.FechaPago.Value.Date >= pFechaInicio.Date
                                 && spc.FechaPago.Value.Date <= pFechaFin.Date)
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public ACEL_CUENTA_INVERSIONISTAS_PAGOS Buscarid(long pidBranch, long pidCuenta, long pidInversionista, long pidPago)
        {
            ACEL_CUENTA_INVERSIONISTAS_PAGOS elACEL_RG = new ACEL_CUENTA_INVERSIONISTAS_PAGOS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS_PAGOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idInversionista == pidInversionista
                                 && spc.idPago == pidPago
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public bool BuscaReferencia(long pidBranch, long pidCuenta, string pReferencia)
        {
            ACEL_CUENTA_INVERSIONISTAS_PAGOS elACEL_RG = new ACEL_CUENTA_INVERSIONISTAS_PAGOS();
            bool mReferencia = false;
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS_PAGOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.ReferenciaPago == pReferencia
                                 select spc).FirstOrDefault();
                    if (elACEL_RG == null)
                    {
                        mReferencia = false;
                    }
                    else
                    {
                        mReferencia = true;
                    }
                }
                catch
                {
                    elACEL_RG = null;
                    mReferencia = false;
                }
            }
            return mReferencia;
        }

        #endregion
    }
}