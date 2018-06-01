using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGODataAccess;
using SGOEntities;


namespace SGOBusLogic
{
    public class BLTablaGenerica
    {
        DATablaGenerica oDa;
        public BLTablaGenerica() {
            oDa = new DATablaGenerica();
        }

        #region INSERT / UPDATE

        

        public List<BETablaGenerica> usp_SelLocalIdEmpresaUsuario(string IdUsuario)
        {
            return oDa.usp_SelLocalIdEmpresaUsuario(IdUsuario);
        }

        public List<BETablaGenerica> usp_Mant_TIPO_COSECHA(BETablaGenerica oBE)
        {
            return oDa.usp_Mant_TIPO_COSECHA(oBE);
        }
        public List<BETablaGenerica> usp_Mant_CLIENTE_COSECHA(BETablaGenerica oBE)
        {
            return oDa.usp_Mant_CLIENTE_COSECHA(oBE);
        }

        public List<BETablaGenerica> usp_LisTipoCafe(string estado)
        {
            return oDa.usp_LisTipoCafe(estado);
        }

        public List<BETablaGenerica> usp_LisComboSacoCafe(string estado)
        {
            return oDa.usp_LisComboSacoCafe(estado);
        }

        public List<BETablaGenerica> usp_LisLaboratorioDisponibleXCliente(int idCLiente, string idLocal)
        {
            return oDa.usp_LisLaboratorioDisponibleXCliente(idCLiente, idLocal);
        }

        public List<BETablaGenerica> usp_LisTipoRuma(string idLocal, string idEstado)
        {
            return oDa.usp_LisTipoRuma(idLocal, idEstado);
        }

        public List<BETablaGenerica> usp_LisBusquedaClienteFiltro(string filtro)
        {
            return oDa.usp_LisBusquedaClienteFiltro(filtro);
        }

        public List<BETablaGenerica> usp_LisContratoConGI(int idCliente, int cosecha)
        {
            return oDa.usp_LisContratoConGI(idCliente, cosecha);
        }

        #endregion
    }
}
