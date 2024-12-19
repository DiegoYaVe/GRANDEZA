using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACEL.ENT;
using ACEL.DAL;
using ACEL.BO.ai;

namespace ACEL.BO.bo
{
    public class boACEL_USUARIOS : dalConexion
    {
        #region --> Atributos <---------------------------
        private string aClass = "boACEL_USUARIOS";
        #endregion

        #region --> Costructor <--------------------------
        public boACEL_USUARIOS()
        { 
            
        }
        #endregion

        #region --> Metodos <-----------------------------

        public long Inserta(ACEL_CUENTA_USUARIOS1 peiACEL_RG)
        {
            long Valido = 0;
            if (TestConnection())
            {
                try
                {
                    Valido = new aiACEL_USUARIOS(dcACEL).Insertar(peiACEL_RG);
                }
                catch
                {
                    Valido = 0;
                }
            }
            return Valido;
        }
        public bool Actualiza(ACEL_CUENTA_USUARIOS1 peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_USUARIOS(dcACEL).Actualizar(peiACEL_RG);
                    Valido = true;
                }
                catch
                {
                    Valido = false;
                }
            }
            return Valido;
        }
        public bool Baja(ACEL_CUENTA_USUARIOS1 peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_USUARIOS(dcACEL).Baja(peiACEL_RG);
                    Valido = true;
                }
                catch
                {
                    Valido = false;
                }
            }
            return Valido;
        }
        public List<ACEL_CUENTA_USUARIOS1> Buscar()
        {
            List<ACEL_CUENTA_USUARIOS1> elACEL_RG = new List<ACEL_CUENTA_USUARIOS1>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_USUARIOS1
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
        public List<ACEL_CUENTA_USUARIOS1> Buscar(long pidBranch, long pidCuenta)
        {
            List<ACEL_CUENTA_USUARIOS1> elACEL_RG = new List<ACEL_CUENTA_USUARIOS1>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_USUARIOS1
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

        public List<ACEL_CUENTA_USUARIOS1> Buscar(long pidBranch, long pidCuenta, DateTime pFechaInicio, DateTime pFechaFin)
        {
            List<ACEL_CUENTA_USUARIOS1> elACEL_RG = new List<ACEL_CUENTA_USUARIOS1>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_USUARIOS1
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
        public ACEL_CUENTA_USUARIOS1 Buscarid(long pidBranch, long pidCuenta, long pidUsuario)
        {
            ACEL_CUENTA_USUARIOS1 elACEL_RG = new ACEL_CUENTA_USUARIOS1();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_USUARIOS1
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idUsuario == pidUsuario
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }
        public ACEL_CUENTA_USUARIOS1 BuscarCve(long pidBranch, long pidCuenta, string pCveUsuario)
        {
            ACEL_CUENTA_USUARIOS1 elACEL_RG = new ACEL_CUENTA_USUARIOS1();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_USUARIOS1
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.Cve == pCveUsuario
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public ACEL_CUENTA_USUARIOS1 BuscarCvePass(long pidBranch, long pidCuenta, string pCveUsuario, string pPass)
        {
            ACEL_CUENTA_USUARIOS1 elACEL_RG = new ACEL_CUENTA_USUARIOS1();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_USUARIOS1
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.Cve == pCveUsuario
                                 && spc.Pass == pPass
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
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_USUARIOS1
                                 where spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 select spc).Max(x => x.idUsuario) + 1;
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
