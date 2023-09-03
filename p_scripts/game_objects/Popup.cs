namespace Com.LaytonCommunity.RavensCurse.Game;

// [GlobalClass]
public partial class Popup : Control
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] private ColorRect colorRect;
	
	// Member variables
	
	public override void _Ready()
	{
		colorRect.Hide();
	}

	public override void _Process(double delta)
	{
		
	}
}

