namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class Title : UiBase
{
	// Constants
	public const string ANIM_FADE_TITLE = "ui_title/fade_title";
	public const string ANIM_IDLE = "ui_title/idle";
	public const string ANIM_QUIT = "ui_title/quit";
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneSaves;
	[Export] private CanvasItem logoTitle;
	[Export] private TextureButton buttonPlay;
	[Export] private AudioStreamPlayer buttonPlaySfx;
	[Export] private TextureButton buttonQuit;
	
	// Member variables
	public bool skipFadeTitle = false;
	
	public override void _Ready()
	{
		if (!skipFadeTitle)
		{
			animations.PlayAndAdvance(ANIM_FADE_TITLE, 0);
		}
		
		animations.PlayAndAdvance(ANIM_IDLE, 0);
		base._Ready();
		
		animations.AnimationFinished += OnAnimations_AnimationFinished;
	}

	private void OnAnimations_AnimationFinished(StringName name)
	{
		if (!skipFadeTitle && name == ANIM_FADE_IN)
		{
			animations.Play(ANIM_FADE_TITLE);
		}
		else if (skipFadeTitle || name == ANIM_FADE_TITLE)
		{
			animations.AnimationFinished -= OnAnimations_AnimationFinished;
			buttonPlay.Pressed += Play;
			buttonQuit.Pressed += Quit;
			
			animations.Play(ANIM_IDLE);
		}
	}

	private void Quit()
	{
		animations.AnimationFinished += (_) => GetTree().Quit();
		
		animations.Play(ANIM_QUIT);
	}

	private void Play()
	{
		buttonPlaySfx.Finished += () =>
		{
			ChangeSceneToFile(sceneSaves, true);
		};
		
		animations.Play(ANIM_FADE_OUT);
		buttonPlaySfx.Play();
	}
}

