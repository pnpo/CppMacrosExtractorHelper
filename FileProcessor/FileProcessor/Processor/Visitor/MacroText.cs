using Antlr4.Runtime.Misc;
using FileProcessor.Processor.Macros;
using FileProcessor.Processor.Parser;
using System;
using System.Text.RegularExpressions;

namespace FileProcessor.Processor.Visitor
{
    public partial class CPreProcessorVisitor
    {
        public override MacroToken VisitMacro_text(MacroParser.Macro_textContext context)
        {
            var tokens = new MacroListToken();
            foreach (var sourceTextContext in context.source_text())
            {
                var token = Visit(sourceTextContext);
                HandleMacroListTokens(token, ref tokens);
            }
            return tokens;
        }

        public override MacroToken VisitMacroSourceExpression([NotNull] MacroParser.MacroSourceExpressionContext context)
        {
            return Visit(context.sourceExpression());
        }

        public override MacroToken VisitMacroComma([NotNull] MacroParser.MacroCommaContext context)
        {
            return new TextToken(context.COMMA().GetText());
        }

        public override MacroToken VisitMacroLParen([NotNull] MacroParser.MacroLParenContext context)
        {
            return new TextToken(context.LPAREN().GetText());
        }

        public override MacroToken VisitMacroRParen([NotNull] MacroParser.MacroRParenContext context)
        {
            return new TextToken(context.RPAREN().GetText());
        }

        public override MacroToken VisitMacroWhitespace([NotNull] MacroParser.MacroWhitespaceContext context)
        {
            return new TextToken(String.Empty);
        }

        public override MacroToken VisitArguments(MacroParser.ArgumentsContext context)
        {
            var tokens = new MacroListToken();
            tokens.AddToken(new TextToken(context.LPAREN().GetText()));
            var argumentsTokens = Visit(context.args()) as MacroListToken;
            if (argumentsTokens != null) tokens.AddRangeTokens(argumentsTokens.Tokens);
            tokens.AddToken(new TextToken(context.RPAREN().GetText()));
            return tokens;
        }

        public override MacroToken VisitArgs([NotNull] MacroParser.ArgsContext context)
        {
            var arguments = new MacroListToken();

            var firstArgument = Visit(context.mArg());
            HandleMacroListTokens(firstArgument, ref arguments);

            for (int i = 0 ; i < context.multipleArg().Length; i++)
            {
                var token = Visit(context.multipleArg(i));
                HandleMacroListTokens(token, ref arguments);
            }
            return arguments;
        }

        public override MacroToken VisitMultipleArg([NotNull] MacroParser.MultipleArgContext context)
        {
            var tokens = new MacroListToken();
            if(context.WS(0) != null)
            {
                tokens.AddToken(new TextToken(context.WS(0).GetText()));
            }

            tokens.AddToken(new TextToken(context.COMMA().GetText()));

            var arg = Visit(context.mArg());
            HandleMacroListTokens(arg, ref tokens);

            if (context.WS(1) != null)
            {
                tokens.AddToken(new TextToken(context.WS(1).GetText()));
            }

            return tokens;
        }

        public override MacroToken VisitMacroExpansion(MacroParser.MacroExpansionContext context)
        {
            var identifierName = context.IDENTIFIER().GetText();
            var arguments = new MacroListToken();

            if (context.macArgs() != null)
            {
                arguments = Visit(context.macArgs()) as MacroListToken;
            }

            return new FunctionToken(identifierName, arguments);
        }

        public override MacroToken VisitStringized(MacroParser.StringizedContext context)
        {
            string stringizedIdentifier = context.STRING_OP().GetText();
            var pattern = "#[\\s\\r\\f\\t]*";
            Regex rgx = new Regex(pattern);
            return new StringizedToken(rgx.Replace(stringizedIdentifier, String.Empty));
        }

        public override MacroToken VisitSizeof(MacroParser.SizeofContext context)
        {
            return new TextToken(context.SIZEOF().GetText());
        }

        public override MacroToken VisitSemicolon(MacroParser.SemicolonContext context)
        {
            return new TextToken(context.SEMICOLON().GetText());
        }

        public override MacroToken VisitWhitespace(MacroParser.WhitespaceContext context)
        {
            return new TextToken(" ");
        }

        public override MacroToken VisitMacArgs(MacroParser.MacArgsContext context)
        {
            var arguments = new MacroListToken();
            foreach (var mArgContext in context.mArg())
            {
                var token = Visit(mArgContext);
                arguments.Tokens.Add(token);
            }
            return arguments;
        }

        public override MacroToken VisitMArg(MacroParser.MArgContext context)
        {
            var argumentTokenList = new MacroListToken();
            foreach (var sourceExpressionContext in context.sourceExpression())
            {
                var token = Visit(sourceExpressionContext);
                HandleMacroListTokens(token, ref argumentTokenList);
            }
            return argumentTokenList;
        }

        public override MacroToken VisitConcatenate(MacroParser.ConcatenateContext context)
        {
            var firstParam = context.SHARPSHARP(0).Symbol.Column > context.primarySource(0).stop.Column ? Visit(context.primarySource(0)) : new TextToken(String.Empty);
            int index = firstParam.Text.Equals(String.Empty) ? 0 : 1;
            var concatenation = new ConcatenationToken(firstParam, Visit(context.primarySource(index)));
            for (var i = index + 1; i < context.primarySource().Length; i++)
            {
                concatenation = new ConcatenationToken(concatenation, Visit(context.primarySource(i)));
            }
            return concatenation;
        }

        public override MacroToken VisitPrimaryIdentifier(MacroParser.PrimaryIdentifierContext context)
        {
            return new IdentifierToken(context.IDENTIFIER().GetText());
        }

        public override MacroToken VisitPrimaryConstant(MacroParser.PrimaryConstantContext context)
        {
            var constantText = context.constant().GetText();
            int matchCount;
            // Remove BackSlash-Newlines from inside constant literals
            constantText = ReplacePattern(constantText, @"(\\\r\n|\\\n)", "", out matchCount);

            //Append the same number of newlines in order to keep the same line pragma
            while (matchCount-- > 0)
            {
                preprocessedCode.Append(Environment.NewLine);
            }

            return new TextToken(constantText);
        }

        public override MacroToken VisitPrimaryCKeyword(MacroParser.PrimaryCKeywordContext context)
        {
            return new TextToken(context.CKEYWORD().GetText());
        }

        public override MacroToken VisitPrimaryCOperator(MacroParser.PrimaryCOperatorContext context)
        {
            return new TextToken(context.COPERATOR().GetText());
        }

        public override MacroToken VisitPrimaryNewLine(MacroParser.PrimaryNewLineContext context)
        {
            preprocessedCode.Append(Environment.NewLine);
            return new TextToken(String.Empty);
        }

        //public override MacroToken VisitVariadicArguments([NotNull] MacroParser.VariadicArgumentsContext context)
        //{
        //    return new VariadicToken(context.VA_ARGS().GetText());
        //}

        public override MacroToken VisitPasteOperator(MacroParser.PasteOperatorContext context)
        {
            return new TextToken(context.SHARPSHARP().GetText());
        }

        public override MacroToken VisitSpaces([NotNull] MacroParser.SpacesContext context)
        {
            int count = context.BACKSLASH_NEWLINE().Length;
            while(count-- > 0)
            {
                preprocessedCode.Append(Environment.NewLine);
            }

            return new TextToken(String.Empty);
        }
    }
}
