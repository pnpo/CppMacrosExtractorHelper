using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileProcessor.Processor.Macros
{
    public class MacroListToken : MacroToken
    {
        private readonly List<MacroToken> _tokens;

        public List<MacroToken> Tokens => _tokens;

        public MacroListToken() : base("list")
        {
            _tokens = new List<MacroToken>();
        }

        public void AddToken(MacroToken token)
        {
            if (_tokens.Count > 0 && token is TextToken && _tokens.Last() is TextToken)
            {
                _tokens.Last().Text += token.Text;
            }
            else
            {
                _tokens.Add(token);
            }
        }

        public void AddRangeTokens(List<MacroToken> tokens)
        {
            tokens.ForEach(AddToken);
        }

        public string GetText()
        {
            var text = new StringBuilder();
            _tokens.ForEach(token => text.Append(TokenToString(token)));
            return text.ToString();
        }

        protected string TokenToString(MacroToken token)
        {
            if (token is TextToken) return token.Text;
            if (token is IdentifierToken) return token.Text;
            if (token is FunctionToken) return ((FunctionToken)token).GetFunctionText();
            if (token is StringizedToken) return ((StringizedToken)token).GetStringizedText();
            if (token is MacroListToken) return ((MacroListToken)token).GetText();
            return String.Empty;
        }
    }
}
