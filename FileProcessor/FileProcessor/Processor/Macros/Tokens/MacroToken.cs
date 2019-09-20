namespace FileProcessor.Processor.Macros
{
    public abstract class MacroToken
    {
        public string Text { get; set; }
        protected MacroToken( string text)
        {
            Text = text;
        }
    }
}
