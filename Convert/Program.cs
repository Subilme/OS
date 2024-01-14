namespace Convert
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Запуск из консоли: Convert.exe <mealy-to-moore/moore-to-mealy> <input.txt> <output.txt>
            Args arguments = new Args(args);

            if (arguments.ConversionType == "moore-to-mealy")
            {
                MooreToMealy.Convert(arguments);
            }
            else if (arguments.ConversionType == "mealy-to-moore")
            {
                MealyToMoore.Convert(arguments);
            }
        }
    }
}