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
    private readonly Func<Task> _execute;
    private readonly Func<bool>? _canExecute;

    public BasicCommand(Func<Task> execute, Func<bool>? canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object? parameter)
    {
        _execute();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}

