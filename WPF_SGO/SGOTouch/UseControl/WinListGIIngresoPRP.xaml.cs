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
using System.Data;
//using SGOBusLogic;

using SGOUtil;
using SGOTouch.UseControl;
using System.Text.RegularExpressions;
using SGOTouch.ServiceUsuario;
using SGOTouch.ServiceTablaGeneral;
using SGOTouch.ServiceGuiaIngresoZona;
namespace SGOTouch.UseControl
{
    /// <summary>
    /// Lógica de interacción para WinListGIIngresoPRP.xaml
    /// </summary>
    /// 
    
    public partial class WinListGIIngresoPRP : Window
    {
        private readonly ucRegistrarGI _oRegGI;
        string strOfOrigen = "";
        string strGR = "";
        private static TablaGeneralClient _TablaGeneralClient;
        private static GuiaIngresoZonaClient _GuiaIngresoZonaClient;
        public WinListGIIngresoPRP(ucRegistrarGI oRegGI)
        {
            InitializeComponent();
            _TablaGeneralClient = new TablaGeneralClient();
            _GuiaIngresoZonaClient = new GuiaIngresoZonaClient();
            _oRegGI = oRegGI;
            strOfOrigen = _oRegGI.lblHiddenOfOrigen.Content.ToString();
            strGR = _oRegGI.lblHiddenGRSGO.Content.ToString();
            OfOrigen();
            if (strOfOrigen != "")
            {
                cboOfOrigen.SelectedValue = strOfOrigen;

                /*Cargar el combo de GR*/
                Llenar_GuiaRemision(strOfOrigen);
                cboGR.SelectedValue = strGR;

                /*Cargar el listview con el idGuiaRemision*/
                Llenar_Grid(Convert.ToInt32(strGR));
            }
            
            
        }
        private void OfOrigen()
        {
            var lstT = _TablaGeneralClient.usp_SelLocalIdEmpresaUsuario(Util.GetIdEmpresa(), ((Usuario)Application.Current.Resources["UserData"]).IdUsuario);
            this.cboOfOrigen.ItemsSource = lstT;
            this.cboOfOrigen.DisplayMemberPath = "vDescripcion";
            this.cboOfOrigen.SelectedValuePath = "idLocal";

            this.cboOfOrigen.SelectedIndex = 0;
        }
        
        public void  Llenar_GuiaRemision(string idLocal)
        {
            string data = "";
            
            
            usp_LisRecepcionGuiaRemisionZona_Result oTG = new usp_LisRecepcionGuiaRemisionZona_Result();
            List<usp_LisRecepcionGuiaRemisionZona_Result> lst = new List<usp_LisRecepcionGuiaRemisionZona_Result>();
            

            oTG.TRASLADO = "[Seleccione]";
            oTG.IDTRASLADO = 0;
            var lista = _GuiaIngresoZonaClient.usp_LisRecepcionGuiaRemisionZona(idLocal).ToList();
            //lista = oBl.usp_LisRecepcionGuiaRemisionZona(idLocal);
            
            if (lista.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (usp_LisRecepcionGuiaRemisionZona_Result item in lista)
                {                    
                    oTG = new usp_LisRecepcionGuiaRemisionZona_Result ();
                    oTG.TRASLADO = item.TRASLADO + " / " + item.FECHATRASLADO + " / " + "Saco: " + item.GRSACO + " / KB: " + item.GRKGBRUTO;
                    oTG.IDTRASLADO = Convert.ToInt32(item.IDTRASLADO);                    
                    lst.Add(oTG);
                }                
            }

            this.cboGR.ItemsSource = lst;
            this.cboGR.DisplayMemberPath = "TRASLADO";
            this.cboGR.SelectedValuePath = "IDTRASLADO";
            this.cboGR.SelectedIndex = 0;
                       
        }
        public void Llenar_Grid(int idTraslado)
        {            
            _oRegGI.lblHiddenGRSGO.Content = idTraslado;
            /*Cargando el Grid de esta ventana */
            
            var lista = _GuiaIngresoZonaClient.usp_ListadoDetalleRemision(idTraslado);
            dataGridGR.ItemsSource = null;
            dataGridGR.ItemsSource = lista;

            /*Cargando el Grid de ucRegistrarGI de la pestaña "Detalles DE GI" */
            _oRegGI.listViewGR.ItemsSource = null;
            _oRegGI.listViewGR.ItemsSource = lista;
            /*Cargar DataGrid*/
        }
        /*Inicio Eventos*/
   

        private void dataGridCert_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*regresar a regGI*/
            _oRegGI.chkIngreoPRP.Focus();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            
            _oRegGI.lblHiddenOfOrigen.Content = cboOfOrigen.SelectedValue == null ? "0":cboOfOrigen.SelectedValue.ToString();
            _oRegGI.lblHiddenGRSGO.Content = cboGR.SelectedValue== null ? "0": cboGR.SelectedValue.ToString();
            _oRegGI.chkIngreoPRP.Focus();
            this.Close();
        }

        private void cboGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            _oRegGI.lblHiddenGRSGO.Content = cboGR.SelectedValue;
            Llenar_Grid(Convert.ToInt32(cboGR.SelectedValue));
        }
        private void cboOfOrigen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _oRegGI.lblHiddenOfOrigen.Content =  strOfOrigen!="" ? strOfOrigen: cboOfOrigen.SelectedValue ;
            if (cboOfOrigen.SelectedValue.ToString() == "0" || cboOfOrigen.SelectedValue.ToString() == "" || cboOfOrigen.SelectedValue.ToString() == "SGOTouch.ServiceTablaGeneral.SelLocalIdEmpresaUsuario") return;
            Llenar_GuiaRemision(cboOfOrigen.SelectedValue.ToString());
        }
        /*Fin Eventos*/

    }
}
