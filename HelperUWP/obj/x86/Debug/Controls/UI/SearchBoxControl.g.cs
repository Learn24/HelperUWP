﻿#pragma checksum "C:\Users\Robel\Documents\GitHub\HelperUWP\HelperUWP\Controls\UI\SearchBoxControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2C4CE69EF802A404A9C216F3CA3962C4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelperUWP.Controls.UI
{
    partial class SearchBoxControl : 
        global::Windows.UI.Xaml.Controls.UserControl, 
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
                    this.WarterMarkText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2:
                {
                    this.SearchBoxName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 21 "..\..\..\..\Controls\UI\SearchBoxControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBoxName).GotFocus += this.Searchbox_GotFocus;
                    #line 22 "..\..\..\..\Controls\UI\SearchBoxControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBoxName).LostFocus += this.Searchbox_LostFocus;
                    #line default
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 25 "..\..\..\..\Controls\UI\SearchBoxControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.SearchButton_Click;
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
