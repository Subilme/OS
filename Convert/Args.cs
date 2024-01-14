namespace Convert
{
    public class Args
    {
        public string ConversionType { get; }
        public string Input { get; }
        public string Output { get; }

        public Args(string[] args)
        {
            ConversionType = args[0];
            Input = args[1];
            Output = args[2];
        }
    }
}
