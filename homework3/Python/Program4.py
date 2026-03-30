from enum import Enum


class Color(Enum):
    """
    Standard enum for basic iteration/introspection testing.
    """
    RED = 1
    GREEN = 2
    BLUE = 3


class DuplicateEnum(Enum):
    """
    Attempt to assign the same integer to two enum values by name.
    In Python, this creates an ALIAS, not a separate value.
    """
    FIRST = 1
    SECOND = 2
    # Attempting to assign the same value to a new name
    ALIAS_TO_FIRST = 1  # This becomes an alias, not a new member


def print_enum_without_hardcoding(enum_class: type) -> None:
    """
    Iterate over all enum values without hardcoding them individually.
    This demonstrates Python's built-in introspection support.
    """
    print(f"All values in {enum_class.__name__}:")
    for member in enum_class:
        print(f"  {member.name} = {member.value}")


def main():
    print("=" * 70)
    print("PART 1: Iteration Without Hardcoding")
    print("=" * 70)
    
    print("\nIterating over Color enum:")
    print_enum_without_hardcoding(Color)
    
    print("\n" + "=" * 70)
    print("PART 2: Duplicate Integer Assignment (Aliases)")
    print("=" * 70)
    
    print("\nIterating over DuplicateEnum:")
    print_enum_without_hardcoding(DuplicateEnum)
    
    print("\nDetailed analysis of DuplicateEnum members:")
    for member in DuplicateEnum:
        print(f"  {member.name} = {member.value}")
    
    print("\nDirect access checks:")
    print(f"  DuplicateEnum.FIRST = {DuplicateEnum.FIRST}")
    print(f"  DuplicateEnum.SECOND = {DuplicateEnum.SECOND}")
    print(f"  DuplicateEnum.ALIAS_TO_FIRST = {DuplicateEnum.ALIAS_TO_FIRST}")
    
    print("\nAre FIRST and ALIAS_TO_FIRST the same object?")
    print(f"  FIRST is ALIAS_TO_FIRST: {DuplicateEnum.FIRST is DuplicateEnum.ALIAS_TO_FIRST}")
    
    print("\nAttempting iteration - what appears?")
    print("Members when iterating:")
    all_members = list(DuplicateEnum)
    for i, member in enumerate(all_members, 1):
        print(f"  {i}. {member.name} = {member.value}")
    
    print(f"\nTotal members iterated: {len(all_members)}")
    print("Note: ALIAS_TO_FIRST does NOT appear in iteration - it's hidden as an alias!")
    
    print("\nCan we access the alias by name directly?")
    try:
        alias = DuplicateEnum.ALIAS_TO_FIRST
        print(f"  Yes: DuplicateEnum.ALIAS_TO_FIRST = {alias}")
    except AttributeError as e:
        print(f"  No: {e}")
    
    print("\n" + "=" * 70)
    print("PART 3: Code Complexity Comparison (Python vs explicit iteration)")
    print("=" * 70)
    
    print("\nPython built-in iteration (elegant, 1 line of logic):")
    print("  for member in ColorEnum:")
    print("      print(member.name, member.value)")
    
    print("\nWithout enum support (what we'd need to do):")
    print("  colors = [('RED', 1), ('GREEN', 2), ('BLUE', 3)]")
    print("  for name, value in colors:")
    print("      print(name, value)")
    print("  # Plus manual synchronization needed if enum changes!")
    
    print("\n" + "=" * 70)
    print("ANALYSIS:")
    print("=" * 70)
    print("Python iteration and introspection:")
    print("  - Built-in: 'for member in EnumClass' works immediately")
    print("  - Accessing members: member.name and member.value available")
    print("  - Duplicate integer assignment: Creates ALIASES, not separate values")
    print("  - Aliases: Appear only as attributes, hidden from iteration")
    print("  - Can access aliases by name: DuplicateEnum.ALIAS_TO_FIRST works")
    print("  - Iteration count: Only counts unique enum members (excludes aliases)")
    print("  - Code efficiency: Much shorter than manual list management")


if __name__ == "__main__":
    main()
