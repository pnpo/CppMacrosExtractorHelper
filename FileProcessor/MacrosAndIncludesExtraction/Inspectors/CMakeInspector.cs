using MacrosAndIncludesInspector.Inspectors.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MacrosAndIncludesInspector.Inspectors
{
    class CMakeInspector : IBuildFileInspector
    {

        public CMakeInspector() {
        }

        public List<string> ExtractIncludes(string filePath)
        {
            List<string> includes = new List<string>();
            MatchCollection foundIncludes = CommandOptionsRegEx.CMakeIncludes.Matches(File.ReadAllText(filePath));

            foreach (Match m in foundIncludes) {
                string dirname = m.Groups["dir"].Value.TrimEnd(' ', ',').Trim('"');
                if (dirname.StartsWith("${PROJECT_SOURCE_DIR}"))
                {
                    dirname = dirname.Substring("${PROJECT_SOURCE_DIR}".Length);
                }
                else if (dirname.StartsWith("${PROJECT_BINARY_DIR}")) {
                    dirname = dirname.Substring("${PROJECT_BINARY_DIR}".Length);
                }
                else if (dirname.StartsWith("~"))
                {
                    dirname = dirname.Substring(1);
                }

                includes.Add(dirname==""? "." : dirname);
                
            }

            return includes;

        }

        public Dictionary<string, string> ExtractMacros(string filePath)
        {
            Dictionary<string,string> macros = new Dictionary<string, string>();
            string fileContent = File.ReadAllText(filePath);
            MatchCollection foundMacrosOld = CommandOptionsRegEx.CMakeMacrosOld.Matches(fileContent);
            MatchCollection foundMacrosNew = CommandOptionsRegEx.CMakeMacrosNew.Matches(fileContent);

            foreach (Match m in foundMacrosOld)
            {
                extractMacroFromMatch(m, ref macros);
            }

            foreach (Match m in foundMacrosNew)
            {
                extractMacroFromMatch(m, ref macros);
            }

            return macros;
        }


        private void extractMacroFromMatch(Match m, ref Dictionary<string,string> macros)
        {
            for (int i = 0; i < m.Groups["macro"].Captures.Count; i++) {
                string macroName = m.Groups["macro"].Captures[i].Value;
                string macroValue = m.Groups["value"].Captures[i].Value.TrimStart('=').Trim();

                if (!string.IsNullOrEmpty(macroValue))
                {
                    macros.Add(macroName, macroValue);
                }
                else
                {
                    macros.Add(macroName, "1");
                }
            }
        }
    }


    

}
