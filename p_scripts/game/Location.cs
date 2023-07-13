namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Location : Ui.UiBase
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] protected Texture2D trunkPressed1;
	[Export] protected Texture2D movePressed0;
	[Export] protected Texture2D movePressed1;
	[Export] protected TextureButton buttonTrunk;
	[Export] protected TextureButton buttonMove;
	
	// Member variables
	
	public override void _Ready()
	{
		base._Ready();

		animations.SpeedScale = 2;
		buttonTrunk.Pressed += OnButtonTrunk_Pressed;
		buttonMove.Pressed += OnButtonMove_Pressed;
	}

	private void OnButtonTrunk_Pressed()
	{
		buttonTrunk.Disabled = true;
		buttonTrunk.TextureDisabled = trunkPressed1;
		animations.AnimationFinished += (_) =>
		{
			Print.Info("Show trunk");
		};
		
		GetTree().CreateTimer(0.2).Timeout += () => animations.Play(ANIM_FADE_OUT);
	}

	private void OnButtonMove_Pressed()
	{
		buttonTrunk.Disabled = true;
		buttonMove.Disabled = true;
		buttonMove.TextureDisabled = movePressed1;
		
		GetTree().CreateTimer(0.2).Timeout += () =>
		{
			buttonMove.TextureDisabled = movePressed0;
			
			GetTree().CreateTimer(0.2).Timeout += () =>
			{
				buttonTrunk.Visible = false;
				buttonMove.Visible = false;
			};
		};
	}
}

