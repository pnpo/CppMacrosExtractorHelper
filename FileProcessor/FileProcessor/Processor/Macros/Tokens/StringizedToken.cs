namespace FileProcessor.Processor.Macros
{
    public class StringizedToken : MacroToken
    {
        public StringizedToken(string text) : base(text) { }

        public string GetStringizedText()
        {
            return $"#{Text}";
        }
    }
}
