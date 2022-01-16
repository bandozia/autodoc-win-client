using Andozias.Autodoc.Data;
using System.Diagnostics;
using Tesseract;

namespace Andozias.Autodoc.Service.Ocr;

public class TesseractService : ITesseractService, IDisposable
{
    private TesseractEngine? _currentEngine;
    
    public void InitializeEngine(EngineOptions engineOptions)
    {
        _currentEngine?.Dispose();
        _currentEngine = new TesseractEngine(engineOptions.DataPath, engineOptions.Lang, EngineMode.LstmOnly);        
    }

    public PageResult DecodePage(string filePath)
    {
        if (_currentEngine == null)
            throw new InvalidOperationException("Tesseract engine is not initialized");
                
        using var image = Pix.LoadFromFile(filePath);
        using var page = _currentEngine.Process(image);

        var score = page.GetMeanConfidence();
        var text = page.GetText();
        
        using var it = page.GetIterator();
        it.Begin();

        var words = new List<Word>();

        while (it.Next(PageIteratorLevel.Word))
        {
            // TODO: get word rect
            words.Add(new Word(
                it.GetConfidence(PageIteratorLevel.Word), 
                it.GetText(PageIteratorLevel.Word)));
        }
                
        return new PageResult(score, text, words);
    }

    public void Dispose()
    {
        _currentEngine?.Dispose();
    }

}

