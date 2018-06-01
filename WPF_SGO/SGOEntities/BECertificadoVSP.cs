using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BECertificadoVSP
    {
        public int IdGuia { get; set; }
        public int IdCertificadoVsp { get; set; }
        public int IdCliente { get; set; }
        public string Descripcion { get; set; }
        public int Cosecha { get; set; }
        public decimal Tope { get; set; }
        public decimal Limite { get; set; }
        public decimal Operacion { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoCalculado { get; set; }
        public string Sello { get; set; }
        public string TipoOperacion { get; set; }
        public string Codigo { get; set; }
        public string NomCliente { get; set; }
        public string FechaRegistro { get; set; }
        public string Certificado { get; set; }
    }// FIN PUBLIC CLASS
} // FIN NAMESPACE
