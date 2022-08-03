extends ColorPicker


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	# Hides the bars of the colour picker
	get_child(4).hide()
	get_child(1).hide()
	self.rect_scale.y = .4

