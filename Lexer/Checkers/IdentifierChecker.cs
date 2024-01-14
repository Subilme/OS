namespace Lexer.Checkers
{
    public class IdentifierChecker : IChecker
    {
        public LexemType LexemType => LexemType.Identifier;

        public bool Check(string data)
        {
            foreach (var item in data)
            {
                if (item != '_' && !char.IsLetterOrDigit(item))
                { 
                    return false;
                }
            }

            return true;
        }
    }
}
