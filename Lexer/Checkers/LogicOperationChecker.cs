namespace Lexer.Checkers
{
    public class LogicOperationChecker : IChecker
    {
        private static List<string> _operations = new List<string>
        {
            "!=",
            "==",
            ">",
            "<",
            ">=",
            "<=",
            "||",
            "&&"
        };

        public LexemType LexemType => LexemType.LogicOperation;

        public bool Check(string data)
        {
            return _operations.Contains(data);
        }
    }
}
