@icon("res://a_sprites/icons/node_custom.svg")
class_name NodeGDS
extends Node


func find_component_parent(component: Node) -> Node:
	var parent = component.get_parent();
	
#	Check if the node is under a node named "Components" (To make the tree less cluttered)
	if (parent != null && parent.name == "Components"):
		parent = parent.get_parent();
	
	return parent


func is_node_usable(value: Node) -> bool:
	return value != null && is_instance_valid(value) && value.is_inside_tree();
