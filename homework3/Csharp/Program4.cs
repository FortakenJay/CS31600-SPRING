using System;

namespace Homework3.Csharp
{
    // Task 4: Iteration and Introspection over enums
    // Standalone console program mirroring Python Program4.py behavior.
    internal static class Program4
    {
        private enum Color
        {
            Red = 1,
            Green = 2,
            Blue = 3
        }

        private enum DuplicateEnum
        {
            // Duplicate underlying values are allowed in C#.
            First = 1,
            Second = 2,
            AliasToFirst = 1
        }

        private static void PrintEnumWithoutHardcoding<TEnum>() where TEnum : struct, Enum
        {
            Console.WriteLine($"All values in {typeof(TEnum).Name}:");
            foreach (TEnum value in Enum.GetValues<TEnum>())
            {
                Console.WriteLine($"  {value} = {(int)(object)value}");
            }
        }

        private static void Main()
        {
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("PART 1: Iteration Without Hardcoding");
            Console.WriteLine(new string('=', 70));

            Console.WriteLine();
            Console.WriteLine("Iterating over Color enum:");
            PrintEnumWithoutHardcoding<Color>();

            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("PART 2: Duplicate Integer Assignment");
            Console.WriteLine(new string('=', 70));

            Console.WriteLine();
            Console.WriteLine("Iterating over DuplicateEnum:");
            PrintEnumWithoutHardcoding<DuplicateEnum>();

            Console.WriteLine();
            Console.WriteLine("Detailed analysis of DuplicateEnum members:");
            foreach (DuplicateEnum member in Enum.GetValues<DuplicateEnum>())
            {
                Console.WriteLine($"  {member} = {(int)member}");
            }

            Console.WriteLine();
            Console.WriteLine("Direct access checks:");
            Console.WriteLine($"  DuplicateEnum.First = {DuplicateEnum.First}");
            Console.WriteLine($"  DuplicateEnum.Second = {DuplicateEnum.Second}");
            Console.WriteLine($"  DuplicateEnum.AliasToFirst = {DuplicateEnum.AliasToFirst}");

            Console.WriteLine();
            Console.WriteLine("Are First and AliasToFirst equal as values?");
            Console.WriteLine($"  First == AliasToFirst: {DuplicateEnum.First == DuplicateEnum.AliasToFirst}");

            Console.WriteLine();
            Console.WriteLine("Members when iterating:");
            DuplicateEnum[] allMembers = Enum.GetValues<DuplicateEnum>();
            for (int i = 0; i < allMembers.Length; i++)
            {
                DuplicateEnum member = allMembers[i];
                Console.WriteLine($"  {i + 1}. {member} = {(int)member}");
            }

            Console.WriteLine($"\nTotal members iterated: {allMembers.Length}");
            Console.WriteLine("Note: C# lists ALL names, even those sharing the same underlying value.");

            Console.WriteLine();
            Console.WriteLine("ANALYSIS:");
            Console.WriteLine("- C# has built-in enum iteration via Enum.GetValues<TEnum>().");
            Console.WriteLine("- Each defined name appears in iteration, even if underlying values are duplicated.");
            Console.WriteLine("- Equality compares the underlying value; First == AliasToFirst is true.");
            Console.WriteLine("- Compared to manual lists, this is short and automatically stays in sync with the enum.");
        }
    }
}
