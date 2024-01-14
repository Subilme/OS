using System.Collections.Immutable;

namespace RegularGrammar
{
    public class LeftSideGrammar
    {
        private Dictionary<string, ImmutableSortedSet<string>> _rules;
        private List<string> _signals;
        private List<string> _states;

        public LeftSideGrammar(Dictionary<string, ImmutableSortedSet<string>> rules)
        {
            _rules = rules;

            _signals = new List<string>();
            _states = new List<string> { "H" };
            foreach (var rule in rules)
            {
                _states.Add(rule.Key.Trim());
                foreach (var item in rule.Value)
                {
                    if (!_signals.Contains(item[0].ToString()))
                    {
                        if (item.Length < 2)
                        {
                            if (!_signals.Contains(item[0].ToString()))
                            {
                                _signals.Add(item[0].ToString());
                            }

                            continue;
                        }

                        if (!_signals.Contains(item[1].ToString()))
                        {
                            _signals.Add(item[1].ToString());
                        }
                    }
                }
            }

            _states.Remove("S");
            _states.Add("S");
            _signals.Remove("#");
            _signals.Add("#");
        }

        public List<List<List<int>>> Convert()
        {
            List<List<List<int>>> result = new List<List<List<int>>>();
            for (int index = 0; index < _states.Count; index++)
            {
                var transitionsForStates = new List<List<int>>();
                foreach (var signal in _signals)
                {
                    transitionsForStates.Add(new List<int>());
                }
                result.Add(transitionsForStates);
            }

            int counter = 0;
            foreach (var rule in _rules)
            {
                foreach (var item in rule.Value)
                {
                    if (item.Length < 2)
                    {
                        result[0][_signals.IndexOf(item[0].ToString())].Add(_states.IndexOf(rule.Key));
                        continue;
                    }

                    result[_states.IndexOf(item[0].ToString())][_signals.IndexOf(item[1].ToString())]
                        .Add(_states.IndexOf(rule.Key));
                }
                counter++;
            }

            for (int state = 0; state < result.Count; state++)
            {
                for (int signal = 0; signal < result[state].Count; signal++)
                {
                    result[state][signal].Sort();
                }
            }

            return result;
        }
    }
}
