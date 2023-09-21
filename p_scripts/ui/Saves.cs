namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class Saves : UiBase
{
	// Constants

	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneTitle;
	[Export(PropertyHint.File,"*.tscn")] private string sceneAskName;
	[Export(PropertyHint.File,"*.tscn")] private string sceneGame;
	[Export] private TextureButton buttonBack;
	
	// Member variables
	
	public override void _Ready()
	{
		base._Ready();
				
		buttonBack.Pressed += OnButtonBack_Pressed;
	}

	public void OnButtonUser_Pressed()
	{
		foreground.Show();
		foreground.Color = Colors.Transparent;
	}

	public void OnButtonUser_Finished(bool isSaveEmpty)
	{
		animations.AnimationFinished += (_) =>
		{
			ChangeSceneToFile(isSaveEmpty ? sceneAskName : sceneGame);
		};
		
		animations.Play(ANIM_FADE_OUT);
	}
	
	public void OnButtonBack_Pressed()
	{
		buttonBack.Pressed -= OnButtonBack_Pressed;
		animations.AnimationFinished += (_) =>
		{
			var title = sceneTitle.InstantiateFromPath<Title>();
			title.skipFadeTitle = true;
			
			AddSibling(title, true);
			QueueFree();
		};
		
		animations.Play(ANIM_FADE_OUT);
	}
}
