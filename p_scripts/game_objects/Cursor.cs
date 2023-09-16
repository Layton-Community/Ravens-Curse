namespace Com.LaytonCommunity.RavensCurse;

public partial class Cursor : Node
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] private AnimatedSprite2D sprites;
	[Export] private Node2D poly;
	
	// Member variables
	
	public override void _Ready()
	{
		sprites.Stop();
	}
	
	public void ClickEffect()
	{
		var instance = sprites.Duplicate() as AnimatedSprite2D;
		
		AddChild(instance);
		instance.Play();
		
		instance.GlobalPosition = instance.GetGlobalMousePosition();
		instance.AnimationFinished += () =>
		{
			instance.QueueFree();
		};
	}
}

