using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autodoc.GUI.Model;

public record TessDataSource(string Name, string Path);

public record TessSavedFile(string LangCode, long? FileSize, string? FilePath = null);
