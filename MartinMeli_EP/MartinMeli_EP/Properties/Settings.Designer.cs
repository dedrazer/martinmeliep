﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MartinMeli_EP.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("DefaultEndpointsProtocol=https;AccountName=melistorageaccount;AccountKey=PhhJ2ViO" +
            "fYM30z01O16uR2u2/yJ1jvZSEM3FiZyvfneVmnx43EfLOjb0sKgBIy57UsEPG5I0MhAzEWAsYHuWGg==" +
            ";EndpointSuffix=core.windows.net")]
        public string melistorageaccount_AzureStorageConnectionString {
            get {
                return ((string)(this["melistorageaccount_AzureStorageConnectionString"]));
            }
        }
    }
}
