using System;
using System.Collections.Generic;

namespace Homework3.Csharp
{
    // Task 3: Extended Enum Behavior and null/uninitialized handling, nullable value types and etc.
    internal static class Program3
    {
        private enum ShapeKind
        {
            Circle = 1,
            Square = 2,
            Rectangle = 3,
            Error = 99
        }

        private record ShapeData(
            string Name,
            double Radius = 0,
            double Side = 0,
            double Width = 0,
            double Height = 0,
            int? ErrorCode = null,
            string? ErrorMessage = null);

        private static readonly IReadOnlyDictionary<ShapeKind, ShapeData> ShapeDefinitions = {
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
            Console.WriteLine("Enum values with associated data (via lookup table)");
            Console.WriteLine();

            ShapeKind[] shapes = { ShapeKind.Circle, ShapeKind.Square, ShapeKind.Rectangle };

            foreach (ShapeKind shape in shapes)
            {
                ShapeData data = GetShapeInformation(shape);
                double area = CalculateShapeArea(shape);

                Console.WriteLine($"{shape}:");
                Console.WriteLine($"\tData: {data}");
                Console.WriteLine($"\tArea: {area:F2}");
            }

            Console.WriteLine();
            Console.WriteLine("Accessing data on null / uninitialized enum variable");
            Console.WriteLine();

            Console.WriteLine("Safe access using Nullable<ShapeKind> and HasValue check:");
            ShapeKind? uninitialized = null;

            try
            {
                if (uninitialized.HasValue)
                {
                    Console.WriteLine($"Data: {GetShapeInformation(uninitialized.Value)}");
                }
                else
                {
                    Console.WriteLine("Variable is null, safe check prevents InvalidOperationException");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.GetType().Name}: {e.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Unsafe access using .Value on a null nullable enum:");

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
            Console.WriteLine("Accessing enum methods safely:");

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
            Console.WriteLine("Using non-null enum with associated data:");

            ShapeKind? enumVar = ShapeKind.Error;
            if (enumVar.HasValue)
            {
                Console.WriteLine($"Safely retrieved: {GetShapeInformation(enumVar.Value)}");
            }
        }
    }
}
