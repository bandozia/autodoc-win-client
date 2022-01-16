using Andozias.Autodoc.Data;

namespace Andozias.Autodoc.Service.Ocr;

public record EngineOptions(string DataPath, string Lang);

public interface ITesseractService
{    
    void InitializeEngine(EngineOptions engineOptions);

    PageResult DecodePage(string filePath);
}

