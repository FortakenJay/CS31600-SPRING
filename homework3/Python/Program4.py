from enum import Enum

#This is a normal enum where there are 3 different names and different values. 
class Color(Enum):
    RED = 1
    GREEN = 2
    BLUE = 3

#This is a different enum where there are 3 different names but only 2 different values since ALIAS_TO_FIRST 
# has the same value as FIRST.
class DuplicateEnum(Enum):
    FIRST = 1
    SECOND = 2
    ALIAS_TO_FIRST = 1  

# This is a function where it takes an enum class as an argument and iterates over all the members
# and prints the values using the built-in __name__ and __value__ attributes of the enum members. 
def printEnumWithoutHardcoding(enumClass: type) -> None:
    print(f"All values in {enumClass.__name__}:")
    for member in enumClass:
        print(f"  {member.name} = {member.value}")


# main function evaluates the program and tests for many cases. The first case is normal and nothing special,
# part 1 just prints the values of Color Enum. 
# part 2 tests for the duplicate cases since duplicate alias is a special case in enums,
# where it tries to print all the values of the duplicate enum but prints 2 values instead of 3 
# this is because DuplicateEnum is considered to have only 2 unique values since FIRST and ALIAS_TO_FIRST are the same value.
# then we also tried to call the function directly with the alias name and it works since it's just an alias to the first value.
# Then we checked if FIRST and the duplicate alias are the same object and it returns true, meaning that they are the same member in the enum.
# Lastly we checked for how many mombers it appear and which members appear and it only shows FIRST and SECOND but not the alias since it's hidden as an alias.
# However we can still access the alias by the name directly but will return the same value as First. 
# 
def main():

    print("PART 1: Iteration Without Hardcoding")
    
    print("\nIterating over Color enum:")
    printEnumWithoutHardcoding(Color)
    
    print("\n")
    print("PART 2: Duplicate Alias of Existing Enum Values")
    
    print("\nIterating over DuplicateEnum:")
    printEnumWithoutHardcoding(DuplicateEnum)
    
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
    
    print("\n")

main()