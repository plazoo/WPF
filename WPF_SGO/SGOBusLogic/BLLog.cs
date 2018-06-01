using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGOEntities;
using SGODataAccess;

namespace SGOBusLogic
{
    public class BLLog
    {
        DALog oDa;
        public BLLog()
        {
            oDa = new DALog();
        }
        public int BLInsLog(BELog oEntData)
        {
            return oDa.DAInsLog(oEntData);
        }

    }
}
