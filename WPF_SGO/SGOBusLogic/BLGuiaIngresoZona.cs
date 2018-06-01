using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGOEntities;
using SGODataAccess;
using System.Data;

namespace SGOBusLogic
{
    public class BLGuiaIngresoZona
    {

        DAGuiaIngresoZona oDa;

        public BLGuiaIngresoZona() {
            oDa = new DAGuiaIngresoZona();
        }

        public int usp_InsGuiaIngresoCabecera(BEGuiaIngresoZona oBe, List<BEGuiaIngresoZona> lstDetalle, BECertificadoVSP oCertificado)
        {
            return oDa.usp_InsGuiaIngresoCabecera(oBe, lstDetalle, oCertificado);
        }

        public List<BEGuiaIngresoZona> usp_LisGuiaIngresoZona(string idlocal, string estado, string filtro, string fechaIncio, string fechaFin)
        {
            return oDa.usp_LisGuiaIngresoZona(idlocal, estado, filtro, fechaIncio, fechaFin);
        }
               
        public List<BEGuiaIngresoZona> usp_LisDatoGuiaIngresoZona(string idGuiaIngreso)
        {
            return oDa.usp_LisDatoGuiaIngresoZona(idGuiaIngreso);
        }
      
        public DataTable usp_ReporteGuiaIngreso(string idGuiaIngreso)
        {
            return oDa.usp_ReporteGuiaIngreso(idGuiaIngreso);
        }

        public int usp_UpdGuiaIngresoRendHumZona(string idGuiaIngreso)
        {
            return oDa.usp_UpdGuiaIngresoRendHumZona(idGuiaIngreso);
        }
       
        public BEGuiaIngresoZona usp_LisGuiaIngresoEditar(int idGuia)
        {
            return oDa.usp_LisGuiaIngresoEditar(idGuia);
        }
        public List<BEGuiaIngresoZona> usp_ListadoDetalleRemision(int idTraslado)
        {
            return oDa.usp_ListadoDetalleRemision(idTraslado);
        }
        public List<BEGuiaIngresoZona> usp_LisGuiaIngresoDetalle(int idGuia)
        {
            return oDa.usp_LisGuiaIngresoDetalle(idGuia);
        }

    }
}
