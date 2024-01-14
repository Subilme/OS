namespace Lexer
{
    public class Args
    {
        public string Input { get; }
        public string Output { get; }

        public Args(string[] args)
        {
            Input = "input.txt";
            Output = "output.txt";
        }
    }
}
