using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGOEntities;
using SGODataAccess;

namespace SGOBusLogic
{
    public class BLUsuario
    {

        public BEUsuario Validar_Usuario(BEUsuario oBE)
        {
            var oDa = new DAUsuario();
            return oDa.Validar_Usuario(oBE);

        }
    //    public BEUsuario CambiarPwd(BEUsuario oBE)
    //    {
    //        var oDa = new DAUsuario();
    //        return oDa.CambiarPwd(oBE);
    //    }
   }
}
