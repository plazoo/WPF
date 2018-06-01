using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BETablaGenerica
    {
        public int Opcion { get;set; }
        public int Codigo { get; set; }
        public string DescripcionUno { get; set;}
        public string DescripcionDos { get; set; }
        public string DescripcionTres { get; set; }
        public string DescripcionCuatro { get; set; }
        public string DescripcionCinco { get; set; }
        public string DescripcionSeis { get; set; }
        public string DescripcionSiete { get; set; }
        public string DescripcionOcho { get; set; }
        public string DescripcionNueve { get; set; }
        public string DescripcionDiez { get; set; }

        public int Estado {get; set;}
        public int BlackList { get; set; }
        public string Comentario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Usuario { get; set; }
        public string Fecha { get; set; }

        public decimal NumeroUno { get; set; }
        public decimal NumeroDos { get; set; }
        public decimal NumeroTres { get; set; }
        public decimal NumeroCuatro { get; set; }
        public decimal NumeroCinco { get; set; }

        public int EnteroUno { get; set; }
        public int EnteroDos { get; set; }
        public int EnteroTres { get; set; }

        public int IdTraslado { get; set; }
        public int inResult { get; set; }

        /*INICIO PLAZO 20171206*/
        public int IdCosecha { get; set; }
        public int IdCliente { get; set; }
        public int InEstado { get; set; }
        public int BUS { get; set; }
        public int InVigente { get; set; }
        /*FIN PLAZO 20171206*/
    }
}
