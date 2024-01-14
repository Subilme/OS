namespace Lexer.Checkers
{
    public class NumberChecker : IChecker
    {
        public LexemType LexemType => LexemType.Number;

        public bool Check(string data)
        {
            return IfInt(data) || IfFloat(data);
        }

        private bool IfInt(string data)
        {
            return data.All(x => char.IsDigit(x));
        }
        private bool IfFloat(string data)
        {
            var number = data.Split(".");
            return number[0].All(x => char.IsDigit(x)) && number[1].All(x => char.IsDigit(x));
        }
    }
}
