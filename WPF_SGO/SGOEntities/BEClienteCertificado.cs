using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BEClienteCertificado
    {
        public int IdCliente { get;set; }
        public int IdCertificado { get;set; }
        public string DescCertificado { get; set; }
        public decimal Limite { get; set; }
        public int Sobregiro { get; set; }
        public int IdEstado { get;set; }
        public string UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }
    }
}
