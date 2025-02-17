using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACEL.ENT;
using ACEL.DAL;
using ACEL.BO.ai;

namespace ACEL.BO.bo
{
    public class boACEL_INDICES : dalConexion
    {
        #region --> Atributos <---------------------------
        private string aClass = "boACEL_INDICES";
        #endregion

        #region --> Constructor <--------------------------
        public boACEL_INDICES()
        {

        }
        #endregion

        #region --> Métodos <-----------------------------

        public List<ACEL_CUENTA_INDICES> Buscar()
        {
            List<ACEL_CUENTA_INDICES> elACEL_RG = new List<ACEL_CUENTA_INDICES>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INDICES
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

        public List<ACEL_CUENTA_INDICES> BuscarIndice(long pidBranch, long pidCuenta, long pidIndice)
        {
            List<ACEL_CUENTA_INDICES> elACEL_RG = new List<ACEL_CUENTA_INDICES>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INDICES
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idIndice == pidIndice
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public List<ACEL_CUENTA_INDICES> BuscarValorAsociado(long pidBranch, long pidCuenta, long pidValorAsociado)
        {
            List<ACEL_CUENTA_INDICES> elACEL_RG = new List<ACEL_CUENTA_INDICES>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INDICES
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.ValorAsociado == pidValorAsociado
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public List<ACEL_CUENTA_INDICES> BuscarValorAsociadoDetalle(long pidBranch, long pidCuenta, long pidValorAsociado)
        {
            List<ACEL_CUENTA_INDICES> elACEL_RG = new List<ACEL_CUENTA_INDICES>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INDICES
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.ValorAsociado == pidValorAsociado
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public List<ACEL_CUENTA_INDICES> Buscar(long pidBranch, long pidCuenta, DateTime pFechaInicio, DateTime pFechaFin)
        {
            List<ACEL_CUENTA_INDICES> elACEL_RG = new List<ACEL_CUENTA_INDICES>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INDICES
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && (spc.FechaAlta.Value.Date >= pFechaInicio.Date
                                 && spc.FechaAlta.Value.Date <= pFechaFin.Date)
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public ACEL_CUENTA_INDICES Buscarid(long pidBranch, long pidCuenta, long pidIndice, long pidDetalle)
        {
            ACEL_CUENTA_INDICES elACEL_RG = new ACEL_CUENTA_INDICES();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INDICES
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idIndice == pidIndice
                                 && spc.idDetalle == pidDetalle
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public long BuscaridConsecutivo(long pidBranch, long pidCuenta)
        {
            long elACEL_RG = 0;
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INDICES
                                 where spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 select spc).Max(x => x.idIndice) + 1;
                }
                catch (Exception ex)
                {
                    elACEL_RG = 1;
                }
            }
            return elACEL_RG;
        }

        #endregion
    }
}
