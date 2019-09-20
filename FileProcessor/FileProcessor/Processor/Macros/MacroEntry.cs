using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FileProcessor.Processor.Macros
{
    public class ObjectLikeMacro
    {
        public string Name { get; }
        protected readonly List<MacroToken> replacementTokensList;

        public List<MacroToken> ReplacementTokensList => replacementTokensList;

        public ObjectLikeMacro(string name)
        {
            Name = name;
            replacementTokensList = new List<MacroToken>();
        }

        public ObjectLikeMacro(string name, List<MacroToken> replacementTokensList)
        {
            Name = name;
            this.replacementTokensList = replacementTokensList;
        }

        public void AppendMacroToken(MacroToken macroTroken)
        {
            if (replacementTokensList.Count > 0 && macroTroken is TextToken && replacementTokensList.Last() is TextToken)
            {
                replacementTokensList.Last().Text += macroTroken.Text;
            }
            else
            {
                replacementTokensList.Add(macroTroken);
            }
        }
    }

    public class FunctionLikeMacro : ObjectLikeMacro
    {
        private readonly string[] parameters;

        public string[] Parameters => parameters;

        public FunctionLikeMacro(string name, string[] parameters) : base(name)
        {
            this.parameters = parameters;
        }

        public FunctionLikeMacro(string name, List<MacroToken> replacementTokensList, string[] parameters) : base(name, replacementTokensList)
        {
            this.parameters = parameters;
        }
    }

    public enum MacroTokenEnum
    {
        Text,
        Identifier,
        Function
    }

    
}
