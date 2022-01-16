using Autodoc.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autodoc.GUI.Controls;

public partial class AddFilesControl : UserControl
{
    private readonly AddFilesViewModel _viewModel;

    public AddFilesControl(AddFilesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    private void FileDroped(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            return;

        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        
        if (files?.Any() ?? false)
            _viewModel.AddFiles(files);

        dropFeedback.Visibility = Visibility.Collapsed;
    }

    private void FileDragOverr(object sender, DragEventArgs e) => dropFeedback.Visibility = Visibility.Visible;

    private void FileDragLeave(object sender, DragEventArgs e) => dropFeedback.Visibility= Visibility.Collapsed;
}

