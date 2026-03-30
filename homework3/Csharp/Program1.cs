using System;

namespace Homework3.Csharp
{
    // Task 1: Enumeration Basics
    // Standalone console program mirroring Python Program1.py behavior.
    internal static class Program1
    {
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
            Console.WriteLine($"Enum variable name: {chosenColor.ToString()}");
            Console.WriteLine($"Enum variable integer value: {(int)chosenColor}");

            int outOfRange = 99;
            Console.WriteLine();
            Console.WriteLine($"Attempting to cast integer {outOfRange} to Color...");

            // In C#, casting an out-of-range int to an enum does NOT throw at compile time
            // or at runtime. The enum simply holds an undefined underlying value.
            Color invalidColor = (Color)outOfRange;

            Console.WriteLine($"Resulting enum variable: {invalidColor} (numeric value {(int)invalidColor})");

            bool isDefined = Enum.IsDefined(typeof(Color), invalidColor);
            Console.WriteLine($"Is {outOfRange} a defined Color value? {isDefined}");

            Console.WriteLine();
            Console.WriteLine("ANALYSIS:");
            Console.WriteLine("- C# allows casting any underlying integer to an enum type.");
            Console.WriteLine("- The compiler does not reject out-of-range values.");
            Console.WriteLine("- At runtime, no exception is thrown; the enum simply contains an undefined value.");
            Console.WriteLine("- You must call Enum.IsDefined(...) to detect invalid enum values explicitly.");
        }
    }
}
