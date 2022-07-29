extends Control

var player_name = ""
onready var textbox = $Background/Textbox

func _ready():
	pass 

func _on_Button_Confirm_pressed():
	player_name = textbox.get_text()
	print(player_name)
	print("To implement next sections")
	pass # Replace with function body.
