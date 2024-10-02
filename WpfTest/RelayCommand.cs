using System.Windows.Input;

namespace WpfTest
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeObj;
        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _executeObj = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayCommand()
        {
        }

        public bool CanExecute(object parameter) => (_canExecute == null || _canExecute(parameter)) || (_execute != null);

        public void Execute(object parameter)
        {
            if (_executeObj != null)
                _executeObj(parameter);
            else if (_execute != null)
                _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }



}
