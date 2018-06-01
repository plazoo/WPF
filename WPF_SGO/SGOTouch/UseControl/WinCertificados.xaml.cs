

using System;
using System.Collections.Generic;
using System.Data;
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
using SGOTouch.ServiceCertificadoVSP;
namespace SGOTouch.UseControl
{
    /// <summary>
    /// Lógica de interacción para WinCertificados.xaml
    /// </summary>
    public partial class WinCertificados : Window
    {
        private readonly ucRegistrarGI _regGi;
        //private readonly BLCertificadoVSP _blCertificadoVSP;
        private static CertificadoVSPClient _CertificadoVSPClient;
        private int idCliente = 0;
        private string strCosecha = "";
        public WinCertificados(ucRegistrarGI regGI)
        {
            _regGi = regGI;            
            InitializeComponent();
            _CertificadoVSPClient = new CertificadoVSPClient();
            listView.SelectionMode = SelectionMode.Multiple;
            idCliente = Convert.ToInt32(_regGi.lblHiddenIdCliente.Content);            
            strCosecha = (DateTime.Today).Year.ToString();
            if (idCliente != 0)
            {
                usp_LisSaldoCertificado(idCliente, strCosecha);             
            }
            RecorrerGridObtenerMenorValor();
            
        }
        public void usp_LisSaldoCertificado(int idCliente, string cosecha)
        {
            var lista = _CertificadoVSPClient.usp_LisSaldoCertificado(idCliente, cosecha).ToList();            
            listView.ItemsSource = lista;
            
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RecorrerGridObtenerMenorValor()
        {
            decimal inSaldoMin = 10000000000;
            int inCount = 0;            
            if(listView.Items.Count==0)
            {
                lblSaldo.Content = "0";
                return;
            }
            foreach (var item in listView.Items)
            {
                //DataGridRow row = (DataGridRow)dataGridCert.ItemContainerGenerator.ContainerFromItem(item);                
                //inSaldoMinNuevo = Convert.ToInt32(((TextBox)dataGridCert.Columns[7].GetCellContent(row)).Text);  
                inCount++;
                if (inCount > 9) continue;
                usp_LisSaldoCertificado_Result oC = new usp_LisSaldoCertificado_Result();                
                
                oC =(usp_LisSaldoCertificado_Result)item;

                decimal inSaldoMinNuevo = 0;
                inSaldoMinNuevo = Convert.ToDecimal(oC.SALDO_CALCULADO);
                inSaldoMin = inSaldoMinNuevo < inSaldoMin ? inSaldoMinNuevo : inSaldoMin;
            }
            lblSaldo.Content = inSaldoMin;
            _regGi.lblSaldoCerGI.Content = inSaldoMin == 10000000000 ? 0 : inSaldoMin;

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*Objetivo: Concatenar los id y descrip de los certificados seleccionados*/
            var selected = listView.SelectedItems;
            usp_LisSaldoCertificado_Result oC = new usp_LisSaldoCertificado_Result();
            if (selected == null) return;

            _regGi.lblHiddenDescCert.Content = "";
            _regGi.lblHiddenIdCert.Content = "";
            for (int i = 0; i < selected.Count; i++)
            {
                oC = new usp_LisSaldoCertificado_Result();
                oC = (usp_LisSaldoCertificado_Result)selected[i];
                _regGi.lblHiddenDescCert.Content +=  oC.DESCRIPCION.ToString()+ "|";
                _regGi.lblHiddenIdCert.Content += oC.IDCERTIFICADOVSP.ToString() + "|";
            }

            var Oselected = selected;
        }
        private void SetItemsSelectedListview()
        {
            string certificadosId = _regGi.lblHiddenIdCert.Content.ToString();
            certificadosId = certificadosId.Substring(0, certificadosId.Length - 1);
            string[] arr = certificadosId.Split('|');
            for (int j = 0; j < listView.Items.Count; j++)
            {

                string IdCertificadoVsp = ((usp_LisSaldoCertificado_Result)listView.Items[j]).IDCERTIFICADOVSP.ToString();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (IdCertificadoVsp == arr[i])
                    {                        
                        ListViewItem lvi = (ListViewItem)listView.ItemContainerGenerator.ContainerFromIndex(j);                        
                        if (lvi!=null)
                        lvi.IsSelected = true;

                    }
                }
                
            }
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_regGi.lblHiddenIdCert.Content.ToString() != "")
            {
                SetItemsSelectedListview();
            }
        }

        private void chkSelecAll_Click(object sender, RoutedEventArgs e)
        {
            if (chkSelecAll.IsChecked == true)
            {
                listView.SelectAll();

            }
            else {
                listView.SelectedIndex = -1;


            }
        }
    }
}
