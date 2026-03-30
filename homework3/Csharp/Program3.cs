using System;
using System.Collections.Generic;

namespace Homework3.Csharp
{
    // Task 3: Extended Enum Behavior and null/uninitialized handling
    // Standalone console program mirroring Python Program3.py behavior.
    internal static class Program3
    {
        private enum ShapeKind
        {
            Circle = 1,
            Square = 2,
            Rectangle = 3,
            Error = 99
        }

        private sealed class ShapeData
        {
            public string Name { get; }
            public double Radius { get; }
            public double Side { get; }
            public double Width { get; }
            public double Height { get; }
            public int? ErrorCode { get; }
            public string? ErrorMessage { get; }

            public ShapeData(
                string name,
                double radius = 0,
                double side = 0,
                double width = 0,
                double height = 0,
                int? errorCode = null,
                string? errorMessage = null)
            {
                Name = name;
                Radius = radius;
                Side = side;
                Width = width;
                Height = height;
                ErrorCode = errorCode;
                ErrorMessage = errorMessage;
            }

            public override string ToString()
            {
                return $"Name={Name}, Radius={Radius}, Side={Side}, Width={Width}, Height={Height}, ErrorCode={ErrorCode}, ErrorMessage={ErrorMessage}";
            }
        }

        private static readonly IReadOnlyDictionary<ShapeKind, ShapeData> ShapeDefinitions =
            new Dictionary<ShapeKind, ShapeData>
            {
                { ShapeKind.Circle, new ShapeData("circle", radius: 5.0) },
                { ShapeKind.Square, new ShapeData("square", side: 4.0) },
                { ShapeKind.Rectangle, new ShapeData("rectangle", width: 6.0, height: 3.0) },
                { ShapeKind.Error, new ShapeData("error", errorCode: 500, errorMessage: "Internal Server Error") }
            };

        private static ShapeData GetShapeInformation(ShapeKind shape)
        {
            return ShapeDefinitions[shape];
        }

        private static double CalculateShapeArea(ShapeKind shape)
        {
            ShapeData data = GetShapeInformation(shape);

            return shape switch
            {
                ShapeKind.Circle => Math.PI * data.Radius * data.Radius,
                ShapeKind.Square => data.Side * data.Side,
                ShapeKind.Rectangle => data.Width * data.Height,
                ShapeKind.Error => -1,
                _ => 0
            };
        }

        private static void Main()
        {
            Console.WriteLine("PART 1: Enum values with associated data (via lookup table)");
            Console.WriteLine();

            ShapeKind[] shapes = { ShapeKind.Circle, ShapeKind.Square, ShapeKind.Rectangle };

            foreach (ShapeKind shape in shapes)
            {
                ShapeData data = GetShapeInformation(shape);
                double area = CalculateShapeArea(shape);

                Console.WriteLine($"{shape}:");
                Console.WriteLine($"  Data: {data}");
                Console.WriteLine($"  Area: {area:F2}");
            }

            Console.WriteLine();
            Console.WriteLine("PART 2: Accessing data on null / uninitialized enum variable");
            Console.WriteLine();

            Console.WriteLine("Test 1: Safe access using nullable<ShapeKind> and HasValue check:");
            ShapeKind? uninitialized = null;

            try
            {
                if (uninitialized.HasValue)
                {
                    Console.WriteLine($"Data: {GetShapeInformation(uninitialized.Value)}");
                }
                else
                {
                    Console.WriteLine("Variable is null - safe check prevents InvalidOperationException");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.GetType().Name}: {e.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Test 2: Unsafe access using .Value on a null nullable enum:");

            try
            {
                // This line throws InvalidOperationException: Nullable object must have a value.
                ShapeData data = GetShapeInformation(uninitialized!.Value);
                Console.WriteLine($"Data: {data}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"CAUGHT ERROR: {e.GetType().Name}: {e.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Test 3: Accessing enum methods safely:");

            try
            {
                if (!uninitialized.HasValue)
                {
                    Console.WriteLine("Cannot call methods on a null enum variable");
                }
                else
                {
                    Console.WriteLine($"Name: {uninitialized.Value}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.GetType().Name}: {e.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Test 4: Using non-null enum with associated data:");

            ShapeKind? enumVar = ShapeKind.Error;
            if (enumVar.HasValue)
            {
                Console.WriteLine($"Safely retrieved: {GetShapeInformation(enumVar.Value)}");
            }

            Console.WriteLine();
            Console.WriteLine("ANALYSIS:");
            Console.WriteLine("- C# enums cannot directly store per-member data like Python tuples.");
            Console.WriteLine("- Workaround: use a separate data class (ShapeData) and a lookup table.");
            Console.WriteLine("- Nullable<Enum> must be checked with HasValue before .Value access.");
            Console.WriteLine("- Calling .Value on a null nullable enum throws InvalidOperationException at runtime.");
        }
    }
}
