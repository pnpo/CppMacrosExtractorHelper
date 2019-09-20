using MacrosAndIncludesInspector.Inspectors.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MacrosAndIncludesInspector.Inspectors
{
    public class MakefileInspector : IBuildFileInspector
    {
        public List<string> ExtractIncludes(string filePath)
        {
            List<string> includes = new List<string>();
            string fileContent = File.ReadAllText(filePath);
            MatchCollection foundIncludes = CommandOptionsRegEx.CommandLineInclude.Matches(File.ReadAllText(filePath));

            foreach (Match m in foundIncludes) {
                string dirname = m.Groups["directory"].Value.Trim('"', ' ');
                dirname = expandMakefileMacros(dirname, fileContent);
                includes.Add(dirname);
            }

            return includes;
        }

        public Dictionary<string, string> ExtractMacros(string filePath)
        {
            Dictionary<string,string> macros = new Dictionary<string, string>();
            string fileContent = File.ReadAllText(filePath);
            MatchCollection foundMacros = CommandOptionsRegEx.CommandLineMacro.Matches(File.ReadAllText(filePath));
            foreach (Match m in foundMacros)
            {
                string macroName = m.Groups["macro"].Value;
                string macroValue = m.Groups["value"].Value.TrimStart('=').Trim();
                
                macroValue = expandMakefileMacros(macroValue, fileContent);
                if (!string.IsNullOrEmpty(macroValue))
                {
                    macros.Add(macroName, macroValue);
                }
                else
                {
                    macros.Add(macroName, "1");
                }
                
            }

            return macros;
        }



        private string expandMakefileMacros(string searchString, string fileContent) {
            Regex macrosInString = new Regex(@"\$\((?<MACRO>[^)]+)\)");

            Match macrosFound = macrosInString.Match(searchString);
            while (macrosFound.Success) {
                string macroName = macrosFound.Groups["MACRO"].Value;
                Regex macroDefRegex = new Regex($"{macroName}\\s*=\\s*(?<MACRODEF>[^\r\n]+)");
                Match macroDefMatch = macroDefRegex.Match(fileContent);
                if (macroDefMatch != null) {
                    string expandedMacro = expandMakefileMacros(macroDefMatch.Groups["MACRODEF"].Value, fileContent);
                    searchString = searchString.Replace(macrosFound.Value, expandedMacro);
                }
                macrosFound = macrosFound.NextMatch();
            }

            return searchString;
        }

    }
}
