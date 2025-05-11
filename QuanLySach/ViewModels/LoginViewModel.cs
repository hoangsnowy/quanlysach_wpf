using System;
using System.ComponentModel;
using System.Windows;
using QuanLySach.Business.Interfaces;
using QuanLySach.Commands;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using QuanLySach.Common.Helpers;

namespace QuanLySach.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticationLogic _authenticationLogic;
        private readonly IAccountLogic _accountLogic;
        private readonly IServiceProvider _provider;

        public delegate void CloseCommand();
        public event CloseCommand OnClose;

        public LoginViewModel(IAuthenticationLogic authenticationLogic, IAccountLogic accountLogic, IServiceProvider provider)
        {
            _authenticationLogic = authenticationLogic;
            _accountLogic = accountLogic;
            _provider = provider;
            LoginCommand = new ReplayCommand(LoginCommand_Execute, LoginCommand_CanExecute);
        }

        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public bool RememberedMe { get; set; }
        public bool IsRememberedPassword { get; set; }
        public ICommand LoginCommand { get; set; }

        public async void LoginCommand_Execute()
        {
            try
            {
                bool success = false;
                if (IsRememberedPassword)
                {
                    success = await _authenticationLogic.SignInWithHashedPassword(EmailAddress, Password);
                }
                else
                {
                    success = await _authenticationLogic.SignIn(EmailAddress, Password);
                }

                if (success)
                {
                    string hashedPassword = IsRememberedPassword ? Password : SecureHelper.ComputeSha256Hash(Password);
                    _accountLogic.StoreAccount(EmailAddress, hashedPassword, RememberedMe);
                    var mainWindow = _provider.GetRequiredService<MainWindow>();
                    mainWindow.Show();
                    OnClose.Invoke();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Đăng nhập thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool LoginCommand_CanExecute()
        {
            return !string.IsNullOrEmpty(EmailAddress);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
