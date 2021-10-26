tool
extends EditorPlugin

func _enter_tree():	
	# add_custom_type("B_Empty Leaf", "Node", preload("res://addons/behaviour_nodes/LeafNode_.cs"), null)
	add_custom_type("B_Dialog Node", "Node", preload("res://Scripts/Entity/BehaviourTree/VN Nodes/DialogNode.cs"), null)
	add_custom_type("B_Background Node", "Node", preload("res://Scripts/Entity/BehaviourTree/VN Nodes/ChangeBackground.cs"), null)
	add_custom_type("B_Scene Loader", "Node", preload("res://Scripts/Entity/BehaviourTree/VN Nodes/EsceneLoader.cs"), null)
	add_custom_type("B_Sequence", "Node", preload("res://Scripts/Entity/BehaviourTree/Base Nodes/SequenceNode.cs"), null)
	add_custom_type("B_Choice Selector", "Node", preload("res://Scripts/Entity/BehaviourTree/VN Nodes/ChoiceSelector.cs"), null)
	add_custom_type("B_Choice Route", "Node", preload("res://Scripts/Entity/BehaviourTree/VN Nodes/ChoiceSelector.cs"), null)
	add_custom_type("B_Reference Node", "Node", preload("res://Scripts/Entity/BehaviourTree/VN Nodes/ReferenceNode_U.cs"), null)
	add_custom_type("B_Change BGM", "Node", preload("res://Scripts/Entity/BehaviourTree/Sound Nodes/ChangeBGM.cs"), null)
	add_custom_type("B_Stop BGM", "Node", preload("res://Scripts/Entity/BehaviourTree/Sound Nodes/StopBGM.cs"), null)
	add_custom_type("B_Play Sound", "Node", preload("res://Scripts/Entity/BehaviourTree/Sound Nodes/PLaySound.cs"), null)

func _exit_tree():
	# remove_custom_type("B_Empty Leaf")
	remove_custom_type("B_Dialog Node")
	remove_custom_type("B_Background Node")
	remove_custom_type("B_Scene Loader")
	remove_custom_type("B_Sequence")
	remove_custom_type("B_Choice Selector")
	remove_custom_type("B_Choice Route")
	remove_custom_type("B_Reference Node")
	remove_custom_type("B_Change BGM")
	remove_custom_type("B_Stop BGM")
	remove_custom_type("B_Play Sound")

