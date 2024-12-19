using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEL.DAL
{
    public class dalOperacion
    {
        #region --> Constantes <-----------------------------
        public const int Ninguno = 0;
        public const int Inserta = 1;
        public const int Actualiza = 2;
        public const int Busca = 3;
        public const int Consulta = 4;
        public const int Activa = 5;
        public const int Lista = 6;
        public const int Baja = 8;
        public const int Proceso = 9;
        public const int Valida = 10;
        #endregion
    }
    public class dalFormaPago
    {
        public const string TarjetaCredito = "TARJETA CREDITO";
        public const string TarjetaDebito = "TARJETA DEBITO";
        public const string Cheque = "CHEQUE";
        public const string Transferencia = "TRANSFERENCIA";
    }
}
