namespace NFAtoDFA
{
    public class Determinatio
    {
        private static readonly List<List<List<string>>> _transitions = new List<List<List<string>>>();
        private static readonly List<List<string>> _eClose = new List<List<string>>();
        private static int k, m;

        public static void Determine(Args args)
        {
            Read(args);

            for (int index = 0; index < k; index++)
            {
                var list = new SortedSet<string> { index.ToString() };
                FillEClose(list, index);
                _eClose.Add(list.ToList());
            }

            List<List<string>> passed = new List<List<string>> { new List<string> { "0" } };
            List<List<string>> result = new List<List<string>>();
            Queue<List<string>> queue = new Queue<List<string>>();
            queue.Enqueue(_eClose[0]);

            while (queue.Any())
            {
                var temp = new List<string>();
                var list = queue.Dequeue();
                for (int index = 0; index < m; index++)
                {
                    List<string> state = new();
                    SortedSet<string> newEclose = new SortedSet<string>();
                    foreach (var num in list)
                    {
                        foreach (var n in _transitions[int.Parse(num)][index])
                        {
                            if (n != "-")
                            {
                                state.Add(n);
                            }
                        }
                    }
                    state.Sort();

                    if (passed.FirstOrDefault(x => x.SequenceEqual(state)) == null && state.Count != 0)
                    {
                        passed.Add(state);
                        foreach (var item in state)
                        {
                            newEclose.UnionWith(_eClose[int.Parse(item)]);
                        }
                        queue.Enqueue(newEclose.ToList());
                    }

                    if (state.Count == 0)
                    {
                        temp.Add("-");
                    }
                    else
                    {
                        var elem = passed.FirstOrDefault(x => x.SequenceEqual(state));
                        if (elem is null)
                        {
                            temp.Add(passed.Count.ToString());
                            continue;
                        }

                        temp.Add(passed.IndexOf(elem).ToString());
                    }
                }
                result.Add(temp);
            }

            Print(args, result);
        }

        private static void Read(Args args)
        {
            using (StreamReader reader = new StreamReader(args.Input))
            {
                var line = reader.ReadLine().Split();
                k = int.Parse(line[0]);
                m = int.Parse(line[1]);

                while (!reader.EndOfStream)
                {
                    var temp = new List<List<string>>();
                    line = reader.ReadLine().Split();
                    for (int index = 0; index <= m; index++)
                    {
                        var list = new List<string>();
                        foreach (var item in line[index].Split(","))
                        {
                            list.Add(item);
                        }
                        temp.Add(list);
                    }
                    _transitions.Add(temp);
                }
            }
        }

        private static void FillEClose(SortedSet<string> eClose, int index)
        {
            foreach (var item in _transitions[index][m])
            {
                if (!eClose.Contains(item) && item != "-")
                {
                    eClose.Add(item);
                    FillEClose(eClose, int.Parse(item));
                }
            }
        }

        private static void Print(Args args, List<List<string>> result)
        {
            using (StreamWriter writer = new StreamWriter(args.Output))
            {
                foreach (var state in result)
                {
                    foreach (var signal in state)
                    {
                        Console.Write($"{signal} ");
                        writer.Write($"{signal} ");
                    }
                    Console.WriteLine();
                    writer.WriteLine();
                }
            }
        }
    }
}
