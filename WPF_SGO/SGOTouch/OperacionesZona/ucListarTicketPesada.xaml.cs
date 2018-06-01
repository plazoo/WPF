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
using System.Data;


using SGOUtil;
using SGOTouch.UseControl;
using SGOTouch.ServiceTicketPesada;
using SGOTouch.ServiceUsuario;
using SGOTouch.ServiceTablaGeneral;
using SGOTouch.Menus;

namespace SGOTouch
{
    /// <summary>
    /// Lógica de interacción para ucListarTicketPesada.xaml
    /// </summary>
    

    public partial class ucListarTicketPesada : UserControl
    {
        DataTable tbl = new DataTable();
        
        
        Util objUtil = new Util();
        
        private static TicketPesadaClient _ticketPesadaClient;
        private static TablaGeneralClient _TablaGeneralClient;
        private readonly MainWindow _main;
        public ucListarTicketPesada(MainWindow main)
        {
            InitializeComponent();            
            _main = main;
            _ticketPesadaClient = new TicketPesadaClient();
            _TablaGeneralClient = new TablaGeneralClient();
            CargarLocales();
            DateTime dthoy = DateTime.Today;
            dtFechaFin.Text =dthoy.ToString();
            dtFechaInicio.Text= dthoy.AddDays(-10).ToString();
        }


        private void CargarLocales()
        {
            SelLocalIdEmpresaUsuario oB = new SelLocalIdEmpresaUsuario();
            oB.vDescripcion= "[ Todos ]";
            oB.idLocal = "0";
            
            var lst = _TablaGeneralClient.usp_SelLocalIdEmpresaUsuario(Util.GetIdEmpresa(), Convert.ToInt32(((Usuario)Application.Current.Resources["UserData"]).IdUsuario.ToString())).ToList();
            /*Usuario => entidad del webservice*/
            lst.Add(oB);
            this.cboLocal.ItemsSource = lst;
            this.cboLocal.DisplayMemberPath = "vDescripcion";
            this.cboLocal.SelectedValuePath = "idLocal";
            this.cboLocal.SelectedValue = 0;
            
        }


        private void EditarTicket(usp_LisTicketPesada_Result objTicket)
        {
            Application.Current.Resources["GridDataListarTP"] = objTicket;
            ucRegistrarTicket ucRegTicket = new ucRegistrarTicket();
            _main.sPanelOne.Children.Clear();
            _main.sPanelOne.Children.Add(ucRegTicket);

            _main.InPMenuNavegacion = 0;
            _main.InSMenuNavegacion = 4;

            var uc = new ucMenuNavegacion(_main);
            _main.SPanelNavegacion.Children.Clear();
            _main.SPanelNavegacion.Children.Add(uc);

        }
        
        public static string usp_ReporteTicketPesada(string idTicketPesada)
        {
            string response = String.Empty;
            if (idTicketPesada.Equals(String.Empty))
                idTicketPesada = "0";

            try
            {
                var lst = _ticketPesadaClient.usp_ReporteTicketPesada(idTicketPesada).ToList();
                Application.Current.Resources["Dt_Reporte"] = lst;
                Application.Current.Resources["NombreReporte"] = "rptFormTicketPesada.rdlc";
                Application.Current.Resources["TipoDoc_Reporte"] = "TP";
                SGOTouch.Reporte.Reporte objRpt = new SGOTouch.Reporte.Reporte();
                objRpt.Show();                
            }
            catch (Exception ex)
            {
                response = string.Empty;
            }

            return response;
        }
        /*Inicio: Eventos*/
        private void imgEditar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listViewListTicket.SelectedItem == null) return;
            var selectedClient = listViewListTicket.SelectedItem as usp_LisTicketPesada_Result;
            EditarTicket(selectedClient);

        }
        private void imgPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listViewListTicket.SelectedItem == null) return;
            var selectedClient = listViewListTicket.SelectedItem as usp_LisTicketPesada_Result;
            string IdTicket = selectedClient.IDTICKETPESADA.ToString();
            usp_ReporteTicketPesada(IdTicket);
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string strEstado = cboEstado.SelectionBoxItem.ToString(); /*texto*/
            strEstado = strEstado.ToUpper() == "ACTIVO" ? "1" : strEstado.ToUpper() == "NO ACTIVOS" ? "0" : "%";
            string strFiltro = txtDesc.Text;
            string strLocal = (cboLocal.SelectedValue.ToString() == "0" ? "%" : cboLocal.SelectedValue.ToString());
            string strFecInicio = dtFechaInicio.Text;
            DateTime dthoy = DateTime.Today;
            string strFecFin;
            strFecFin = dthoy.AddDays(+1).ToString();
            var oLst = _ticketPesadaClient.usp_LisTicketPesada(strEstado, strFiltro, strLocal, strFecInicio, strFecFin);
            listViewListTicket.ItemsSource = null;
            listViewListTicket.Items.Refresh();
            listViewListTicket.ItemsSource = oLst;
        }
        /*Fin: Eventos*/


    }
}
