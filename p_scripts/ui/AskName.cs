namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class AskName : UiBase
{
	// Constants

	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneSaves;
	[Export(PropertyHint.File,"*.tscn")] private string sceneGame;
	[Export] private ButtonLarge buttonOk;
	[Export] private AudioStreamPlayer buttonOkSfxForbidden;
	[Export] private TextureButton buttonBack;
	[Export] private LineEdit username;

	// Member variables

	public override void _EnterTree() => buttonOk.Pressed += OnButtonOk_Pressed;

	public override void _Ready()
	{
		base._Ready();
		
		buttonBack.Pressed += OnButtonBack_Pressed;
	}
	
	public void OnButtonOk_Pressed()
	{
		if (string.IsNullOrWhiteSpace(username.Text))
		{
			buttonOk.soundOverride = buttonOkSfxForbidden;
		}
		else
		{
			buttonOk.soundOverride = null;
			animations.Play(ANIM_FADE_OUT);
			Resources.PlayerSave.Singleton.username = username.Text;
			Resources.PlayerSave.StoreSingleton();
			// load new game
		}
	}
	
	public void OnButtonBack_Pressed()
	{
		buttonBack.Pressed -= OnButtonBack_Pressed;
		animations.AnimationFinished += (_) =>
		{
			AddSibling(sceneSaves.InstantiateFromPath(), true);
			QueueFree();
		};
		
		animations.Play(ANIM_FADE_OUT);
	}
}

