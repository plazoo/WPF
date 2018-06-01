using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BEClienteBanco
    {
        public int IdCliente { get; set; }
        public int IdBanco { get; set; }
        public string RazonSocial { get; set; }
        public int IdMoneda { get; set; }
        public string DescMoneda { get; set; }
        public string NroCuenta { get; set; }
        public string NroCuentaInterbancario { get; set; }
        public string CodigoSwift { get; set; }
        public string Detraccion { get; set; }
        public int IdEstado { get; set; }
        public string UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }
        public int IdClienteBanco { get; set; } // PLAZO 20171106
    }
}
