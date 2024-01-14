using Lexer.Checkers;

namespace Lexer
{
    public class Lexer
    {
        private List<IChecker> _checkers;
        private Args _args;

        public Lexer(List<IChecker> checkers, Args args)
        {
            _checkers = checkers;
            _args = args;
        }

        public List<Lexem> Disassemble()
        {
            List<Lexem> lexems = new List<Lexem>();
            using StreamReader reader = new StreamReader(_args.Input);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().DivideElements().Trim().Split();
                for (int index = 0; index < line.Length; index++)
                {
                    if (line[index] == string.Empty)
                    {
                        continue;
                    }

                    Lexem semicolon = null;
                    if (line[index].Last() == ';')
                    {
                        semicolon = new Lexem
                        {
                            LexemType = ToString(LexemType.Semicolon),
                            Data = line[index].Last().ToString()
                        };
                        line[index] = line[index].Substring(0, line[index].Length - 1);
                    }

                    lexems.Add(new Lexem
                    {
                        LexemType = ToString(GetLexemType(line[index])),
                        Data = line[index].Replace("\"", string.Empty)
                    });

                    if (semicolon != null)
                    {
                        lexems.Add(semicolon);
                    }
                }
            }

            return lexems;
        }

        private LexemType GetLexemType(string data)
        {
            foreach (var item in _checkers)
            {
                if (item.Check(data))
                {
                    return item.LexemType;
                }
            }

            return LexemType.Error;
        }

        private static string ToString(LexemType lexemType)
        {
            switch (lexemType)
            {
                case LexemType.Keyword:
                    return "Keyword";
                case LexemType.Identifier:
                    return "Identifier";
                case LexemType.StreamIdentifier:
                    return "Stream Identifier";
                case LexemType.Brace:
                    return "Brace";
                case LexemType.Semicolon:
                    return "Semicolon";
                case LexemType.Operation:
                    return "Operation";
                case LexemType.LogicOperation:
                    return "LogicOperation";
                case LexemType.BinaryOperation:
                    return "BinaryOperation";
                case LexemType.Number:
                    return "Number";
                case LexemType.String: 
                    return "String";
                case LexemType.Error:
                    return "Error";
                default:
                    throw new Exception();
            }
        }
    }
}
