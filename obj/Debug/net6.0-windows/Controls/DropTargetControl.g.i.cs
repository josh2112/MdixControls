﻿#pragma checksum "..\..\..\..\Controls\DropTargetControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E0E32D20C7D7E728058C050A26B1EF8F9A2EB423"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Com.Josh2112.MdixControls.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Com.Josh2112.MdixControls.Controls {
    
    
    /// <summary>
    /// DropTargetControl
    /// </summary>
    public partial class DropTargetControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MdixControls;component/controls/droptargetcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Controls\DropTargetControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Controls\DropTargetControl.xaml"
            ((Com.Josh2112.MdixControls.Controls.DropTargetControl)(target)).DragEnter += new System.Windows.DragEventHandler(this.Control_DragOver);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\Controls\DropTargetControl.xaml"
            ((Com.Josh2112.MdixControls.Controls.DropTargetControl)(target)).DragOver += new System.Windows.DragEventHandler(this.Control_DragOver);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\Controls\DropTargetControl.xaml"
            ((Com.Josh2112.MdixControls.Controls.DropTargetControl)(target)).DragLeave += new System.Windows.DragEventHandler(this.Control_DragLeave);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\Controls\DropTargetControl.xaml"
            ((Com.Josh2112.MdixControls.Controls.DropTargetControl)(target)).Drop += new System.Windows.DragEventHandler(this.Control_Drop);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

