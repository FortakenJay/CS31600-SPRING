from enum import Enum


class Status(Enum):
    PENDING = 1
    APPROVED = 2
    REJECTED = 3
    
    
    # CANCELLED = 4  






def StatusComplete(status: Status) -> str:

    match status:
        case Status.PENDING:
            return "Status is PENDING - waiting for review"
        case Status.APPROVED:
            return "Status is APPROVED - action taken"
        case Status.REJECTED:
            return "Status is REJECTED - request denied"
        
        
        # case Status.CANCELLED:
        #     return "Status is CANCELLED - withdrawn"




def StatusIncomplete(status: Status) -> str:
    match status:
        case Status.PENDING:
            return "Status is PENDING - waiting for review"
        case Status.APPROVED:
            return "Status is APPROVED - action taken"
        
        # missing REJECTED case
        # case Status.REJECTED:
        #     return "Status is REJECTED - request denied"





# The main function goes through all the values of the Status enum. There are 2 parts, one where the function is complete and goes through all the cases and the other is missing 
# enum REJECTED case. Even if the REJECTED case is missing, the program will still run without any compilation errors because Python does not check for problems during compilation. 
# However, during execution/runtime, Python will raise a RuntimeError when it encounters the missing case because said case is not handled in the StatusIncomplete Function. 
def main():
    print("\n")
    print("PART 1: Exhaustive match")

    
    for status in Status:
        try:
            result = StatusComplete(status)
            print(f"{status.name}: {result}")
        except Exception as e:
            print(f"{status.name}: ERROR - {type(e).__name__}: {e}")
    
    print("\n")
    print("PART 2: Incomplete match)")
    
    for status in Status:
        try:
            result = StatusIncomplete(status)
            print(f"{status.name}: {result}")
        except Exception as e:
            print(f"{status.name}: RUNTIME ERROR - {type(e).__name__}: {e}")
    
    print("\n")

main()
