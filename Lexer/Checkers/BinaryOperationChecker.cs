namespace Lexer.Checkers
{
    public class BinaryOperationChecker : IChecker
    {
        private static List<string> _operations = new List<string>
        {
            "<<",
            ">>"
        };

        public LexemType LexemType => LexemType.BinaryOperation;

        public bool Check(string data)
        {
            return _operations.Contains(data);
        }
    }
}
