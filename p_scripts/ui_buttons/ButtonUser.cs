namespace Com.LaytonCommunity.RavensCurse.Ui;

public partial class ButtonUser : TextureButton
{
	// Constants

	// Enums

	// Signals

	// Export variables
	[Export] private Saves saves;
	[ExportGroup("Imports")]
	[Export] private Label labelId;
	[Export] private Label labelName;
	[Export] private Label labelLocation;
	[Export] private Label labelHours;
	[Export] private Label labelMinutes;
	[Export] private Label labelPuzzleFound;
	[Export] private Label labelPuzzleCompleted;
	[Export] private TextureRect textureEmpty;
	[Export] private TextureRect texturePopup;
	[Export] private TextureRect texturePopupCreate;
	[Export] private TextureRect texturePopupLoading;
	[Export] private AudioStreamPlayer texturePopupSFX;
	
	// Member variables
	private Resources.PlayerSave save = null;
	private uint id;

	public override void _Ready()
	{
		id = uint.Parse(labelId.Text) - 1;
		
		if (Resources.PlayerSave.Exist(id))
		{
			save = Resources.PlayerSave.LoadFromDisk(id);
			var timeSpan = TimeSpan.FromMilliseconds(save.timePlayed);
				
			labelName.Text = save.username;
			labelLocation.Text = save.locationCurrent;
			labelHours.Text = timeSpan.Hours.ToString(labelHours.Text);
			labelMinutes.Text = timeSpan.Minutes.ToString(labelMinutes.Text);
			labelPuzzleFound.Text = save.puzzleFound.ToString();
			labelPuzzleCompleted.Text = save.puzzleCompleted.ToString();
			
			textureEmpty.Hide();
		}
		
		Pressed += OnButtonUser_Pressed;
	}

	private void OnButtonUser_Pressed()
	{
		Pressed -= OnButtonUser_Pressed;
		texturePopupSFX.Finished += () => saves.OnButtonUser_Finished(textureEmpty.Visible);
		
		(textureEmpty.Visible ? texturePopupCreate : texturePopupLoading).Show();
		
		Resources.PlayerSave.LoadToSingleton(id);
		saves.OnButtonUser_Pressed();
		texturePopup.Show();
		texturePopupSFX.Play();
	}
}

