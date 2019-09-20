using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MacrosAndIncludesInspector.Inspectors.Contracts
{
    public interface IBuildFileInspector
    {
        Dictionary<string, string> ExtractMacros(string filePath);
        List<string> ExtractIncludes(string filePath);
    }
}
