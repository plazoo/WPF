using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SGODataAccess;
using SGOEntities;

namespace SGOBusLogic
{
    public class BLTicketPesada
    {
        DATicketPesada oDa;

        public BLTicketPesada()
        {
            oDa = new DATicketPesada();
        }

        public string usp_InsTicketPesada(BETicketPesada oBe)
        {
            return oDa.usp_InsTicketPesada(oBe);
        }
        public List<BETicketPesada> usp_LisTicketPesada(string estado, string filtro, string idlocal, string fechaIncio, string fechaFin)
        {
            return oDa.usp_LisTicketPesada(estado, filtro, idlocal, fechaIncio, fechaFin);
        }
        public DataTable usp_ReporteTicketPesada(string idTicketPesada)
        {
            return oDa.usp_ReporteTicketPesada(idTicketPesada);
        }
        public List<BEGuiaRemision> usp_LisRecepcionGuiaRemisionZona(string idlocal)
        {
            return oDa.usp_LisRecepcionGuiaRemisionZona(idlocal);
        }
    }
}
