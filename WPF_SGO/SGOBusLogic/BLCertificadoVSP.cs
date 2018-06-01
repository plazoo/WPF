using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGOEntities;
using SGODataAccess;
using System.Data;

namespace SGOBusLogic
{
   public class BLCertificadoVSP
   {
      DACertificadoVSP oDa;

      public BLCertificadoVSP()
      {
         oDa = new DACertificadoVSP();
      }

        public List<BECertificadoVSP> usp_LisSaldoCertificado(string idCliente, string cosecha)
        {
            return oDa.usp_LisSaldoCertificado(idCliente, cosecha);
        }
      
    }
}
