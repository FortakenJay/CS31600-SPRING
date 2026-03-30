from enum import Enum
from typing import Optional



# Since Python does not have a way to directly write name = value,value
# We did a tuple to handle multiple values and inside, we did a dictionary to store the shape's information
class Shape(Enum):
    CIRCLE = (1, {"shape": "circle", "radius": 5.0})
    SQUARE = (2, {"shape": "square", "side": 4.0})
    RECTANGLE = (3, {"shape": "rectangle", "width": 6.0, "height": 3.0})
    ERROR = (99, {"error_code": 500, "message": "Internal Server Error"})

#this is a get function that takes the shape enum and return the data of the shape by getting shape.value[1]
def getShapeInformation(shape: Shape) -> dict:
    return shape.value[1] 

#this calculates the area of the shape based on the shape's information and returns the area. Otherwise, it will return 1 if the shape is ERROR. 
#first this calls getShapeInformation, then it checks the shape type and lastly it calculates the area based on the type of enum. 
def calculateShapeArea(shape: Shape) -> float:
    data = getShapeInformation(shape)
    
    if shape == Shape.CIRCLE:
        radius = data.get("radius", 0)
        return 3.14159 * (radius ** 2)
    elif shape == Shape.SQUARE:
        side = data.get("side", 0)
        return side ** 2
    elif shape == Shape.RECTANGLE:
        width = data.get("width", 0)
        height = data.get("height", 0)
        return width * height
    elif shape == Shape.ERROR:
        return -1  
    return 0

# The main function executes the program and tests for many cases.
# In part 1, the basic of the program is tested and passed by ensuring all the appropriate data is printed and area calculated.
# In part 2 we tests the data access by using a library in Python that allows us to declare a variable 
# that can be None or a Shape. This way we can test for None and safely return something or get a shape and return the shape
# The other test is to try to access the data without checking for None, so the console will return an error at runtime. 
# For test 3 is just another way to test for None before accessing the data. 
# Test 4 shows how we can safely guard check by using isinstance to check if the variable is of type Shape 
# before trying to access the data. This way we are strictly getting shape and nothing else. 
def main():
    print("PART 1: Enum Values with Associated Data")
    print("\n")
   
    for shape in [Shape.CIRCLE, Shape.SQUARE, Shape.RECTANGLE]:
        data = getShapeInformation(shape)
        area = calculateShapeArea(shape)
        print(f"{shape.name}:")
        print(f"  Data: {data}")
        print(f"  Area: {area:.2f}")
    
    print("\n")
    print("PART 2: Accessing Data on Null/Uninitialized Enum Variable")

    print("\n Test 1: Accessing data on None:")
    uninitialized: Optional[Shape] = None
    try:
        if uninitialized is not None:
            print(f"Data: {getShapeInformation(uninitialized)}")
        else:
            print("Variable is None - safe check prevents AttributeError")
    except AttributeError as e:
        print(f"ERROR: {type(e).__name__}: {e}")
    
    print("\n Test 2: Trying to access data without None check:")
    try:
        
        data = getShapeInformation(uninitialized)  
        print(f"Data: {data}")
    except AttributeError as e:
        print(f"CAUGHT ERROR: {type(e).__name__}: {e}")
    except TypeError as e:
        print(f"CAUGHT ERROR: {type(e).__name__}: {e}")
    
    
    print("\nTest 3: Accessing enum method safely:")
    try:
        if uninitialized is None:
            print("Cannot call methods on None")
        else:
            print(f"Name: {uninitialized.name}")
    except AttributeError as e:
        print(f"ERROR: {type(e).__name__}: {e}")
    
   
    print("\nTest 4: Using conditional assignment and type guards:")
    enum_var: Optional[Shape] = Shape.ERROR
    if isinstance(enum_var, Shape):
        print(f"Safely retrieved: {getShapeInformation(enum_var)}")
    
    print("\n")

main()
