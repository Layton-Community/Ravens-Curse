namespace Com.LaytonCommunity.RavensCurse.Components;

[GlobalClass]
public abstract partial class Component : Node
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	
	// Member variables
	protected Node parent;
	
	public override void _Ready()
	{
		parent = GetParent();
		
		if (parent == null)
		{
			NoParentDestructor();
			return;
		}
		// Check if the node is under a node named "Component" (To make the tree less cluttered)
		else if (parent.Name == nameof(Component))
		{
			parent = parent.GetParent();
			
			if (parent == null)
			{
				NoParentDestructor();
				return;
			}
		}
		
		ReadyComponent();
	}
	
	protected virtual void NoParentDestructor()
	{
		var msg = "I cannot work without a parent! Destroying!";
		
		GD.PrintRich($"[color=ffdd65][{GetType().Name}][{Name}] {msg}[/color]");
		QueueFree();
	}
	
    /// <summary> Called when a parent has been found </summary>
	protected virtual void ReadyComponent()
	{
		
	}
}

