﻿#pragma checksum "..\..\PhotoPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E919D08D8732CE22197BEAB01F12100F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace PhotoGallery {
    
    
    /// <summary>
    /// PhotoPage
    /// </summary>
    public partial class PhotoPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menu1;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem renameMenu;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem fixMenu;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem printMenu;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem editMenu;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel imageView;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel fixBar;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button previousButton;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button slideshowButton;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\PhotoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PhotoGallery;component/photopage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PhotoPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.menu1 = ((System.Windows.Controls.Menu)(target));
            return;
            case 2:
            this.renameMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 38 "..\..\PhotoPage.xaml"
            this.renameMenu.Click += new System.Windows.RoutedEventHandler(this.renameMenu_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 40 "..\..\PhotoPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.exitMenu_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.fixMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 42 "..\..\PhotoPage.xaml"
            this.fixMenu.Click += new System.Windows.RoutedEventHandler(this.fixMenu_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.printMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 43 "..\..\PhotoPage.xaml"
            this.printMenu.Click += new System.Windows.RoutedEventHandler(this.printMenu_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.editMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 44 "..\..\PhotoPage.xaml"
            this.editMenu.Click += new System.Windows.RoutedEventHandler(this.editMenu_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.imageView = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 8:
            this.fixBar = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            
            #line 49 "..\..\PhotoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.fix_RotateClockwise_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 50 "..\..\PhotoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.fix_RotateCounterclockwise_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 51 "..\..\PhotoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.fix_Save_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 13:
            this.previousButton = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\PhotoPage.xaml"
            this.previousButton.Click += new System.Windows.RoutedEventHandler(this.previousButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.slideshowButton = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\PhotoPage.xaml"
            this.slideshowButton.Click += new System.Windows.RoutedEventHandler(this.slideshowButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.nextButton = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\PhotoPage.xaml"
            this.nextButton.Click += new System.Windows.RoutedEventHandler(this.nextButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

