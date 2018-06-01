using System;
using System.Windows;

using System.Windows.Navigation;
using SGOTouch.Menus;

using System.Configuration;
using UIShell.NavigationService;
using System.Windows.Forms;
//using MahApps.Metro.Controls;

namespace SGOTouch
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    //public partial class MainWindow : MetroWindow
    public partial class MainWindow : Window //MetroWindow

    {
        NotifyIcon MiniIcon;
        public int InPMenuNavegacion=-1;
        public int InSMenuNavegacion=-1;
        //System.Windows.Forms.NotifyIcon iconoNotificacion;
        public MainWindow()
        {
            if (IsLoaded)
                return;
            InitializeComponent();




            MiniIcon = new System.Windows.Forms.NotifyIcon();
            MiniIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            string IcoPathFileName = ConfigurationManager.AppSettings["IcoPath"].ToString();
            MiniIcon.Icon = new System.Drawing.Icon(IcoPathFileName);

            MiniIcon.BalloonTipText = "SGOTouch sigue ejecutandose...";
            MiniIcon.BalloonTipTitle = "Información";
            MiniIcon.Text = "SGO Touch";
            MiniIcon.Visible = true;

            MiniIcon.Click += new EventHandler(MiniIcon_Click);


            System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem Maximizar = new System.Windows.Forms.MenuItem("Maximizar");
            Maximizar.Click += new EventHandler(Maximizar_Click);
            System.Windows.Forms.MenuItem Cerrar = new System.Windows.Forms.MenuItem("Cerrar");
            Cerrar.Click += new EventHandler(Cerrar_Click);
            menu.MenuItems.Add(Maximizar);
            menu.MenuItems.Add(Cerrar);
            MiniIcon.ContextMenu = menu;

            ucMenuNavegacion uc = new ucMenuNavegacion(this);
            this.SPanelNavegacion.Children.Clear();
            this.SPanelNavegacion.Children.Add(uc);

            ucMenuPrincipal ucP = new ucMenuPrincipal(this);
            this.sPanelOne.Children.Clear();
            this.sPanelOne.Children.Add(ucP);


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
      
        }

        void Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            MiniIcon = null;
        }
        void Maximizar_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = System.Windows.WindowState.Maximized;
        }
        void MiniIcon_Click(object sender, EventArgs e)
        {
            /// se verifica si se encuentra minimizada y se procede a mostrar la ventana.
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.Show();
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }
        private void SubMenuTicketLst_Click(object sender, RoutedEventArgs e)
        {
            
            ucListarTicketPesada uc = new ucListarTicketPesada(this);
            this.sPanelOne.Children.Clear();
            this.sPanelOne.Children.Add(uc);
        }

        private void SubMenuTicketGrab_Click(object sender, RoutedEventArgs e)
        {
            ucRegistrarTicket uc = new ucRegistrarTicket();
            this.sPanelOne.Children.Clear();
            this.sPanelOne.Children.Add(uc);
        }

        private void SubMenuGILst_Click(object sender, RoutedEventArgs e)
        {
            ucListarGI uc = new ucListarGI(this);
            this.sPanelOne.Children.Clear();
            this.sPanelOne.Children.Add(uc);
        }

        private void SubMenuGIReg_Click(object sender, RoutedEventArgs e)
        {
            ucRegistrarGI uc = new ucRegistrarGI();
            this.sPanelOne.Children.Clear();
            this.sPanelOne.Children.Add(uc);
        }

   
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
            MiniIcon.Dispose();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CloseAllWindows();
        }

        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();

                if (MiniIcon != null)
                {
                    MiniIcon.ShowBalloonTip(500);

                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ucMenuNavegacion uc = new ucMenuNavegacion(this);
           
        }
    }
}
