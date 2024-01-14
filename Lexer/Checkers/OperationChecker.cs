namespace Lexer.Checkers
{
    public class OperationChecker : IChecker
    {
        private static List<string> _operations = new List<string>
        {
            "+",
            "-",
            "*",
            "/",
            "="
        };

        public LexemType LexemType => LexemType.Operation;

        public bool Check(string data)
        {
            return _operations.Contains(data);
        }
    }
}
