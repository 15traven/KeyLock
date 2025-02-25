using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace KeyLock
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;

            var AppWindowPresenter = this.AppWindow.Presenter as OverlappedPresenter;
            if (AppWindowPresenter != null)
            {
                AppWindowPresenter.IsResizable = false;
                AppWindowPresenter.IsMaximizable = false;
            }

            AppWindow.Resize(new Windows.Graphics.SizeInt32(300, 300));
        }
    }
}
