namespace Andozias.Autodoc.Service.Managers;

public record DownloadResult(bool Success, long FileSize, string? FilePath, string? Message);

public interface IRemoteManager
{   
    Task StartDownload(string source, string destination, Action<double>? progress = null, Action<DownloadResult>? complete = null);
}

