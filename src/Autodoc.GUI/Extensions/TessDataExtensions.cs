using Autodoc.GUI.Model;
using System.IO;

namespace Autodoc.GUI.Extensions;

public static class TessDataExtensions
{
    public static TessSavedFile ToTessSavedFile(this FileInfo fileInfo) => 
        new(fileInfo.Name[..3], fileInfo.Length, fileInfo.FullName);

}

