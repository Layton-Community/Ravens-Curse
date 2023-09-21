namespace Com.LaytonCommunity.RavensCurse.Game;

[GlobalClass]
[Icon("res://a_sprites/game_objects/hint_coin.png")]
public partial class HintCoin : Control
{
	// Constants
	private const string ANIM_SPIN = "spin";
	public const string GROUP = "coins";
	
	// Enums
	
	// Signal
	[Signal] public delegate void CollectedEventHandler();
	
	// Export variables
	[ExportGroup("Imports")]
	[Export] private TextureButton button;
	[Export] private AnimationPlayer animations;

	// Member variables

	public override void _Ready()
	{
		Modulate = Colors.Transparent;
		
		button.Pressed += OnButton_Pressed;
	}

	private void OnButton_Pressed()
	{
		if (animations != null && !animations.IsPlaying())
		{
			Modulate = Colors.White;
			
			animations.AnimationFinished += (_) =>
			{
				QueueFree();
			};
			
			animations.Play(ANIM_SPIN);
		}
	}
}

