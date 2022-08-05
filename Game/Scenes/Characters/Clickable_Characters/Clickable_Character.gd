extends Node2D
var dialogue_scene = preload("res://Scenes/Other_Scenes/Dialogue.tscn")
var dialogue_scene_instance = dialogue_scene.instance()
var current_scene
onready var button = $Button

# Called when the node enters the scene tree for the first time.
func _ready():
	current_scene = get_tree().current_scene.filename
	pass # Replace with function body.

func _on_Button_pressed():
	yield(CHANGE_SCENE.fade_out(), "completed")
	add_child(dialogue_scene_instance)
	button.visible = false
