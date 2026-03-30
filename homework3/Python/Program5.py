from enum import Enum
from typing import Optional


class OrderStatus(Enum):
    """
    Real-world enum: Order processing pipeline.
    Enums drive the logic - removing them would require major redesign.
    Each status has behavior, validation rules, and transitions.
    """
    PENDING = "pending"
    CONFIRMED = "confirmed"
    PROCESSING = "processing"
    SHIPPED = "shipped"
    DELIVERED = "delivered"
    CANCELLED = "cancelled"
    REFUNDED = "refunded"


class Order:
    """
    Order object that uses enum for state management.
    Logic depends entirely on the enum value.
    """
    def __init__(self, order_id: str, items: list, total: float):
        self.order_id = order_id
        self.items = items
        self.total = total
        self.status: OrderStatus = OrderStatus.PENDING
        self.tracking_number: Optional[str] = None
    
    def can_transition_to(self, new_status: OrderStatus) -> bool:
        """
        Validate status transitions based on enum values.
        This logic is enum-driven and cannot be replaced with strings/integers.
        """
        valid_transitions = {
            OrderStatus.PENDING: [OrderStatus.CONFIRMED, OrderStatus.CANCELLED],
            OrderStatus.CONFIRMED: [OrderStatus.PROCESSING, OrderStatus.CANCELLED],
            OrderStatus.PROCESSING: [OrderStatus.SHIPPED, OrderStatus.CANCELLED],
            OrderStatus.SHIPPED: [OrderStatus.DELIVERED],
            OrderStatus.DELIVERED: [OrderStatus.REFUNDED],
            OrderStatus.CANCELLED: [],  # Terminal state
            OrderStatus.REFUNDED: [],   # Terminal state
        }
        
        allowed_next_states = valid_transitions.get(self.status, [])
        return new_status in allowed_next_states
    
    def transition_to(self, new_status: OrderStatus) -> bool:
        """
        Attempt to move the order to a new status.
        Uses enum-driven validation and enum-specific actions.
        """
        if not self.can_transition_to(new_status):
            return False
        
        # Enum-specific side effects
        if new_status == OrderStatus.CONFIRMED:
            print(f"  → Sending confirmation email for order {self.order_id}")
        elif new_status == OrderStatus.PROCESSING:
            print(f"  → Processing payment and preparing items")
        elif new_status == OrderStatus.SHIPPED:
            self.tracking_number = f"TRACK-{self.order_id}-{id(self)}"
            print(f"  → Order shipped! Tracking: {self.tracking_number}")
        elif new_status == OrderStatus.DELIVERED:
            print(f"  → Order delivery confirmed, thank you!")
        elif new_status == OrderStatus.REFUNDED:
            print(f"  → Refund processed: ${self.total:.2f}")
        elif new_status == OrderStatus.CANCELLED:
            print(f"  → Order cancelled")
        
        self.status = new_status
        return True
    
    def get_customer_message(self) -> str:
        """
        Generate status-specific customer message using enum.
        Each enum value maps to unique user-facing logic.
        """
        messages = {
            OrderStatus.PENDING: "Your order is pending. Please confirm.",
            OrderStatus.CONFIRMED: "Order confirmed! We'll process it soon.",
            OrderStatus.PROCESSING: "Your order is being prepared...",
            OrderStatus.SHIPPED: f"Your order shipped! Tracking: {self.tracking_number}",
            OrderStatus.DELIVERED: "Your order was delivered. Leave a review!",
            OrderStatus.CANCELLED: "Your order has been cancelled.",
            OrderStatus.REFUNDED: f"Your refund of ${self.total:.2f} has been processed.",
        }
        return messages.get(self.status, "Unknown status")
    
    def display_status(self) -> None:
        """Show current order state."""
        print(f"Order {self.order_id}: {self.status.value.upper()}")
        print(f"  Items: {', '.join(self.items)}")
        print(f"  Total: ${self.total:.2f}")
        print(f"  Message: {self.get_customer_message()}")


def main():
    print("=" * 70)
    print("REAL-WORLD ENUM APPLICATION: Order Processing Pipeline")
    print("=" * 70)
    print("\nDemonstrating how enum values drive the logic:\n")
    
    # Create an order
    order = Order(
        order_id="ORD-12345",
        items=["Laptop", "Mouse", "Keyboard"],
        total=1299.99
    )
    
    print(f"Created order {order.order_id}")
    order.display_status()
    
    # Process the order through valid state transitions
    transitions_to_try = [
        OrderStatus.CONFIRMED,
        OrderStatus.PROCESSING,
        OrderStatus.SHIPPED,
        OrderStatus.DELIVERED,
        OrderStatus.REFUNDED,
    ]
    
    print("\n" + "=" * 70)
    print("Processing Valid State Transitions:")
    print("=" * 70)
    
    for target_status in transitions_to_try:
        print(f"\nAttempt transition: {order.status.value} → {target_status.value}")
        if order.transition_to(target_status):
            print(f"✓ Transition successful")
        else:
            print(f"✗ Invalid transition")
        order.display_status()
    
    # Try invalid transition
    print("\n" + "=" * 70)
    print("Attempting Invalid Transition:")
    print("=" * 70)
    
    print(f"\nOrder is in state: {order.status.value}")
    print(f"Attempt transition: {order.status.value} → {OrderStatus.PROCESSING.value}")
    if order.transition_to(OrderStatus.PROCESSING):
        print(f"✓ Transition successful")
    else:
        print(f"✗ Invalid transition (already delivered/refunded)")
    
    # Create another order to show a cancellation path
    print("\n" + "=" * 70)
    print("Alternative Path: Order Cancellation")
    print("=" * 70)
    
    order2 = Order(
        order_id="ORD-54321",
        items=["Book"],
        total=29.99
    )
    
    print(f"\nCreated order {order2.order_id}")
    order2.display_status()
    
    cancel_path = [
        OrderStatus.CONFIRMED,
        OrderStatus.CANCELLED,
    ]
    
    for target_status in cancel_path:
        print(f"\nAttempt transition: {order2.status.value} → {target_status.value}")
        if order2.transition_to(target_status):
            print(f"✓ Transition successful")
        else:
            print(f"✗ Invalid transition")
    
    print("\n" + "=" * 70)
    print("WHY ENUMS ARE ESSENTIAL HERE:")
    print("=" * 70)
    print("""
1. Type Safety: 
   - Only valid OrderStatus values allowed, no typos like "shippped"
   
2. Transition Rules Enforced:
   - Can only move through valid states, e.g., SHIPPED → DELIVERED
   - Cannot jump directly from PENDING → DELIVERED
   - Enum drives the validation logic
   
3. Status-Specific Behavior:
   - Each enum value triggers unique actions (emails, tracking numbers, refunds)
   - Impossible to implement without explicit enum handling
   
4. Refactoring Impact:
   - Removing enum would require:
     * Replacing all status checks with string comparisons
     * Manually managing valid transitions as separate rules
     * Type safety lost - any string could be a status
     * Major logic rewrite to map behavior to status values
   
Without enums, this would be fragile and error-prone code.
    """)


if __name__ == "__main__":
    main()
