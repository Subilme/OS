namespace RegularGrammar
{
    public class Args
    {
        public string Mode { get; set; }
        public string Input { get; }
        public string Output { get; }

        public Args(string[] args)
        {
            Mode = string.Empty;
            Input = args[1];
            Output = args[2];
        }
    }
}
