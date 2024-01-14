namespace Lexer.Checkers
{
    public class BraceChecker : IChecker
    {
        private static List<string> _braces = new List<string>
        {
            "(",
            ")",
            "{",
            "}",
        };

        public LexemType LexemType => LexemType.Brace;

        public bool Check(string data)
        {
            return _braces.Contains(data);
        }
    }
}
