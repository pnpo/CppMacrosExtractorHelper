using System.Collections.Generic;
using System.Linq;

namespace FileProcessor.Processor.Macros
{
    public class FunctionToken : MacroToken
    {
        public MacroListToken Arguments { get; }

        public FunctionToken(string text, MacroListToken arguments) : base(text)
        {
            Arguments = arguments;
        }

        public string GetFunctionText()
        {
            var parametersText = Arguments.Tokens.Aggregate("", (current, type) => current + (type.Text + ","));
            return $"{Text}({parametersText.TrimEnd(',')})";
        }

        public string GetFunctionText(string functionName,List<string> expandedArguments)
        {
            var parametersText = expandedArguments.Aggregate("", (current, param) => current + (param + ","));
            return $"{functionName}({parametersText.TrimEnd(',')})";
        }
    }
}
