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
using SGOTouch.Helpers;
using SGOTouch.ServiceUsuario;
using SGOTouch.ServiceGuiaIngresoZona;
using SGOTouch.ServiceTablaGeneral;
using SGOTouch.Menus;

namespace SGOTouch
{
    /// <summary>
    /// Lógica de interacción para ucListarGI.xaml
    /// </summary>
    public partial class ucListarGI : UserControl
    {
        DataTable tbl = new DataTable();
        Util objUtil = new Util();
        private static TablaGeneralClient _TablaGeneralClient;
        private static GuiaIngresoZonaClient _GuiaIngresoZonaClient;
        private readonly MainWindow _main;
        public ucListarGI(MainWindow main)
        {
            InitializeComponent();
            _TablaGeneralClient = new TablaGeneralClient();
            _GuiaIngresoZonaClient = new GuiaIngresoZonaClient();
            _main = main;
            CargarLocales();
            DateTime dthoy = DateTime.Today;
            dtFechaFin.Text = dthoy.ToString();
            dtFechaInicio.Text = dthoy.AddDays(-10).ToString();
        }
        private void CargarLocales()
        {

            SelLocalIdEmpresaUsuario oB = new SelLocalIdEmpresaUsuario();
            oB.vDescripcion = "[ Todos ]";
            oB.idLocal = "0";

            var lst = _TablaGeneralClient.usp_SelLocalIdEmpresaUsuario(Util.GetIdEmpresa(), Convert.ToInt32(((Usuario)Application.Current.Resources["UserData"]).IdUsuario.ToString())).ToList();
            /*Usuario => entidad del webservice*/
            lst.Add(oB);
            this.cboLocal.ItemsSource = lst;
            this.cboLocal.DisplayMemberPath = "vDescripcion";
            this.cboLocal.SelectedValuePath = "idLocal";
            this.cboLocal.SelectedValue = 0;            
        }
        
        private void EditarGI(usp_LisGuiaIngresoZona_Result oGuia)
        {
            Application.Current.Resources["GridDataListarGI"] = oGuia;
            ucRegistrarGI ucRegGI = new ucRegistrarGI();
            _main.sPanelOne.Children.Clear();
            _main.sPanelOne.Children.Add(ucRegGI);

            _main.InPMenuNavegacion = 1;
            _main.InSMenuNavegacion = 5;

            var uc = new ucMenuNavegacion(_main);
            _main.SPanelNavegacion.Children.Clear();
            _main.SPanelNavegacion.Children.Add(uc);
        }
        public static string usp_ReporteGuiaIngreso(string IdGuia)
        {

            string response = String.Empty;
            if (IdGuia.Equals(String.Empty))
                IdGuia = "0";
            try
            {
                var lst = _GuiaIngresoZonaClient.usp_ReporteGuiaIngreso(IdGuia).ToList();
                Application.Current.Resources["Dt_Reporte"] = lst;
                Application.Current.Resources["NombreReporte"] = "rptFormGuiaIngresoZona.rdlc";
                Application.Current.Resources["TipoDoc_Reporte"] = "GI";
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
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string strEstado = cboEstado.SelectionBoxItem.ToString(); /*texto*/
            strEstado = strEstado == "Activo" ? "1" : strEstado == "NO ACTIVO" ? "0" : "%";
            string strFiltro = txtDesc.Text;
            string strLocal = (cboLocal.SelectedValue.ToString() == "0" ? "%" : cboLocal.SelectedValue.ToString());
            string strFecInicio = dtFechaInicio.Text;
            DateTime dthoy = DateTime.Today;
            string strFecFin;
            strFecFin = dthoy.AddDays(+1).ToString();
                  
            var lst= _GuiaIngresoZonaClient.usp_LisGuiaIngresoZona(strEstado, strFiltro, strLocal,strFecInicio, strFecFin).ToList();
            listViewListGI.ItemsSource = null;
            listViewListGI.Items.Refresh();
            listViewListGI.ItemsSource = lst;
        }
        private void imgEditar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listViewListGI.SelectedItem == null) return;
            var selectedClient = listViewListGI.SelectedItem as usp_LisGuiaIngresoZona_Result;
            EditarGI(selectedClient);
        }
        private void imgPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listViewListGI.SelectedItem == null) return;
            var selectedClient = listViewListGI.SelectedItem as usp_LisGuiaIngresoZona_Result;
            string IdGuia = selectedClient.IDGUIA.ToString();
            usp_ReporteGuiaIngreso(IdGuia);
        }
        /*Fin: Eventos*/
    }
}
