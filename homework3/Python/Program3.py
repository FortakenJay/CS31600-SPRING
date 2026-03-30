from enum import Enum
from typing import Optional


class Shape(Enum):
    """
    Enum where each value carries its own data.
    In Python, we can attach arbitrary data to enum members using the functional API
    or by storing tuples/objects as values.
    """
    # Format: name = (value, associated_data)
    CIRCLE = (1, {"shape": "circle", "radius": 5.0})
    SQUARE = (2, {"shape": "square", "side": 4.0})
    RECTANGLE = (3, {"shape": "rectangle", "width": 6.0, "height": 3.0})
    ERROR = (99, {"error_code": 500, "message": "Internal Server Error"})


def get_shape_info(shape: Shape) -> dict:
    """Extract associated data from an enum value."""
    return shape.value[1]  # Second element of the tuple is the data


def calculate_area(shape: Shape) -> float:
    """Calculate area based on the shape's associated data."""
    data = get_shape_info(shape)
    
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
        return -1  # Error case
    return 0


def main():
    print("=" * 70)
    print("PART 1: Enum Values with Associated Data")
    print("=" * 70)
    
    # Demonstrate accessing data from enum values
    for shape in [Shape.CIRCLE, Shape.SQUARE, Shape.RECTANGLE]:
        data = get_shape_info(shape)
        area = calculate_area(shape)
        print(f"{shape.name}:")
        print(f"  Data: {data}")
        print(f"  Area: {area:.2f}")
    
    print("\n" + "=" * 70)
    print("PART 2: Accessing Data on Null/Uninitialized Enum Variable")
    print("=" * 70)
    
    # Test 1: What happens with None?
    print("\nTest 1: Accessing data on None:")
    uninitialized: Optional[Shape] = None
    try:
        if uninitialized is not None:
            print(f"Data: {get_shape_info(uninitialized)}")
        else:
            print("Variable is None - safe check prevents AttributeError")
    except AttributeError as e:
        print(f"ERROR: {type(e).__name__}: {e}")
    
    # Test 2: What if we skip the None check?
    print("\nTest 2: Trying to access data without None check:")
    try:
        # This will fail
        data = get_shape_info(uninitialized)  # type: ignore
        print(f"Data: {data}")
    except AttributeError as e:
        print(f"CAUGHT ERROR: {type(e).__name__}: {e}")
    except TypeError as e:
        print(f"CAUGHT ERROR: {type(e).__name__}: {e}")
    
    # Test 3: Accessing a method on the enum itself vs. the data
    print("\nTest 3: Accessing enum method safely:")
    try:
        if uninitialized is None:
            print("Cannot call methods on None")
        else:
            print(f"Name: {uninitialized.name}")
    except AttributeError as e:
        print(f"ERROR: {type(e).__name__}: {e}")
    
    # Test 4: What Python provides as workaround - Type checking
    print("\nTest 4: Using conditional assignment and type guards:")
    enum_var: Optional[Shape] = Shape.ERROR
    if isinstance(enum_var, Shape):
        print(f"Safely retrieved: {get_shape_info(enum_var)}")
    
    print("\n" + "=" * 70)
    print("ANALYSIS:")
    print("=" * 70)
    print("Python enum with associated data:")
    print("  - Enums store tuples or objects as values, accessing via .value")
    print("  - Methods can extract and work with associated data")
    print("  - None checks and type guards prevent AttributeError on uninitialized vars")
    print("  - Workaround: Use Optional[EnumType] and isinstance() checks")


if __name__ == "__main__":
    main()
