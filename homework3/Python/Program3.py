from enum import Enum
from typing import Optional




class Shape(Enum):
    CIRCLE = (1, {"shape": "circle", "radius": 5.0})
    SQUARE = (2, {"shape": "square", "side": 4.0})
    RECTANGLE = (3, {"shape": "rectangle", "width": 6.0, "height": 3.0})
    ERROR = (99, {"error_code": 500, "message": "Internal Server Error"})


def getShapeInformation(shape: Shape) -> dict:
    return shape.value[1] 


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

    print("\nTest 1: Accessing data on None:")
    uninitialized: Optional[Shape] = None
    try:
        if uninitialized is not None:
            print(f"Data: {getShapeInformation(uninitialized)}")
        else:
            print("Variable is None - safe check prevents AttributeError")
    except AttributeError as e:
        print(f"ERROR: {type(e).__name__}: {e}")
    
    print("\nTest 2: Trying to access data without None check:")
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
    print("ANALYSIS:")
    print("Python enum with associated data:")
    print("  - Enums store tuples or objects as values, accessing via .value")
    print("  - Methods can extract and work with associated data")
    print("  - None checks and type guards prevent AttributeError on uninitialized vars")
    print("  - Workaround: Use Optional[EnumType] and isinstance() checks")



main()
