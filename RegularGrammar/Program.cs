using System.Collections.Immutable;

namespace RegularGrammar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Запуск из консоли: RegularGrammar.exe <input.txt> <output.txt>
            Args arguments = new Args(args);

            Dictionary<string, ImmutableSortedSet<string>> rules = new Dictionary<string, ImmutableSortedSet<string>>();
            ReadData(arguments, rules);

            List<List<List<int>>> result = new List<List<List<int>>>();
            if (arguments.Mode == "R")
            {
                RightSideGrammar grammar = new RightSideGrammar(rules);
                result = grammar.Convert();
            }
            else if (arguments.Mode == "L")
            {
                LeftSideGrammar grammar = new LeftSideGrammar(rules);
                result = grammar.Convert();
            }

            Print(arguments.Output, result);
        }

        private static void ReadData(Args args, Dictionary<string, ImmutableSortedSet<string>> rules)
        {
            using (StreamReader sr = new StreamReader(args.Input))
            {
                args.Mode = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split("-");
                    rules.Add(line[0].Trim(), line[1].Split("|").Select(x => x.Trim()).ToImmutableSortedSet());
                }
            }
        }

        private static void Print(string output, List<List<List<int>>> result)
        {
            using (StreamWriter writer = new StreamWriter(output))
            {
                foreach (var state in result)
                {
                    foreach (var signal in state)
                    {
                        if (!signal.Any())
                        {
                            writer.Write("-");
                        }

                        foreach (var item in signal)
                        {
                            writer.Write($"{item},");
                        }
                        writer.Write(" ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}