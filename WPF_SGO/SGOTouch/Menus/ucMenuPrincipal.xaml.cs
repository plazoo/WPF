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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGOTouch.Menus
{
    /// <summary>
    /// Lógica de interacción para ucMenuPrincipal.xaml
    /// </summary>
    /// 
       
    public partial class ucMenuPrincipal :  UserControl
    {
        public string datoP { get; set; }
        private readonly MainWindow _mainWindow;   


        public ucMenuPrincipal( MainWindow mainWindow)
        {
            _mainWindow = mainWindow;  
            InitializeComponent();
        }


        private void btnTicket_Click(object sender, RoutedEventArgs e)
        {
            ucMenuSegundarioTicketPesada _ucMenuSegundarioTicket = new ucMenuSegundarioTicketPesada(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(_ucMenuSegundarioTicket);

            _mainWindow.InPMenuNavegacion = 0;
            _mainWindow.InSMenuNavegacion = -1;

            ucMenuNavegacion ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

         
        }

        private void btnGI_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InPMenuNavegacion = 1;
            _mainWindow.InSMenuNavegacion = -1;

            ucMenuSegundarioGI _ucMenuSegundarioGI = new ucMenuSegundarioGI(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(_ucMenuSegundarioGI);

            ucMenuNavegacion ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

        }
    }
}
