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
    /// Lógica de interacción para ucMenuSegundarioGI.xaml
    /// </summary>
    public partial class ucMenuSegundarioGI : UserControl
    {
        private readonly MainWindow _mainWindow;
  
        public ucMenuSegundarioGI(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;


          
        }

        private void btnListarGI_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InPMenuNavegacion = 1;
            _mainWindow.InSMenuNavegacion = 2;

            ucMenuNavegacion ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

            ucListarGI uc = new ucListarGI(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);
            
        }

        private void btnRegistrarGI_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InPMenuNavegacion = 1;
            _mainWindow.InSMenuNavegacion = 3;

            ucMenuNavegacion ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

            ucRegistrarGI uc = new ucRegistrarGI();
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);
            

        }
    }
}
