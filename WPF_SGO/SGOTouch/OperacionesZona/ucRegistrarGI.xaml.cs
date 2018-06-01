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
//using SGOBusLogic;

using SGOUtil;
using SGOTouch.UseControl;
using System.Text.RegularExpressions;
using System.Collections;
using SGOTouch.Helpers;
using SGOTouch.ServiceUsuario;
using SGOTouch.ServiceTablaGeneral;
using SGOTouch.ServiceTicketPesada;
using SGOTouch.ServiceCliente;
using SGOTouch.ServiceSolicitudServicio;
using SGOTouch.ServiceCertificadoVSP;
using Newtonsoft.Json;
//using SGOTouch.GI;
using SGOTouch.ServiceGuiaIngresoZona;

namespace SGOTouch
{
    /// <summary>
    /// Lógica de interacción para ucRegistrarGI.xaml
    /// </summary>

    public partial class ucRegistrarGI : UserControl
    {
        //public int asd(string vc) { return Convert.ToInt32(vc); }
        
        private static string strGestion = "";
        private static TablaGeneralClient _TablaGeneralClient;
        private static TicketPesadaClient _ticketPesadaClient;
        private static ClienteClient _ClienteClient;
        private static SolicitudServicioClient _SolicitudServicioClient;
        private static GuiaIngresoZonaClient _GuiaIngresoZonaClient;
        private static CertificadoVSPClient _CertificadoVSPClient;
        
        public ucRegistrarGI()
        {

            InitializeComponent();
            _TablaGeneralClient = new TablaGeneralClient();
            _ClienteClient = new ClienteClient();
            _ticketPesadaClient = new TicketPesadaClient();
            _SolicitudServicioClient = new SolicitudServicioClient();
            _GuiaIngresoZonaClient = new GuiaIngresoZonaClient();
            _CertificadoVSPClient = new CertificadoVSPClient();
            CargarLocales();
            CargarCosecha();
            Llenar_TipoCafe();
            Cargar_cboTipoIngreso();
            btnIcoGRSGO.Visibility = Visibility.Hidden;
            MostrarControlesDivisinTicket(false);
            lblHiddenDscAgua.Visibility = Visibility.Hidden;
            lblHiddenOfOrigen.Visibility = Visibility.Hidden;
            lblHiddenGRSGO.Visibility = Visibility.Hidden;

            btnIcoGRSGO.Visibility = Visibility.Hidden;
            lblHiddenOfOrigen.Content = "";
            lblHiddenGRSGO.Content = "";
            lblTituloGRSGO.Visibility = Visibility.Hidden;
            listViewGR.Visibility = Visibility.Hidden;

            listViewResumen.Visibility = Visibility.Hidden;
            lblTituloResumen.Visibility = Visibility.Hidden;

            txtDscAgua.Text = "0";
            txtObservaciones.Focus();
            txtDscAgua.IsEnabled = false;
            
            
        }
        private void ValidarOperacion()
        {
            usp_LisGuiaIngresoZona_Result oGI = new usp_LisGuiaIngresoZona_Result();
            oGI = ((usp_LisGuiaIngresoZona_Result)Application.Current.Resources["GridDataListarGI"]);
            if (oGI != null)
            {                
                strGestion = "Editar";
                btnImprimir.IsEnabled = true;
                btnImprimir.Visibility = Visibility.Visible;

                lblCodigoInterno.Visibility = Visibility.Visible;
                txtCodigo.Visibility = Visibility.Visible;
                /*Cargando las cosechas en el cbo Cosecha que le corresponden a este cliente*/
                usp_Mant_CLIENTE_COSECHA(oGI.IDCLIENTE.ToString());
                lblHiddenIdGuia.Content = oGI.IDGUIA.ToString();
                listViewResumen.Visibility = Visibility.Visible;
                lblTituloResumen.Visibility = Visibility.Visible;

                usp_LisDatoGuiaIngresoZona(oGI.GUIAINGRESO.ToString().Replace("-",""));
                Llenar_Ruma_Server(Convert.ToInt32(oGI.IDLOCAL));
                listViewTicket.Visibility = Visibility.Hidden;
                lblTituloTicket.Visibility = Visibility.Hidden;

                cboOfOperacion.SelectedValue = oGI.IDLOCAL;
                lblHiddenIdCliente.Content = oGI.IDCLIENTE;

                lblHiddenGRSGO.Content = oGI.TRASLADO;
                
                txtCliente.Text = oGI.CLIENTE.ToString();

                txtCodigo.Text = oGI.GUIAINGRESO.ToString();

                lblHiddenIdProveedor.Content = oGI.IDCONTACTOCLIENTE.ToString();
                txtProveedor.Text = oGI.CONTACTOCLIENTE == null ? "": oGI.CONTACTOCLIENTE.ToString();
                cboRuma.SelectedValue = oGI.RUMADESTINO;
                txtTotalSacos.Text = oGI.TOTALSACO.ToString();
                txtKgBruto.Text = oGI.TOTALKGBRUTO.ToString();
                txtTara.Text = oGI.TOTALTARA.ToString();
                txtDscAgua.Text = oGI.TOTALDSCTOAGUA.ToString();
                txtKgNeto.Text = oGI.TOTALKGNETO.ToString();

                lblHiddenIdCert.Content = oGI.IdCertificadoVSP.ToString();
                lblHiddenDescCert.Content = oGI.DESCCERTIFICACION.ToString();
                cboModIngreso.SelectedValue = oGI.MODALIDADINGRESO.ToString();

                cboTipoProducto.SelectedValue = oGI.IdTipoCaFe.ToString();
                txtGRTerceros.Text = oGI.GUIAREMISIONEXTERNA.ToString();

                txtObservaciones.Text = oGI.OBSERVACION.ToString();

                /*Dentro de este metodo se indica la cosecha de esta GI*/
                usp_LisGuiaIngresoEditar(oGI.IDGUIA);
                usp_ListIngresoSaldo(oGI.IDGUIA);
                chkContraServ.IsChecked = oGI.IDPROCESO == 1 ? true : false;
                /*Pestaña Detalle*/
                Application.Current.Resources["GridDataListarGI"] = null;

                txtObservaciones.IsEnabled = false;
                txtGISaco.IsEnabled = false;
                txtGIKgBruto.IsEnabled = false;
            }
            else
            {
                strGestion = "Nuevo";
                btnImprimir.IsEnabled = false;
                btnImprimir.Visibility = Visibility.Hidden;

                Llenar_Ruma_Server(Convert.ToInt32(cboOfOperacion.SelectedValue));
                lblCodigoInterno.Visibility = Visibility.Hidden;
                txtCodigo.Visibility = Visibility.Hidden;
            }
        }
        private bool ValidarDatos()
        {
            bool boReturn = true;
            int inCount = 0;
            decimal dcTotalkgneto = Convert.ToDecimal(txtTicketKgNeto.Text);
            decimal dcSaldoCert = Convert.ToDecimal(lblSaldoCerGI.Content.ToString());
            string strModIngre = cboModIngreso.SelectedValue.ToString();
            if (dcTotalkgneto > dcSaldoCert && (strModIngre=="CON" || strModIngre =="CER"))
            {
                inCount++;
            }
            if (inCount > 0) { boReturn = false; }
            return boReturn;
        }
        private bool ValidarVacios()
        {
            /*Si hay al menos un control que esta sin seleccionar o vacio return false*/
            bool boReturn = false;
            int inCount = 0;
            
            if (lblHiddenIdProveedor.Content.ToString() == "0") boReturn = false;
            if (lblHiddenIdCert.Content.ToString() == "0")
            {
                inCount++;
                SetBorderColor(btnIcoCert,true);
            }

            if (!cboRuma.HasItems )
            {
                SetBorderColor(cboRuma,true);
                inCount++;
            }
            else if(cboRuma.SelectedValue.ToString() == "0")
            {
                SetBorderColor(cboRuma, true);
                inCount++;
            }
            else if (cboRuma.SelectedValue.ToString() != "0")
            {
                SetBorderColor(cboRuma,false);
            }

            if (cboModIngreso.SelectedValue.ToString() == "0" || cboModIngreso.Items.Count==0)
            {
                SetBorderColor(cboModIngreso, true);
                inCount++;
            }
            else
            {
                SetBorderColor(cboModIngreso, false);
            }

            if (cboTipoProducto.SelectedValue.ToString() == "0" || cboTipoProducto.Items.Count==0)
            {
                SetBorderColor(cboTipoProducto, true);
                inCount++;
            }
            else
            {
                SetBorderColor(cboTipoProducto, false);
            }

            //if (cboContrato.SelectedValue.ToString() == "0" || cboContrato.HasItems)
            //if (!cboContrato.HasItems) /*valida si estan vacios*/
            //{
            //    SetBorderColor(cboContrato, true);
            //    inCount++;
            //}
            //else if (cboContrato.SelectedValue.ToString() == "0") {
            //    SetBorderColor(cboContrato, true);
            //    inCount++;
            //} else if(cboContrato.SelectedValue.ToString() != "0")
            //{
            //    SetBorderColor(cboContrato, false);
            //}
            if (lblHiddenIdCliente.Content.ToString() == "0")
            {
                btnBuscarcliente.BorderBrush = Brushes.Red;
                btnBuscarcliente.BorderThickness = new Thickness(5, 5, 5, 5);
                inCount++;
            }
            else {
                btnBuscarcliente.BorderBrush = Brushes.Gray;
                btnBuscarcliente.BorderThickness = new Thickness(1, 1, 1, 1);
            }

            boReturn = inCount > 0 ? false : true;
            return boReturn;
        }
        private void SetBorderColor(Control strControl,bool boSet)
        {
            if (boSet == true)
            {
                strControl.BorderBrush =  Brushes.Red;
                strControl.BorderThickness = new Thickness(5, 5, 5, 5);
            }
            else
            {
                strControl.BorderBrush = Brushes.Gray;
                strControl.BorderThickness = new Thickness(1, 1, 1, 1);
            }
            
        }
        /*Inicio Cargar Controles*/
        private void MostrarControlesDivisinTicket(bool bOp)
        {
            lbl1.Visibility = bOp? Visibility.Visible:Visibility.Hidden;
            lbl2.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            lbl3.Visibility= bOp ? Visibility.Visible : Visibility.Hidden;

            txtTicketSaco.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtTicketKgBruto.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtTicketTara.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtTicketKgNeto.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;

            lblsacosN.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            lblBrutoKG.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            lblTaraN.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            lblNetoKG.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;

            txtGISaco.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtGIKgBruto.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtGITara.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtGIKgNeto.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;

            txtSaldoSaco.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtSaldoKgBruto.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtSaldoTara.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;
            txtSaldoKgNeto.Visibility = bOp ? Visibility.Visible : Visibility.Hidden;

        }
        private void Cargar_cboTipoIngreso()
        {
            
            
            string[] arrValues = { "0","CON", "CER", "ZON", "FTO", "FT", "B2B", "TER", "LID" };
            string[] arrContent = { "[Seleccione]","CONVENCIONAL", "CERT", "RECEPCION DE ZONA", "FTO", "FT", "B2B", "TERCEROS", "LIDER" };


            List<TablaGenerica> lst = new List<TablaGenerica>();

            // Add parts to the list.
            
            for (int i = 0; i < arrValues.Length; i++)
            {
                lst.Add(new TablaGenerica() { DescripcionUno = arrContent[i], DescripcionDos = arrValues[i] });
            }
            this.cboModIngreso.ItemsSource = lst;
            this.cboModIngreso.DisplayMemberPath = "DescripcionUno";
            this.cboModIngreso.SelectedValuePath = "DescripcionDos";
            this.cboModIngreso.SelectedValue = 0;
        }
        private void Limpiarcontroles()
        {
            //string n = "";
            //n = n.MultiplyBy();  
        }
        public  void usp_LisDatoGuiaIngresoZona(string idGuiaIngreso)
        {
            var lst = _GuiaIngresoZonaClient.usp_LisDatoGuiaIngresoZona(Convert.ToInt32(idGuiaIngreso));
            listViewResumen.ItemsSource = null;
            listViewResumen.ItemsSource = lst;
        }

        private void Llenar_Ruma_Server(int idLocal)
        {
            
            
            usp_LisTipoRuma_Result oT = new usp_LisTipoRuma_Result();
            oT.DESCRIPCION= "[Seleccione]";
            oT.IDTIPORUMA = 0;                        
            var lst= _TablaGeneralClient.usp_LisTipoRuma(idLocal.ToString(), "1").ToList();
            lst.Add(oT);
            this.cboRuma.ItemsSource = lst;
            this.cboRuma.DisplayMemberPath = "DESCRIPCION";
            this.cboRuma.SelectedValuePath = "IDTIPORUMA";
            this.cboRuma.SelectedValue = 0;
            
        }
        private void CargarLocales()
        {
            var lstT = _TablaGeneralClient.usp_SelLocalIdEmpresaUsuario(Util.GetIdEmpresa(), ((Usuario)Application.Current.Resources["UserData"]).IdUsuario);
            this.cboOfOperacion.ItemsSource = lstT;
            this.cboOfOperacion.DisplayMemberPath = "vDescripcion";
            this.cboOfOperacion.SelectedValuePath = "idLocal";
            this.cboOfOperacion.SelectedIndex = 0;
        }
        private void CargarCosecha()
        {
            
            TIPO_COSECHA oT = new TIPO_COSECHA();
            var lstT = _TablaGeneralClient.usp_Mant_TIPO_COSECHA(1, oT).ToList();

            cboCosecha.ItemsSource = lstT.Where(_ => _.IdEstado == 1); /*estado==1 cosecha vigente*/
            cboCosecha.DisplayMemberPath = "DESCRIPCION";
            cboCosecha.SelectedValuePath = "IdCosecha";
            cboCosecha.SelectedIndex = 0;


        }
        private void Llenar_TipoCafe()
        {
            usp_LisTipoCafe_Result oET = new usp_LisTipoCafe_Result();
            oET.DESCRIPCION = "[ Seleccione ]";
            oET.IDTIPOCAFE = 0;

            var listaCafe = _TablaGeneralClient.usp_LisTipoCafe("1").ToList();

            listaCafe.Add(oET);
            this.cboTipoProducto.ItemsSource = listaCafe;
            this.cboTipoProducto.DisplayMemberPath = "DESCRIPCION";
            this.cboTipoProducto.SelectedValuePath = "IDTIPOCAFE";
            this.cboTipoProducto.SelectedValue = 0;
            this.cboTipoProducto.IsEnabled = false;
        }
        /*Fin Cargar Controles*/

    

     

   


        private void btnIcoGRSGO_Click(object sender, RoutedEventArgs e)
        {
            UseControl.WinListGIIngresoPRP oIngresoPRP = new UseControl.WinListGIIngresoPRP(this);
            oIngresoPRP.Show();
        }

        private void chkIngreoPRP_Unchecked(object sender, RoutedEventArgs e)
        {
            btnIcoGRSGO.Visibility = Visibility.Hidden;
            lblHiddenOfOrigen.Content = "";
            lblHiddenGRSGO.Content = "";
            lblTituloGRSGO.Visibility = Visibility.Hidden;
            listViewGR.Visibility = Visibility.Hidden;
        }

        private void btnBuscarcliente_Click(object sender, RoutedEventArgs e)
        {
            WinCliente objWincliente = new WinCliente(null,this );
            objWincliente.Show();
        }
   
        public void usp_Mant_CLIENTE_COSECHA(string vcidCliente)
        {
            CLIENTE_COSECHA oP = new CLIENTE_COSECHA();
            oP.IdCliente = Convert.ToInt32(vcidCliente);
            oP.InVigente = 1;
            oP.UsuarioRegistro = ((Usuario)Application.Current.Resources["UserData"]).IdUsuario;
            var lst = _TablaGeneralClient.usp_Mant_CLIENTE_COSECHA(1, oP);

            cboCosecha.ItemsSource = null;

            cboCosecha.ItemsSource = lst.Where(_ => _.InVigente == 1);
            cboCosecha.DisplayMemberPath = "DESCRIPCION";
            cboCosecha.SelectedValuePath = "IdCosecha";
            cboCosecha.SelectedIndex = 0;
        }
        
 
        
        public void usp_LisTickedPesadaEnProceso(int idlocal, int cliente)
        {
            int estado = Convert.ToInt32(Util.Estado.Activo);
            int proceso = Convert.ToInt32(Util.EstadoTicketPesada.No_requerido);
            
            var lst = _SolicitudServicioClient.usp_LisTickedPesadaEnProceso(proceso, idlocal.ToString(), cliente, estado).ToList();
            
            listViewTicket.ItemsSource = null;
            listViewTicket.ItemsSource = lst;
        }

        public void usp_LisContratoConGI(int idCliente, string cosecha)
        {
            string data = String.Empty;            
            try
            {                
                var lst = _TablaGeneralClient.usp_LisContratoConGI(idCliente, Convert.ToInt32(cosecha)).ToList();
                
                if (lst.Count > 0)
                {
                    var oET = new usp_LisContratoConGI_Result();                    
                    oET.COMBO = "[ Seleccione ]";
                    oET.IDCONTRATO = 0;
                    lst.Insert(0, oET);
                    cboContrato.ItemsSource = null;
                    cboContrato.ItemsSource = lst;
                    this.cboContrato.DisplayMemberPath = "COMBO";
                    this.cboContrato.SelectedValuePath = "IDCONTRATO";
                    this.cboContrato.SelectedValue = 0;
                }
            }
            catch (Exception) { }
        }

        private void DetaGICalcularSaldos()
        {
            txtSaldoSaco.Text = (Convert.ToDecimal(txtTicketSaco.Text == "" ? "0" : txtTicketSaco.Text) - Convert.ToDecimal(txtGISaco.Text == "" ? "0" : txtGISaco.Text)).ToString();
            txtSaldoKgBruto.Text = (Convert.ToDecimal(txtTicketKgBruto.Text == "" ? "0" : txtTicketKgBruto.Text) - Convert.ToDecimal(txtGIKgBruto.Text == "" ? "0" : txtGIKgBruto.Text)).ToString();
            txtSaldoTara.Text = (Convert.ToDecimal(txtTicketTara.Text == "" ? "0" : txtTicketTara.Text) - Convert.ToDecimal(txtGITara.Text == "" ? "0" : txtGITara.Text)).ToString();

            txtGIKgNeto.Text =(Convert.ToDecimal(txtGIKgBruto.Text==""? "0": txtGIKgBruto.Text) - Convert.ToDecimal(txtGITara.Text==""? "0": txtGITara.Text)).ToString();
            txtSaldoKgNeto.Text = (Convert.ToDecimal(txtTicketKgNeto.Text == "" ? "0" : txtTicketKgNeto.Text) - Convert.ToDecimal(txtGIKgNeto.Text == "" ? "0" : txtGIKgNeto.Text)).ToString();

        }
        private void DetaGICacularkgNeto(){
            txtKgNeto.Text = (Convert.ToDecimal(lblHiddenDscAgua.Content.ToString()) - Convert.ToDecimal(txtDscAgua.Text == "" ? "0" : txtDscAgua.Text)).ToString() ;
            
        }


        /*Inicio de eventos*/
        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarVacios())
            {
                MessageBox.Show("Ingresar los datos en los elementos resaltados");
                return;
            }else if (!ValidarDatos())
            {
                MessageBox.Show("VALOR DE INGRESO no deb e ser mayoral LIMITE DE CERTIFICADO.");
                return;
            }

            usp_InsGuiaIngresoCabecera();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            if (lblHiddenIdGuia.Content == null || lblHiddenIdGuia.Content.ToString() == "") return;
            string IdGuia = lblHiddenIdGuia.Content.ToString();

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

            }

        }

        private void chkIngreoPRP_Checked(object sender, RoutedEventArgs e)
        {
            btnIcoGRSGO.Visibility = Visibility.Visible;
            lblTituloGRSGO.Visibility = Visibility.Visible;
            listViewGR.Visibility = Visibility.Visible;
        }
        private void cboOfOperacion_DropDownClosed(object sender, EventArgs e)
        {
            Llenar_Ruma_Server(Convert.ToInt32(cboOfOperacion.SelectedValue));
        }
        private void cboRuma_DropDownClosed(object sender, EventArgs e)
        {
            Llenar_Ruma_Server(Convert.ToInt32(cboOfOperacion.SelectedValue));
        }

        private void btnIcoCert_Click(object sender, RoutedEventArgs e)
        {
            WinCertificados oC = new WinCertificados(this);
            oC.Show();
        }

        private void chkDivTicket_Checked(object sender, RoutedEventArgs e)
        {
            MostrarControlesDivisinTicket(true);
            listViewTicket.SelectionMode = SelectionMode.Single;
        }
        private void chkDivTicket_Unchecked(object sender, RoutedEventArgs e)
        {
            MostrarControlesDivisinTicket(false);
            listViewTicket.SelectionMode = SelectionMode.Multiple;
        }



        private void txtGISaco_KeyDown(object sender, KeyEventArgs e)
        {
            /*Solo enteros*/
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
            
        }

        private void txtGIKgBruto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
            
        }

        private void txtDscAgua_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

        }

        private void txtGISaco_KeyUp(object sender, KeyEventArgs e)
        {
            
            DetaGICalcularSaldos();
            txtTotalSacos.Text = txtGISaco.Text;
            txtTara.Text = txtGITara.Text;
            txtKgNeto.Text = txtGIKgNeto.Text;
        }

        private void txtGIKgBruto_KeyUp(object sender, KeyEventArgs e)
        {
            
            DetaGICalcularSaldos();
            txtKgBruto.Text = txtGIKgBruto.Text;
            txtTara.Text = txtGITara.Text;
            txtKgNeto.Text = txtGIKgNeto.Text;
        }

        private void txtDscAgua_KeyUp(object sender, KeyEventArgs e)
        {
            DetaGICacularkgNeto();
        }
        private void listViewTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = listViewTicket.SelectedItems;
            usp_LisTickedPesadaEnProceso_Result oT = new usp_LisTickedPesadaEnProceso_Result();
            if (selected == null) { return; } else { txtDscAgua.IsEnabled = true; }

            txtTotalSacos.Text = "";
            txtTara.Text = "";
            txtKgBruto.Text = "";
            txtKgNeto.Text = "";

            for (int i = 0; i < selected.Count; i++)
            {
                oT = new usp_LisTickedPesadaEnProceso_Result();
                oT = (usp_LisTickedPesadaEnProceso_Result)selected[i];

                /*asignando valores generales de la GI*/
                txtTotalSacos.Text = (Convert.ToDecimal(txtTotalSacos.Text == "" ? "0" : txtTotalSacos.Text) + Convert.ToDecimal(oT.NroSaco)).ToString();
                txtTara.Text = (Convert.ToDecimal(txtTara.Text == "" ? "0" : txtTara.Text) + Convert.ToDecimal(oT.Tara)).ToString();
                txtKgBruto.Text = (Convert.ToDecimal(txtKgBruto.Text == "" ? "0" : txtKgBruto.Text) + Convert.ToDecimal(oT.KgBruto)).ToString();
                txtKgNeto.Text = (Convert.ToDecimal(txtKgNeto.Text == "" ? "0" : txtKgNeto.Text) + Convert.ToDecimal(oT.KgNeto)).ToString();

                txtDscAgua.Text = "0";
                lblHiddenDscAgua.Content = oT.KgNeto.ToString();

                txtProveedor.Text = oT.Proveedor == null ? "" : oT.Proveedor.ToString();
                lblHiddenIdProveedor.Content = oT.IDCONTACTOCLIENTE.ToString();
                cboTipoProducto.SelectedValue = oT.IdTipoCafe;

            }



            var selectedClient = listViewTicket.SelectedItem as usp_LisTickedPesadaEnProceso_Result;
            if (selectedClient == null) return;


            /*Asignnado valores a la divición de ticket*/
            txtTicketSaco.Text = selectedClient.NroSaco.ToString();


            txtTicketKgBruto.Text = selectedClient.KgBruto.ToString();
            txtTicketTara.Text = selectedClient.Tara.ToString();
            txtTicketKgNeto.Text = selectedClient.KgNeto.ToString();

            txtGISaco.Text = selectedClient.NroSaco.ToString();
            txtGIKgBruto.Text = selectedClient.KgBruto.ToString();
            txtGITara.Text = selectedClient.Tara.ToString();
            txtGIKgNeto.Text = selectedClient.KgNeto.ToString();

            DetaGICalcularSaldos();

        }

        private void listViewGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGR = listViewGR.SelectedItem as usp_ListadoDetalleRemision_Result;
            if (selectedGR == null) return;

            chkDivTicket.IsChecked = true;
            txtGISaco.Text = selectedGR.GRSACO.ToString();
            txtGIKgBruto.Text = selectedGR.GRKGBRUTO.ToString();
            txtGITara.Text = selectedGR.GRTARA.ToString();
            txtGIKgNeto.Text = selectedGR.GRKGNETO.ToString();

            lblHiddenDetalleIdCliente.Content = selectedGR.IDCLIENTE;
            lblHiddenDetalleIdResultado.Content = selectedGR.IDRESULTADO;

            DetaGICalcularSaldos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ValidarOperacion();
        }
        /*Fin eventos*/

        public void usp_LisGuiaIngresoEditar(int idGuia)
        {
            var arrGi = new ArrayList();

            var oEnt = _GuiaIngresoZonaClient.usp_LisGuiaIngresoEditar(idGuia).ToList();
            
            if (oEnt != null)
            {
                cboCosecha.SelectedItem = oEnt[0].COSECHA;

                usp_LisContratoConGI(oEnt[0].IDCLIENTE, oEnt[0].COSECHA.ToString());
                cboContrato.SelectedValue = oEnt[0].IDCONTRATO;

                RecorrerGridObtenerMenorValor(oEnt[0].IDCLIENTE.ToString(), oEnt[0].COSECHA.ToString());
                Llenar_Grid(Convert.ToInt32(oEnt[0].IDTRASLADO));
                
                chkContraServ.IsChecked     = oEnt[0].IDPROCESO==1 ? true : false;                
                lblHiddenIdCert.Content     = oEnt[0].IDCERTIFICADOVSP;
                lblHiddenDescCert.Content   = oEnt[0].DESCCERTIFICACION;                
                lblHiddenOfOrigen.Content   = oEnt[0].IDLOCAL;
                lblHiddenGRSGO.Content      = oEnt[0].IDTRASLADO;                
                txtGRTerceros.Text          = oEnt[0].GUIAREMISIONEXTERNA;
                chkDivTicket.IsChecked      = oEnt[0].IdDivision == 1 ? true : false;                
                chkIngreoPRP.IsChecked      = oEnt[0].IDINGRESOPRP == 1 ? true : false;
                
                cboContrato.SelectedValue = oEnt[0].IDCONTRATO;
            }
            
        }

        public void usp_ListIngresoSaldo(int idGuia)
        {
            var oEnt = _GuiaIngresoZonaClient.usp_ListIngresoSaldo(idGuia).ToList();
            txtTicketSaco.Text = oEnt[0].Saco.ToString();
            txtTicketKgBruto.Text = oEnt[0].KgBruto.ToString();
            txtTicketTara.Text = oEnt[0].Tara.ToString();
            txtTicketKgNeto.Text = oEnt[0].KgNeto.ToString();

            txtGISaco.Text = oEnt[0].SacoOperacion.ToString();
            txtGIKgBruto.Text = oEnt[0].KgBrutoOperacion.ToString();
            txtGITara.Text = oEnt[0].TaraOperacion.ToString();
            txtGIKgNeto.Text = oEnt[0].KgNetoOperacion.ToString();

            txtSaldoSaco.Text = oEnt[0].SacoSaldo.ToString();
            txtSaldoKgBruto.Text = oEnt[0].KgBrutoSaldo.ToString();
            txtSaldoTara.Text = oEnt[0].TaraSaldo.ToString();
            txtSaldoKgNeto.Text = oEnt[0].KgNetoSaldo.ToString();
        }
        public void usp_InsGuiaIngresoCabecera()
        {
            int tipoOperacion = strGestion == "Editar" ? 2 : strGestion == "Nuevo" ? 1 : 0;
            int IdGuia = Convert.ToInt32(lblHiddenIdGuia.Content==null || lblHiddenIdGuia.Content.ToString() == ""? "0": lblHiddenIdGuia.Content.ToString());
            string IdGuiaIngreso = txtCodigo.Text;
            string IdLocal = cboOfOperacion.SelectedValue.ToString();
            int IdCliente = Convert.ToInt32(lblHiddenIdCliente.Content==null || lblHiddenIdCliente.Content.ToString()==""?"0": lblHiddenIdCliente.Content.ToString());
            int IdContactoCliente = Convert.ToInt32(lblHiddenIdProveedor.Content == null || lblHiddenIdProveedor.Content.ToString()==""? "0": lblHiddenIdProveedor.Content.ToString());
            string RumaDestino = cboRuma.SelectedValue.ToString();
            int TotalSaco = Convert.ToInt32(txtTotalSacos.Text==""?"0": txtTotalSacos.Text);
            decimal TotalKgBruto = Convert.ToDecimal(txtKgBruto.Text);
            decimal TotalTara=Convert.ToDecimal(txtTara.Text);
            decimal TotalDsctoAgua = Convert.ToDecimal(txtDscAgua.Text);
            decimal TotalKgNeto=Convert.ToDecimal(txtKgNeto.Text);


            
            string IdCertificadoVSP = lblHiddenIdCert.Content == null || lblHiddenIdCert.Content.ToString()==""?"0": lblHiddenIdCert.Content.ToString();
            string DescCertificacion = lblHiddenDescCert.Content == null || lblHiddenDescCert.Content.ToString() ==""? "0": lblHiddenDescCert.Content.ToString();
            string ModalidadIngreso = cboModIngreso.SelectedValue.ToString();
            string idTipoCafe = cboTipoProducto.SelectedValue.ToString();

            string GuiaRemisionExterna = txtGRTerceros.Text;
            int _detalleIdCliente = Convert.ToInt32(lblHiddenDetalleIdCliente.Content==null || lblHiddenDetalleIdCliente.Content.ToString()==""? "0": lblHiddenDetalleIdCliente.Content.ToString());
            int _detalleIdResultado = Convert.ToInt32(lblHiddenDetalleIdResultado.Content==null || lblHiddenDetalleIdResultado.Content.ToString()==""? "0": lblHiddenDetalleIdResultado.Content.ToString());
            string observacion = txtObservaciones.Text;
            int IdContrato = Convert.ToInt32(cboContrato.SelectedValue);
            string cosecha = cboCosecha.Text;
            
            

            
            var _estado = 0;
            var _proceso = chkContraServ.IsChecked == true ? 1 : 0; 
            var _division = chkDivTicket.IsChecked == true ? 1 : 0;            
                _estado = 1;            
    


            var _IdIngresoPRP = 0;
            var _IdOficinaOrigen = 0;
            var _IdTraslado = 0;

            _IdIngresoPRP = chkIngreoPRP.IsChecked == true ? 1 : 0;
            if (chkIngreoPRP.IsChecked == true)
            {
                _IdOficinaOrigen = Convert.ToInt32(lblHiddenOfOrigen.Content);
                _IdTraslado = Convert.ToInt32(lblHiddenGRSGO.Content);
            }
            
                  
            
            var oSGI = new GUIA_INGRESO_CAB();
            oSGI.IdGuia = IdGuia;
            oSGI.IdLocal = IdLocal;
            oSGI.IdCliente = IdCliente;
            oSGI.IdContactoCliente = IdContactoCliente;
            oSGI.RumaDestino = RumaDestino;
            oSGI.IdDivision = _division;
            oSGI.TotalSaco = TotalSaco;
            oSGI.TotalKgBruto = TotalKgBruto;
            oSGI.TotalTara = TotalTara;
            oSGI.TotalDsctoAgua = TotalDsctoAgua;
            oSGI.TotalKgNeto = TotalKgNeto;
            oSGI.IdCertificadoVSP = IdCertificadoVSP;
            oSGI.DescCertificacion = DescCertificacion;
            oSGI.ModalidadIngreso = ModalidadIngreso;
            oSGI.IdIngresoPRP = _IdIngresoPRP;
            oSGI.IdOficinaOrigen = _IdOficinaOrigen;
            oSGI.IdTraslado = _IdTraslado;
            oSGI.GuiaRemisionExterna = GuiaRemisionExterna;
            oSGI.IdClienteTrazabilidad = _detalleIdCliente;
            oSGI.IdTrasladoFila = _detalleIdResultado;
            oSGI.IdProceso = _proceso;
            oSGI.IdEstado = _estado;
            oSGI.UsuarioRegistro = ((Usuario)Application.Current.Resources["UserData"]).IdUsuario.ToString();
            oSGI.IdTipoCafe = Convert.ToInt32(idTipoCafe);
            oSGI.Observacion = observacion;
            oSGI.IdContrato = IdContrato;
            oSGI.Cosecha = Convert.ToInt32(cosecha);


            /*set ListadoDetalle al recorrer la tabla ticket*/
            var selected = listViewTicket.SelectedItems;
            usp_LisTickedPesadaEnProceso_Result oT = new usp_LisTickedPesadaEnProceso_Result();

            usp_LisTickedPesadaEnProceso_Result oST = new usp_LisTickedPesadaEnProceso_Result();
            GUIA_INGRESO_DET oSGDeta = new GUIA_INGRESO_DET();
            List<GUIA_INGRESO_DET> oSLstD = new List<GUIA_INGRESO_DET>();
            GUIA_INGRESO_SALDO oSGSaldo = new GUIA_INGRESO_SALDO();
            List<GUIA_INGRESO_SALDO> oSLstS = new List<GUIA_INGRESO_SALDO>();

            if (selected == null)  return; 

            for (int i = 0; i < selected.Count; i++)
            {
                oST = new usp_LisTickedPesadaEnProceso_Result();
                oSGDeta = new GUIA_INGRESO_DET();
                oSGSaldo = new GUIA_INGRESO_SALDO();
                oST = (usp_LisTickedPesadaEnProceso_Result)selected[i];

                oSGDeta.IdTicketPesada = Convert.ToInt32(oST.IdTicketPesada);
                oSGDeta.IdOrdenServicio = 0;
                oSGDeta.IdResultado = 0;
                oSGDeta.NroSaco = Convert.ToInt32(oST.NroSaco);
                oSGDeta.KgBruto = Convert.ToDecimal(oST.KgBruto);
                oSGDeta.Tara = Convert.ToDecimal(oST.Tara);
                oSGDeta.KgNeto = Convert.ToDecimal(oST.KgNeto);
                oSLstD.Add(oSGDeta);

                oSGSaldo.IdTicketPesada = Convert.ToInt32(oST.IdTicketPesada);
                oSGSaldo.IdCliente = IdCliente;
                oSGSaldo.Cosecha = Convert.ToInt32(cosecha);
                oSGSaldo.Saco = Convert.ToInt32(oST.NroSaco);
                oSGSaldo.KgBruto = Convert.ToDecimal(oST.KgBruto);
                oSGSaldo.Tara = Convert.ToDecimal(oST.Tara);
                oSGSaldo.KgNeto = Convert.ToDecimal(oST.KgNeto);

                oSGSaldo.SacoOperacion = Convert.ToInt32(txtGISaco.Text);
                oSGSaldo.KgBrutoOperacion = Convert.ToDecimal(txtGIKgBruto.Text);
                oSGSaldo.TaraOperacion = Convert.ToDecimal(txtGITara.Text);
                oSGSaldo.KgNetoOperacion = Convert.ToDecimal(txtGIKgNeto.Text);

                oSGSaldo.SacoSaldo = Convert.ToInt32(txtSaldoSaco.Text);
                oSGSaldo.KgBrutoSaldo = Convert.ToDecimal(txtSaldoKgBruto.Text);
                oSGSaldo.TaraSaldo = Convert.ToDecimal(txtSaldoTara.Text);
                oSGSaldo.KgNetoSaldo = Convert.ToDecimal(txtSaldoKgNeto.Text);
                oSLstS.Add(oSGSaldo);
            }
                        
            var oSLstDJson = JsonConvert.SerializeObject(oSLstD);
            var oSLstSJson = JsonConvert.SerializeObject(oSLstS);
            var oSGIJson = JsonConvert.SerializeObject(oSGI);
            int inScopeComplete = _GuiaIngresoZonaClient.InsertarGuiaIngresoCompleto(tipoOperacion, oSGIJson, oSLstDJson, oSLstSJson);
 

            if (inScopeComplete >= 1)
                MessageBox.Show("Operacion realizada correctamente");
        } // FIN usp_InsGuiaIngresoCabecera

      
        private void RecorrerGridObtenerMenorValor(string IdCliente,string cosecha)
        {

            decimal inSaldoMin = 10000000000;

            var lst = _CertificadoVSPClient.usp_LisSaldoCertificado(Convert.ToInt32(IdCliente),cosecha).ToList();
            ListView listView = new ListView();
            listView.ItemsSource = lst;

            int inCount = 0;
            //foreach (var item in dataGridCert.Items)
            if (listView.Items.Count == 0)
            {
                lblSaldoCerGI.Content = "0";
                return;
            }
            foreach (var item in listView.Items)
            {
                //DataGridRow row = (DataGridRow)dataGridCert.ItemContainerGenerator.ContainerFromItem(item);                
                //inSaldoMinNuevo = Convert.ToInt32(((TextBox)dataGridCert.Columns[7].GetCellContent(row)).Text);  
                inCount++;
                if (inCount > 8) continue;
                usp_LisSaldoCertificado_Result oC = new usp_LisSaldoCertificado_Result();
                oC = (usp_LisSaldoCertificado_Result)item;

                decimal inSaldoMinNuevo = 0;
                inSaldoMinNuevo = Convert.ToDecimal(oC.SALDO_CALCULADO);
                inSaldoMin = inSaldoMinNuevo < inSaldoMin ? inSaldoMinNuevo : inSaldoMin;

            }
            lblSaldoCerGI.Content = inSaldoMin;
            

        }
        public void Llenar_Grid(int idTraslado)
        {
            //var lista = new BLGuiaIngresoZona().usp_ListadoDetalleRemision(idTraslado);
            var lista = _GuiaIngresoZonaClient.usp_ListadoDetalleRemision(idTraslado);
            /*Cuando voy a editar Cargo la GR de la pestaña DETALLE*/
            listViewGR.ItemsSource = null;
            listViewGR.ItemsSource = lista;
            
            
        }





    }
}
