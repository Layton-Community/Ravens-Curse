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
		parent = FindComponentParent();
		
		if (parent != null)
		{
			ReadyComponent();
		}
	}

	protected Node FindComponentParent()
	{
		var parent = GetParent();

		if (parent == null)
		{
			NoParentDestructor();
			return null;
		}
		// Check if the node is under a node named "Components" (To make the tree less cluttered)
		else if (parent.Name == "Components")
		{
			parent = parent.GetParent();

			if (parent == null)
			{
				NoParentDestructor();
				return null;
			}
		}
		
		return parent;
	}

	protected virtual void NoParentDestructor()
	{
		var msg = "I cannot work without a parent! Destroying!";
		
		Print.Warn(GetType().Name, msg);
		QueueFree();
	}
	
    /// <summary> Called when a parent has been found </summary>
	protected virtual void ReadyComponent()
	{
		
	}
}

