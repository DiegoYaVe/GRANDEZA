using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACEL.ENT;
using ACEL.DAL;
using ACEL.BO.ai;

namespace ACEL.BO.bo
{
    public class boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS : dalConexion
    {
        #region --> Atributos <---------------------------
        private string aClass = "boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS";
        #endregion

        #region --> Constructor <--------------------------
        public boACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS()
        {

        }
        #endregion

        #region --> Métodos <-----------------------------
        public long Inserta(ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiACEL_RG)
        {
            long Valido = 0;
            if (TestConnection())
            {
                try
                {
                    Valido = new aiACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS(dcACEL).Insertar(peiACEL_RG);
                }
                catch
                {
                    Valido = 0;
                }
            }
            return Valido;
        }

        public bool Actualiza(ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS(dcACEL).Actualizar(peiACEL_RG);
                    Valido = true;
                }
                catch
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public bool Baja(ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS(dcACEL).Baja(peiACEL_RG);
                    Valido = true;
                }
                catch
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> Buscar()
        {
            List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
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

        public List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> Buscar(long pidBranch, long pidCuenta)
        {
            List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }
        public ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS BuscarEscalaPago(long pidBranch, long pidCuenta, long pidEvento, string pTipoEscala,
             string pTipoInversionista)
        {
            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS elACEL_RG = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
                                 && spc.TipoEscala == pTipoEscala
                                 && spc.TipoInversionista == pTipoInversionista
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }
        public List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> BuscarEventoInv(long pidBranch, long pidCuenta, long pidEvento, string ptipoInv)
        {
            List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
                                 && spc.TipoInversionista == ptipoInv
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> BuscarEvento(long pidBranch, long pidCuenta, long pidEvento, DateTime pFechaInicio, DateTime pFechaFin)
        {
            List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
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
        public List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> BuscarEvento(long pidBranch, long pidCuenta, long pidEvento)
        {
            List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
                                 select spc).ToList();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS Buscarid(long pidBranch, long pidCuenta, long pidEvento, long pidEscala)
        {
            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS elACEL_RG = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
                                 && spc.idEscala == pidEscala
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }
        public ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS BuscartTipoInversionista(long pidBranch, long pidCuenta, long pidEvento, string pTipoInv, string pTipoEscala, string pTipoPago)
        {
            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS elACEL_RG = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
                                 && spc.TipoInversionista == pTipoInv
                                 && spc.TipoEscala == pTipoEscala
                                 && spc.TipoPago == pTipoPago
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS BuscartTipoInversionista(long pidBranch, long pidCuenta, long pidEvento, string pTipoInv, string pTipoEscala)
        {
            ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS elACEL_RG = new ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS_ESCALAS_CERTIFICADOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
                                 && spc.TipoInversionista == pTipoInv
                                 && spc.TipoEscala == pTipoEscala
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }
        #endregion
    }
}
