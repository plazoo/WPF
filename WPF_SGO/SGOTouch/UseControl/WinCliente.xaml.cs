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
using SGOTouch.ServiceCliente;
using SGOTouch.ServiceTablaGeneral;
using SGOTouch.ServiceCertificadoVSP;
//using MahApps.Metro.Controls;

namespace SGOTouch.UseControl
{
    /// <summary>
    /// Lógica de interacción para WinCliente.xaml
    /// </summary>
    //public partial class WinCliente : MetroWindow
    public partial class WinCliente : Window
    {
        //private readonly ucRegistrarTicket _regTicket;
        //public WinCliente(ucRegistrarTicket regTicket)
        private readonly ucRegistrarTicket _regTicket;
        private readonly ucRegistrarGI _regGI;
        private static ClienteClient _ClienteClient;
        private static TablaGeneralClient _TablaGeneralClient;
        private static CertificadoVSPClient _CertificadoVSPClient;
        public WinCliente(ucRegistrarTicket regTicket,ucRegistrarGI regGI)
        {
            _regTicket = regTicket;
            _regGI = regGI;
            InitializeComponent();
            _ClienteClient = new ClienteClient();
            _TablaGeneralClient = new TablaGeneralClient();
            _CertificadoVSPClient = new CertificadoVSPClient();
            txtDesc.Focus();
            //txtDesc.FontSize = 20;
            //txtDesc.Foreground = Brushes.Gray;
            //txtDesc.Text = "Nombre comercial, Documento o Codigo";

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            this.listViewCliente.ItemsSource = null;
            this.listViewCliente.Items.Refresh();            
            var lst = _TablaGeneralClient.usp_LisBusquedaClienteFiltro(txtDesc.Text).ToList();
            this.listViewCliente.ItemsSource = lst;
        }
        
        private void txtDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return && e.Key != Key.Enter)
            { return; }
            else {
                this.listViewCliente.ItemsSource = null;
                this.listViewCliente.Items.Refresh();
                //this.listViewCliente.ItemsSource = usp_LisBusquedaClienteFiltro(txtDesc.Text);
                var lst= _TablaGeneralClient.usp_LisBusquedaClienteFiltro(txtDesc.Text).ToList();
                this.listViewCliente.ItemsSource = lst;
            }            
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void listViewCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (_regTicket != null)
            {
                /*Si viene desde ucRegistrarTicket entra  y realiza las siguientes acciones*/            
                string vcIdCliente = "";
                string vcCliente = "";
                string vcLocal = "";
                string vcSector = "";
                string vcDistrito = "";
                string idLocal = _regTicket.cboOfOperacion.SelectedValue.ToString();

                if (listViewCliente.SelectedItem == null) return;
                var selectedClient = listViewCliente.SelectedItem as usp_LisBusquedaClienteFiltro_Result;
                if (selectedClient.IDCLIENTESGO == 0) return;
                vcIdCliente = selectedClient.IDCLIENTESGO.ToString();
                vcCliente = selectedClient.DOCIDENTIDAD.ToString() + "-" + selectedClient.CLIENTE.ToString();
                vcLocal = selectedClient.DEPARTAMENTO.ToString();
                vcSector = selectedClient.PROVINCIA.ToString();
                vcDistrito = selectedClient.DISTRITO.ToString();

                _regTicket.txtCliente.Text = vcCliente;
                _regTicket.lblIdCliente.Content = vcIdCliente;
                _regTicket.txtLocal.Text = vcLocal;
                _regTicket.txtSector.Text = vcSector;
                _regTicket.txtDistrito.Text = vcDistrito;



                LisClienteContacto obT = new LisClienteContacto();
                obT.NOMBRE = "[ Seleccione ]";
                obT.IDCONTACTOCLIENTE = 0;

                //lst = _regTicket.usp_LisClienteContacto(vcIdCliente);
                var lst2 = _ClienteClient.usp_LisClienteContacto(Convert.ToInt32(vcIdCliente),"1").ToList();
                lst2.Add(obT);
                LisClienteContacto oSrvClie = new LisClienteContacto();
                List<LisClienteContacto> lstSrvClie = new List<LisClienteContacto>();
                
                foreach (LisClienteContacto item in lst2)
                {
                    oSrvClie = new LisClienteContacto();
                    oSrvClie.NOMBRE = item.NOMBRE + " - " + item.APELLIDO;
                    oSrvClie.IDCONTACTOCLIENTE = item.IDCONTACTOCLIENTE;
                    lstSrvClie.Add(oSrvClie);
                }



                //lst.Add(obT);
                _regTicket.cboProveedor.ItemsSource = lstSrvClie;
                _regTicket.cboProveedor.DisplayMemberPath = "NOMBRE";
                _regTicket.cboProveedor.SelectedValuePath = "IDCONTACTOCLIENTE";
                _regTicket.cboProveedor.SelectedValue = 0;

                //lstT = _regTicket.usp_LisLaboratorioDisponibleXCliente(Convert.ToInt32(vcIdCliente), idLocal);
                var lstL = _TablaGeneralClient.usp_LisLaboratorioDisponibleXCliente(Convert.ToInt32(vcIdCliente), idLocal).ToList();

                usp_LisLaboratorioDisponibleXCliente_Result obTG = new usp_LisLaboratorioDisponibleXCliente_Result();
                obTG.CodigoLaboratorio = "[ Seleccione ]";
                obTG.IdLaboratorio = "0*0*0*0";
                lstL.Insert(0, obTG);

                _regTicket.cboAnalisis.ItemsSource = null;
                _regTicket.cboAnalisis.ItemsSource = lstL;
                _regTicket.cboAnalisis.DisplayMemberPath = "CodigoLaboratorio";
                _regTicket.cboAnalisis.SelectedValuePath = "IdLaboratorio";
                _regTicket.cboAnalisis.SelectedIndex = 0;


                _regTicket.usp_Mant_CLIENTE_COSECHA(vcIdCliente);
            }
            else if (_regGI != null)
            {
                /*Si viene desde ucRegistrarGI entra  y realiza las siguientes acciones*/

                //int inColumna = this.listViewCliente.CurrentCell.Column.DisplayIndex;
                string vcIdCliente = "";
                string vcCliente = "";
                string vcLocal = "";
                string vcSector = "";
                string vcDistrito = "";
                int idLocal = Convert.ToInt32(_regGI.cboOfOperacion.SelectedValue);
                string vcCosecha = _regGI.cboCosecha.Text;
                
                if (listViewCliente.SelectedItem == null) return;
                var selectedClient = listViewCliente.SelectedItem as usp_LisBusquedaClienteFiltro_Result;
                if (selectedClient.IDCLIENTESGO == 0) return;
                vcIdCliente = selectedClient.IDCLIENTESGO.ToString();
                vcCliente = selectedClient.DOCIDENTIDAD.ToString() + "-" + selectedClient.CLIENTE.ToString();
                vcLocal = selectedClient.DEPARTAMENTO.ToString();
                vcSector = selectedClient.PROVINCIA.ToString();
                vcDistrito = selectedClient.DISTRITO.ToString();

                _regGI.txtCliente.Text = vcCliente;
                _regGI.lblHiddenIdCliente.Content = vcIdCliente;


                _regGI.usp_Mant_CLIENTE_COSECHA(vcIdCliente);
                _regGI.usp_LisContratoConGI(Convert.ToInt32(vcIdCliente), _regGI.cboCosecha.Text);

                _regGI.usp_LisTickedPesadaEnProceso(idLocal, Convert.ToInt32(vcIdCliente));

                
                
                //lista = oBl.usp_LisSaldoCertificado(vcIdCliente.ToString(), );
                ListView listView = new ListView();
                var listaCert = _CertificadoVSPClient.usp_LisSaldoCertificado(Convert.ToInt32(vcIdCliente), (DateTime.Today).Year.ToString()).ToList();
                listView.ItemsSource = listaCert;

                decimal dcSaldoMin = 10000000000000;
                foreach (var item in listaCert)
                {
                    dcSaldoMin = Convert.ToDecimal(item.SALDO_CALCULADO) < dcSaldoMin ? Convert.ToDecimal(item.SALDO_CALCULADO) : dcSaldoMin;
                }
                _regGI.lblSaldoCerGI.Content = dcSaldoMin == 10000000000000 ? 0 : dcSaldoMin;

                

                //_regGI.txtCliente.Focus();
            }

            this.Close();
        }

   

    }
}
