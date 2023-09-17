namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Popup : Ui.UiBase
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] private Button buttonNext;
	
	// Member variables
	
	public override void _Ready()
	{
		base._Ready();
		buttonNext.Hide();
		
		animations.AnimationFinished += OnAnimationsFadeIn_Finished;
	}

	private void OnAnimationsFadeIn_Finished(StringName _)
	{
		buttonNext.Pressed += OnButtonNext_Pressed;
		
		buttonNext.Show();
	}

	private void OnButtonNext_Pressed()
	{
		buttonNext.Pressed -= OnButtonNext_Pressed;
		animations.AnimationFinished += _ => QueueFree();
		
		buttonNext.Hide();
		animations.Play(ANIM_FADE_OUT);
	}
}

