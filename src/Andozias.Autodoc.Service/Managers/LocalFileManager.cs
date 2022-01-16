namespace Andozias.Autodoc.Service.Managers;

public class LocalFileManager : ILocalFileManager
{
    private static readonly string[] AllowedExtensions = new string[]
    {
        ".jpg", ".png", ".jpeg", ".bmp"
    };

    public void Initialize()
    {
        if (!Directory.Exists("./tessdata"))
            Directory.CreateDirectory("tessdata");
    }

    public IEnumerable<FileInfo> GetFiles(string path, string ext, bool recursive = false)
    {        
        foreach (var f in Directory.GetFiles(path, ext, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
        {   
            yield return new FileInfo(f);
        }
    }

    public IEnumerable<FileInfo> GetValidImgFileList(string[] filePaths)
    {
        foreach (var path in filePaths)
        {
            var fi = new FileInfo(path);
            if (fi.Exists && AllowedExtensions.Contains(fi.Extension))
                yield return fi;
        }
    }

    public async Task<long> CopyTo(Stream source, string dst, Action<long>? progress)
    {
        var buffer = new byte[81920];
        int read;
        long totalRead = 0;

        using var fs = new FileStream(dst, FileMode.OpenOrCreate);

        if (!fs.CanWrite)
            throw new InvalidOperationException("its not possible to write the file on destination");

        while ((read = await source.ReadAsync(buffer, CancellationToken.None)) > 0)
        {
            await fs.WriteAsync(buffer.AsMemory(0, read));
            totalRead += read;
            progress?.Invoke(totalRead);
        }

        return totalRead;
    }
}
