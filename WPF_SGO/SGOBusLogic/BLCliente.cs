using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGODataAccess;
using SGOEntities;

namespace SGOBusLogic
{
    public class BLCliente
    {
        DACliente oDa;
        public BLCliente() {
            oDa = new DACliente();
        }
        //public int usp_InsCliente(BECliente oBe, List<BEClienteBanco> lstClienteBanco, List<BEClienteContacto> lstClienteContacto, List<BEClienteCertificado> lstClienteCertificado)
        //{
        //    return oDa.usp_InsCliente(oBe, lstClienteBanco, lstClienteContacto, lstClienteCertificado);
        //}
        //public List<BETablaGenerica> usp_LisComboCliente(string estado)
        //{
        //    return oDa.usp_LisComboCliente(estado);
        //}
        //public List<BETablaGenerica> usp_LisComboDetalleCliente(string estado)
        //{
        //    return oDa.usp_LisComboDetalleCliente(estado);
        //}
        //public List<BECliente> usp_LisCliente(string estado, string filtro)
        //{
        //    return oDa.usp_LisCliente(estado, filtro);
        //}
        public List<BEClienteContacto> usp_LisClienteContacto(string idCliente, string estado)
        {
            return oDa.usp_LisClienteContacto(idCliente, estado);
        }
        //public List<BEClienteCertificado> usp_LisClienteCertificado(string idCliente, string estado)
        //{
        //    return oDa.usp_LisClienteCertificado(idCliente, estado);
        //}
        //public List<BEClienteBanco> usp_LisClienteBanco(string idCliente, string estado)
        //{
        //    return oDa.usp_LisClienteBanco(idCliente, estado);
        //}
        //public int usp_UpdClienteBanco(BEClienteBanco oBe)
        //{
        //    return oDa.usp_UpdClienteBanco(oBe);
        //}

        //public List<BETablaGenerica> usp_LisStatusVSPxCliente(string idCliente, string estado){
        //    return oDa.usp_LisStatusVSPxCliente(idCliente, estado);
        //}
        //public int usp_LisValidaDocumentoCliente(string numDocumento){
        //    return oDa.usp_LisValidaDocumentoCliente(numDocumento);
        //}
        //public List<BECliente> usp_LisLiderComercial(string filtro, int idCliente, int opcion)
        //{
        //    return oDa.usp_LisLiderComercial(filtro, idCliente, opcion);
        //}

        //public int usp_UpdLiderComercial(int idLider, int idCliente)
        //{
        //    return oDa.usp_UpdLiderComercial(idLider, idCliente);
        //}
        //#region "07-08-2017"
        //public ICollection<BECliente> usp_LisClienteEstado(int idEstado)
        //{
        //    return oDa.usp_LisClienteEstado(idEstado);
        //}
        //#endregion
    }
}
