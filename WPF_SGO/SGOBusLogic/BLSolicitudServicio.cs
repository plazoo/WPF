using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGOEntities;
using SGODataAccess;
using System.Data;

namespace SGOBusLogic
{
    public class BLSolicitudServicio
    {
        DASolicitudServicio oDa;

        public BLSolicitudServicio() {
            oDa = new DASolicitudServicio();
        }

        public List<BETicketPesada> usp_LisTickedPesadaEnProceso(int proceso, string idlocal, int cliente, int estado)
        {
            return oDa.usp_LisTickedPesadaEnProceso(proceso, idlocal, cliente, estado);
        }

    } // FIN TODO
}
