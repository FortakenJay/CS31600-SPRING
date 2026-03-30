using System;
using System.Collections.Generic;

namespace Homework3.Csharp
{
    // Task 5: Applied Enumeration - Order processing pipeline
    // Standalone console program mirroring Python Program5.py behavior.
    internal static class Program5
    {
        private enum OrderStatus
        {
            Pending,
            Confirmed,
            Processing,
            Shipped,
            Delivered,
            Cancelled,
            Refunded
        }

        private sealed class Order
        {
            public string OrderId { get; }
            public IReadOnlyList<string> Items { get; }
            public decimal Total { get; }
            public OrderStatus Status { get; private set; }
            public string? TrackingNumber { get; private set; }

            public Order(string orderId, IReadOnlyList<string> items, decimal total)
            {
                OrderId = orderId;
                Items = items;
                Total = total;
                Status = OrderStatus.Pending;
            }

            public bool CanTransitionTo(OrderStatus newStatus)
            {
                // Enum-driven state machine for orders.
                var validTransitions = new Dictionary<OrderStatus, OrderStatus[]>
                {
                    { OrderStatus.Pending,   new[] { OrderStatus.Confirmed, OrderStatus.Cancelled } },
                    { OrderStatus.Confirmed, new[] { OrderStatus.Processing, OrderStatus.Cancelled } },
                    { OrderStatus.Processing,new[] { OrderStatus.Shipped, OrderStatus.Cancelled } },
                    { OrderStatus.Shipped,   new[] { OrderStatus.Delivered } },
                    { OrderStatus.Delivered, new[] { OrderStatus.Refunded } },
                    { OrderStatus.Cancelled, Array.Empty<OrderStatus>() },
                    { OrderStatus.Refunded,  Array.Empty<OrderStatus>() }
                };

                OrderStatus[] allowedNextStates = validTransitions.TryGetValue(Status, out OrderStatus[]? allowed)
                    ? allowed
                    : Array.Empty<OrderStatus>();

                foreach (OrderStatus candidate in allowedNextStates)
                {
                    if (candidate == newStatus)
                    {
                        return true;
                    }
                }

                return false;
            }

            public bool TransitionTo(OrderStatus newStatus)
            {
                if (!CanTransitionTo(newStatus))
                {
                    return false;
                }

                // Enum-specific side effects
                switch (newStatus)
                {
                    case OrderStatus.Confirmed:
                        Console.WriteLine($"  \\u2192 Sending confirmation email for order {OrderId}");
                        break;
                    case OrderStatus.Processing:
                        Console.WriteLine("  \\u2192 Processing payment and preparing items");
                        break;
                    case OrderStatus.Shipped:
                        TrackingNumber = $"TRACK-{OrderId}-{GetHashCode()}";
                        Console.WriteLine($"  \\u2192 Order shipped! Tracking: {TrackingNumber}");
                        break;
                    case OrderStatus.Delivered:
                        Console.WriteLine("  \\u2192 Order delivery confirmed, thank you!");
                        break;
                    case OrderStatus.Refunded:
                        Console.WriteLine($"  \\u2192 Refund processed: ${Total:F2}");
                        break;
                    case OrderStatus.Cancelled:
                        Console.WriteLine("  \\u2192 Order cancelled");
                        break;
                }

                Status = newStatus;
                return true;
            }

            public string GetCustomerMessage()
            {
                return Status switch
                {
                    OrderStatus.Pending => "Your order is pending. Please confirm.",
                    OrderStatus.Confirmed => "Order confirmed! We'll process it soon.",
                    OrderStatus.Processing => "Your order is being prepared...",
                    OrderStatus.Shipped => $"Your order shipped! Tracking: {TrackingNumber}",
                    OrderStatus.Delivered => "Your order was delivered. Leave a review!",
                    OrderStatus.Cancelled => "Your order has been cancelled.",
                    OrderStatus.Refunded => $"Your refund of ${Total:F2} has been processed.",
                    _ => "Unknown status"
                };
            }

            public void DisplayStatus()
            {
                Console.WriteLine($"Order {OrderId}: {Status.ToString().ToUpperInvariant()}");
                Console.WriteLine($"  Items: {string.Join(", ", Items)}");
                Console.WriteLine($"  Total: ${Total:F2}");
                Console.WriteLine($"  Message: {GetCustomerMessage()}");
            }
        }

        private static void Main()
        {
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("REAL-WORLD ENUM APPLICATION: Order Processing Pipeline");
            Console.WriteLine(new string('=', 70));
            Console.WriteLine();
            Console.WriteLine("Demonstrating how enum values drive the logic:\n");

            var order = new Order(
                orderId: "ORD-12345",
                items: new[] { "Laptop", "Mouse", "Keyboard" },
                total: 1299.99m);

            Console.WriteLine($"Created order {order.OrderId}");
            order.DisplayStatus();

            var transitionsToTry = new[]
            {
                OrderStatus.Confirmed,
                OrderStatus.Processing,
                OrderStatus.Shipped,
                OrderStatus.Delivered,
                OrderStatus.Refunded
            };

            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("Processing Valid State Transitions:");
            Console.WriteLine(new string('=', 70));

            foreach (OrderStatus targetStatus in transitionsToTry)
            {
                Console.WriteLine();
                Console.WriteLine($"Attempt transition: {order.Status} \\u2192 {targetStatus}");
                bool ok = order.TransitionTo(targetStatus);
                Console.WriteLine(ok ? "\\u2713 Transition successful" : "\\u2717 Invalid transition");
                order.DisplayStatus();
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("Attempting Invalid Transition:");
            Console.WriteLine(new string('=', 70));

            Console.WriteLine();
            Console.WriteLine($"Order is in state: {order.Status}");
            Console.WriteLine($"Attempt transition: {order.Status} \\u2192 {OrderStatus.Processing}");
            bool invalid = order.TransitionTo(OrderStatus.Processing);
            Console.WriteLine(invalid ? "\\u2713 Transition successful" : "\\u2717 Invalid transition (already delivered/refunded)");

            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("Alternative Path: Order Cancellation");
            Console.WriteLine(new string('=', 70));

            var order2 = new Order(
                orderId: "ORD-54321",
                items: new[] { "Book" },
                total: 29.99m);

            Console.WriteLine();
            Console.WriteLine($"Created order {order2.OrderId}");
            order2.DisplayStatus();

            var cancelPath = new[]
            {
                OrderStatus.Confirmed,
                OrderStatus.Cancelled
            };

            foreach (OrderStatus targetStatus in cancelPath)
            {
                Console.WriteLine();
                Console.WriteLine($"Attempt transition: {order2.Status} \\u2192 {targetStatus}");
                bool ok = order2.TransitionTo(targetStatus);
                Console.WriteLine(ok ? "\\u2713 Transition successful" : "\\u2717 Invalid transition");
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("WHY ENUMS ARE ESSENTIAL HERE:");
            Console.WriteLine(new string('=', 70));
            Console.WriteLine();
            Console.WriteLine("1. Type Safety:");
            Console.WriteLine("   - Only valid OrderStatus values are allowed; no typos like 'shippped'.");
            Console.WriteLine();
            Console.WriteLine("2. Transition Rules Enforced:");
            Console.WriteLine("   - Valid paths (e.g., Shipped \\u2192 Delivered) are encoded in a single place.");
            Console.WriteLine("   - Impossible transitions are rejected by CanTransitionTo/TransitionTo.");
            Console.WriteLine();
            Console.WriteLine("3. Status-Specific Behavior:");
            Console.WriteLine("   - Each enum value triggers distinct actions (emails, tracking numbers, refunds).");
            Console.WriteLine();
            Console.WriteLine("4. Refactoring Impact:");
            Console.WriteLine("   - Replacing the enum with strings/integers would remove type safety,");
            Console.WriteLine("     duplicate status values could appear, and transition logic would be fragile.");
            Console.WriteLine();
            Console.WriteLine("Without enums, this order pipeline would be much harder to maintain and reason about.");
        }
    }
}
