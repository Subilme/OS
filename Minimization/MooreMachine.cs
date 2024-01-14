namespace Minimization
{
    public class MooreMachine
    {
        private string _input;
        private string _output;

        private int k, m;

        private List<List<string>> _transitions;
        private List<string> _signals;
        private Dictionary<int, string> _stateAndGroup;

        public MooreMachine(Args args)
        {
            _transitions = new List<List<string>>();
            _signals = new List<string>();
            _stateAndGroup = new Dictionary<int, string>();
            _input = args.Input;
            _output = args.Output;

            Read(_input);
        }

        public void Minimize()
        {
            List<string> uniqSignals = new List<string>();
            for (int index = 0; index < k; index++)
            {
                if (!uniqSignals.Contains(_signals[index]))
                {
                    uniqSignals.Add(_signals[index]);
                }
                _stateAndGroup[index] = uniqSignals.IndexOf(_signals[index]).ToString();
            }
            _stateAndGroup = _stateAndGroup.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            List<List<string>> newTransitions = new List<List<string>>();
            for (int index = 0; index < k; index++)
            {
                newTransitions.Add(new List<string>());
            }
            Update(newTransitions);
            
            List<Dictionary<string, int>> newStateAndGroups = new List<Dictionary<string, int>>();
            Dictionary<int, string> updatedStateAndGroups = new Dictionary<int, string>();
            while (true)
            {
                newStateAndGroups.Clear();
                newStateAndGroups.Add(new Dictionary<string, int>());
                int oldGroup = 0, max = 0;
                foreach (var (key, _) in _stateAndGroup)
                {
                    string group = string.Empty;
                    for (int index = 0; index < m; index++)
                    {
                        group += newTransitions[key][index];
                    }
                    if (oldGroup != int.Parse(_stateAndGroup[key]))
                    {
                        newStateAndGroups.Add(new Dictionary<string, int>());
                    }
                    oldGroup = int.Parse(_stateAndGroup[key]);
                    if (!newStateAndGroups[oldGroup].ContainsKey(group))
                    {
                        newStateAndGroups[oldGroup].Add(group, max);
                        max++;
                    }
                    updatedStateAndGroups[key] = newStateAndGroups[oldGroup][group].ToString();
                }

                newTransitions.Clear();
                for (int index = 0; index < k; index++)
                {
                    newTransitions.Add(new List<string>());
                }
                Update(newTransitions);
                
                updatedStateAndGroups = updatedStateAndGroups.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                if (updatedStateAndGroups.SequenceEqual(_stateAndGroup))
                {
                    _stateAndGroup = new Dictionary<int, string>(updatedStateAndGroups);
                    break;
                }
                _stateAndGroup = new Dictionary<int, string>(updatedStateAndGroups);
            }

            int counter = 0;
            Dictionary<int, int> result = new Dictionary<int, int>();
            foreach (var (key, value) in _stateAndGroup)
            {
                if (int.Parse(value) != counter - 1)
                {
                    result.Add(counter, key);
                    counter++;
                }
            }

            Print(_output, result);
        }

        private void Update(List<List<string>> newTransitions)
        {
            foreach (var (key, _) in _stateAndGroup)
            {
                for (int index = 0; index < m; index++)
                {
                    if (_transitions[key][index] == "-")
                    {
                        newTransitions[key].Add("-");
                        continue;
                    }

                    newTransitions[key].Add(_stateAndGroup[int.Parse(_transitions[key][index])]);
                }
            }
        }

        private void Read(string input)
        {
            using (StreamReader sr = new StreamReader(input))
            {
                var line = sr.ReadLine().Split();
                k = int.Parse(line[0]);
                m = int.Parse(line[1]);

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split();
                    _signals.Add(line[0]);
                    var temp = new List<string>();
                    for (int state = 1; state <= m; state++)
                    {
                        temp.Add(line[state]);
                    }
                    _transitions.Add(temp);
                }
            }
        }

        private void Print(string output, Dictionary<int, int> result)
        {
            using (StreamWriter sw = new StreamWriter(output))
            {
                foreach (var (_, value) in result)
                {
                    Console.Write($"{_signals[value]} ");
                    sw.Write($"{_signals[value]} ");
                    for (int index = 0; index < m; index++)
                    {
                        if (_transitions[value][index] == "-")
                        {
                            Console.Write("- ");
                            sw.Write("- ");
                            continue;
                        }
                        Console.Write($"{_stateAndGroup[int.Parse(_transitions[value][index])]} ");
                        sw.Write($"{_stateAndGroup[int.Parse(_transitions[value][index])]} ");
                    }
                    Console.WriteLine();
                    sw.WriteLine();
                }
            }
        }
    }
}
