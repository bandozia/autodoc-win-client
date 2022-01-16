using Andozias.Autodoc.Data;

namespace Andozias.Autodoc.Service.Ocr;

public interface IOcrService
{
    Task<OcrResult> Decode(FileInfo file);

    IAsyncEnumerable<OcrResult> Decode(IEnumerable<FileInfo> fileList, CancellationToken cancellation = default);
}

