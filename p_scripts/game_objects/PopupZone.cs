namespace Com.LaytonCommunity.RavensCurse.Game;

// [GlobalClass]
public partial class PopupZone : TextureButton
{
	// Constants
	public const string GROUP = "popup_zone";
	
	// Enums
	
	// Signal
	[Signal] public delegate void ZonePressedEventHandler(PackedScene popupScene);
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] private ColorRect colorRect;
	[Export] private PackedScene popupScene;
	
	// Member variables
	
	public override void _Ready()
	{
		colorRect.Hide();
		
		Pressed += OnThis_Pressed;
	}

	private void OnThis_Pressed()
	{
		EmitSignal(SignalName.ZonePressed, popupScene);
	}
}

