namespace Lexer.Checkers
{
    public class StreamIdentifierChecker : IChecker
    {
        private static List<string> _identifiers = new List<string>
        {
            "cout",
            "cin"
        };

        public LexemType LexemType => LexemType.StreamIdentifier;

        public bool Check(string data)
        {
            return _identifiers.Contains(data);
        }
    }
}
