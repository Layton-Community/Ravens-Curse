extends Node2D

# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Correct_Button_pressed():
	print("correct")
	Global.puzzle_correct = true
	pass # Replace with function body.


func _on_Incorrect_Button_pressed():
	print("Incorrect")
	Global.current_puzzle_times_wrong += 1
	print(Global.current_puzzle_times_wrong)
	pass # Replace with function body.
