namespace Convert
{
    public class MooreToMealy
    {
        private static string[,] _moore = null;
        private static string[] _outSignals = null;
        private static int k, m;

        public static void Convert(Args args)
        {
            Read(args);

            string[,] mealy = new string[k, m];
            for (int row = 0; row < k; row++)
            {
                for (int column = 0; column < m; column++)
                {
                    if (_moore[row, column] == "-")
                    {
                        mealy[row, column] = "-";
                        continue;
                    }
                    mealy[row, column] = $"S{_moore[row, column].Substring(1)} " +
                        $"{_outSignals[int.Parse(_moore[row, column].Substring(1))]}";
                }
            }

            Print(args, mealy);
        }

        private static void Read(Args args)
        {
            using (StreamReader reader = new StreamReader(args.Input))
            {
                var temp = reader.ReadLine().Split(" ");
                k = int.Parse(temp[0].ToString());
                m = int.Parse(temp[1].ToString());
                _moore = new string[k, m];
                _outSignals = new string[k];

                for (int i = 0; i < k; i++)
                {
                    var line = reader.ReadLine();
                    _outSignals[i] = line.Substring(0, line.IndexOf(' '));
                    string[] states = line.Split(" ").Skip(1).ToArray();
                    for (int j = 0; j < m; j++)
                    {
                        _moore[i, j] = states[j].Trim();
                    }
                }
            }
        }

        private static void Print(Args args, string[,] mealy)
        {
            using (StreamWriter writer = new StreamWriter(args.Output))
            {
                for (int row = 0; row < k; row++)
                {
                    for (int column = 0; column < m; column++)
                    {
                        Console.Write(mealy[row, column] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
