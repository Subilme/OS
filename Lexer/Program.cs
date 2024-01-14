using Lexer.Checkers;

namespace Lexer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Запуск из консоли: Lexer.exe <input.txt> <output.txt>
            List<IChecker> checkers = new List<IChecker>
            {
                new BraceChecker(),
                new KeywordChecker(),
                new LogicOperationChecker(),
                new BinaryOperationChecker(),
                new NumberChecker(),
                new OperationChecker(),
                new StreamIdentifierChecker(),
                new StringChecker(),
                new IdentifierChecker()
            };

            Args arguments = new Args(args);
            Lexer lexer = new Lexer(checkers, arguments);
            var list = lexer.Disassemble();

            using (StreamWriter writer = new StreamWriter(arguments.Output))
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"{item.Data} - {item.LexemType}");
                    writer.WriteLine($"{item.Data} - {item.LexemType}");
                }
            }
        }
    }
}