namespace Lexer
{
    public static class StringExtensions
    {
        public static string DivideElements(this string s)
        {
            if (s.Contains("("))
            {
                s = s.Substring(0, s.IndexOf("(")) + " ( " + s.Substring(s.IndexOf("(") + 1);
            }
            if (s.Contains(")"))
            {
                s = s.Substring(0, s.IndexOf(")")) + " ) " + s.Substring(s.IndexOf(")") + 1);
            }
            return s;
        }
    }
}
