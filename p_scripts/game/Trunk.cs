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
	[Export] protected AudioStreamPlayer buttonCloseSfx;
	[Export] protected Label labelLocationCurrent;
	[Export] protected Label labelLocationGoto;
	[Export] protected Label labelPicarats;
	[Export] protected Label labelTimeHours;
	[Export] protected Label labelTimeMinutes;
	[Export] protected Label labelPuzzleCompleted;
	[Export] protected Label labelPuzzleFound;
	[Export] protected Label labelCoinsCurrent;
	[Export] protected Label labelCoinsFound;
	
	// Member variables

	public override void _Ready()
	{
		base._Ready();
		
		buttonClose.Pressed += OnButtonClose_Pressed;
		
		animations.SpeedScale = 2;
		
		var currentSave = Resources.PlayerSave.Singleton ?? new Resources.PlayerSave();
		var timePlayed = TimeSpan.FromMilliseconds(currentSave.timePlayed);

		labelLocationCurrent.Text = currentSave.locationCurrent;
		labelLocationGoto.Text = currentSave.locationGoto;
		labelPicarats.Text = currentSave.picarats.ToString();
		labelTimeHours.Text = timePlayed.Hours.ToString();
		labelTimeMinutes.Text = timePlayed.Minutes.ToString();
		labelPuzzleCompleted.Text = currentSave.puzzleCompleted.ToString();
		labelPuzzleFound.Text = currentSave.puzzleFound.ToString();
		labelCoinsCurrent.Text = currentSave.coinsCurrent.ToString();
		labelCoinsFound.Text = currentSave.coinsFound.ToString();
	}

	private void OnButtonClose_Pressed()
	{
		buttonClose.Disabled = true;
		buttonClose.TextureDisabled = buttonClose.TexturePressed;
		
		buttonCloseSfx.Play();
		foreground.Show();
		
		GetTree().CreateTimer(0.2).Timeout += () =>
		{
			animations.AnimationFinished += (_) =>
			{
				AddSibling(sceneLocation.InstantiateFromPath(), true);
				QueueFree();
			};
				
			animations.Play(ANIM_FADE_OUT);
		};
	}
}

