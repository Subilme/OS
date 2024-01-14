namespace Convert
{
    public class MealyToMoore
    {
        private static string[,] mealy = null;
        private static int k, m;

        public static void Convert(Args args)
        {
            Read(args);

            SortedSet<string> sortedStates = new SortedSet<string>(mealy.Cast<string>().ToList());

            string[,] moore = new string[k, m];
            for (int row = 0; row < k; row++)
            {
                for (int column = 0; column < m; column++)
                {
                    int t = 0;
                    foreach (string item in sortedStates)
                    {
                        if (mealy[row, column] == item)
                        {
                            break;
                        }
                        t++;
                    }

                    moore[row, column] = $"q{t}";
                }
            }

            string[] outSignals = new string[sortedStates.Count];
            string[,] result = new string[sortedStates.Count, m];
            for (int row = 0; row < k; row++)
            {
                for (int column = 0; column < m; column++)
                {
                    for (int i = 0; i < m; i++)
                    {
                        result[int.Parse(moore[row, column][1].ToString()), i]
                            = moore[int.Parse(sortedStates.ElementAt(int.Parse(moore[row, column][1].ToString()))[1].ToString()), i];
                    }

                    outSignals[int.Parse(moore[row, column][1].ToString())]
                        = $"Y{sortedStates.ElementAt(int.Parse(moore[row, column][1].ToString()))[4]}";
                }
            }

            Print(args, sortedStates, outSignals, result);
        }

        private static void Read(Args args)
        {
            using (StreamReader reader = new StreamReader(args.Input))
            {
                var temp = reader.ReadLine().Split(" ");
                k = int.Parse(temp[0].ToString());
                m = int.Parse(temp[1].ToString());
                mealy = new string[k, m];

                for (int i = 0; i < k; i++)
                {
                    string[] line = reader.ReadLine()!.Split(",");
                    for (int j = 0; j < m; j++)
                    {
                        mealy[i, j] = line[j].Trim();
                    }
                }
            }
        }

        private static void Print(Args args, SortedSet<string> sortedStates, string[] outSignals, string[,] result)
        {
            using (StreamWriter writer = new StreamWriter(args.Output))
            {
                for (int row = 0; row < sortedStates.Count; row++)
                {
                    writer.Write($"{outSignals[row]} ");
                    for (int column = 0; column < m; column++)
                    {
                        writer.Write($"{result[row, column]} ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
