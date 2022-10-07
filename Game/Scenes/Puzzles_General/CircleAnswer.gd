extends Node2D

var pressed=false;
var active_line;
var startpoint=Vector2(0,0);
var endpoint=Vector2(0,0);
var distance;
var answer_correct:int; #since we need 1 right, 2 wrong and 0 undecided
func _ready():
	active_line=Line2D.new();
	active_line.default_color = Color("#fcba03"); #color not final
	active_line.width=10.0;

func _on_Drawable_Area_input_event(viewport, event, shape_idx):
	if event is InputEventMouseButton and event.is_pressed(): #activate line drawing only while button is pressed
		reset()
		pressed=true;
		startpoint=get_viewport().get_mouse_position();
		endpoint=startpoint;#ensure the distance is not calculated to the origin
		pass
	elif event is InputEventMouseButton and event.is_pressed() == false: #stop drawing
		pressed=false;
		check_answer();
	if event is InputEventMouseMotion and pressed==true:
		var currentpoint=get_viewport().get_mouse_position();
		active_line.add_point(event.position);
		add_child(active_line);
		distance=(currentpoint-startpoint).length();#get distance from start to current pos
		if distance>(endpoint-startpoint).length():#if distance is greater than the old one, overwrite
			endpoint=currentpoint;
			$"Pointer".set_position(startpoint+((endpoint-startpoint)/2))


func _on_CorrectAnswer_area_entered(area): 
	#####DrawableArea is on a different Collision Layer so it doesnt trip these checks
	answer_correct=1;


func _on_WrongAnswer_area_entered(area):
	answer_correct=2;

func reset():
	answer_correct=0;
	startpoint=Vector2(0,0);
	endpoint=Vector2(0,0);
	distance=0;
	print("reset")
	active_line.points=[]; #clear all point in the line, erase it

func check_answer():
	if answer_correct==1:
		print("correct");
	elif answer_correct==2:
		print("false");
	else:
		print("Redraw Circle")
	pass
