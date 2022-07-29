extends Node2D

var drawing = false
var active_line = null
var line_width = 6.0
var line_colour = Color.green
var pressed = false
#var active_line


func _on_Area2D_input_event(viewport, event, shape_idx):
	if event is InputEventMouseButton:
		if event.pressed:
			drawing = true
			active_line = Line2D.new()
			active_line.default_color = line_colour
			active_line.width = line_width
			active_line.z_index = 4
			add_child(active_line)
		else:
			drawing = false
	
	if event is InputEventMouseMotion:
		if drawing:
			active_line.add_point(event.position)

func _on_Back_Button_pressed():
	get_tree().paused = false
	self.position.y = -600
	#get_tree().change_scene("res://Puzzles_General/PuzzleRoom.tscn")
	
func _on_Thick_Button_pressed():
	line_width = 12

func _on_Thin_Button_pressed():
	line_width = 6

func _on_Colour_1_Button_pressed():
	line_colour = Color.black

func _on_Colour_2_Button_pressed():
	line_colour = Color.red

func _on_Colour_3_Button_pressed():
	line_colour = Color.green

func _on_Colour_4_Button_pressed():
	line_colour = Color.beige

func _on_Colour_5_Button_pressed():
	line_colour = Color.white

func _on_Colour_6_button_pressed():
	line_colour = Color.blue

func _on_Colour_7_Button_pressed():
	line_colour = Color.orange

func _on_Colour_8_Button_pressed():
	line_colour = Color.brown


func _on_Erase_All_Button_pressed():
	for child in get_children():
		if "@@" in child.name:
			remove_child(child)

func _on_Rubber_Button_pressed():	
	line_colour = Color.white



