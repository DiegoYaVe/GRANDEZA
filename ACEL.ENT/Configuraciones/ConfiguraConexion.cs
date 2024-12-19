using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEL.ENT.Configuraciones
{
    public class ConfiguraConexion
    {
        #region --> Constructor <----------------------------
        public ConfiguraConexion() { }
        #endregion

        #region --> Metodos <--------------------------------
        public string Config()
        {
            try
            {
                //LOCAL
                //return CANSY.ENT.Properties.Settings.Default.DEMO_SYSConnectionString;

                //PRODUCCION
                return ACEL.ENT.Properties.Settings.Default.DataSourceConnectionString;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
