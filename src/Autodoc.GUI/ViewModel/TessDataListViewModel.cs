using Andozias.Autodoc.Service.Managers;
using Autodoc.GUI.Extensions;
using Autodoc.GUI.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Autodoc.GUI.ViewModel;

public class TessDataListViewModel : ViewModelBase
{
    private readonly IRemoteManager _remoteManager;
    private readonly ILocalFileManager _localFileManager;
    
    public string? LangCode { get; set; }

    public List<TessDataSource> TessDataSources { get; set; }
    public TessDataSource? SelectedSource { get; set; }
    public BasicCommand DownloadCommand { get; set; }
    public ObservableCollection<TessSavedFile> Downloads { get; set; }
    
    private TessSavedFile? _currentDownload;
    public TessSavedFile? CurrentDownload
    {
        get => _currentDownload;
        set
        {
            _currentDownload = value;
            Notify();
        }
    }

    private double _currentProgress;
    public double CurrentProgress
    {
        get => _currentProgress;
        set
        {
            _currentProgress = value;
            Notify();
            Notify(nameof(ShowAsIndeterminade));
        }
    }

    public Visibility DownloadVisibility => CurrentDownload is not null ? Visibility.Visible : Visibility.Collapsed;
    public bool ShowAsIndeterminade => CurrentProgress == 0;

    public TessDataListViewModel(IRemoteManager remoteManager, ILocalFileManager localFileManager)
    {
        _remoteManager = remoteManager;
        _localFileManager = localFileManager;

        TessDataSources = new List<TessDataSource>
        {
            new TessDataSource("Regular", "https://github.com/tesseract-ocr/tessdata/raw/main"),
            new TessDataSource("Best", ""),
            new TessDataSource("Fast", "")
        };

        _localFileManager.Initialize();

        var tessSavedFiles = _localFileManager.GetFiles("./tessdata", "*.traineddata")
            .Select(f => f.ToTessSavedFile());

        Downloads = new ObservableCollection<TessSavedFile>(tessSavedFiles);

        DownloadCommand = new BasicCommand(Download, () => DownloadVisibility == Visibility.Collapsed);
    }

    public async Task Download()
    {
        if (string.IsNullOrEmpty(LangCode) || SelectedSource is null)
            return;

        CurrentDownload = new TessSavedFile(LangCode, null);
        Notify(nameof(DownloadVisibility));
        DownloadCommand.RaiseCanExecuteChanged();
                
        await _remoteManager.StartDownload($"{SelectedSource.Path}/{LangCode}.traineddata", 
            $"./tessdata/{LangCode}.traineddata", p => CurrentProgress = p * 100, DownloadFinished);
    }

    private void DownloadFinished(DownloadResult result)
    {
        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            CleanCurrentDownload();
            return;
        }

        Application.Current.Dispatcher.Invoke(() =>
        {            
            Downloads.Add(CurrentDownload! with { FileSize = result.FileSize});            
        });

        CleanCurrentDownload();
    }

    private void CleanCurrentDownload()
    {
        CurrentDownload = null;
        CurrentProgress = 0;
        Notify(nameof(DownloadVisibility));

        Application.Current.Dispatcher.Invoke(DownloadCommand.RaiseCanExecuteChanged);
    }
   
}

