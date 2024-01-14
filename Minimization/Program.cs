namespace Minimization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Запуск из консоли: Minimization.exe <mealy/moore> <input.txt> <output.txt>
            Args arguments = new Args(args);

            if (arguments.ConversionType == "mealy")
            {
                MealyMachine machine = new MealyMachine(arguments);
                machine.Minimize();
            }
            else if (arguments.ConversionType == "moore")
            {
                MooreMachine machine = new MooreMachine(arguments);
                machine.Minimize();
            }
        }
    }
}