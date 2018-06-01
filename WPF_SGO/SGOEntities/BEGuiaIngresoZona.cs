using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BEGuiaIngresoZona
    {
        public int TipoOperacion { get; set; }
        public int Indice { get; set; }
        public int IdGuia { get; set; }
        public int IdGuiaIngreso { get; set; }
        public string IdLocal { get; set; }
        public string Local { get; set; }
        public int IdCliente { get; set; }
        public int IdContactoCliente { get; set; }
        public string ContactoCliente { get; set; }
        public string RumaDestino { get; set; }
        public string Ruma { get; set; }
        public int TotalSaco { get; set; }
        public decimal TotalKgBruto { get; set; }
        public decimal TotalTara { get; set; }
        public decimal TotalDsctoAgua { get; set; }

        public decimal TotalKgNeto { get; set; }
        public int IdEstado { get; set; }
        public int Nuevo { get; set; }
        public int IdProceso { get; set; }
        public string UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioModifica { get; set; }
        public string FechaModifica { get; set; }

        //****

        public int IdGuiaIngresoDetalle { get; set; }
        public int IdTicketPesada { get; set; }
        public int IdOrdenServicio { get; set; }
        public string OrdenServicio { get; set; }
        public int Lote { get; set; }
        public string TipoServicio { get; set; }
        public int IdResultado { get; set; }
        public int NroSaco { get; set; }
        public int Saco { get; set; }
        public decimal KgBruto { get; set; }
        public decimal Tara { get; set; }
        public decimal KgNeto { get; set; }

        //*****
        public string IdCertificadoVSP { get; set; }
        public string DescCertificacion { get; set; }

        public string IdCertificado { get; set; }
        public string ModalidadIngreso { get; set; }

        public string Id { get; set; }
        public string GuiaIngreso { get; set; }
        public string DescCliente { get; set; }
        public string DescEstado { get; set; }
        public string DescPropiedad { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Calidad { get; set; }

        public string Observaciones { get; set; }

        public string IdTipoCafe { get; set; }
        public string DescTipoCafe { get; set; }

        public int Impresion { get; set; }
        public string Certificado { get; set; }
        public int Cosecha { get; set; }

        public decimal Operacion { get; set; }
        public decimal Saldo { get; set; }

        public decimal PromedioRendimiento { get; set; }
        public decimal PromedioHumedad { get; set; }
        public decimal PromedioTaza { get; set; }

        public string CodigoInternoGrilla { get; set; }

        //*****
        public int DivisionGuia { get; set; }
        public int SacoOperacion { get;set; }
        public decimal KgBrutoOperacion { get;set; }
        public decimal TaraOperacion { get;set; }
        public decimal KgNetoOperacion  { get;set; }
        public int SacoSaldo { get;set; }
        public decimal KgBrutoSaldo  { get;set; }
        public decimal TaraSaldo  { get;set; }
        public decimal KgNetoSaldo  { get;set; }

        public int idProductor { get; set; }
        public string Productor { get; set; }

        public int IdNew { get; set; }

        public int IdIngresoPRP { get; set; }
        public int IdOficinaOrigen { get; set; }
        public int IdTraslado { get; set; }

        public string DescOficinaOrigen{ get; set; }

        public string GuiaRemision { get; set; }
        public string NroFila { get; set; }
        public string GuiaRemisionExterna { get; set; }

        public int DetalleIdCliente { get; set; }
        public int DetalleIdResultado { get; set; }

		///Solicitud de servicio
		public string CODIGOORDENSERVICIO { get; set; }
		public string VUsuario { get; set; }
		///Agregado por: Slater AR (ElDragon)
		public string ProcesoDescripcion { get; set; }
        public string Observacion { get; set; }
        public string ContratoRef { get; set; }
        public string Liquidacion { get; set; }
        public string Servicio { get; set; }

        public int IdContrato { get; set; }
        public string vcClieMascara { get; set; } //ADD PLAZO 20170901
        public int idClieMascara { get; set; } //ADD PLAZO 20170901
        public decimal DIF_NETOYAGUA { get; set; }
        public string vcCosecha { get; set; } // ADD PLAZO 20171211

    }
}
