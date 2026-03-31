using System;

namespace Homework3.Csharp
{
    // Task 2: Exhaustive Control Flow with enums
    internal static class Program2
    {
        // C# enum can only be value-types such as int, long, and byte. By default int is used.
        private enum Status
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3,
            Cancelled = 4
        }

        private static string StatusWithSwitchStatementIncomplete(Status status)
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

        private static string StatusWithSwitchExpressionComplete(Status status) => status switch
        {
            Status.Pending => "Status is PENDING",
            Status.Approved => "Status is APPROVED",
            Status.Rejected => "Status is REJECTED",
            Status.Cancelled => "Status is CANCELLED"
            // If a new Status value is added to the enum above and not handled here,
            // this switch expression will cause a compile-time error until updated.
        };

        private static void Main()
        {
            Console.WriteLine();
            Console.WriteLine("Exhaustive with complete Switch Expression:");

            foreach (Status status in Enum.GetValues<Status>())
            {
                string result = StatusWithSwitchExpressionComplete(status);
                
                Console.WriteLine($"\t{status}: {result}");
            }

            Console.WriteLine();
            Console.WriteLine("Exhaustive with incomplete, defaulted Switch Statement:");

            foreach (Status status in Enum.GetValues<Status>())
            {
                string result = StatusWithSwitchStatementIncomplete(status);
                
                Console.WriteLine($"\t{status}: {result}");
            }
        }
    }
}
