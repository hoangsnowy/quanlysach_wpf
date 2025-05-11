using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;

namespace QuanLySach.Commands
{
    public class OpenOrderListCommand : ICommand
    {
        private readonly IServiceProvider _provider;
        public OpenOrderListCommand(IServiceProvider provider)
        {
            _provider = provider;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var productWindow = _provider.GetRequiredService<OrderListWindow>();
            productWindow.ShowDialog();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
