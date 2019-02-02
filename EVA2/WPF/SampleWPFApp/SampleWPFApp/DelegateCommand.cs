using System;
using System.Windows.Input;

/// <summary>
/// Roberto mintakódjából: az ICommand interface-t valósítja meg;
/// lehetővé teszi egy parancs összekötését egy UI elemmel
/// </summary>
namespace SampleWPFApp
{
    class DelegateCommand : ICommand
    {
        private readonly Action<Object> myExecute;
        private readonly Func<Object, Boolean> myCanExecute;

        public DelegateCommand(Action<Object> execute) : this(null, execute) { }

        public DelegateCommand(Func<Object, Boolean> canExecute, Action<Object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            myExecute = execute;
            myCanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return myCanExecute == null ? true : myCanExecute(parameter);
        }

        public void Execute(Object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            myExecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}

