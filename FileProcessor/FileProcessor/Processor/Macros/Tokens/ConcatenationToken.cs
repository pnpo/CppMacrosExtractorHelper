namespace FileProcessor.Processor.Macros
{
    public class ConcatenationToken : MacroToken
    {
        public MacroToken Left { get; }

        public MacroToken Right { get; }

        public ConcatenationToken(MacroToken left, MacroToken right) : base("##")
        {
            Left = left;
            Right = right;
        }
    }
}
