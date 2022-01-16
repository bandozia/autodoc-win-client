using Andozias.Autodoc.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Andozias.Autodoc.Service.Ocr;

public class OcrService : IOcrService
{
    private readonly ITesseractService _tesseractService;
    
    public OcrService(ITesseractService tesseractService)
    {        
        _tesseractService = tesseractService;        
        //TODO: initialization flow
        _tesseractService.InitializeEngine(new EngineOptions("./tessdata", "por"));
    }

    public Task<OcrResult> Decode(FileInfo file)
    {
        return Task.Run(() => {         
            var watch = Stopwatch.StartNew();
            var result = _tesseractService.DecodePage(file.FullName);
            return new OcrResult(file, result, watch.ElapsedMilliseconds);
        });
    }

    public async IAsyncEnumerable<OcrResult> Decode(IEnumerable<FileInfo> fileList, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var f in fileList)
        {
            if (cancellation.IsCancellationRequested)
                break;

            yield return await Decode(f);
        }
    }
   
   
}

