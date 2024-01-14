namespace NFAtoDFA
{
    public class Args
    {
        public string Input { get; }
        public string Output { get; }

        public Args(string[] args)
        {
            Input = args[0];
            Output = args[1];
        }
    }
}
