﻿#pragma checksum "..\..\..\..\GUI\modifyTypeOfBook.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04A5C9A6833BA4B734EE7570B67C80E33270CF70"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project01_MyBook.GUI;
using Project01_MyBook.Helpers;
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


namespace Project01_MyBook.GUI {
    
    
    /// <summary>
    /// modifyTypeOfBook
    /// </summary>
    public partial class modifyTypeOfBook : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 116 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border border;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minButton;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button maxButton;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reDownButton;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 219 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox idType;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameType;
        
        #line default
        #line hidden
        
        
        #line 306 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView typeOfBook;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project01_MyBook;component/gui/modifytypeofbook.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            ((Project01_MyBook.GUI.modifyTypeOfBook)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.border = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.minButton = ((System.Windows.Controls.Button)(target));
            
            #line 146 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            this.minButton.Click += new System.Windows.RoutedEventHandler(this.minButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.maxButton = ((System.Windows.Controls.Button)(target));
            
            #line 160 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            this.maxButton.Click += new System.Windows.RoutedEventHandler(this.maxButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.reDownButton = ((System.Windows.Controls.Button)(target));
            
            #line 166 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            this.reDownButton.Click += new System.Windows.RoutedEventHandler(this.maxButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 175 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.idType = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.nameType = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 259 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.add_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.typeOfBook = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            
            #line 342 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.modify);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 347 "..\..\..\..\GUI\modifyTypeOfBook.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.delete);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

