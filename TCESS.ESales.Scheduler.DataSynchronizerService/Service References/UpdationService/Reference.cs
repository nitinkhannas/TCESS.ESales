﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TCESS.ESales.Scheduler.DataSynchronizerService.UpdationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UpdationService.ISMSService")]
    public interface ISMSService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMSService/RespondSms", ReplyAction="http://tempuri.org/ISMSService/RespondSmsResponse")]
        string RespondSms(string phoneNumber, string message, string messageTruck);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMSService/UpdateDCAPercentage", ReplyAction="http://tempuri.org/ISMSService/UpdateDCAPercentageResponse")]
        string UpdateDCAPercentage();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISMSServiceChannel : TCESS.ESales.Scheduler.DataSynchronizerService.UpdationService.ISMSService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SMSServiceClient : System.ServiceModel.ClientBase<TCESS.ESales.Scheduler.DataSynchronizerService.UpdationService.ISMSService>, TCESS.ESales.Scheduler.DataSynchronizerService.UpdationService.ISMSService {
        
        public SMSServiceClient() {
        }
        
        public SMSServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SMSServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMSServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMSServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string RespondSms(string phoneNumber, string message, string messageTruck) {
            return base.Channel.RespondSms(phoneNumber, message, messageTruck);
        }
        
        public string UpdateDCAPercentage() {
            return base.Channel.UpdateDCAPercentage();
        }
    }
}
