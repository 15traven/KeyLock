using KeyLock.Services;
using KeyLock.Contracts;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace KeyLock
{
        
    public sealed partial class MainWindow : Window
    {
        private readonly IKeyboardBlocker keyboardBlocker = new KeyboardBlocker();

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

            this.Closed += (sender, args) =>
            {
                keyboardBlocker.UnblockKeyboard();
            };
        }

        private void blockInput(object sender, RoutedEventArgs e)
        {
            keyboardBlocker.BlockKeyboard();
            toggle.Content = "Unlock";
        }

        private void unblockInput(object sender, RoutedEventArgs e)
        {
            keyboardBlocker.UnblockKeyboard();
            toggle.Content = "Lock";
        }
    }
}
