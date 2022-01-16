using Andozias.Autodoc.Service.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Autodoc.GUI.ViewModel;

public class AddFilesViewModel : ViewModelBase
{
    private readonly ILocalFileManager _localFileManager;

    public ObservableCollection<FileInfo> Files { get; set; }

    public ICommand RemoveCommand { get; set; }

    public AddFilesViewModel(ILocalFileManager localFileManager)
    {
        _localFileManager = localFileManager;

        Files = new ObservableCollection<FileInfo>();
        RemoveCommand = new BasicCommand(RemoveFile);
    }

    public void AddFiles(string[] files)
    {
        foreach (var f in _localFileManager.GetValidImgFileList(files))
            Files.Add(f);
    }

    public void RemoveFile(object? file)
    {
        if (file != null && file is FileInfo f)
            Files.Remove(f);
    }
}

