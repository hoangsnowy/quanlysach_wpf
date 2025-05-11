using System.Windows;
using QuanLySach.ViewModels;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private SettingViewModel _viewModel;
        public SettingWindow(SettingViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnClose += Close;
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
