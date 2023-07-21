#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class DebugToolsDock : BaseDock
{
	// Constants
	private const string USER_PATH = "user://save/";
	private const string ENCRIPT_PATH = "game/saves/encript_saves";

	// Enums

	// Signal

	// Export variables
	[Export] private ConfirmationDialog confirmationDialog;
	[Export] private Button buttonDeleteSaves;
	[Export] private string textButtonDeleteSaves;
	[Export] private Button buttonOpenSaves;
	[Export] private CheckBox checkBoxEncriptSaves;
	
	// Member variables
	private string globalPath;
	private Action debugEvent;
	
	public override void _Ready()
	{
		globalPath = ProjectSettings.GlobalizePath(USER_PATH);
		checkBoxEncriptSaves.ButtonPressed = (bool)ProjectSettings.GetSetting(ENCRIPT_PATH);
		
		confirmationDialog.Confirmed += OnConfirmationDialog_Confirmed;
		buttonDeleteSaves.Pressed += OnButtonDeleteSaves_Pressed;
		buttonOpenSaves.Pressed += OnButtonOpenSaves_Pressed;
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
			Print("Deleting user saves!");
		
			if (DirAccess.DirExistsAbsolute(globalPath))
			{
				OS.MoveToTrash(globalPath);
			}
			
			DirAccess.MakeDirAbsolute(globalPath);	
			confirmationDialog.DialogText = string.Empty;
		};
		
		confirmationDialog.DialogText = textButtonDeleteSaves;
		confirmationDialog.Popup();
	}
	
	private void OnButtonOpenSaves_Pressed()
	{
		if (!DirAccess.DirExistsAbsolute(globalPath))
		{
			DirAccess.MakeDirAbsolute(globalPath);
		}
		
		OS.ShellOpen(globalPath/* .Replace('/', '\\') */);
	}

	private void OnCheckBoxEncriptSaves_Pressed()
	{
		ProjectSettings.SetSetting(ENCRIPT_PATH, checkBoxEncriptSaves.ButtonPressed);
		Print($"Saves encryption {((bool)ProjectSettings.GetSetting(ENCRIPT_PATH) ? "enabled" : "disabled")}!");
	}
}
#endif
