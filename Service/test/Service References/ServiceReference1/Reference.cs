﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/OverlapID", ReplyAction="http://tempuri.org/IService1/OverlapIDResponse")]
        bool OverlapID(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/OverlapID", ReplyAction="http://tempuri.org/IService1/OverlapIDResponse")]
        System.Threading.Tasks.Task<bool> OverlapIDAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/OverlapPhoneNum", ReplyAction="http://tempuri.org/IService1/OverlapPhoneNumResponse")]
        bool OverlapPhoneNum(string pnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/OverlapPhoneNum", ReplyAction="http://tempuri.org/IService1/OverlapPhoneNumResponse")]
        System.Threading.Tasks.Task<bool> OverlapPhoneNumAsync(string pnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/JoinMember", ReplyAction="http://tempuri.org/IService1/JoinMemberResponse")]
        bool JoinMember(string id, string pw, string name, string email, string pnum, string dnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/JoinMember", ReplyAction="http://tempuri.org/IService1/JoinMemberResponse")]
        System.Threading.Tasks.Task<bool> JoinMemberAsync(string id, string pw, string name, string email, string pnum, string dnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LoginIDCheck", ReplyAction="http://tempuri.org/IService1/LoginIDCheckResponse")]
        bool LoginIDCheck(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LoginIDCheck", ReplyAction="http://tempuri.org/IService1/LoginIDCheckResponse")]
        System.Threading.Tasks.Task<bool> LoginIDCheckAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LoginPWCheck", ReplyAction="http://tempuri.org/IService1/LoginPWCheckResponse")]
        bool LoginPWCheck(string pw);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LoginPWCheck", ReplyAction="http://tempuri.org/IService1/LoginPWCheckResponse")]
        System.Threading.Tasks.Task<bool> LoginPWCheckAsync(string pw);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LoginIDPWCheck", ReplyAction="http://tempuri.org/IService1/LoginIDPWCheckResponse")]
        bool LoginIDPWCheck(string id, string pw);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LoginIDPWCheck", ReplyAction="http://tempuri.org/IService1/LoginIDPWCheckResponse")]
        System.Threading.Tasks.Task<bool> LoginIDPWCheckAsync(string id, string pw);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckNameEmail", ReplyAction="http://tempuri.org/IService1/CheckNameEmailResponse")]
        bool CheckNameEmail(string name, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckNameEmail", ReplyAction="http://tempuri.org/IService1/CheckNameEmailResponse")]
        System.Threading.Tasks.Task<bool> CheckNameEmailAsync(string name, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckNamePhone", ReplyAction="http://tempuri.org/IService1/CheckNamePhoneResponse")]
        bool CheckNamePhone(string name, string pnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckNamePhone", ReplyAction="http://tempuri.org/IService1/CheckNamePhoneResponse")]
        System.Threading.Tasks.Task<bool> CheckNamePhoneAsync(string name, string pnum);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : test.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<test.ServiceReference1.IService1>, test.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool OverlapID(string id) {
            return base.Channel.OverlapID(id);
        }
        
        public System.Threading.Tasks.Task<bool> OverlapIDAsync(string id) {
            return base.Channel.OverlapIDAsync(id);
        }
        
        public bool OverlapPhoneNum(string pnum) {
            return base.Channel.OverlapPhoneNum(pnum);
        }
        
        public System.Threading.Tasks.Task<bool> OverlapPhoneNumAsync(string pnum) {
            return base.Channel.OverlapPhoneNumAsync(pnum);
        }
        
        public bool JoinMember(string id, string pw, string name, string email, string pnum, string dnum) {
            return base.Channel.JoinMember(id, pw, name, email, pnum, dnum);
        }
        
        public System.Threading.Tasks.Task<bool> JoinMemberAsync(string id, string pw, string name, string email, string pnum, string dnum) {
            return base.Channel.JoinMemberAsync(id, pw, name, email, pnum, dnum);
        }
        
        public bool LoginIDCheck(string id) {
            return base.Channel.LoginIDCheck(id);
        }
        
        public System.Threading.Tasks.Task<bool> LoginIDCheckAsync(string id) {
            return base.Channel.LoginIDCheckAsync(id);
        }
        
        public bool LoginPWCheck(string pw) {
            return base.Channel.LoginPWCheck(pw);
        }
        
        public System.Threading.Tasks.Task<bool> LoginPWCheckAsync(string pw) {
            return base.Channel.LoginPWCheckAsync(pw);
        }
        
        public bool LoginIDPWCheck(string id, string pw) {
            return base.Channel.LoginIDPWCheck(id, pw);
        }
        
        public System.Threading.Tasks.Task<bool> LoginIDPWCheckAsync(string id, string pw) {
            return base.Channel.LoginIDPWCheckAsync(id, pw);
        }
        
        public bool CheckNameEmail(string name, string email) {
            return base.Channel.CheckNameEmail(name, email);
        }
        
        public System.Threading.Tasks.Task<bool> CheckNameEmailAsync(string name, string email) {
            return base.Channel.CheckNameEmailAsync(name, email);
        }
        
        public bool CheckNamePhone(string name, string pnum) {
            return base.Channel.CheckNamePhone(name, pnum);
        }
        
        public System.Threading.Tasks.Task<bool> CheckNamePhoneAsync(string name, string pnum) {
            return base.Channel.CheckNamePhoneAsync(name, pnum);
        }
    }
}
