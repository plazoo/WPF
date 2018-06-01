using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGOTouch.ServiceUsuario;
using System.Windows;

namespace SGOTouch.Helpers
{
    public static class Helper
    {
        public static int GetUserId()
        {
            var userId = ((Usuario)Application.Current.Resources["UserData"]).IdUsuario;
            return userId;
        }
        public static Usuario GetUser()
        {
            var usuario = (Usuario)Application.Current.Resources["UserData"];
            return usuario;
        }
    }
}
