namespace Lexer.Checkers
{
    public class KeywordChecker : IChecker
    {
        private static List<string> _keywords = new List<string>
        {
            "return",
            "int",
            "float",
            "string",
            "char",
            "bool",
            "true",
            "false",
            "for",
            "while",
            "if",
            "break",
            "auto",
            "void",
            "const"
        };

        public LexemType LexemType => LexemType.Keyword;

        public bool Check(string data)
        {
            return _keywords.Contains(data);
        }
    }
}
