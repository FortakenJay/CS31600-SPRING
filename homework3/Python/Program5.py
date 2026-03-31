from enum import Enum
from typing import Optional

# THis is an enum that tracks different Order Statuses of an e-commerce platform. 
# These are predefined by the client and/or business rules
class OrderStatus(Enum):

    PENDING = "pending"
    CONFIRMED = "confirmed"
    PROCESSING = "processing"
    SHIPPED = "shipped"
    DELIVERED = "delivered"
    CANCELLED = "cancelled"
    REFUNDED = "refunded"

# This class defines what an Order i and how it behaves. First we build a constructor of what can an order have
# then, we create a chart that defines what can a status transition so this means that each orderstatus is dependent on
# other orderstatuses. Then we created a function that transition the status of the order to the new status depending on the chart and user. 
# There is also another function that gives a predetermined message to the user depending on the order status. 
# Finally, we have a function that displays the order status and other information about the order.
class Order:
 
    def __init__(self, orderID: str, items: list, total: float):
        self.orderID = orderID
        self.items = items
        self.total = total
        self.status: OrderStatus = OrderStatus.PENDING
        self.orderTrackingNumber: Optional[str] = None
    
    def orderTransitionChart(self,newOrderStatus: OrderStatus) -> bool:
       
        validTransitions = {
            OrderStatus.PENDING: [OrderStatus.CONFIRMED, OrderStatus.CANCELLED],
            OrderStatus.CONFIRMED: [OrderStatus.PROCESSING, OrderStatus.CANCELLED],
            OrderStatus.PROCESSING: [OrderStatus.SHIPPED, OrderStatus.CANCELLED],
            OrderStatus.SHIPPED: [OrderStatus.DELIVERED],
            OrderStatus.DELIVERED: [OrderStatus.REFUNDED],
            OrderStatus.CANCELLED: [],  
            OrderStatus.REFUNDED: [],   
        }
        
        allowedNextStates = validTransitions.get(self.status, [])
        return newOrderStatus in allowedNextStates
    
    def transitionFunction(self, newOrderStatus: OrderStatus) -> bool:
      
        if not self.orderTransitionChart(newOrderStatus):
            return False
        if newOrderStatus == OrderStatus.CONFIRMED:
            print(f"  → Sending confirmation email for order {self.orderID}")
        elif newOrderStatus == OrderStatus.PROCESSING:
            print(f"  → Processing payment and preparing items")
        elif newOrderStatus == OrderStatus.SHIPPED:
            self.orderTrackingNumber = f"TRACK-{self.orderID}-{id(self)}"
            print(f"  → Order shipped! Tracking: {self.orderTrackingNumber}")
        elif newOrderStatus == OrderStatus.DELIVERED:
            print(f"  → Order delivery confirmed, thank you!")
        elif newOrderStatus == OrderStatus.REFUNDED:
            print(f"  → Refund processed: ${self.total:.2f}")
        elif newOrderStatus == OrderStatus.CANCELLED:
            print(f"  → Order cancelled")
        
        self.status = newOrderStatus
        return True
    
    def getCustomerMessage(self) -> str:
        
        messages = {
            OrderStatus.PENDING: "Your order is pending. Please confirm.",
            OrderStatus.CONFIRMED: "Order confirmed! We'll process it soon.",
            OrderStatus.PROCESSING: "Your order is being prepared...",
            OrderStatus.SHIPPED: f"Your order shipped! Tracking: {self.orderTrackingNumber}",
            OrderStatus.DELIVERED: "Your order was delivered. Leave a review!",
            OrderStatus.CANCELLED: "Your order has been cancelled.",
            OrderStatus.REFUNDED: f"Your refund of ${self.total:.2f} has been processed.",
        }
        return messages.get(self.status, "Unknown status")
    
    def display_status(self) -> None:
       
        print(f"Order {self.orderID}: {self.status.value.upper()}")
        print(f"  Items: {', '.join(self.items)}")
        print(f"  Total: ${self.total:.2f}")
        print(f"  Message: {self.getCustomerMessage()}")



# The main function creates a predetermined order with information, these are just examples. 
# In main the order goes through different steps of the processing pipeline where it emulates a real processing of an order.
# an order is created -> changes from pending to confirmed -> processing -> shipped -> delivered -> refunded. 
# Then we try to make an invalid transition and we see that it is not possible. 
# Finally, we create another order and we try to cancel it.
# So, the main idea is that without enum, the logic has to be different since we would have to write more code to check for the different states
# Also, we would have to write many ways to safely transition between states, but with enums we can just define the valid
# states that a state can transition to
# Removing enum will overall lead to a huge refactoring that it can be avoided with the use of enums. 
def main():
    print("REAL-WORLD ENUM APPLICATION: Order Processing Pipeline")
    print("\nDemonstrating how enum values drive the logic:\n")
    
    order = Order(
        orderID="ORD-12345",
        items=["Laptop", "Mouse", "Keyboard"],
        total=1299.99
    )
    
    print(f"Created order {order.orderID}")
    order.display_status()

    transitionToTry = [
        OrderStatus.CONFIRMED,
        OrderStatus.PROCESSING,
        OrderStatus.SHIPPED,
        OrderStatus.DELIVERED,
        OrderStatus.REFUNDED,
    ]
    
    print("\n" )
    print("Processing Valid State Transitions:")
    
    for target_status in transitionToTry:
        print(f"\nAttempt transition: {order.status.value} → {target_status.value}")
        if order.transitionFunction(target_status):
            print(f"✓ Transition successful")
        else:
            print(f"✗ Invalid transition")
        order.display_status()
    
  
    print("\n" )
    print("Attempting Invalid Transition:")
    
    print(f"\nOrder is in state: {order.status.value}")
    print(f"Attempt transition: {order.status.value} → {OrderStatus.PROCESSING.value}")
    if order.transitionFunction(OrderStatus.PROCESSING):
        print(f"Transition successful")
    else:
        print(f"Invalid transition (already delivered/refunded)")
    
    print("\n" )
    print("Alternative Path: Order Cancellation")
    
    order2 = Order(
        orderID="ORD-54321",
        items=["Book"],
        total=29.99
    )
    
    print(f"\nCreated order {order2.orderID}")
    order2.display_status()
    
    cancel_path = [
        OrderStatus.CONFIRMED,
        OrderStatus.CANCELLED,
    ]
    
    for target_status in cancel_path:
        print(f"\nAttempt transition: {order2.status.value} → {target_status.value}")
        if order2.transitionFunction(target_status):
            print(f"Transition successful")
        else:
            print(f"Invalid transition")
    
    print("\n" )


main()
