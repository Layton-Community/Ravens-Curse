namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Trunk : Ui.UiBase
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneLocation;
	[Export] protected TextureButton buttonClose;
	
	// Member variables

	public override void _Ready()
	{
		base._Ready();
		
		animations.SpeedScale = 2;
		buttonClose.Pressed += OnButtonClose_Pressed;
	}

	private void OnButtonClose_Pressed()
	{
		buttonClose.Disabled = true;
		buttonClose.TextureDisabled = buttonClose.TexturePressed;
		animations.AnimationFinished += (_) =>
		{
			AddSibling(sceneLocation.InstantiateFromPath(), true);
			QueueFree();
		};
		
		GetTree().CreateTimer(0.2).Timeout += () => animations.Play(ANIM_FADE_OUT);
	}
}

