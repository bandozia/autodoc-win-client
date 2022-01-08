namespace Andozias.Autodoc.Service.Managers;

public class RemoteManager : IRemoteManager
{
    private readonly HttpClient _httpClient;
    private readonly ILocalFileManager _localFileManager;

    public RemoteManager(HttpClient httpClient, ILocalFileManager localFileManager)
    {
        _httpClient = httpClient;
        _localFileManager = localFileManager;
    }

    public async Task StartDownload(string source, string destination, Action<double>? progress = null, Action<DownloadResult>? complete = null)
    {        
        try
        {
            using var response = await _httpClient.GetAsync(source, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var content = await response.Content.ReadAsStreamAsync();

            var len = response.Content.Headers.ContentLength;

            if (len is null)
                progress = null;

            var fileSize = await _localFileManager.CopyTo(content, destination, p => progress?.Invoke(p / (double)len!));

            complete?.Invoke(new DownloadResult(true, fileSize, destination, null));
        }
        catch (Exception ex)
        {
            complete?.Invoke(new DownloadResult(false, 0, null, ex.Message));
        }
    }
}

