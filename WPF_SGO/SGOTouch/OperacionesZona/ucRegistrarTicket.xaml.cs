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
using MahApps.Metro.Controls;
using SGOTouch.ServiceUsuario;
using SGOTouch.ServiceTablaGeneral;
using SGOTouch.ServiceTicketPesada;
using SGOTouch.ServiceCliente;


namespace SGOTouch
{
    /// <summary>
    /// Lógica de interacción para ucRegistrarTicket.xaml
    /// </summary>
    public partial class ucRegistrarTicket : UserControl
    {
        private static string strGestion = "";
        private static TablaGeneralClient _TablaGeneralClient;
        private static TicketPesadaClient _ticketPesadaClient;
        private static ClienteClient _ClienteClient;
        usp_LisTicketPesada_Result oTicket = new usp_LisTicketPesada_Result();
      
        //private IDialogCoordinator dialogCoordinator;
        public ucRegistrarTicket()
        {
            InitializeComponent();
            _TablaGeneralClient = new TablaGeneralClient();
            _ClienteClient = new ClienteClient();
            _ticketPesadaClient = new TicketPesadaClient();
            CargarLocales();
            CargarCosecha();
            Llenar_TipoCafe();
            Llenar_TipoSaco();
            //ValidarOperacion();
            dtFecha.Text= DateTime.Today.ToString();
          
        }

    
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ValidarOperacion();
        }
        private bool ValidarVacios()
        {
            bool boReturn = true;
            int inCount = 0;
            if (lblIdCliente.Content.ToString() == "") { boReturn = false; }
            
 

            if (cboTipoProducto.SelectedValue.ToString() == "0")
            {
                cboTipoProducto.BorderBrush = Brushes.Red;
                cboTipoProducto.BorderThickness = new Thickness(5, 5, 5, 5);
                inCount++;
            }
            else
            {
                cboTipoProducto.BorderBrush = Brushes.Gray;
                cboTipoProducto.BorderThickness = new Thickness(1, 1, 1, 1);
            }

            if (cboTipoSaco.SelectedValue.ToString() == "0|0")
            {
                cboTipoSaco.BorderBrush = Brushes.Red;
                cboTipoSaco.BorderThickness = new Thickness(5, 5, 5, 5);

                inCount++;
            }
            else
            {
                cboTipoSaco.BorderBrush = Brushes.Gray;
                cboTipoSaco.BorderThickness = new Thickness(1, 1, 1, 1);

            }

            if (txtNroSacos.Text=="")
            {
                txtNroSacos.BorderBrush = Brushes.Red;
                txtNroSacos.BorderThickness = new Thickness(5, 5, 5, 5);

                inCount++;
            }
            else
            {
                txtNroSacos.BorderBrush = Brushes.Gray;
                txtNroSacos.BorderThickness = new Thickness(1, 1, 1, 1);
            }

            if (txtKgBruto.Text == "")
            {
                txtKgBruto.BorderBrush = Brushes.Red;
                txtKgBruto.BorderThickness = new Thickness(5, 5, 5, 5);

                inCount++;
            }
            else
            {
                txtKgBruto.BorderBrush = Brushes.Gray;
                txtKgBruto.BorderThickness = new Thickness(1, 1, 1, 1);

            }

            if (lblIdCliente.Content.ToString() == "0")
            {
                btnBuscarcliente.BorderBrush = Brushes.Red;
                btnBuscarcliente.BorderThickness = new Thickness(5, 5, 5, 5);
                inCount++;
            }
            else {
                btnBuscarcliente.BorderBrush = Brushes.Gray;
                btnBuscarcliente.BorderThickness = new Thickness(1,1,1,1);
            }
                
            boReturn = inCount > 0 ? false : true;
            return boReturn;
        }

        private void ValidarOperacion()
        {
            
            oTicket = ((usp_LisTicketPesada_Result)Application.Current.Resources["GridDataListarTP"]);
            if(oTicket!= null )
            {
                strGestion = "Editar";
                btnImprimir.Visibility = Visibility.Visible;
                label.Visibility = Visibility.Visible;
                txtCodigo.Visibility = Visibility.Visible;
                cboAnalisis.Visibility = Visibility.Hidden;
                txtAnalisis.Visibility = Visibility.Visible;
                txtCodigo.Text = oTicket.CODIGOTICKETPESADA;
                dtFecha.Text = oTicket.FECHATICKET;
                cboOfOperacion.SelectedValue = oTicket.IDLOCAL.Trim()=="1"?"01": oTicket.IDLOCAL;
                txtCliente.Text = oTicket.CLIENTE;
                lblIdCliente.Content = oTicket.IDCLIENTE;
                lblIdTicket.Content = oTicket.IDTICKETPESADA;

                

                /*Inicio Cargar Combo Contacto*/  
                LisClienteContacto oSrvClie = new LisClienteContacto();
                List<LisClienteContacto> lstSrvClie = new List<LisClienteContacto>();                
                var lst2 = _ClienteClient.usp_LisClienteContacto(oTicket.IDCLIENTE,"1").ToList();                
                foreach (LisClienteContacto item in lst2)
                {
                    oSrvClie = new LisClienteContacto();
                    oSrvClie.NOMBRE = item.NOMBRE +" - "+ item.APELLIDO;
                    oSrvClie.IDCONTACTOCLIENTE = item.IDCONTACTOCLIENTE;
                    lstSrvClie.Add(oSrvClie);
                }
                
                cboProveedor.ItemsSource = lstSrvClie;
                cboProveedor.DisplayMemberPath = "NOMBRE";
                cboProveedor.SelectedValuePath = "IDCONTACTOCLIENTE";
                cboProveedor.SelectedValue = oTicket.IDCONTACTOCLIENTE;
                /*Fin Cargar Combo Contacto*/
                txtLocal.Text = oTicket.DEPARTAMENTO;
                txtSector.Text = oTicket.PROVINCIA;
                txtDistrito.Text = oTicket.DISTRITO;
                lblidLaboratorio.Content = oTicket.IDLABORATORIO;
                txtAnalisis.Text = oTicket.CODIGOLABORATORIO;
                oTicket.DESCLABORATORIO = oTicket.DESCLABORATORIO == null ? "0*0*0*0" : oTicket.DESCLABORATORIO;
                string[] arrLaboratorio = oTicket.DESCLABORATORIO.Split('*');
                if (arrLaboratorio.Length ==4)
                {
                    txtZona.Text = arrLaboratorio[0];
                    txtCalidad.Text = arrLaboratorio[1];
                    txtHumedad.Text = arrLaboratorio[2];
                    txtRendimiento.Text = arrLaboratorio[3];
                }
                
                txtNroSacos.Text = oTicket.NROSACO.ToString();
                txtKgBruto.Text = oTicket.KGBRUTO.ToString();
                cboTipoProducto.SelectedValue = oTicket.IDTIPOCAFE;

                cboTipoSaco.SelectedValue = oTicket.IDSACO+"|"+ oTicket.PESOSACO+"00";
                CalcularTotales_cboTipoSaco();
                txtObservaciones.Text = oTicket.OBSERVACIONES;
                chkEstadoTicket.IsChecked = (oTicket.IDESTADO == 1) ? true : false;

                Application.Current.Resources["GridDataListarTP"] = null;
            }else
            {
                strGestion = "Nuevo";
                btnImprimir.Visibility = Visibility.Hidden;
                label.Visibility = Visibility.Hidden;
                txtCodigo.Visibility = Visibility.Hidden;
            }
        }
        private void CargarLocales()
        {
            var lstT = _TablaGeneralClient.usp_SelLocalIdEmpresaUsuario(Util.GetIdEmpresa(),((Usuario)Application.Current.Resources["UserData"]).IdUsuario);
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
        }
        private void Llenar_TipoSaco()
        {
            
            
            var lst = _TablaGeneralClient.usp_LisComboSacoCafe("1").ToList();

            lst.Add(new usp_LisComboSacoCafe_Result { CODIGO = "0|0", DESCRIPCION = "[ Seleccione ]" });

            this.cboTipoSaco.ItemsSource = lst;
            this.cboTipoSaco.DisplayMemberPath = "DESCRIPCION";
            this.cboTipoSaco.SelectedValuePath = "CODIGO";
            this.cboTipoSaco.SelectedIndex = cboTipoSaco.Items.Count - 1;

            //this.cboTipoSaco.SelectedValue = "0|0";
        }

        public void usp_Mant_CLIENTE_COSECHA(string vcidCliente)
        {
            
            CLIENTE_COSECHA oP = new CLIENTE_COSECHA();
            oP.IdCliente= Convert.ToInt32(vcidCliente);
            oP.InVigente = 1;
            oP.UsuarioRegistro= ((Usuario)Application.Current.Resources["UserData"]).IdUsuario;
            var lst = _TablaGeneralClient.usp_Mant_CLIENTE_COSECHA(1, oP);
                      
            cboCosecha.ItemsSource = null;

            cboCosecha.ItemsSource = lst.Where(_ => _.InVigente==1);
            cboCosecha.DisplayMemberPath = "DESCRIPCION";
            cboCosecha.SelectedValuePath = "IdCosecha";
            cboCosecha.SelectedIndex = 0;
        }



        /*Inicio Eventos*/
        //private void FooMessage()
        //{
        //    dialogCoordinator = new IDialogCoordinator();
        //    await dialogCoordinator.ShowMessageAsync(this, "HEADER", "MESSAGE");
        //}

        //private void FooProgress()
        //{
        //    // Show...
        //    ProgressDialogController controller = await dialogCoordinator.ShowProgressAsync(this, "HEADER", "MESSAGE");
        //    controller.SetIndeterminate();

        //    // Do your work... 

        //    // Close...
        //    await controller.CloseAsync();
        //}
        private void btnBuscarcliente_Click(object sender, RoutedEventArgs e)
        {
            WinCliente objWincliente = new WinCliente(this,null);
            objWincliente.Show();
        }

        private void cboAnalisis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAnalisis.Items.Count > 0)
            {
                string[] arrVal = (cboAnalisis.SelectedValue.ToString()).Split('*');
                if (arrVal.Length > 1)
                {
                    txtZona.Text = arrVal[0];
                    txtCalidad.Text = arrVal[1];
                    txtHumedad.Text = arrVal[2];
                    txtRendimiento.Text = arrVal[3];
                }
            }
        }

        private void cboTipoSaco_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTipoSaco.Items.Count > 0)
            {
                string[] arrVal = (cboTipoSaco.SelectedValue.ToString()).Split('|');
                if (arrVal.Length > 1)
                {
                    txtPesoRef.Text = arrVal[1];
                    CalcularTara();
                    CalcularKgNeto();
                    CalcularKgseco();
                }
            }
        }
        private  void CalcularTotales_cboTipoSaco()
        {
            if (cboTipoSaco.Items.Count > 0)
            {
                string[] arrVal = (cboTipoSaco.SelectedValue.ToString()).Split('|');
                if (arrVal.Length > 1)
                {
                    txtPesoRef.Text = arrVal[1];
                    CalcularTara();
                    CalcularKgNeto();
                    CalcularKgseco();

                }
            }
        }
        private async void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarVacios())
            {
                MessageBox.Show("Ingresar los datos en los elementos resaltados");
                return;
            }
            int IdTicket = Convert.ToInt32(lblIdTicket.Content);
            int Cosecha = Convert.ToInt32(cboCosecha.Text);
            int IdCliente = Convert.ToInt32(lblIdCliente.Content);
            int IdContactoCliente = Convert.ToInt32(cboProveedor.SelectedValue);
            string IdLocal = cboOfOperacion.SelectedValue.ToString();
            string FechaTicket = dtFecha.Text;
            string[] arrAnalisis;
            string IdLaboratorio = "";
            if (strGestion == "Nuevo")
            {
                arrAnalisis = cboAnalisis.SelectedValue.ToString().Split('*');
                IdLaboratorio = arrAnalisis[arrAnalisis.Length - 1];
            }
            else if (strGestion == "Editar")
            {
                IdLaboratorio = Convert.ToInt32(lblidLaboratorio.Content.ToString()).ToString(); /*0000012123 => 12123*/
            }

            int NroSaco = Convert.ToInt32(txtNroSacos.Text);
            string[] arrTipoSaco = cboTipoSaco.SelectedValue.ToString().Split('|');
            int IdSaco = Convert.ToInt32(arrTipoSaco[0]);
            decimal PesoSaco = Convert.ToDecimal(txtPesoRef.Text);
            decimal Tara = Convert.ToDecimal(txtTara.Text);
            decimal KgBruto = Convert.ToDecimal(txtKgBruto.Text);
            decimal KgNeto = Convert.ToDecimal(txtKgNeto.Text);
            decimal DsctoAgua = Convert.ToDecimal(0);
            decimal KgSeco = Convert.ToDecimal(lblKgSeco.Content.ToString());
            string IdProceso = "0";
            string Observacion = txtObservaciones.Text;
            string IdEstado = chkEstadoTicket.IsChecked == true ? "1" : "0";
            /*♦ IdEstado==0 => en el usp set idLaboratorio=0*/
            int IdTipoCafe = Convert.ToInt32(cboTipoProducto.SelectedValue);
            string IdGuiaRemision = "";
            string GuiaRemision = "";

            string data = "";
            int tipoOperacion = 0;

            string oResultado = "0";


            if (IdTicket.ToString().Equals("0"))
                tipoOperacion = Convert.ToInt32(Util.TipoOperacion.Insertar);
            else
                tipoOperacion = Convert.ToInt32(Util.TipoOperacion.Modificar);
            var oTicket = new TICKET_PESADA()
            {
                Cosecha = Cosecha,
                IdTicketPesada = IdTicket,
                IdCliente = IdCliente,
                IdContactoCliente = IdContactoCliente,
                IdLocal = IdLocal,
                FechaTicket = Convert.ToDateTime(FechaTicket),
                IdLaboratorio = Convert.ToInt32(IdLaboratorio),
                NroSaco = NroSaco,
                IdSaco = IdSaco,
                PesoSaco = PesoSaco,
                Tara = Tara,
                KgBruto = KgBruto,
                KgNeto = KgNeto,
                DsctoAgua = DsctoAgua,
                KgSeco = KgSeco,
                IdProceso = Convert.ToInt32(IdProceso),
                Observaciones = Observacion,
                IdEstado = Convert.ToInt32(IdEstado),
                UsuarioRegistro = ((Usuario)Application.Current.Resources["UserData"]).IdUsuario.ToString(),
                IdTipoCafe = IdTipoCafe,
                IdGuiaRemision = IdGuiaRemision,
                GuiaRemision = GuiaRemision
            };



            try
            {

                //oResultado = oBl.usp_InsTicketPesada(oBe);


                var oSResultado = _ticketPesadaClient.usp_InsTicketPesada(tipoOperacion, oTicket);
                oResultado = "1";
            }
            catch (Exception ex)
            {
                /*¡LOG!*/

            }

            if (oResultado == "1")
            {
              

                data = "Operacion realizada correctamente";
                //btnImprimir.Visibility = Visibility.Visible;

            }
            else
            {
                data = "Fail";
                //btnImprimir.Visibility = Visibility.Hidden;
            }


            

            MessageBox.Show(data);
            

        }

        //private async static Task<MessageDialogResult> ShowMessageAsync(string title, string Message,
        //    MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
        //{
        //    return await ((MetroWindow)(Application.Current.MainWindow)).ShowMessageAsync(title, Message, style, settings);
        //}

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            if ( lblIdTicket.Content == null) return;
            usp_ReporteTicketPesada(lblIdTicket.Content.ToString());
        }

        private void txtKgBruto_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularTara();
            CalcularKgNeto();
            CalcularKgseco();

        }

        private void txtNroSacos_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularTara();
            CalcularKgNeto();
            CalcularKgseco();

        }

        private void txtKgBruto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
        private void txtNroSacos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));            
            
        }
        private void cboOfOperacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblIdCliente.Content.ToString() != "")
            {
                int idCliente = Convert.ToInt32(lblIdCliente.Content.ToString());
                string idLocal = cboOfOperacion.SelectedValue.ToString();
                var lst = _TablaGeneralClient.usp_LisLaboratorioDisponibleXCliente(idCliente,idLocal).ToList();

                usp_LisLaboratorioDisponibleXCliente_Result obTG = new usp_LisLaboratorioDisponibleXCliente_Result();
                obTG.CodigoLaboratorio= "[ Seleccione ]";
                obTG.IdLaboratorio = "0*0*0*0";
                lst.Insert(0,obTG);

                this.cboAnalisis.ItemsSource = null;
                this.cboAnalisis.ItemsSource = lst;
                this.cboAnalisis.DisplayMemberPath = "CodigoLaboratorio";
                this.cboAnalisis.SelectedValuePath = "IdLaboratorio";
                this.cboAnalisis.SelectedIndex = 0;
            }
            else { return; }
        }
        /*Fin Eventos*/

        /*INICIO Metodos Calculo*/
        private void CalcularTara()
        {
            string Saco = txtNroSacos.Text.Trim()==""?"0": txtNroSacos.Text.Trim();
            
            decimal dcTara = 0;
            decimal dcTotalTara=0;

            string[] arrTipoSaco = cboTipoSaco.SelectedValue.ToString().Split('|');
            if(arrTipoSaco.Length>0)
            {
                dcTara = Convert.ToDecimal(arrTipoSaco[1]);
            }
            dcTotalTara = Convert.ToInt32(Saco) * dcTara;
            txtTara.Text = Decimal.Round(dcTotalTara,2).ToString();
        }
        private void CalcularKgNeto()
        {
            decimal dcTara =txtTara.Text.Trim() == ""? 0 : Convert.ToDecimal(txtTara.Text.Trim());
            decimal dcKgBruto = txtKgBruto.Text.Trim()==""? 0: Convert.ToDecimal(txtKgBruto.Text.Trim());

            decimal dcTotalKgNeto = dcKgBruto - dcTara;
            txtKgNeto.Text = Decimal.Round(dcTotalKgNeto,2).ToString();
        }
        private void CalcularKgseco()
        {
            decimal KgNeto = Convert.ToDecimal(txtKgNeto.Text.ToString());
            lblKgSeco.Content = KgNeto-0;
        }
        /*FIN Metodo Calculo*/

        /*Inicio Reporte*/
        public static string usp_ReporteTicketPesada(string idTicketPesada)
        {
            string response = String.Empty;
            if (idTicketPesada.Equals(String.Empty))
                idTicketPesada = "0";

            try
            {
                //---Datos reporte
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
        /*Fin Reporte*/
        
    }


}
