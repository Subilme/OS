using System.Collections.Immutable;

namespace RegularGrammar
{
    public class RightSideGrammar
    {
        private Dictionary<string, ImmutableSortedSet<string>> _rules;
        private List<string> _signals;
        private List<string> _states;

        public RightSideGrammar(Dictionary<string, ImmutableSortedSet<string>> rules)
        {
            _rules = rules;

            _signals = new List<string>();
            _states = new List<string>();
            foreach (var rule in rules)
            {
                _states.Add(rule.Key.Trim());
                foreach (var item in rule.Value)
                {
                    if (!_signals.Contains(item[0].ToString()))
                    { 
                        _signals.Add(item[0].ToString());
                    }
                }
            }
            _states.Add("F");
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
                    if (item == "#")
                    {
                        result[counter][_signals.IndexOf(item[0].ToString())].Add(_states.IndexOf("F"));
                        continue;
                    }

                    result[counter][_signals.IndexOf(item[0].ToString())].Add(_states.IndexOf(item[1].ToString()));
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
