from enum import Enum

# This initializes an enum class that defines three colors(RED, GREEN, BLUE) with associated integer values (1, 2, 3).
class Color(Enum):
	RED = 1
	GREEN = 2
	BLUE = 3



# This is the main function. Main function demonstrates the usage of enum where the color GREEN was chosen to display the name and value.
# The user can access the name by using chosen_name and the integer value by using chosen_value. 
# Since the enum only has 3 defined values, as soon as we try a value that is not defined in the enum Color, the program will raise a runtime error. 
# The reason why it's a runtime error is because Python has dynamic typing and does not enforce enum membership at compile time.
# The validation only occurs when Color(99) is actually executed, triggering ValueError in the enum constructor. 

def main():
	chosenColor = Color.GREEN
	print(f"Enum variable value: {chosenColor}")
	print(f"Enum variable name: {chosenColor.name}")
	print(f"Enum variable integer value: {chosenColor.value}")

	outOfRange = 99
	print(f"\nAttempting to create Color from integer {outOfRange}...")

	try:
		invalidColor = Color(outOfRange)
		print(f"Created enum: {invalidColor}")
	except ValueError as error:
		print("Python result: Runtime error (ValueError) when converting to enum.")
		print(f"Details: {error}")



main()