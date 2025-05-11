using System;
using System.Windows.Input;

namespace QuanLySach.Commands
{
    public class ReplayCommand : ICommand
    {
        private Action methodToExecute;
        private Func<bool>? canExecuteEvaluator;

        public ReplayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public ReplayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteEvaluator == null)
                return true;

            bool result = this.canExecuteEvaluator.Invoke();
            return result;
        }

        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke();
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}