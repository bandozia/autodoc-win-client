using Autodoc.GUI.Controls.Vison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Autodoc.GUI;

internal static class Messages
{
    private static ToastControl? _currentToast;
    private static ToastControl ToastInstance
    {
        get
        {
            if (_currentToast == null)
                _currentToast = new ToastControl
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                };

            return _currentToast;
        }
    }

    public static void Toast(string message)
    {
        var mainGrid = Application.Current.MainWindow.FindName("mainGrid") as Grid;
        ToastInstance.msgBody.Text = message;

        ToastInstance.Width = message.Length * 10;
        ToastInstance.Height = 50;

        mainGrid?.Children.Add(ToastInstance);
    }

    
   
}

