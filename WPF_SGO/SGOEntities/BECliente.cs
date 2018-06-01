using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BECliente
    {
        public int TipoOperacion { get; set; }
        public int IdCliente {get; set; }

        public int IdTipoCliente { get; set; }
        public string DescTipoCliente { get; set; }

        public int IdTipoOrigen { get; set; }
        public string DescTipoOrigen { get; set; }

        public int IdTipoDocumento { get; set; }
        public string DescTipoDocumento { get; set; }

        public string DocIdentidad { get; set; }
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string TelefonoPrincipal { get; set; }
        public string TelefonoAlternativo { get; set; }
        public int IdDepartamento { get; set; }

        public string DescDepartamento { get; set; }
        public string DescProvincia { get; set; }
        public string DescDistrito { get; set; }
        
        public int IdProvincia { get; set; }
        public int IdDistrito { get; set; }
        public string CentroPoblado { get; set; }
        public string Caserio { get; set; }
        public string DireccionContrato { get; set; }
        
        public string DireccionFactura { get; set; }
        public string ReferenciaFactura { get; set; }
        public string DireccionInstruccion { get; set; }
        public string DireccionMuestra { get; set; }
        public int IdIdioma { get; set; }
        public string NombreLegal { get; set; }
        
        public string ApellidoLegal { get; set; }
        public string DocIdentidadLegal { get; set; }
        public string TelefonoLegal { get; set; }
        public string CorreoLegal { get; set; }
        public int IdEstado { get; set; }

        public int LiderComercial { get; set; }
        public string CodigoExcel { get; set; }

        public string UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }

        public string UsuarioModifica { get; set; }
        public string FechaModifica { get; set; }

        public string codCafePractice1 { get; set; }
        public string codCafePractice2 { get; set; }

        public string CadenaCP { get; set; }
        public string FLOID { get; set; }
        public string Cosecha { get; set; } /*PLAZO 20171206*/
        public string CosechaVigente { get; set; } /*PLAZO 20171211*/
    }
}
