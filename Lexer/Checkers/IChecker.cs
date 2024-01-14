namespace Lexer.Checkers
{
    public interface IChecker
    {
        public LexemType LexemType { get; }
        public bool Check(string data);
    }
}
