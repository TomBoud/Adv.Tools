﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Adv.Tools.RevitAddin.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.6.0.0")]
    internal sealed partial class DataAccess : global::System.Configuration.ApplicationSettingsBase {
        
        private static DataAccess defaultInstance = ((DataAccess)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new DataAccess())));
        
        public static DataAccess Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Server=localhost;Port=3306;user id=Admin;\"password=QAZ56okm;CharSet=utf8;")]
        public string ProdDb {
            get {
                return ((string)(this["ProdDb"]));
            }
        }
    }
}
