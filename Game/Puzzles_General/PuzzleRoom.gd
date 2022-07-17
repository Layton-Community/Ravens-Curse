extends Node2D

var puzzle_scene = preload("res://Puzzles_General/Puzzles/Puzzle_2.tscn")
var puzzle = puzzle_scene.instance()
var puzzle_number = Global.current_puzzle
onready var picarats_number = $HBoxContainer/Picarats_Number
onready var video_player = $VideoPlayer
onready var memo = $Memo

var start_picarats = Global.puzzle_max_picarats[puzzle_number]
var picarats = start_picarats
var times_wrong = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	#var puzzle = puzzle_scene.instance()
	puzzle.position = Vector2(358, 51) 
	add_child(puzzle)
	video_player.visible = false

var x = false

func _process(delta):
	if x == true:
		x = false
		puzzle_correct()
	picarats_reduction()
	times_wrong = Global.current_puzzle_times_wrong
	puzzle_correct()

func puzzle_correct():
	if Global.puzzle_correct == true:
		Global.puzzle_correct = false
		video_player.visible = true
		video_player.play()
		Global.picarats += picarats
		remove_child(puzzle)

func picarats_reduction():
	if times_wrong == 1:
		picarats = start_picarats * 0.5
	if times_wrong >= 2:
		picarats = start_picarats * 0.25
	picarats_number.text = str(picarats)

func _on_Reset_Button_pressed():
	remove_child(puzzle)
	add_child(puzzle)


func _on_Quit_Button_pressed():
	get_tree().quit()

func _on_Memo_Button_pressed():
	get_tree().paused = true
	memo.position.y = 0

