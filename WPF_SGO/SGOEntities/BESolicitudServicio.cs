using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BESolicitudServicio
    {
        public int TipoOperacion { get; set; }

        public int IdOrdenServicio { get; set; }
        public string IdLocal { get; set; }
        public string DescLocal { get; set; }
        public int IdTipoServicio { get; set; }
        public string DescTipoServicio { get; set; }

        public string TipoDocumento { get; set; }

        public int IdCliente { get; set; }
        public string DescCliente { get; set; }

        public string DescTipoCafe { get; set; }

        public int InicioSaco { get; set; }
        public decimal InicioTara { get; set; }
        public decimal InicioKgBruto { get; set; }
        public decimal InicioKgNeto { get; set; }

        public int FinTipoSaco { get; set; }
        public int FinSaco { get; set; }
        public decimal FinTara { get; set; }
        public decimal FinKgBruto { get; set; }
        public decimal FinRendimiento { get; set; }
        public decimal FinKgNeto { get; set; }

        public decimal DiferenciaSacoInOut { get; set; }
        public decimal DiferenciaKgInOut { get; set; }
        public decimal PorcentajeInOut { get; set; }

        public int IdDetalle { get; set; }
        public int DetSaco { get; set; }
        public decimal DetTara { get; set; }
        public decimal DetKgBruto { get; set; }
        public decimal DetRendimiento { get; set; }
        public decimal DetKgNeto { get; set; }

        public int IdEstado { get; set; }
        public string DescEstado { get; set; }
        public string UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }

        public int IdTicketPesada { get; set; }
        public int IdGuia { get; set; }

        public int TotalDia { get; set; }
        public string OrdenServicio { get; set; }
        public string CodigoOrdenServicio { get; set; }


        public int IdCalidad { get; set; }
        public string DescCalidad { get; set; }
        public string Instruccion { get; set; }

        public int IdTipo { get; set; }
        public int IdTipoSaco { get; set; }
        public string DescTipoSaco { get; set; }
        public int Lote { get; set; }
        public int IdSegunda { get; set; }
        public string DescSegunda { get; set; }
        public int NroSaco { get; set; }
        public int IdTipoCafe { get; set; }
        public decimal KgBruto { get; set; }
        public decimal Tara { get; set; }
        public decimal KgNeto { get; set; }

        public decimal Rendimiento { get; set; }
        ///Agregado por: Salter Abanto
        public string Certificado { get; set; }
        public string IdCertificado { get; set; }
        public string TipoServicioDescripcion { get; set; }
        public string IdTipoServicioUno { get; set; }
        //Agregado por: Slater Abanto
        public string CodigoInternoGrilla { get; set; }
        public decimal Humedad { get; set; }
        public int Saco { get; set; }
        public string Propiedad { get; set; }
        public decimal Operacion { get; set; }
        public decimal saldo { get; set; }
        public int GrSaco { get; set; }
        public decimal GrKgBruto { get; set; }
        public decimal GrTara { get; set; }
        public decimal GrKgNeto { get; set; }
        public decimal GrRendimiento { get; set; }
        public decimal GrHumedad { get; set; }
        public int FSaco { get; set; }
        public decimal FKgBruto { get; set; }
        public decimal FTara { get; set; }
        public decimal FKgNeto { get; set; }
        public int Fila { get; set; }
        public int IdDetalleTraslado { get; set; }
        //add: 24-05-2017
        public Nullable<int> IdTipoRuma { get; set; }
        public string TipoRuma { get; set; }
        public string ClienteMascara { get; set; } // ADD PLAZO 20170831
        public int inValidado { get; set; } // ADD PLAZO 20171003
        public string vUsuario { get; set; } // ADD PLAZO 20171003
        public string dtFechaValida { get; set; } // ADD PLAZO 20171003

        /*INICIO PLAZO 20171012*/
        public int IdResultado { get; set; }
        public int IdTipoOperacion { get; set; }
        public string vcTipoOperacion { get; set; }
        public string CODIGO { get; set; }
        public string CODIGO_USADO { get; set; }
        public int SacoUsado { get; set; }
        public decimal KgNetoUsado { get; set; }
        public int SacoSaldo { get; set; }
        public decimal KgNetoSaldo { get; set; }
        public Int64 num { get; set; }
        /*FIN PLAZO 20171012*/

        public string CodigoLiquidacion { get; set; } /*20171114*/
        public string CodigoTraslado { get; set; } /*20171115*/
    }
}
