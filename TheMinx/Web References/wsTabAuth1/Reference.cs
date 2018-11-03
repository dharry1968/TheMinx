﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34003.
// 
#pragma warning disable 1591

namespace TheMinx.wsTabAuth1 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="thirdPartyAuthenticateSoapBinding", Namespace="http://service.thirdparty.api.neo.tabcorp.com.au/")]
    public partial class thirdPartyAuthenticate : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback authenticateAccountOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public thirdPartyAuthenticate() {
            this.Url = global::TheMinx.Properties.Settings.Default.TheMinx_wsTabAuth1_thirdPartyAuthenticate;
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
        public event authenticateAccountCompletedEventHandler authenticateAccountCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://service.thirdparty.api.neo.tabcorp.com.au/", ResponseNamespace="http://service.thirdparty.api.neo.tabcorp.com.au/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public thirdPartyCustomerAuthenticateResponse authenticateAccount([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] apiMeta apiMeta, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] thirdPartyCustomerAuthenticateRequest authRequest) {
            object[] results = this.Invoke("authenticateAccount", new object[] {
                        apiMeta,
                        authRequest});
            return ((thirdPartyCustomerAuthenticateResponse)(results[0]));
        }
        
        /// <remarks/>
        public void authenticateAccountAsync(apiMeta apiMeta, thirdPartyCustomerAuthenticateRequest authRequest) {
            this.authenticateAccountAsync(apiMeta, authRequest, null);
        }
        
        /// <remarks/>
        public void authenticateAccountAsync(apiMeta apiMeta, thirdPartyCustomerAuthenticateRequest authRequest, object userState) {
            if ((this.authenticateAccountOperationCompleted == null)) {
                this.authenticateAccountOperationCompleted = new System.Threading.SendOrPostCallback(this.OnauthenticateAccountOperationCompleted);
            }
            this.InvokeAsync("authenticateAccount", new object[] {
                        apiMeta,
                        authRequest}, this.authenticateAccountOperationCompleted, userState);
        }
        
        private void OnauthenticateAccountOperationCompleted(object arg) {
            if ((this.authenticateAccountCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.authenticateAccountCompleted(this, new authenticateAccountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.thirdparty.api.neo.tabcorp.com.au/")]
    public partial class apiMeta {
        
        private long deviceIdField;
        
        private bool deviceIdFieldSpecified;
        
        private long jurisdictionIdField;
        
        private long requestChannelField;
        
        private bool requestChannelFieldSpecified;
        
        private string usernamePasswordTokenField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long deviceId {
            get {
                return this.deviceIdField;
            }
            set {
                this.deviceIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deviceIdSpecified {
            get {
                return this.deviceIdFieldSpecified;
            }
            set {
                this.deviceIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long jurisdictionId {
            get {
                return this.jurisdictionIdField;
            }
            set {
                this.jurisdictionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long requestChannel {
            get {
                return this.requestChannelField;
            }
            set {
                this.requestChannelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool requestChannelSpecified {
            get {
                return this.requestChannelFieldSpecified;
            }
            set {
                this.requestChannelFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string usernamePasswordToken {
            get {
                return this.usernamePasswordTokenField;
            }
            set {
                this.usernamePasswordTokenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.thirdparty.api.neo.tabcorp.com.au/")]
    public partial class thirdPartyCustomerAuthenticateResponse {
        
        private double accountBalanceField;
        
        private long accountIdField;
        
        private string emailIdField;
        
        private string firstNameField;
        
        private long jurisdictionIdField;
        
        private bool jurisdictionIdFieldSpecified;
        
        private string lastNameField;
        
        private int tierCodeField;
        
        private string usernamePasswordTokenField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double accountBalance {
            get {
                return this.accountBalanceField;
            }
            set {
                this.accountBalanceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long accountId {
            get {
                return this.accountIdField;
            }
            set {
                this.accountIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string emailId {
            get {
                return this.emailIdField;
            }
            set {
                this.emailIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string firstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long jurisdictionId {
            get {
                return this.jurisdictionIdField;
            }
            set {
                this.jurisdictionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool jurisdictionIdSpecified {
            get {
                return this.jurisdictionIdFieldSpecified;
            }
            set {
                this.jurisdictionIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int tierCode {
            get {
                return this.tierCodeField;
            }
            set {
                this.tierCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string usernamePasswordToken {
            get {
                return this.usernamePasswordTokenField;
            }
            set {
                this.usernamePasswordTokenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.thirdparty.api.neo.tabcorp.com.au/")]
    public partial class thirdPartyCustomerAuthenticateRequest {
        
        private long accountIdField;
        
        private string accountPasswordField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long accountId {
            get {
                return this.accountIdField;
            }
            set {
                this.accountIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string accountPassword {
            get {
                return this.accountPasswordField;
            }
            set {
                this.accountPasswordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void authenticateAccountCompletedEventHandler(object sender, authenticateAccountCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class authenticateAccountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal authenticateAccountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public thirdPartyCustomerAuthenticateResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((thirdPartyCustomerAuthenticateResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591