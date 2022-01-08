using Andozias.Autodoc.Service.Managers;
using Autodoc.GUI.ViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Autodoc.GUI.Controls;

public partial class TessDataListControl : UserControl
{   
    public TessDataListControl(TessDataListViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

