@tool
class_name AutoPivot
extends Component


# Signals

# Constants
const EditorDescription: String = "Automatically centres the pivot of the parent control.\nYou cannot change the value of the exported string Target, its purpose is to show you which target the AutoPivot is working on."
const IS_PRINTING: bool = false
const IS_PRINTING_DBG: bool = false

# Enums

# Export variables
@export var target: String

# OnReady variables

# Member variables
var parent: Control
var script_name: String = "AutoPivot"


func _ready():
	if Engine.is_editor_hint():
		editor_description = EditorDescription

		_print_dbg("_Ready")
		_on_tree_entered()
		tree_entered.connect(_on_tree_entered)
		_print_dbg("")
	else:
		queue_free()


func _print(what: String):
	if IS_PRINTING:
		print_rich("[color=8b8b8b][" + script_name + "] " + what + " " + "[/color]")


func _print_dbg(what: String):
	if IS_PRINTING_DBG:
		_print(what)


func _on_target_resized():
	parent.pivot_offset = parent.size / 2


func _on_tree_entered():
	if is_node_usable(parent):
		_print_dbg("On_TreeEntered Disconnect")
		
		if parent.resized.is_connected(_on_target_resized):
			parent.resized.disconnect(_on_target_resized)
			_print("Removal of " + parent.name + " as the target!")
	
	parent = find_component_parent(self);
	
	if parent != null:
		target = parent.name
		
		parent.resized.connect(_on_target_resized)
		_on_target_resized()
		_print_dbg("On_TreeEntered Connect")
		_print("Setting " + parent.name + " as the target!")
	else:
		_print("Parent " + parent.name + " is not a Control node!")
