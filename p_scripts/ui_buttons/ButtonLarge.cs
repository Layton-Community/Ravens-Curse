namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class ButtonLarge : TextureButton
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] private TextureRect image;
	[Export] private AudioStreamPlayer sound;
	
	// Member variables
	public AudioStreamPlayer soundOverride;
	
	public override void _Ready()
	{
		Pressed += On_Pressed;
		ButtonDown += On_ButtonDown;
		ButtonUp += On_ButtonUp;
	}

	private void On_Pressed()
	{
		if (soundOverride != null)
		{
			soundOverride.Play();
		}
		else
		{
			sound.Play();
		}
	}

	private void On_ButtonDown()
	{
		image.Position = Vector2.One * 4;
	}

	private void On_ButtonUp()
	{
		image.Position = Vector2.Zero;
	}
}

