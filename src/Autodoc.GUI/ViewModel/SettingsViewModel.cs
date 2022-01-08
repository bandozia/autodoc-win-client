namespace Autodoc.GUI.ViewModel;

public class SettingsViewModel : ViewModelBase
{
    private string _fileServiceAddress = "http://localhost";
    private string _regularDataSource = "https://github.com/tesseract-ocr/tessdata/raw/main";
    private string _bestDataSource = "https://github.com/tesseract-ocr/tessdata_best/raw/main";
    private string _fastDataSource = "https://github.com/tesseract-ocr/tessdata_fast/raw/main";

    public string FileServiceAddress 
    { 
        get => _fileServiceAddress;
        set { 
            _fileServiceAddress = value;
            Notify();
        }
    }

    public string RegularDataSource
    {
        get => _regularDataSource;
        set
        {
            _regularDataSource = value;
            Notify();
        }
    }

    public string FastDataSource
    {
        get => _fastDataSource;
        set
        {
            _fastDataSource = value;
            Notify();
        }
    }

    public string BestDataSource
    {
        get => _bestDataSource;
        set
        {
            _bestDataSource = value;
            Notify();
        }
    }
}