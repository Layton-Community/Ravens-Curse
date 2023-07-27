namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Location : Ui.UiBase
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneTrunk;
	[Export] protected Texture2D textureTrunkDust;
	[Export] protected Texture2D textureMoveDust;
	[Export] protected TextureButton buttonTrunk;
	[Export] protected TextureButton buttonMove;
	[Export] protected AudioStreamPlayer buttonTrunkSfx;
	[Export] protected AudioStreamPlayer buttonMoveSfx;
	
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
		buttonTrunk.TextureDisabled = textureTrunkDust;
		
		buttonTrunkSfx.Play();
		foreground.Show();
		
		GetTree().CreateTimer(0.2).Timeout += () =>
		{
			animations.AnimationFinished += (_) =>
			{
				AddSibling(sceneTrunk.InstantiateFromPath(), true);
				QueueFree();
			};
			
			animations.Play(ANIM_FADE_OUT);
		};
	}

	private void OnButtonMove_Pressed()
	{
		buttonTrunk.Disabled = true;
		buttonMove.Disabled = true;
		buttonMove.TextureDisabled = buttonMove.TexturePressed;
		
		buttonMoveSfx.Play();
		foreground.Show();
		
		GetTree().CreateTimer(0.15).Timeout += () =>
		{
			buttonMove.TextureDisabled = textureMoveDust;
			
			GetTree().CreateTimer(0.3).Timeout += () =>
			{
				buttonTrunk.Visible = false;
				buttonMove.Visible = false;
			};
		};
	}
}
