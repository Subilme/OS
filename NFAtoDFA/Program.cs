namespace NFAtoDFA
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Запуск из консоли: NFAtoDFA.exe <input.txt> <output.txt>
            Args arguments = new Args(args);

            Determinatio.Determine(arguments);
        }
    }
}