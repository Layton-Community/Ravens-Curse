#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class DebugToolsDock : BaseDock
{
	private const string USER_PATH = "user://save/";

	// Constants

	// Enums

	// Signal

	// Export variables
	[Export] private Button buttonDeleteSaves;
	[Export] private ConfirmationDialog dialogDeleteSaves;
	
	// Member variables
	
	public override void _Ready()
	{
		buttonDeleteSaves.Pressed += OnButtonDeleteSaves_Pressed;
		dialogDeleteSaves.Confirmed += OnDialogDeleteSaves_Confirmed;
	}
	
	private void OnButtonDeleteSaves_Pressed()
	{
		dialogDeleteSaves.PopupWindow = true;
		dialogDeleteSaves.Popup();
	}

	private void OnDialogDeleteSaves_Confirmed()
	{
		Print("Deleting user saves!");
		
		if (DirAccess.DirExistsAbsolute(USER_PATH))
		{
			OS.MoveToTrash(ProjectSettings.GlobalizePath(USER_PATH));
		}
		
		DirAccess.MakeDirAbsolute(ProjectSettings.GlobalizePath(USER_PATH));
	}
}
#endif
