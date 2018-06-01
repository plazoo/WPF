using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;




using SGOUtil;
using System.Text.RegularExpressions;
using SGOTouch.ServiceUsuario;

namespace SGOTouch
{
    /// <summary>
    /// Lógica de interacción para Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        public int inCount = 0;
        private UsuarioServiceClient _usuarioService;
        
        public Index()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            txtUsuario.Focus();
            _usuarioService = new UsuarioServiceClient();

            //MainWindow objM = new MainWindow();
            //objM.Show();
            //this.Hide();
        }


        private void Login()
        {
            MainWindow objM = new MainWindow(); 

            string user = txtUsuario.Text.Trim();
            string password = txtPassword.Password.Trim();

            /*Webservice*/
            var response = _usuarioService.GetValidarUsuario(user, password);


            if (response == null)
            {
                MessageBox.Show("Usuario y/o Contraseña inválidos");
                
                
                
                
            }
            else
            {      
                Application.Current.Resources["UserData"] = response;
                objM.Show();
                this.Hide();
            }
        }
        /*Inicio : Eventos*/
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {            
            Login();  
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            /* cerrar cuando presiona "esc" */
            if (e.Key == Key.Escape)
                CloseAllWindows();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CloseAllWindows();
        }
        
        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return && e.Key != Key.Enter)
            { return; }
            else { Login(); }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return && e.Key != Key.Enter)
            { return; }
            else { Login(); }
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
        /*Fin : Eventos*/
    }
}
