namespace Andozias.Autodoc.Service.Managers;

public interface ILocalFileManager
{
    void Initialize();

    IEnumerable<FileInfo> GetFiles(string path, string ext, bool recursive = false);

    Task<long> CopyTo(Stream source, string dst, Action<long> progress);
}

