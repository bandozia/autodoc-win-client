using Autodoc.GUI.Controls;
using Autodoc.GUI.ViewModel;
using System.Windows.Controls;

namespace Autodoc.GUI.View;

public partial class DecodeView : UserControl
{        
    public DecodeView(DecodeViewModel viewModel, TessDataListControl tessDataListControl, 
        AddFilesControl addFilesControl)
    {
        InitializeComponent();

        DataContext = viewModel;
                
        tessDataContent.Content = tessDataListControl;
        fileListContent.Content = addFilesControl;
    }
}

