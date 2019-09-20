using FileProcessor.Processor.Macros;
using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using FileProcessor.Processor.Parser;

namespace FileProcessor.Processor.Visitor
{
    public partial class CPreProcessorVisitor
    {
        protected readonly Func<string, string> includeTemplate = i => $"void INCLUDEREPLACE(){{CX_INCL(\"{i}\");}}";
        
        public override MacroToken VisitMacroDefine(MacroParser.MacroDefineContext context)
        {
            var a = Visit(context.defineType());
            return null;
        }

        public override MacroToken VisitDefineObjectLikeDirective(MacroParser.DefineObjectLikeDirectiveContext context)
        {
            string macroName = context.IDENTIFIER().GetText();
            Visit(context.spaces());

            var tokens = new List<MacroToken>();
            if (context.macro_text() != null)
            {
                var tokensText = Visit(context.macro_text()) as MacroListToken;
                tokens = tokensText?.Tokens;
                if (tokens[0]?.Text != null)
                {
                    if (!macros.ContainsKey(macroName))
                        macros.Add(macroName, tokens[0].Text);
                }

                else
                {
                    if (!macros.ContainsKey(macroName))
                        macros.Add(macroName, string.Empty);
                }
                return null;
            }
            if (!macros.ContainsKey(macroName))
                macros.Add(macroName, String.Empty);
            return null;
        }

        public override MacroToken VisitDefineFunctionLikeDirective(MacroParser.DefineFunctionLikeDirectiveContext context)
        {
            var macroName = context.IDENTIFIER().GetText();
            helperBuilder.Append(macroName);
            helperBuilder.Append("(");
            foreach (var spaces in context.spaces())
            {
                Visit(spaces);
            }

            var tokens = new List<MacroToken>();
            if (context.macro_text() != null)
            {
                var tokensText = Visit(context.macro_text()) as MacroListToken;
                tokens = tokensText?.Tokens;
            }
            var parameters = new List<string>();
            foreach (var paramContext in context.macroParam())
            {
                parameters.Add(paramContext.GetText());
                helperBuilder.Append(paramContext.GetText() + ", ");
            }
            helperBuilder.Length--;
            helperBuilder.Length--;
            helperBuilder.Append(")");
            if (context.macro_text() != null)
            {
                if (!macros.ContainsKey(macroName))
                    macros.Add(helperBuilder.ToString(), tokens[0].Text);
            }
            else
            {
                if (!macros.ContainsKey(macroName))
                    macros.Add(helperBuilder.ToString(), String.Empty);
            }

            return null;
        }
    }
}
