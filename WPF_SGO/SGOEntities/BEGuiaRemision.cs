using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOEntities
{
    public class BEGuiaRemision
    {
        public int GuiaRemision { get; set; }
        public string TipoDocumento { get; set; }
        public string Serie { get; set; }
        public bool Anulado { get; set; }
        public string Fecha { get; set; }
        public int CodigoPartida { get; set; }
        public string LugarPartida { get; set; }
        public int CodigoLlegada { get; set; }
        public string LugarLlegada { get; set; }
        public string RucCliente { get; set; }
        public string Cliente { get; set; }
        public string PlacaVehiculo { get; set; }
        public string Constancia { get; set; }
        public string RucTransporte { get; set; }
        public string NombreTransporte { get; set; }
        public string Licencia { get; set; }
        public string EmbarqueDestino { get; set; }
        public string Observacion { get; set; }
        public string ServicioConformacion { get; set; }
        public string EmbarqueMarca { get; set; }
        public string EmbarqueContenedor { get; set; }
        public string EmbarquePrecinto { get; set; }
        public string EmbarqueContrato { get; set; }
        public string EmbarqueVapor { get; set; }
        public string EmbarqueTCon { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoCategoria { get; set; }
        public string NombreProducto { get; set; }
        public decimal KgBruto { get; set; }
        public int Cantidad { get; set; }
        public int IdMedida { get; set; }
        public string UndMedida { get; set; }
        public decimal Factor { get; set; }
        public decimal Tara { get; set; }
        public decimal KgNeto { get; set; }
        



        /*** EMC ***/
        public string CodigoTraslado { get; set; }
        public int Opcion { get; set; }
        public int IdTraslado { get; set; }
        public string IdLocal { get; set; }
        public string FechaTraslado { get; set; }
        public string FECHATRASLADO2 { get; set; } //ADD PLAZO 20170911
        public int IdOrigen { get; set; }
        public string Origen { get; set; }
        public string OrigenDireccion { get; set; }
        public int IdDestino { get; set; }
        public string Destino { get; set; }
        public string DestinoDireccion { get; set; }
        public int IdMotivo { get; set; }
        public string Motivo { get; set; }
        public int IdTipoCafe { get; set; }
        public string TipoCafe { get; set; }
        public int IdEmpresaTransporte { get; set; }
        public string EmpresaTransporte { get; set; }
        public int IdChofer { get; set; }
        public string Chofer { get; set; }
        public string NroInscripcion { get; set; }
        //public decimal KgBruto { get; set; }
        //public decimal Tara { get; set; }
        //public decimal KgNeto { get; set; }
        public int Saco { get; set; }

        public int NroFila { get; set; }

        public int IdGuiaIngreso { get; set; }
        public string GuiaIngreso { get; set; }
        public string Ruma { get; set; }
        public string Lote { get; set; }
        public string Certificado { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
        public string UsuarioRegistro { get; set; }
        public int IdResult { get; set; }

        public decimal PesadaKgNeto { get; set; }
        public decimal Diferencia { get; set; }

        public decimal GiKgBruto { get; set; }
        public decimal GiTara { get; set; }
        public decimal GiKgNeto { get; set; }
        public int GiSaco { get; set; }
        public decimal GiRendimiento { get; set; }
        public decimal GiHumedad { get; set; }
            
        public decimal GrKgBruto { get; set; }
        public decimal GrTara { get; set; }
        public decimal GrKgNeto { get; set; }
        public int GrSaco { get; set; }
        public decimal GrRendimiento { get; set; }
        public decimal GrHumedad { get; set; }

        public int GuiaSaco { get; set; }
        public decimal GuiaKgBruto { get; set; }
        public decimal GuiaTara { get; set; }
        public decimal GuiaKgNeto { get; set; }
        


        public string UsuarioRegistra { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioModifica { get; set; }
        public string FechaModifica { get; set; }
       ///Agregado por: Slater Abanto
        public string IdCertificado { get; set; }
        public int IdDetalleTraslado { get; set; }
        public int FSaco { get; set; }
        public decimal FKgBruto { get; set; }
        public decimal FTara { get; set; }
        public decimal FKgNeto { get; set; }
        public int Fila { get; set; }
        public string CodigoOrdenServicio { get; set; }
        public int IdOrdenServicio { get; set; }
        public string CodigoInternoGrilla { get; set; }
        public decimal PonRendimiento { get; set; }
        public decimal PonHumedad { get; set; }

      public int IdTrasladoEmbarque { get; set; }
      public string Marcas { get; set; }
      public string DestinoEmbarque { get; set; }
      public string Contenedor { get; set; }
      public string Precinto { get; set; }
      public string NumContrato { get; set; }
      public string Vapor { get; set; }
      public decimal TCon { get; set; }
      public string CostoMinimo { get; set; }
      public string TipoGuia { get; set; }
      public string TipoGuiaDescripcion { get; set; }
      public string NSerie { get; set; }
      public string NNumero { get; set; }

    }
}
