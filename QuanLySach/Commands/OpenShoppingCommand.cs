using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;
using QuanLySach.ViewModels;

namespace QuanLySach.Commands
{
    public class OpenShoppingCommand : ICommand
    {
        private readonly IServiceProvider _provider;
        public OpenShoppingCommand(IServiceProvider provider)
        {
            _provider = provider;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var shoppingCartWindow = _provider.GetRequiredService<ShoppingCartWindow>();
            shoppingCartWindow.Mode = WindowMode.Add;
            shoppingCartWindow.ShowDialog();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
