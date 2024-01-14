using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Minimization
{
    public class MealyMachine
    {
        private string _input;
        private string _output;

        private int k, m;

        private List<List<string>> _transitions;
        private List<List<string>> _signals;
        private Dictionary<int, string> _stateAndGroup;

        public MealyMachine(Args args)
        {
            _transitions = new List<List<string>>();
            _signals = new List<List<string>>();
            _stateAndGroup = new Dictionary<int, string>();
            _input = args.Input;
            _output = args.Output;

            Read(_input);
        }

        public void Minimize()
        {
            List<string> uniqSignals = new List<string>();
            for (int transition = 0; transition < k; transition++)
            {
                string signals = string.Empty;
                for (int index = 0; index < m; index++)
                {
                    signals += _signals[transition][index];
                }

                if (!uniqSignals.Contains(signals))
                {
                    uniqSignals.Add(signals);
                }
                _stateAndGroup[transition] = uniqSignals.IndexOf(signals).ToString();
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
                    string newGroup = string.Empty;
                    for (int index = 0; index < m; index++)
                    {
                        newGroup += newTransitions[key][index];
                    }

                    if (oldGroup != int.Parse(_stateAndGroup[key]))
                    {
                        newStateAndGroups.Add(new Dictionary<string, int>());
                    }
                    oldGroup = int.Parse(_stateAndGroup[key]);
                    if (!newStateAndGroups[oldGroup].ContainsKey(newGroup))
                    {
                        newStateAndGroups[oldGroup].Add(newGroup, max);
                        max++;
                    }
                    updatedStateAndGroups[key] = newStateAndGroups[oldGroup][newGroup].ToString();
                }

                newTransitions.Clear();
                for (int index = 0; index < k; index++)
                {
                    newTransitions.Add(new List<string>());
                }

                updatedStateAndGroups = updatedStateAndGroups.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                if (updatedStateAndGroups.SequenceEqual(_stateAndGroup))
                {
                    _stateAndGroup = new Dictionary<int, string>(updatedStateAndGroups);
                    break;
                }

                _stateAndGroup = new Dictionary<int, string>(updatedStateAndGroups);
                Update(newTransitions);
            }

            Dictionary<int, int> result = new Dictionary<int, int>();
            int counter = 0;
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
            using (StreamReader reader = new StreamReader(input))
            {
                var line = reader.ReadLine().Split();
                k = int.Parse(line[0]);
                m = int.Parse(line[1]);
                
                while (!reader.EndOfStream)
                {
                    var tempTransitions = new List<string>();
                    var tempSignals = new List<string>();
                    line = reader.ReadLine().Split();
                    for (int index = 0; index < m * 2; index += 2)
                    {
                        if (line[index] == "-")
                        {
                            tempTransitions.Add(line[index]);
                            tempSignals.Add(line[index]);
                            continue;
                        }

                        tempTransitions.Add(line[index]);
                        tempSignals.Add(line[index + 1]);
                    }
                    _transitions.Add(tempTransitions);
                    _signals.Add(tempSignals);
                }
            }
        }

        private void Print(string output, Dictionary<int, int> result)
        {
            using (StreamWriter writer = new StreamWriter(output))
            {
                foreach (var (_, value) in result)
                {
                    for (var index = 0; index < m; index++)
                    {
                        if (_transitions[value][index] == "-")
                        {
                            Console.Write($"- ");
                            writer.Write($"- ");
                            continue;
                        }

                        Console.Write($"{_stateAndGroup[int.Parse(_transitions[value][index])]} {_signals[value][index]} ");
                        writer.Write($"{_stateAndGroup[int.Parse(_transitions[value][index])]} {_signals[value][index]} ");
                    }
                    Console.WriteLine();
                    writer.WriteLine();
                }
            }
        }
    }
}
