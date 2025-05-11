using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

namespace QuanLySach.Commands
{
    public class OpenSettingCommand: ICommand
    {
        private readonly IServiceProvider _provider;
        public OpenSettingCommand(IServiceProvider provider)
        {
            _provider = provider;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var settingWindow = _provider.GetRequiredService<SettingWindow>();
            settingWindow.ShowDialog();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
