﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATS.Service.WCFClient.TimesheetClient {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TimesheetSummary", Namespace="http://schemas.datacontract.org/2004/07/ATS.Service")]
    [System.SerializableAttribute()]
    public partial class TimesheetSummary : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AgentInChargeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StaffIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StaffNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SupervisorNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double WorkingHoursField;
        
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
        public string AgentInCharge {
            get {
                return this.AgentInChargeField;
            }
            set {
                if ((object.ReferenceEquals(this.AgentInChargeField, value) != true)) {
                    this.AgentInChargeField = value;
                    this.RaisePropertyChanged("AgentInCharge");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StaffID {
            get {
                return this.StaffIDField;
            }
            set {
                if ((this.StaffIDField.Equals(value) != true)) {
                    this.StaffIDField = value;
                    this.RaisePropertyChanged("StaffID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StaffName {
            get {
                return this.StaffNameField;
            }
            set {
                if ((object.ReferenceEquals(this.StaffNameField, value) != true)) {
                    this.StaffNameField = value;
                    this.RaisePropertyChanged("StaffName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SupervisorName {
            get {
                return this.SupervisorNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SupervisorNameField, value) != true)) {
                    this.SupervisorNameField = value;
                    this.RaisePropertyChanged("SupervisorName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double WorkingHours {
            get {
                return this.WorkingHoursField;
            }
            set {
                if ((this.WorkingHoursField.Equals(value) != true)) {
                    this.WorkingHoursField = value;
                    this.RaisePropertyChanged("WorkingHours");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TimeSheet", Namespace="http://schemas.datacontract.org/2004/07/ATS.Service")]
    [System.SerializableAttribute()]
    public partial class TimeSheet : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime EndTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HourField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartTimeField;
        
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
        public System.DateTime EndTime {
            get {
                return this.EndTimeField;
            }
            set {
                if ((this.EndTimeField.Equals(value) != true)) {
                    this.EndTimeField = value;
                    this.RaisePropertyChanged("EndTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Hour {
            get {
                return this.HourField;
            }
            set {
                if ((this.HourField.Equals(value) != true)) {
                    this.HourField = value;
                    this.RaisePropertyChanged("Hour");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime StartTime {
            get {
                return this.StartTimeField;
            }
            set {
                if ((this.StartTimeField.Equals(value) != true)) {
                    this.StartTimeField = value;
                    this.RaisePropertyChanged("StartTime");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TimesheetClient.ITimesheetService")]
    public interface ITimesheetService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimesheetService/GetTimesheetSummary", ReplyAction="http://tempuri.org/ITimesheetService/GetTimesheetSummaryResponse")]
        ATS.Service.WCFClient.TimesheetClient.TimesheetSummary[] GetTimesheetSummary(int companyId, int year, int month);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimesheetService/GetTimesheetSummary", ReplyAction="http://tempuri.org/ITimesheetService/GetTimesheetSummaryResponse")]
        System.Threading.Tasks.Task<ATS.Service.WCFClient.TimesheetClient.TimesheetSummary[]> GetTimesheetSummaryAsync(int companyId, int year, int month);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimesheetService/GetTimesheetDetail", ReplyAction="http://tempuri.org/ITimesheetService/GetTimesheetDetailResponse")]
        ATS.Service.WCFClient.TimesheetClient.TimeSheet[] GetTimesheetDetail(int personId, int year, int month);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimesheetService/GetTimesheetDetail", ReplyAction="http://tempuri.org/ITimesheetService/GetTimesheetDetailResponse")]
        System.Threading.Tasks.Task<ATS.Service.WCFClient.TimesheetClient.TimeSheet[]> GetTimesheetDetailAsync(int personId, int year, int month);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITimesheetServiceChannel : ATS.Service.WCFClient.TimesheetClient.ITimesheetService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TimesheetServiceClient : System.ServiceModel.ClientBase<ATS.Service.WCFClient.TimesheetClient.ITimesheetService>, ATS.Service.WCFClient.TimesheetClient.ITimesheetService {
        
        public TimesheetServiceClient() {
        }
        
        public TimesheetServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TimesheetServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimesheetServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimesheetServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ATS.Service.WCFClient.TimesheetClient.TimesheetSummary[] GetTimesheetSummary(int companyId, int year, int month) {
            return base.Channel.GetTimesheetSummary(companyId, year, month);
        }
        
        public System.Threading.Tasks.Task<ATS.Service.WCFClient.TimesheetClient.TimesheetSummary[]> GetTimesheetSummaryAsync(int companyId, int year, int month) {
            return base.Channel.GetTimesheetSummaryAsync(companyId, year, month);
        }
        
        public ATS.Service.WCFClient.TimesheetClient.TimeSheet[] GetTimesheetDetail(int personId, int year, int month) {
            return base.Channel.GetTimesheetDetail(personId, year, month);
        }
        
        public System.Threading.Tasks.Task<ATS.Service.WCFClient.TimesheetClient.TimeSheet[]> GetTimesheetDetailAsync(int personId, int year, int month) {
            return base.Channel.GetTimesheetDetailAsync(personId, year, month);
        }
    }
}
