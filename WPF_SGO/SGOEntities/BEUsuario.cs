using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SGOEntities
{
    public partial class BEUsuario
    {
        
        public int IdUsuario { get; set; }
        public string vNombre { get; set; }
        public string vApePat { get; set; }
        public string vCorreo { get; set; }        
        public string vUsuario { get; set; }
        public string vPassword { get; set; }
        public int nEstado { get; set; }
        public string vApeMat { get; set; }
        public bool bCambioPassword { get; set; }
        public int IdUsuarioPadre { get; set; }
        public int IdRol { get; set; }        
        public int IdSistema { get; set; }
        public string vDescripcionRol { get; set; }
        public string vCodTrabajador { get; set; }
        public string vIdJefatura { get; set; }
        public string vDescripcionJefatura { get; set; }
    }
}
