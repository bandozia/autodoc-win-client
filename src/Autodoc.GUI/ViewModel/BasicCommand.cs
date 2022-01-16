using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Autodoc.GUI.ViewModel;

public class BasicCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly Func<bool>? _canExecute;
    
    private readonly Func<Task>? _execute;
    private readonly Action<object?>? _executeSync;
    
    public BasicCommand(Func<Task> execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public BasicCommand(Action<object?> execute, Func<bool>? canExecute = null)
    {
        _executeSync = execute;
        _canExecute = canExecute;
    }       

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object? parameter)
    {
        if (_execute != null)
            _execute();
        else
            _executeSync!(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}

