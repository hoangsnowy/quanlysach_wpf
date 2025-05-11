using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLySach.Business.Interfaces;
using QuanLySach.DomainModels;
using QuanLySach.ViewModels;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountLogic _accountLogic;
        private Account? _account;
        private LoginViewModel _viewModel;
        public LoginWindow(LoginViewModel viewModel, IAccountLogic accountLogic)
        {
            _viewModel = viewModel;
            _accountLogic = accountLogic;
            InitializeComponent();
            DataContext = _viewModel;

            _viewModel.OnClose += Close;
            LoadRememberedAccount();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                _viewModel.Password = ((PasswordBox)sender).Password;
                _viewModel.IsRememberedPassword = false;
            }
        }

        private void LoadRememberedAccount()
        {
            _account = _accountLogic.GetCurrentAccount();
            if (_account != null && _account.CurrentUser?.RememberedMe == true)
            {
                _viewModel.EmailAddress = _account.CurrentUser?.Email;
                PasswordBox.Password = _account.CurrentUser?.HashPassword;
                _viewModel.RememberedMe = (bool) _account.CurrentUser?.RememberedMe;
                _viewModel.IsRememberedPassword = true;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
