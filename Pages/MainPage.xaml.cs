using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Codea
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string CursorPositionHint
        {
            get { return _cursorRow + ":" + _cursorColumn; }
        }

        private uint _cursorRow = 1;
        private uint _cursorColumn = 1;

        private readonly DispatcherTimer _slowUpdateTimer;

        public MainPage()
        {
            InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.LayoutMetricsChanged += MainPage_LayoutMetricsChanged;

            Window.Current.SetTitleBar(AppTitleBar);

            _slowUpdateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _slowUpdateTimer.Tick += SlowUpdateTimer_Tick;
            _slowUpdateTimer.Start();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CodeEditor.Focus(FocusState.Programmatic);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void SlowUpdateTimer_Tick(object sender, object e)
        {
            var position = await CodeEditor.GetPositionAsync();
            _cursorRow = position.LineNumber;
            _cursorColumn = position.Column;

            OnPropertyChanged("CursorPositionHint");
        }

        private void MainPage_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            AppTitleBar.Height = sender.Height;
        }
    }
}
