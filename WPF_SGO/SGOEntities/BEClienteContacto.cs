using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BEClienteContacto
    {
        public int IdContactoCliente { get; set; }
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int operacion { get; set; }
        public string DireccionFactura { get; set; }
        public string ReferenciaFactura { get; set; }

        public int IdArea { get; set; }
        public string DescArea { get; set; }
        public int IdCargo { get; set; }
        public string DescCargo { get; set; }
        public int IdEstado { get; set; }
        public string CodigoUsuario { get; set; }
        public string UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }
    }
}
