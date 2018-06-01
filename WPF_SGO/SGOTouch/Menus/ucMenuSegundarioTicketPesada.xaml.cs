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
    /// Lógica de interacción para ucMenuSegundarioTicketPesada.xaml
    /// </summary>
    public partial class ucMenuSegundarioTicketPesada : UserControl
    {
        private readonly MainWindow _mainWindow;


        public ucMenuSegundarioTicketPesada(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
         

        }

        private void btnListarTicket_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InPMenuNavegacion = 0;
            _mainWindow.InSMenuNavegacion = 0;

            ucMenuNavegacion ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

            ucListarTicketPesada uc = new ucListarTicketPesada(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);

         
        }

        private void btnRegistrarTicket_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InPMenuNavegacion = 0;
            _mainWindow.InSMenuNavegacion = 1;
            ucMenuNavegacion ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

            ucRegistrarTicket uc = new ucRegistrarTicket();
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);
        
        }
    }
}
