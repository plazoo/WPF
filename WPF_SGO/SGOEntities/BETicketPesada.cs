using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGOEntities
{
  public class BETicketPesada
    {

        /*INICIO***********  TABLA TICKET_PESADA*/
        public string IdTicketPesada { get; set; }
        public string IdTicketPesadaZona { get; set; }        
        public int IdCliente { get; set; }
        public int IdContactoCliente { get; set; }        
        public string IdLocal { get; set; }
        public string FechaTicket { get; set; }
        public int Cosecha { get; set; }
        public string IdLaboratorio { get; set; }
        public int IdTipoCafe { get; set; }
        public int NroSaco { get; set; }
        public int IdSaco { get; set; }
        public decimal PesoSaco { get; set; }
        public decimal Tara { get; set; }
        public decimal KgBruto { get; set; }
        public decimal KgNeto { get; set; }
        public decimal DsctoAgua { get; set; }
        public decimal KgSeco { get; set; }
        public int IdProceso { get; set; }
        public string Observacion { get; set; }
        public int IdEstado { get; set; }
        public string UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioModifica { get; set; }
        public string FechaModifica { get; set; }
        public int IdGuiaIngreso { get; set; }
        public int IdExcel { get; set; }
        public int GiExcel { get; set; }
        public string IdGuiaRemision { get; set; }
        public string GuiaRemision { get; set; }
        /*FIN**************** TABLA TICKET_PESADA*/


        public int TipoOperacion { get; set; }
        public int IdTicket { get; set; }
        public string ContactoCliente { get; set; }
        public string DocIdentidadCliente { get; set; }
        public string DescCliente { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }  
        public string CodigoLaboratorioZona { get; set; }
        public string DescLaboratorio { get; set; }
        public string AnalisisLaboratorio { get; set; }
        public int IdTipoSaco { get; set; }
        public string Proceso { get; set; }
        public string Estado { get; set; }
        public string Calidad { get; set; }
        public string Humedad { get; set; }
        public string Rendimiento { get; set; }
        public string TipoCafe { get; set; }
        
    }
}
