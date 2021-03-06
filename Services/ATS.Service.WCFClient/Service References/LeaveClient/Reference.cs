﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATS.Service.WCFClient.LeaveClient {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LeaveSummary", Namespace="http://schemas.datacontract.org/2004/07/ATS.Service")]
    [System.SerializableAttribute()]
    public partial class LeaveSummary : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ApprovedDurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double PendingDurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double RejectDurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StaffIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StaffNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double TotalDurationField;
        
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
        public double ApprovedDuration {
            get {
                return this.ApprovedDurationField;
            }
            set {
                if ((this.ApprovedDurationField.Equals(value) != true)) {
                    this.ApprovedDurationField = value;
                    this.RaisePropertyChanged("ApprovedDuration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double PendingDuration {
            get {
                return this.PendingDurationField;
            }
            set {
                if ((this.PendingDurationField.Equals(value) != true)) {
                    this.PendingDurationField = value;
                    this.RaisePropertyChanged("PendingDuration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double RejectDuration {
            get {
                return this.RejectDurationField;
            }
            set {
                if ((this.RejectDurationField.Equals(value) != true)) {
                    this.RejectDurationField = value;
                    this.RaisePropertyChanged("RejectDuration");
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
        public double TotalDuration {
            get {
                return this.TotalDurationField;
            }
            set {
                if ((this.TotalDurationField.Equals(value) != true)) {
                    this.TotalDurationField = value;
                    this.RaisePropertyChanged("TotalDuration");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Leave", Namespace="http://schemas.datacontract.org/2004/07/ATS.Service")]
    [System.SerializableAttribute()]
    public partial class Leave : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double DurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime FromDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LeaveCategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ToDateField;
        
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
        public double Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FromDate {
            get {
                return this.FromDateField;
            }
            set {
                if ((this.FromDateField.Equals(value) != true)) {
                    this.FromDateField = value;
                    this.RaisePropertyChanged("FromDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LeaveCategory {
            get {
                return this.LeaveCategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.LeaveCategoryField, value) != true)) {
                    this.LeaveCategoryField = value;
                    this.RaisePropertyChanged("LeaveCategory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ToDate {
            get {
                return this.ToDateField;
            }
            set {
                if ((this.ToDateField.Equals(value) != true)) {
                    this.ToDateField = value;
                    this.RaisePropertyChanged("ToDate");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LeaveClient.ILeaveService")]
    public interface ILeaveService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILeaveService/GetLeaveSummary", ReplyAction="http://tempuri.org/ILeaveService/GetLeaveSummaryResponse")]
        ATS.Service.WCFClient.LeaveClient.LeaveSummary[] GetLeaveSummary(int companyId, int year, int month);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILeaveService/GetLeaveSummary", ReplyAction="http://tempuri.org/ILeaveService/GetLeaveSummaryResponse")]
        System.Threading.Tasks.Task<ATS.Service.WCFClient.LeaveClient.LeaveSummary[]> GetLeaveSummaryAsync(int companyId, int year, int month);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILeaveService/GetLeaveDetails", ReplyAction="http://tempuri.org/ILeaveService/GetLeaveDetailsResponse")]
        ATS.Service.WCFClient.LeaveClient.Leave[] GetLeaveDetails(int personId, int year, int month);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILeaveService/GetLeaveDetails", ReplyAction="http://tempuri.org/ILeaveService/GetLeaveDetailsResponse")]
        System.Threading.Tasks.Task<ATS.Service.WCFClient.LeaveClient.Leave[]> GetLeaveDetailsAsync(int personId, int year, int month);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILeaveServiceChannel : ATS.Service.WCFClient.LeaveClient.ILeaveService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LeaveServiceClient : System.ServiceModel.ClientBase<ATS.Service.WCFClient.LeaveClient.ILeaveService>, ATS.Service.WCFClient.LeaveClient.ILeaveService {
        
        public LeaveServiceClient() {
        }
        
        public LeaveServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LeaveServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LeaveServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LeaveServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ATS.Service.WCFClient.LeaveClient.LeaveSummary[] GetLeaveSummary(int companyId, int year, int month) {
            return base.Channel.GetLeaveSummary(companyId, year, month);
        }
        
        public System.Threading.Tasks.Task<ATS.Service.WCFClient.LeaveClient.LeaveSummary[]> GetLeaveSummaryAsync(int companyId, int year, int month) {
            return base.Channel.GetLeaveSummaryAsync(companyId, year, month);
        }
        
        public ATS.Service.WCFClient.LeaveClient.Leave[] GetLeaveDetails(int personId, int year, int month) {
            return base.Channel.GetLeaveDetails(personId, year, month);
        }
        
        public System.Threading.Tasks.Task<ATS.Service.WCFClient.LeaveClient.Leave[]> GetLeaveDetailsAsync(int personId, int year, int month) {
            return base.Channel.GetLeaveDetailsAsync(personId, year, month);
        }
    }
}
