﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGOTouch.ServiceSolicitudServicio {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="usp_LisTickedPesadaEnProceso_Result", Namespace="http://schemas.datacontract.org/2004/07/Persistence.domain")]
    [System.SerializableAttribute()]
    public partial class usp_LisTickedPesadaEnProceso_Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AnalisisField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoTicketPesadaZonaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FechaTicketField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IDCLIENTEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IDCONTACTOCLIENTEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdLaboratorioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdTicketPesadaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdTipoCafeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal KgBrutoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal KgNetoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LaboratorioZonaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NroSacoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ObservacionesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PesoSacoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProveedorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal TaraField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TipoCafeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Analisis {
            get {
                return this.AnalisisField;
            }
            set {
                if ((object.ReferenceEquals(this.AnalisisField, value) != true)) {
                    this.AnalisisField = value;
                    this.RaisePropertyChanged("Analisis");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodigoTicketPesadaZona {
            get {
                return this.CodigoTicketPesadaZonaField;
            }
            set {
                if ((object.ReferenceEquals(this.CodigoTicketPesadaZonaField, value) != true)) {
                    this.CodigoTicketPesadaZonaField = value;
                    this.RaisePropertyChanged("CodigoTicketPesadaZona");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FechaTicket {
            get {
                return this.FechaTicketField;
            }
            set {
                if ((object.ReferenceEquals(this.FechaTicketField, value) != true)) {
                    this.FechaTicketField = value;
                    this.RaisePropertyChanged("FechaTicket");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> IDCLIENTE {
            get {
                return this.IDCLIENTEField;
            }
            set {
                if ((this.IDCLIENTEField.Equals(value) != true)) {
                    this.IDCLIENTEField = value;
                    this.RaisePropertyChanged("IDCLIENTE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> IDCONTACTOCLIENTE {
            get {
                return this.IDCONTACTOCLIENTEField;
            }
            set {
                if ((this.IDCONTACTOCLIENTEField.Equals(value) != true)) {
                    this.IDCONTACTOCLIENTEField = value;
                    this.RaisePropertyChanged("IDCONTACTOCLIENTE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdLaboratorio {
            get {
                return this.IdLaboratorioField;
            }
            set {
                if ((object.ReferenceEquals(this.IdLaboratorioField, value) != true)) {
                    this.IdLaboratorioField = value;
                    this.RaisePropertyChanged("IdLaboratorio");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdTicketPesada {
            get {
                return this.IdTicketPesadaField;
            }
            set {
                if ((object.ReferenceEquals(this.IdTicketPesadaField, value) != true)) {
                    this.IdTicketPesadaField = value;
                    this.RaisePropertyChanged("IdTicketPesada");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdTipoCafe {
            get {
                return this.IdTipoCafeField;
            }
            set {
                if ((this.IdTipoCafeField.Equals(value) != true)) {
                    this.IdTipoCafeField = value;
                    this.RaisePropertyChanged("IdTipoCafe");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal KgBruto {
            get {
                return this.KgBrutoField;
            }
            set {
                if ((this.KgBrutoField.Equals(value) != true)) {
                    this.KgBrutoField = value;
                    this.RaisePropertyChanged("KgBruto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal KgNeto {
            get {
                return this.KgNetoField;
            }
            set {
                if ((this.KgNetoField.Equals(value) != true)) {
                    this.KgNetoField = value;
                    this.RaisePropertyChanged("KgNeto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LaboratorioZona {
            get {
                return this.LaboratorioZonaField;
            }
            set {
                if ((object.ReferenceEquals(this.LaboratorioZonaField, value) != true)) {
                    this.LaboratorioZonaField = value;
                    this.RaisePropertyChanged("LaboratorioZona");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NroSaco {
            get {
                return this.NroSacoField;
            }
            set {
                if ((this.NroSacoField.Equals(value) != true)) {
                    this.NroSacoField = value;
                    this.RaisePropertyChanged("NroSaco");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Observaciones {
            get {
                return this.ObservacionesField;
            }
            set {
                if ((object.ReferenceEquals(this.ObservacionesField, value) != true)) {
                    this.ObservacionesField = value;
                    this.RaisePropertyChanged("Observaciones");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal PesoSaco {
            get {
                return this.PesoSacoField;
            }
            set {
                if ((this.PesoSacoField.Equals(value) != true)) {
                    this.PesoSacoField = value;
                    this.RaisePropertyChanged("PesoSaco");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Proveedor {
            get {
                return this.ProveedorField;
            }
            set {
                if ((object.ReferenceEquals(this.ProveedorField, value) != true)) {
                    this.ProveedorField = value;
                    this.RaisePropertyChanged("Proveedor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Tara {
            get {
                return this.TaraField;
            }
            set {
                if ((this.TaraField.Equals(value) != true)) {
                    this.TaraField = value;
                    this.RaisePropertyChanged("Tara");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TipoCafe {
            get {
                return this.TipoCafeField;
            }
            set {
                if ((object.ReferenceEquals(this.TipoCafeField, value) != true)) {
                    this.TipoCafeField = value;
                    this.RaisePropertyChanged("TipoCafe");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceSolicitudServicio.ISolicitudServicio")]
    public interface ISolicitudServicio {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISolicitudServicio/usp_LisTickedPesadaEnProceso", ReplyAction="http://tempuri.org/ISolicitudServicio/usp_LisTickedPesadaEnProcesoResponse")]
        SGOTouch.ServiceSolicitudServicio.usp_LisTickedPesadaEnProceso_Result[] usp_LisTickedPesadaEnProceso(int inProceso, string strIdlocal, int intCliente, int intEstado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISolicitudServicio/usp_LisTickedPesadaEnProceso", ReplyAction="http://tempuri.org/ISolicitudServicio/usp_LisTickedPesadaEnProcesoResponse")]
        System.Threading.Tasks.Task<SGOTouch.ServiceSolicitudServicio.usp_LisTickedPesadaEnProceso_Result[]> usp_LisTickedPesadaEnProcesoAsync(int inProceso, string strIdlocal, int intCliente, int intEstado);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISolicitudServicioChannel : SGOTouch.ServiceSolicitudServicio.ISolicitudServicio, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SolicitudServicioClient : System.ServiceModel.ClientBase<SGOTouch.ServiceSolicitudServicio.ISolicitudServicio>, SGOTouch.ServiceSolicitudServicio.ISolicitudServicio {
        
        public SolicitudServicioClient() {
        }
        
        public SolicitudServicioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SolicitudServicioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SolicitudServicioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SolicitudServicioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SGOTouch.ServiceSolicitudServicio.usp_LisTickedPesadaEnProceso_Result[] usp_LisTickedPesadaEnProceso(int inProceso, string strIdlocal, int intCliente, int intEstado) {
            return base.Channel.usp_LisTickedPesadaEnProceso(inProceso, strIdlocal, intCliente, intEstado);
        }
        
        public System.Threading.Tasks.Task<SGOTouch.ServiceSolicitudServicio.usp_LisTickedPesadaEnProceso_Result[]> usp_LisTickedPesadaEnProcesoAsync(int inProceso, string strIdlocal, int intCliente, int intEstado) {
            return base.Channel.usp_LisTickedPesadaEnProcesoAsync(inProceso, strIdlocal, intCliente, intEstado);
        }
    }
}
