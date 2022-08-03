extends Node2D

func _ready():
	pass

func _on_TextureButton_pressed():
	print("Hint coin pressed")
	$TextureButton.set_normal_texture(load("res://GFX/dialogue_icon.png"))
	$AudioStreamPlayer.play()
	#$AnimationPlayer.visible = true
	$AnimationPlayer.play("pressed")
	pass


func _on_AnimationPlayer_animation_finished(anim_name):
	#$Sprite.visible = false
	$TextureButton.visible = false
	pass
