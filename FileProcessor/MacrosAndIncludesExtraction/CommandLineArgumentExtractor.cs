using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MacrosAndIncludesInspector
{
    public class CommandLineArgumentExtractor
    {

        

        public CommandLineArgumentExtractor() {
            
        }

        public Dictionary<string, string> extractMacros(string commandLine) {
            var macros = new Dictionary<string, string>();
            MatchCollection foundMacros = CommandOptionsRegEx.CommandLineMacro.Matches(commandLine);

            foreach (Match m in foundMacros) {
                string macroName = m.Groups["macro"].Value;
                string macroValue = m.Groups["value"].Value.TrimStart('=').Trim();
                if (!string.IsNullOrEmpty(macroValue)) {
                    macros.Add(macroName, macroValue);
                }
                else {
                    macros.Add(macroName, "1");
                }
            }

            return macros;

        }


        public List<string> extractIncludes(string commandLine) {
            var directories = new List<string>();
            MatchCollection foundIncludes = CommandOptionsRegEx.CommandLineInclude.Matches(commandLine);

            foreach (Match m in foundIncludes)
            {
                string dirname = m.Groups["directory"].Value.Trim();
                directories.Add(dirname);
            }

            return directories;

        }



    }
}
