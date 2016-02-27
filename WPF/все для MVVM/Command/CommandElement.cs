using System;
using System.Windows.Input;

namespace RunProcWpf.Command
{
    class CommandElement : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        private Action<object> _exec;
        private Func<object, bool> _canExec;
        public CommandElement(Action<object> exec, Func<object, bool> canExec = null)
        {
            _exec = exec;
            _canExec = canExec;
        }
        public bool CanExecute(object parameter)
        {
            return _canExec == null ? true : _canExec(parameter);
        }
        public void Execute(object parameter)
        {
            _exec(parameter);
        }
    }

}
