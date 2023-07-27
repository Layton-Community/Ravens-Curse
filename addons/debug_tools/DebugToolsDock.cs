#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class DebugToolsDock : BaseDock
{
	// Constants
	private const string USER_PATH = "user://save/";
	private const string ENCRYPT_PATH = "game/saves/encript_saves";

	// Enums

	// Signal

	// Export variables
	[Export] private ConfirmationDialog confirmationDialog;
	[Export] private AcceptDialog popupChooseSaves;
	[Export] private Button buttonDeleteSaves;
	[Export] private Button buttonOpenSaves;
	[Export] private Button buttonEditSaves;
	[Export] private CheckBox checkBoxEncriptSaves;
	[Export] private string buttonDeleteSavesText;
	
	// Member variables
	private string globalPath;
	private Action debugEvent;
	
	public override void _Ready()
	{
		globalPath = ProjectSettings.GlobalizePath(USER_PATH);
		checkBoxEncriptSaves.ButtonPressed = (bool)ProjectSettings.GetSetting(ENCRYPT_PATH);
		
		confirmationDialog.Confirmed += OnConfirmationDialog_Confirmed;
		buttonDeleteSaves.Pressed += OnButtonDeleteSaves_Pressed;
		buttonOpenSaves.Pressed += OnButtonOpenSaves_Pressed;
		buttonEditSaves.Pressed += OnButtonEditSaves_Pressed;
		checkBoxEncriptSaves.Pressed += OnCheckBoxEncriptSaves_Pressed;
	}

	private void OnConfirmationDialog_Confirmed()
	{
		debugEvent?.Invoke();
	}

	private void OnButtonDeleteSaves_Pressed()
	{
		debugEvent = () =>
		{
			Print.Warn(nameof(DebugTools), "Deleting user saves!");
		
			if (DirAccess.DirExistsAbsolute(globalPath))
			{
				OS.MoveToTrash(globalPath);
			}
			
			DirAccess.MakeDirAbsolute(globalPath);	
			confirmationDialog.DialogText = string.Empty;
		};
		
		confirmationDialog.DialogText = buttonDeleteSavesText;
		confirmationDialog.Popup();
	}
	
	private void OnButtonOpenSaves_Pressed()
	{
		if (!DirAccess.DirExistsAbsolute(globalPath))
		{
			DirAccess.MakeDirAbsolute(globalPath);
		}
		
		OS.ShellOpen(globalPath);
	}
	
	private void OnButtonEditSaves_Pressed()
	{
		(popupChooseSaves as DebugToolsChooseSave).UpdateSaveLabel();
		popupChooseSaves.Show();
	}

	private void OnCheckBoxEncriptSaves_Pressed()
	{
		ProjectSettings.SetSetting(ENCRYPT_PATH, checkBoxEncriptSaves.ButtonPressed);
		
		string encryption = (bool)ProjectSettings.GetSetting(ENCRYPT_PATH) ? "enabled" : "disabled";
		
		Print.Info(nameof(DebugTools), $"Saves encryption {encryption}!");
	}
}
#endif
