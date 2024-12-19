using System;
using System.Collections.Generic;
using System.Linq;
using ACEL.ENT;
using ACEL.DAL;
using ACEL.BO.ai;

namespace ACEL.BO.bo
{
    public class boACEL_CUENTA_INVERSIONISTAS : dalConexion
    {
        #region --> Atributos <---------------------------
        private string aClass = "boACEL_CUENTA_INVERSIONISTAS";
        #endregion

        #region --> Constructor <--------------------------
        public boACEL_CUENTA_INVERSIONISTAS()
        {

        }
        #endregion

        #region --> Métodos <-----------------------------

        public long Inserta(ACEL_CUENTA_INVERSIONISTAS peiACEL_RG)
        {
            long Valido = 0;
            if (TestConnection())
            {
                try
                {
                    Valido = new aiACEL_CUENTA_INVERSIONISTAS(dcACEL).InsertarInversionista(peiACEL_RG);
                }
                catch (Exception ex)
                {
                    Valido = 0;
                }
            }
            return Valido;
        }

        public bool Actualiza(ACEL_CUENTA_INVERSIONISTAS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_INVERSIONISTAS(dcACEL).ActualizarInversionista(peiACEL_RG);
                    Valido = true;
                }
                catch (Exception ex)
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public bool Baja(ACEL_CUENTA_INVERSIONISTAS peiACEL_RG)
        {
            bool Valido = true;
            if (TestConnection())
            {
                try
                {
                    new aiACEL_CUENTA_INVERSIONISTAS(dcACEL).BajaInversionista(peiACEL_RG);
                    Valido = true;
                }
                catch
                {
                    Valido = false;
                }
            }
            return Valido;
        }

        public List<ACEL_CUENTA_INVERSIONISTAS> Buscar()
        {
            List<ACEL_CUENTA_INVERSIONISTAS> elACEL_RG = new List<ACEL_CUENTA_INVERSIONISTAS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS
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

        public List<ACEL_CUENTA_INVERSIONISTAS> Buscar(long pidBranch, long pidCuenta)
        {
            List<ACEL_CUENTA_INVERSIONISTAS> elACEL_RG = new List<ACEL_CUENTA_INVERSIONISTAS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS
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

        public List<ACEL_CUENTA_INVERSIONISTAS> BuscarEvento(long pidBranch, long pidCuenta, long pidEvento)
        {
            List<ACEL_CUENTA_INVERSIONISTAS> elACEL_RG = new List<ACEL_CUENTA_INVERSIONISTAS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS
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

        public List<ACEL_CUENTA_INVERSIONISTAS> Buscar(long pidBranch, long pidCuenta, DateTime pFechaInicio, DateTime pFechaFin)
        {
            List<ACEL_CUENTA_INVERSIONISTAS> elACEL_RG = new List<ACEL_CUENTA_INVERSIONISTAS>();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS
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

        public ACEL_CUENTA_INVERSIONISTAS Buscarid(long pidBranch, long pidCuenta, long pidInversionista)
        {
            ACEL_CUENTA_INVERSIONISTAS elACEL_RG = new ACEL_CUENTA_INVERSIONISTAS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.idInversionista == pidInversionista
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public ACEL_CUENTA_INVERSIONISTAS BuscarNombre(long pidBranch, long pidCuenta, string pNombre)
        {
            ACEL_CUENTA_INVERSIONISTAS elACEL_RG = new ACEL_CUENTA_INVERSIONISTAS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && spc.NomComercial.ToUpper().Contains(pNombre.ToUpper())
                                 select spc).FirstOrDefault();
                }
                catch
                {
                    elACEL_RG = null;
                }
            }
            return elACEL_RG;
        }

        public ACEL_CUENTA_INVERSIONISTAS BuscarNombreCorreo(long pidBranch, long pidCuenta, string pNombre, string pCorreo)
        {
            ACEL_CUENTA_INVERSIONISTAS elACEL_RG = new ACEL_CUENTA_INVERSIONISTAS();
            if (TestConnection())
            {
                try
                {
                    elACEL_RG = (from spc in dcACEL.ACEL_CUENTA_INVERSIONISTAS
                                 where spc.Status == "ACTIVO"
                                 && spc.idBranch == pidBranch
                                 && spc.idCuenta == pidCuenta
                                 && (spc.NomComercial.ToUpper().Contains(pNombre.ToUpper())
                                 || spc.CorreoContacto.ToLower().Contains(pCorreo.ToLower()))
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
