extends Node2D


onready var image = $Sprite

func flip():
	image.flip_h = true
