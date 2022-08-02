extends CanvasLayer

onready var animation_player = $AnimationPlayer
onready var black = $Control/Black
onready var control = $Control

func _ready():
	control.visible = false

func fade_out(delay=0):
	control.visible = true
	yield(get_tree().create_timer(delay), "timeout")
	animation_player.play("fade")
	yield(animation_player, "animation_finished")
	pass

func fade_in(delay=0):
	yield(get_tree().create_timer(.5), "timeout")
	animation_player.play_backwards("fade")
	yield(animation_player, "animation_finished")
	control.visible = false

func change_scene(path, delay = 0):
	yield(fade_out(delay), "completed")
	get_tree().change_scene(path)
	yield(fade_in(delay), "completed")


	
	
