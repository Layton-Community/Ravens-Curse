namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class UiBase : Control
{
	// Constants
	public const string ANIM_FADE_IN = "ui_base/fade_in";
	public const string ANIM_FADE_OUT = "ui_base/fade_out";
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] protected ColorRect foreground;
	[Export] protected AnimationPlayer animations;
	
	// Member variables
	
	public override void _Ready()
	{
		animations.PlayAndAdvance(ANIM_FADE_IN, 0);
	}
	
	protected void ShowBarrier()
	{
		foreground.Show();
		foreground.Color = Colors.Transparent;
	}
}

