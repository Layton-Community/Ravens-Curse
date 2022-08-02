extends Control

onready var dialogue_text = $VBoxContainer/Dialogue_Box/TextureRect/Dialogue_Text
onready var dialogue_text_tween = $VBoxContainer/Dialogue_Box/Tween
onready var dialogue_icon = $VBoxContainer/Dialogue_Box/DialogueIcon

var language = Global.language
var dialogue_index = 0
var finished = false
var dialogue_speed_constant = 0.013
var press_delay = false
var character
var dialogue_path = "res://Dialogue/Test/Test.json"
var pos_left_coords = Vector2(200,-145)
var pos_right_coords = Vector2(800,-145)
var characters_left = 30

var left_pos_empty = true
var right_pos_empty = true

var dialogue = read_json()

func _ready():
	yield(CHANGE_SCENE.fade_in(), "completed")
	display_dialogue()
	pass

func _process(delta):
	if Input.is_action_just_pressed("ui_accept"):
		if finished == false and press_delay == false:
			dialogue_text_tween.stop(dialogue_text)
			dialogue_text.percent_visible = 1
			_on_Tween_tween_completed(2,3)
			$Timer.start(.8)
			press_delay = true
		elif finished == true and press_delay == false:
			display_dialogue()
	dialogue_icon.visible = finished

func read_json() -> Array:
	var f = File.new()
	assert(f.file_exists(dialogue_path), "File path does not exist")
	f.open(dialogue_path, File.READ)
	var json = f.get_as_text()
	var output = parse_json(json)
	if typeof(output) == TYPE_ARRAY:
		return output
	else:
		return []

func display_dialogue():
	$Timer.start(.8)
	press_delay = true
	if dialogue_index < dialogue.size():
		var dialogue_dict = dialogue[dialogue_index]
		var display_msg = dialogue_dict[language]
		#characters_left = len(display_msg) - dialogue_text.visible_characters
		#print(characters_left)
		character_positions(dialogue_dict["pos"], dialogue_dict["character_display_name"])
		finished = false
		dialogue_text.bbcode_text = display_msg
		dialogue_text.visible_characters = 0
		var dialogue_print_time = len(display_msg) * dialogue_speed_constant
		dialogue_text_tween.interpolate_property(dialogue_text,"visible_characters", 0, len(display_msg), dialogue_print_time, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT)
		dialogue_text_tween.start()
		$VBoxContainer/Speaker/TextureRect/Speaker_Name.bbcode_text = "[center]" + dialogue_dict["character_sprite"] + "[/center]"
	dialogue_index += 1
	
func character_positions(position, name):
	var charater_scene_location = "res://Scenes/Characters/" + name + ".tscn"
	var character_to_spawn = load(charater_scene_location)
	if position == "left":
		for child in get_children():
			if "left_char" in child.name:
				remove_child(child)
		if left_pos_empty == true:
			pass
		character = character_to_spawn.instance()
		character.position = pos_left_coords
		character.name = "left_char"
		pass
	else:
		for child in get_children():
			if "right_char" in child.name:
				remove_child(child)
		character = character_to_spawn.instance()
		character.position = pos_right_coords
		character.name = "right_char"
	add_child(character)

func _on_Tween_tween_completed(object, key):
	finished = true
	pass # Replace with function body.

func _on_Timer_timeout():
	press_delay = false
