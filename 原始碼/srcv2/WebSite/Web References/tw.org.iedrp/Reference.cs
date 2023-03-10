//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 原始程式碼已由 Microsoft.VSDesigner 自動產生，版本 4.0.30319.42000。
// 
#pragma warning disable 1591

namespace JamZoo.Project.WebSite.tw.org.iedrp {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SessionServicePortSoap11", Namespace="http://webServices.jcs/beans/session")]
    public partial class SessionServicePortService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getSessionOperationCompleted;
        
        private System.Threading.SendOrPostCallback cleanSessionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SessionServicePortService() {
            this.Url = global::JamZoo.Project.WebSite.Properties.Settings.Default.WebSite_tw_org_iedrp_SessionServicePortService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getSessionCompletedEventHandler getSessionCompleted;
        
        /// <remarks/>
        public event cleanSessionCompletedEventHandler cleanSessionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("getSessionResponse", Namespace="http://webServices.jcs/beans/session")]
        public getSessionResponse getSession([System.Xml.Serialization.XmlElementAttribute(Namespace="http://webServices.jcs/beans/session")] getSessionRequest getSessionRequest) {
            object[] results = this.Invoke("getSession", new object[] {
                        getSessionRequest});
            return ((getSessionResponse)(results[0]));
        }
        
        /// <remarks/>
        public void getSessionAsync(getSessionRequest getSessionRequest) {
            this.getSessionAsync(getSessionRequest, null);
        }
        
        /// <remarks/>
        public void getSessionAsync(getSessionRequest getSessionRequest, object userState) {
            if ((this.getSessionOperationCompleted == null)) {
                this.getSessionOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSessionOperationCompleted);
            }
            this.InvokeAsync("getSession", new object[] {
                        getSessionRequest}, this.getSessionOperationCompleted, userState);
        }
        
        private void OngetSessionOperationCompleted(object arg) {
            if ((this.getSessionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSessionCompleted(this, new getSessionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("cleanSessionResponse", Namespace="http://webServices.jcs/beans/session")]
        public cleanSessionResponse cleanSession([System.Xml.Serialization.XmlElementAttribute(Namespace="http://webServices.jcs/beans/session")] cleanSessionRequest cleanSessionRequest) {
            object[] results = this.Invoke("cleanSession", new object[] {
                        cleanSessionRequest});
            return ((cleanSessionResponse)(results[0]));
        }
        
        /// <remarks/>
        public void cleanSessionAsync(cleanSessionRequest cleanSessionRequest) {
            this.cleanSessionAsync(cleanSessionRequest, null);
        }
        
        /// <remarks/>
        public void cleanSessionAsync(cleanSessionRequest cleanSessionRequest, object userState) {
            if ((this.cleanSessionOperationCompleted == null)) {
                this.cleanSessionOperationCompleted = new System.Threading.SendOrPostCallback(this.OncleanSessionOperationCompleted);
            }
            this.InvokeAsync("cleanSession", new object[] {
                        cleanSessionRequest}, this.cleanSessionOperationCompleted, userState);
        }
        
        private void OncleanSessionOperationCompleted(object arg) {
            if ((this.cleanSessionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.cleanSessionCompleted(this, new cleanSessionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webServices.jcs/beans/session")]
    public partial class getSessionRequest {
        
        private string appidField;
        
        /// <remarks/>
        public string appid {
            get {
                return this.appidField;
            }
            set {
                this.appidField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webServices.jcs/beans/session")]
    public partial class getSessionResponse {
        
        private string encryptedTokenField;
        
        private string expiredTimeField;
        
        /// <remarks/>
        public string encryptedToken {
            get {
                return this.encryptedTokenField;
            }
            set {
                this.encryptedTokenField = value;
            }
        }
        
        /// <remarks/>
        public string expiredTime {
            get {
                return this.expiredTimeField;
            }
            set {
                this.expiredTimeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webServices.jcs/beans/session")]
    public partial class cleanSessionRequest {
        
        private string appidField;
        
        /// <remarks/>
        public string appid {
            get {
                return this.appidField;
            }
            set {
                this.appidField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webServices.jcs/beans/session")]
    public partial class cleanSessionResponse {
        
        private bool successField;
        
        /// <remarks/>
        public bool success {
            get {
                return this.successField;
            }
            set {
                this.successField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void getSessionCompletedEventHandler(object sender, getSessionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getSessionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getSessionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public getSessionResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((getSessionResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void cleanSessionCompletedEventHandler(object sender, cleanSessionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class cleanSessionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal cleanSessionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public cleanSessionResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((cleanSessionResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591