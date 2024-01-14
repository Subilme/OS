namespace Lexer.Checkers
{
    public class StringChecker : IChecker
    {
        public LexemType LexemType => LexemType.String;

        public bool Check(string data)
        {
            return data.First() == '\"' && data.Last() == '\"';
        }
    }
}
