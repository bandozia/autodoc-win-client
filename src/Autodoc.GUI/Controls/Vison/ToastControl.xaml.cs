using System.Windows.Controls;
using System.Windows.Input;

namespace Autodoc.GUI.Controls.Vison;
public partial class ToastControl : UserControl
{
    public ToastControl()
    {
        InitializeComponent();
    }

    private void CloseBtMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (Parent is Panel parent)
            parent.Children.Remove(this);
    }
}

