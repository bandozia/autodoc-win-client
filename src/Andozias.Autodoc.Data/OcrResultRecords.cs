namespace Andozias.Autodoc.Data;

public record OcrResult(FileInfo FileInfo, PageResult PageResult, long OperationTime);

public record PageResult(float Score, string Text, IEnumerable<Word> Words);

public record Word(float Score, string Text);