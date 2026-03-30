using System;

namespace Homework3.Csharp
{
    // Task 2: Exhaustive Control Flow with enums
    // Standalone console program mirroring Python Program2.py behavior.
    internal static class Program2
    {
        private enum Status
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3,
            Cancelled = 4
        }

        private static string StatusComplete(Status status) => status switch
        {
            Status.Pending => "Status is PENDING - waiting for review",
            Status.Approved => "Status is APPROVED - action taken",
            Status.Rejected => "Status is REJECTED - request denied",
            Status.Cancelled => "Status is CANCELLED - withdrawn",
            // If a new Status value is added to the enum above and not handled here,
            // this switch expression will cause a compile-time error until updated.
        };

        private static string StatusIncomplete(Status status)
        {
            // Intentionally missing Status.Rejected and Status.Cancelled.
            // C#'s traditional switch statement does NOT require exhaustive handling.
            // Any unhandled values must be caught using a default branch.
            switch (status)
            {
                case Status.Pending:
                    return "Status is PENDING - waiting for review";
                case Status.Approved:
                    return "Status is APPROVED - action taken";

                default:
                    return $"UNHANDLED STATUS: {status} ({(int)status})";
            }
        }

        private static void Main()
        {
            Console.WriteLine();
            Console.WriteLine("PART 1: Exhaustive switch expression");

            foreach (Status status in Enum.GetValues<Status>())
            {
                try
                {
                    string result = StatusComplete(status);
                    Console.WriteLine($"{status}: {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{status}: ERROR - {e.GetType().Name}: {e.Message}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("PART 2: Incomplete switch statement");

            foreach (Status status in Enum.GetValues<Status>())
            {
                try
                {
                    string result = StatusIncomplete(status);
                    Console.WriteLine($"{status}: {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{status}: RUNTIME ERROR - {e.GetType().Name}: {e.Message}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("ANALYSIS:");
            Console.WriteLine("- StatusComplete uses a switch expression that must handle every enum value.");
            Console.WriteLine("  If a new enum member is added and not handled, the code fails to compile.");
            Console.WriteLine("- StatusIncomplete uses a classic switch statement with a default branch.");
            Console.WriteLine("  Adding new enum members without updating the switch still compiles.");
            Console.WriteLine("  At runtime those values fall into 'default' and run silently.");
        }
    }
}
