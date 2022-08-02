extends Node2D
var dialogue_scene = preload("res://Scenes/Other_Scenes/Dialogue.tscn")
var dialogue_scene_instance = dialogue_scene.instance()
var current_scene
onready var button = $Button

# Called when the node enters the scene tree for the first time.
func _ready():
	current_scene = get_tree().current_scene.filename
	pass # Replace with function body.

func a():
	pass


func _on_Button_pressed():
	print("character clicked") 
	#dialogue_scene_instance.z_index = 200
	yield(CHANGE_SCENE.fade_out(), "completed")
	add_child(dialogue_scene_instance)
	button.disabled = true
	button.visible = false
