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
    /// Lógica de interacción para ucMenuNavegacion.xaml
    /// </summary>
    public partial class ucMenuNavegacion : UserControl
    {
        private readonly MainWindow _mainWindow;
        public ucMenuNavegacion(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            /*Cada vez que se llama al ucMenuNavegacion este asigna botones de acuerdo a los InPMenuNavegacion y InSMenuNavegacion
             los valores de estos son asignado antes de llamar a este usecontrol 20180521*/
            SetButtons(_mainWindow.InPMenuNavegacion, _mainWindow.InSMenuNavegacion);
        }
        private void lstTicket_Click(object sender, RoutedEventArgs e)
        {
            var uc = new ucListarTicketPesada(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);
        }
        private void regTicket_Click(object sender, RoutedEventArgs e)
        {
            var uc = new ucRegistrarTicket();
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);
        }
        private void lstGI_Click(object sender, RoutedEventArgs e)
        {
            var uc = new ucListarGI(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);
        }
        private void regGI_Click(object sender, RoutedEventArgs e)
        {
            var uc = new ucRegistrarGI();
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(uc);
        }

        private void btnTicket_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InPMenuNavegacion = 0;
            _mainWindow.InSMenuNavegacion = -1;

            var ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

            ucMenuSegundarioTicketPesada _ucMenuSegundarioTicket = new ucMenuSegundarioTicketPesada(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(_ucMenuSegundarioTicket);
        }

        private void btnGI_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InPMenuNavegacion = 1;
            _mainWindow.InSMenuNavegacion = -1;


            var ucM = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(ucM);

            var _ucMenuSegundarioGI = new ucMenuSegundarioGI(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(_ucMenuSegundarioGI);
            
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {

            var ucP = new ucMenuPrincipal(_mainWindow);
            _mainWindow.sPanelOne.Children.Clear();
            _mainWindow.sPanelOne.Children.Add(ucP);

            _mainWindow.InPMenuNavegacion = -1;
            _mainWindow.InSMenuNavegacion = -1;

            var uc = new ucMenuNavegacion(_mainWindow);
            _mainWindow.SPanelNavegacion.Children.Clear();
            _mainWindow.SPanelNavegacion.Children.Add(uc);
        }
        private void SetButtons(int pMenu,int sMenu)
        {
            List<string> Pnames = new List<string> { "Ticket", "Guia" }; /*0,1*/
            List<string> Snames = new List<string> { "Listar Ticket", "Registrar Ticket","Listar Guia","Registrar Guia","Editar Ticket","Editar Guia" };/*0,1,2,3,4,5*/



            var stuff = Pnames.OfType<string>();
            Button[] buttonArray = new Button[sMenu > -1?3:2];


            buttonArray[0] = new Button();
            buttonArray[0].Content = "Inicio";
            buttonArray[0].Height = 50;
            buttonArray[0].Width = 100;
            buttonArray[0].Name = "Inicio";
            buttonArray[0].FontSize = 25;
            buttonArray[0].Click += new RoutedEventHandler(btnInicio_Click);


            if (pMenu == -1 )
                return;
            buttonArray[1] = new Button();
            buttonArray[1].Content = Pnames[pMenu];
            buttonArray[1].Height = 50;
            buttonArray[1].Width = 140;
            buttonArray[1].FontSize = 25;

            buttonArray[1].Name = Pnames[pMenu].Replace(" ", "_");
            buttonArray[1].Click += pMenu == 0 ? new RoutedEventHandler(btnTicket_Click) : new RoutedEventHandler(btnGI_Click);
            if (sMenu != -1)
                goto Btn3;
            buttonArray[1].Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#428bca"));
            buttonArray[1].Foreground = new SolidColorBrush(Colors.White);

            if (sMenu == -1)
                goto bucle;

            Btn3:
            buttonArray[2] = new Button();
            buttonArray[2].Content = Snames[sMenu];
            buttonArray[2].Height = 50;
            buttonArray[2].Width = 300;
            buttonArray[2].FontSize = 25;
            buttonArray[2].Name = Snames[sMenu].Replace(" ", "_");
            if (sMenu != 4 && sMenu != 5)
            {
            buttonArray[2].Click +=
                sMenu == 0 ? new RoutedEventHandler(lstTicket_Click) :
                sMenu == 1 ? new RoutedEventHandler(regTicket_Click) :
                sMenu == 2 ? new RoutedEventHandler(lstGI_Click) :
                new RoutedEventHandler(regGI_Click);
            }
            
            buttonArray[2].Background= (SolidColorBrush)(new BrushConverter().ConvertFrom("#428bca"));
            buttonArray[2].Foreground= new SolidColorBrush(Colors.White);


            bucle:
            stkPanelNavegacion.Children.Clear();
            /*Set botones en el panel*/
            foreach (var t in buttonArray)
            {
                stkPanelNavegacion.Children.Add(t);
            }
        }
    }
}
