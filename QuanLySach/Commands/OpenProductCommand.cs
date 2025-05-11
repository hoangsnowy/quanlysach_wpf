using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

namespace QuanLySach.Commands
{
    public class OpenProductCommand : ICommand
    {
        private readonly IServiceProvider _provider;
        public OpenProductCommand(IServiceProvider provider)
        {
            _provider = provider;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var productWindow = _provider.GetRequiredService<ProductWindow>();
            productWindow.ShowDialog();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
