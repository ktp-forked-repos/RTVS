﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.R.Host.Monitor {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.R.Host.Monitor.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Start R Remote Services.
        /// </summary>
        public static string Btn_StartBroker {
            get {
                return ResourceManager.GetString("Btn_StartBroker", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stop R Remote Services.
        /// </summary>
        public static string Btn_StopBroker {
            get {
                return ResourceManager.GetString("Btn_StopBroker", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Auto restart failed , exception: {0}.
        /// </summary>
        public static string Error_AutoRestartFailed {
            get {
                return ResourceManager.GetString("Error_AutoRestartFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to R Remote Services startup failed with exception: {0}.
        /// </summary>
        public static string Error_StartUpFailed {
            get {
                return ResourceManager.GetString("Error_StartUpFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to R Remote Services could not be stopped. Exception: {0}.
        /// </summary>
        public static string Error_StopBrokerFailed {
            get {
                return ResourceManager.GetString("Error_StopBrokerFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to R Remote Services is already running, process id {0}..
        /// </summary>
        public static string Info_BrokerAlreadyRunning {
            get {
                return ResourceManager.GetString("Info_BrokerAlreadyRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New R Remote Services instance started, process id {0}..
        /// </summary>
        public static string Info_NewBrokerInstanceStarted {
            get {
                return ResourceManager.GetString("Info_NewBrokerInstanceStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to R Remote Services process started, process Id: {0}.
        /// </summary>
        public static string Status_BrokerStarted {
            get {
                return ResourceManager.GetString("Status_BrokerStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to R Remote Services process stopped..
        /// </summary>
        public static string Status_BrokerStopped {
            get {
                return ResourceManager.GetString("Status_BrokerStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Starts and monitors R Remote Services process..
        /// </summary>
        public static string Text_StartBroker {
            get {
                return ResourceManager.GetString("Text_StartBroker", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stops the monitored R Remote Services process..
        /// </summary>
        public static string Text_StopBroker {
            get {
                return ResourceManager.GetString("Text_StopBroker", resourceCulture);
            }
        }
    }
}
