using System;

namespace Homework3.Csharp
{
    // Task 1: Enumeration Basics
    internal static class Program1
    {
        // C# enum can only be value-types such as int, long, and byte. By default int is used.
        private enum Color
        {
            Red = 1,
            Green = 2,
            Blue = 3
        }

        private static void Main()
        {
            Color chosenColor = Color.Green;

            Console.WriteLine($"Enum variable value: {chosenColor}");
            Console.WriteLine($"Enum variable name: {chosenColor}");
            Console.WriteLine($"Enum variable integer value: {(int)chosenColor}");

            int outOfRange = 99;
            Console.WriteLine();
            Console.WriteLine($"Attempting to cast integer {outOfRange} to Color...");

            // In C#, casting an out-of-range int to an enum does NOT throw at compile time
            // or at runtime. The enum simply holds an undefined underlying value.
            Color invalidColor = (Color)outOfRange;

            Console.WriteLine($"Resulting enum variable: {invalidColor} (numeric value {(int)invalidColor})");

            bool isDefined = Enum.IsDefined(invalidColor);
            Console.WriteLine($"Is {outOfRange} a defined Color value? {isDefined}");
        }
    }
}
