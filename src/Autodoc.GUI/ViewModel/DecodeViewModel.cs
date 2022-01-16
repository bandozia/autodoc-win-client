using Andozias.Autodoc.Service.Ocr;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Autodoc.GUI.ViewModel;

public class DecodeViewModel : ViewModelBase
{
    private readonly AddFilesViewModel _addFilesViewMode;
    private readonly IOcrService _ocrService;
           
    public ICommand OcrCommand { get; set; }
    
    public record CompleteJob(string Name, string Score, string Time);
    public ObservableCollection<CompleteJob> CompleteJobs { get; set; }

    public DecodeViewModel(AddFilesViewModel addFilesViewMode, IOcrService ocrService)
    {
        _addFilesViewMode = addFilesViewMode;

        OcrCommand = new BasicCommand(DoOcr);
        _ocrService = ocrService;
        CompleteJobs = new ObservableCollection<CompleteJob>();
    }

    private async Task DoOcr()
    {
        var toDecode = _addFilesViewMode.Files.ToImmutableArray();

        await foreach (var (fInfo, page, t) in _ocrService.Decode(toDecode))
        {
            _addFilesViewMode.Files.Remove(fInfo);
            CompleteJobs.Add(new CompleteJob(fInfo.Name, $"{page.Score * 100}%", $"{t} ms"));
        }
    }

    //TODO: create do ocr in parallel
               
}

