using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACEL.ENT;
using ACEL.DAL;

namespace ACEL.BO.ai
{
    public class aiACEL_USUARIOS
    {
        #region ----> Atributos <----------------------------
        private dcACELDataContext adcACEL;
        #endregion

        #region --> Constructor <----------------------------
        public aiACEL_USUARIOS(dcACELDataContext padcACEL)
        {
            adcACEL = padcACEL;
        }
        #endregion

        #region --> Metodos <--------------------------------
        public long Insertar(ACEL_CUENTA_USUARIOS1 peiGeneral)
        {
            return adcACEL.sp_ACEL_CUENTA_USUARIOS1(dalOperacion.Inserta,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idUsuario,
                   peiGeneral.Nombre,
                   peiGeneral.Apellidos,
                   peiGeneral.NomComercial,
                   peiGeneral.TipoUsuario,
                   peiGeneral.Correo,
                   peiGeneral.Telefono,
                   peiGeneral.Puesto,
                   peiGeneral.NivelAcceso,
                   peiGeneral.Cve,
                   peiGeneral.Pass,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.NumInternoSirscom,
                   peiGeneral.ImagenUsuario,
                   peiGeneral.ImagenFirma,
                   peiGeneral.PassCorreo,
                   peiGeneral.FechaNacimiento
                   ).FirstOrDefault().Column1.Value;
        }

        public void Actualizar(ACEL_CUENTA_USUARIOS1 peiGeneral)
        {
            adcACEL.sp_ACEL_CUENTA_USUARIOS1(dalOperacion.Actualiza,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idUsuario,
                   peiGeneral.Nombre,
                   peiGeneral.Apellidos,
                   peiGeneral.NomComercial,
                   peiGeneral.TipoUsuario,
                   peiGeneral.Correo,
                   peiGeneral.Telefono,
                   peiGeneral.Puesto,
                   peiGeneral.NivelAcceso,
                   peiGeneral.Cve,
                   peiGeneral.Pass,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.NumInternoSirscom,
                   peiGeneral.ImagenUsuario,
                   peiGeneral.ImagenFirma,
                   peiGeneral.PassCorreo,
                   peiGeneral.FechaNacimiento);
        }

        public void Baja(ACEL_CUENTA_USUARIOS1 peiGeneral)
        {
            adcACEL.sp_ACEL_CUENTA_USUARIOS1(dalOperacion.Baja,
                   peiGeneral.idBranch,
                   peiGeneral.idCuenta,
                   peiGeneral.idUsuario,
                   peiGeneral.Nombre,
                   peiGeneral.Apellidos,
                   peiGeneral.NomComercial,
                   peiGeneral.TipoUsuario,
                   peiGeneral.Correo,
                   peiGeneral.Telefono,
                   peiGeneral.Puesto,
                   peiGeneral.NivelAcceso,
                   peiGeneral.Cve,
                   peiGeneral.Pass,
                   peiGeneral.FechaAlta,
                   peiGeneral.FechaMod,
                   peiGeneral.UsuAlta,
                   peiGeneral.UsuMod,
                   peiGeneral.Status,
                   peiGeneral.NumInternoSirscom,
                   peiGeneral.ImagenUsuario,
                   peiGeneral.ImagenFirma,
                   peiGeneral.PassCorreo,
                   peiGeneral.FechaNacimiento);
        }

        #endregion
    }
}
