﻿#pragma checksum "C:\Users\Robel\Documents\GitHub\HelperUWP\HelperUWP\Dialogs\CustomDialog.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "919B864F9189717FC3C440629F5C06DC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelperUWP.Dialogs
{
    partial class CustomDialog : 
        global::Windows.UI.Xaml.Controls.ContentDialog, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.ButtonContent1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 13 "..\..\..\Dialogs\CustomDialog.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ButtonContent1).Click += this.ButtonContent1_Click;
                    #line default
                }
                break;
            case 2:
                {
                    this.ButtonContent2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 14 "..\..\..\Dialogs\CustomDialog.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ButtonContent2).Click += this.ButtonContent2_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
