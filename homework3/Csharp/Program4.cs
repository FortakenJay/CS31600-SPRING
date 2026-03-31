using System;

namespace Homework3.Csharp
{
    // Task 4: Iteration and Introspection over enums
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
            Console.WriteLine("Iteration Without Hardcoding");
            Console.WriteLine(new string('=', 70));

            Console.WriteLine();
            Console.WriteLine("Iterating over Color enum:");
            PrintEnumWithoutHardcoding<Color>();

            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("Duplicate Integer Assignment");
            Console.WriteLine(new string('=', 70));

            Console.WriteLine();
            Console.WriteLine("Iterating over DuplicateEnum:");
            PrintEnumWithoutHardcoding<DuplicateEnum>();

            // GetValues goes through the enum values then casts them to the generic enum.

            Console.WriteLine();
            Console.WriteLine("Detailed analysis of DuplicateEnum members:");
            foreach (DuplicateEnum member in Enum.GetValues<DuplicateEnum>())
            {
                Console.WriteLine($"  {member} = {(int)member}");
            }

            Console.WriteLine();
            Console.WriteLine("Direct access checks:");
            Console.WriteLine($"\tDuplicateEnum.First = {DuplicateEnum.First}");
            Console.WriteLine($"\tDuplicateEnum.Second = {DuplicateEnum.Second}");
            Console.WriteLine($"\tDuplicateEnum.AliasToFirst = {DuplicateEnum.AliasToFirst}");

            Console.WriteLine();
            Console.WriteLine("Are First and AliasToFirst equal as values?");
            Console.WriteLine($"\tFirst == AliasToFirst: {DuplicateEnum.First == DuplicateEnum.AliasToFirst}");

            Console.WriteLine();
            Console.WriteLine("Members when iterating:");
            DuplicateEnum[] allMembers = Enum.GetValues<DuplicateEnum>();
            for (int i = 0; i < allMembers.Length; i++)
            {
                DuplicateEnum member = allMembers[i];
                Console.WriteLine($"  {i + 1}. {member} = {(int)member}");
            }

            Console.WriteLine($"\nTotal members iterated: {allMembers.Length}");
        }
    }
}
