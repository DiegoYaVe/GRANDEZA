using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACEL.ENT;
using ACEL.DAL;
using ACEL.BO.ai;

namespace ACEL.BO.bo
{
    public class boACEL_CUENTA_EVENTOS : dalConexion
    {
        #region --> Atributos <---------------------------
        private string aClass = "boACEL_CUENTA_EVENTOS";
        #endregion

        #region --> Constructor <--------------------------
        public boACEL_CUENTA_EVENTOS()
        {

        }
        #endregion

        #region --> Métodos <-----------------------------
        public long Inserta(ACEL_CUENTA_EVENTOS peiACEL_RG)
        {
            long Valido = 0;
            if (TestConnection())
            {
                try
                {
                    Valido = new aiACEL_CUENTA_EVENTOS(dcACEL).Insertar(peiACEL_RG);
                }
                catch
                {
                    Valido = 0;
                }
            }
            return Valido;
        }

        public bool Actualiza(ACEL_CUENTA_EVENTOS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_EVENTOS(dcACEL).Actualizar(peiACEL_RG);
                    Valido = true;
                }
                catch
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public bool Baja(ACEL_CUENTA_EVENTOS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_EVENTOS(dcACEL).Baja(peiACEL_RG);
                    Valido = true;
                }
                catch(Exception EX)
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public List<ACEL_CUENTA_EVENTOS> Buscar()
        {
            List<ACEL_CUENTA_EVENTOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS
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

        public List<ACEL_CUENTA_EVENTOS> Buscar(long pidBranch, long pidCuenta)
        {
            List<ACEL_CUENTA_EVENTOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS
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

        public List<ACEL_CUENTA_EVENTOS> Buscar(long pidBranch, long pidCuenta, DateTime pFechaInicio, DateTime pFechaFin)
        {
            List<ACEL_CUENTA_EVENTOS> elACEL_RG = new List<ACEL_CUENTA_EVENTOS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS
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

        public ACEL_CUENTA_EVENTOS Buscarid(long pidBranch, long pidCuenta, long pidEvento)
        {
            ACEL_CUENTA_EVENTOS elACEL_RG = new ACEL_CUENTA_EVENTOS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idEvento == pidEvento
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }
        public ACEL_CUENTA_EVENTOS BuscarNom(long pidBranch, long pidCuenta, string pNombre)
        {
            ACEL_CUENTA_EVENTOS elACEL_RG = new ACEL_CUENTA_EVENTOS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_EVENTOS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.NombreEvento.ToUpper().Contains(pNombre.ToUpper())
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
