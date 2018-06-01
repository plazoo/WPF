using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGOEntities
{
    public class BELog
    {
        public int id { get; set; }
        public string vcException { get; set; }
        public string dtFecReg { get; set; }
        public int idUsuario { get; set; }
    }
}
