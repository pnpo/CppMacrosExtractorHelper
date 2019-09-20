using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using FileProcessor.Processor.Macros;
using FileProcessor.Processor.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileProcessor.Processor.Visitor
{

    public partial class CPreProcessorVisitor : MacroParserBaseVisitor<MacroToken>
    {
        protected readonly StringBuilder preprocessedCode;
        protected StringBuilder helperBuilder;
        protected Dictionary<string, string> macros;
        protected StringBuilder expression;
        protected readonly Stack<bool> siblingConditions;
        protected readonly Stack<bool> parentConditions;
        protected readonly bool unknownMacrosAsTrue;
        
        protected bool isCFileExtension;
        private readonly List<string> reservedWords = new List<string>
        {
            "alignas", "alignof", "and", "and_eq", "asm",
            "bitand", "bitor", "bool",
            "catch", "char16_t", "char32_t", "class", "compl", "concept", "constexpr", "const_cast",
            "decltype", "delete", "dynamic_cast",
            "explicit", "export",
            "false", "friend",
            "mutable",
            "namespace", "new", "noexcept", "not", "not_eq", "nullptr",
            "operator", "or", "or_eq",
            "private", "protected", "public",
            "reinterpret_cast", "requires",
            "static_assert", "static_cast",
            "template", "this", "thread_local", "throw", "true", "try", "typeid", "typename",
            "using",
            "virtual",
            "wchar_t",
            "xor", "xor_eq"
        };

        public CPreProcessorVisitor(string fileContent)
        {
            preprocessedCode = new StringBuilder();
            macros = new Dictionary<string, string>();
            helperBuilder = new StringBuilder();
            siblingConditions = new Stack<bool>();
            siblingConditions.Push(true);
            parentConditions = new Stack<bool>();
            parentConditions.Push(true);
            unknownMacrosAsTrue = false;
        }

        private static bool IsCFileExtension()
        {
            return false;
        }

        public CPreProcessorVisitor()
        {
            preprocessedCode = new StringBuilder();
            helperBuilder = new StringBuilder();
            macros = new Dictionary<string, string>();
            siblingConditions = new Stack<bool>();
            siblingConditions.Push(true);
            parentConditions = new Stack<bool>();
            parentConditions.Push(true);
            unknownMacrosAsTrue = true;
        }

        public string PreProcessedCode => preprocessedCode.ToString();

        public void GenerateJsonFile(string fileName)
        {
            JObject rootNode = new JObject();
            JObject filename = new JObject();
            JObject macrosList = new JObject();
            foreach (var m in macros)
            {
                macrosList.Add(new JProperty(m.Key,m.Value));
            }
            //filename.
            filename.Add(new JProperty(fileName, macrosList));
            rootNode.Add(new JProperty("libraries", filename));

            string json = JsonConvert.SerializeObject(rootNode, Formatting.Indented);

            //write string to file
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            System.IO.File.WriteAllText(desktopPath + @"\CxCPPDefaultMacros.json", json);
        }

        private string ReplaceReservedWordWithTemplate(string identifier, bool useRegex = false)
        {
            if (isCFileExtension)
            {
                if (useRegex)
                {
                    var replacedIdentifierText = Regex.Replace(identifier, @"(?<Word>\w+)",
                        m => reservedWords.Contains(m.Groups["Word"].Value)
                            ? ReservedWordsTemplate(m.Groups["Word"].Value)
                            : m.Groups["Word"].Value);
                    return replacedIdentifierText;
                }

                if (reservedWords.Contains(identifier))
                {
                    return ReservedWordsTemplate(identifier);
                }
            }
            return identifier;
        }

        private string ReservedWordsTemplate(string word)
        {
            StringBuilder sb = new StringBuilder(word) { [word.Length - 2] = '@' };
            return sb.ToString();
        }

        private void HandleMacroListTokens(MacroToken token, ref MacroListToken collection)
        {
            if (token is MacroListToken)
            {
                collection.AddRangeTokens(((MacroListToken)token).Tokens);
            }
            else
            {
                collection.AddToken(token);
            }
        }

        protected bool AppendText()
        {
            return parentConditions.Peek() && siblingConditions.Peek();
        }

        private string GetMacroExpansionText(ObjectLikeMacro macro, string filename)
        {
            return "";
        }

        private string GetMacroExpansionText(FunctionLikeMacro macro, List<string> arguments)
        {
            return "";
        }

        private string AddSpacesToMantainLinePragma(ObjectLikeMacro macro, string expandedMacro, List<string> arguments = null)
        {
            if (expandedMacro.Length < macro.Name.Length) //add spaces when possible to mantain line pragma
            {
                int offset = macro.Name.Length - expandedMacro.Length;
                if (macro is FunctionLikeMacro && arguments != null)
                {
                    offset += arguments.Aggregate(0, (acc, arg) => acc + arg.Length);   //add parameters length
                    offset += 2 + (arguments.Count > 0 ? arguments.Count - 1 : 0);      //add parentheses and commas
                }
                expandedMacro = expandedMacro + new String(' ', offset);
            }
            return expandedMacro;
        }

        protected string HandleIncludeFileLineBreaks(string includedFileName)
        {
            int occ;
            if ((occ = includedFileName.Count(f => f == '\r')) > 0)
            {
                includedFileName = includedFileName.Replace("\\\r\n", string.Empty);
                while (occ-- > 0)
                {
                    preprocessedCode.Append("\r\n");
                }
            }
            else if ((occ = includedFileName.Count(f => f == '\n')) > 0)
            {
                includedFileName = includedFileName.Replace("\\\n", string.Empty);
                preprocessedCode.Append('\n', occ);
            }
            return includedFileName;
        }

        protected virtual string HandleFileInclusion(string includedFileName)
        {
            return "";
        }

        private string ReplacePattern(string source, string pattern, string replace, out int count)
        {
            var countAux = 0;
            string replaced = Regex.Replace(source, pattern,
                match =>
                {
                    countAux++;
                    return match.Result(replace);
                });
            count = countAux;
            return replaced;
        }

        private string ReplaceWithEmpty(int startLine, int stopLine, string newLine)
        {
            int offset = stopLine - startLine;
            string emptyLines = string.Empty;
            while (offset-- > 0)
            {
                emptyLines += newLine;
            }
            return emptyLines;
        }
    }
}
