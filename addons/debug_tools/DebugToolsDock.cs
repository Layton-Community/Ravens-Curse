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
	[Export] private Button buttonMergeTsv;
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
		buttonMergeTsv.Pressed += OnButtonMergeTsv_Pressed;
	}
	
	private void PrintInfo(string what)
	{
		Print.Info(nameof(DebugTools), what);
	}
	
	private void PrintWarn(string what)
	{
		Print.Warn(nameof(DebugTools), what);
	}

	private void OnConfirmationDialog_Confirmed()
	{
		debugEvent?.Invoke();
	}

	private void OnButtonDeleteSaves_Pressed()
	{
		debugEvent = () =>
		{
			PrintWarn("Deleting user saves!");
		
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
		
		PrintInfo($"Saves encryption {encryption}!");
	}
	
	private void OnButtonMergeTsv_Pressed()
	{
		var dirPath = "res://a_dialogues/tsv/";
		var dir = DirAccess.Open(dirPath);
		
		if (dir == null)
		{
			PrintWarn(DirAccess.GetOpenError().ToString());
			return;
		}
		
		PrintInfo($"Opening TSV folder!");
		
		var localization = new List<string>();
				
		foreach (var filePath in dir.GetFiles())
		{
			var fileAbsolutePath = dirPath + filePath;
			var file = FileAccess.Open(fileAbsolutePath, FileAccess.ModeFlags.Read);
			
			if (file == null)
			{
				PrintWarn($"{FileAccess.GetOpenError()} : {fileAbsolutePath}");
				return;
			}
			
			var text = file.GetAsText();
			localization.Capacity = localization.Count + text.Count("\n");

			localization.AddRange(text.Split('\n')[1..]);
			file.Close();
			PrintInfo($"Found: \"{fileAbsolutePath}\"");
		}
		
		var keys = "keys	en	fr	de	es";
		var pathLocalization = "res://a_dialogues/localization.csv";
		var fileOut = FileAccess.Open(pathLocalization, FileAccess.ModeFlags.Write);
		
		localization.Sort();
		localization.Insert(0, keys);
		fileOut.StoreString(string.Join("\n", localization));
		fileOut.Close();
		PrintInfo($"Stored data to: \"{pathLocalization}\"");
	}
	
/*
	private bool IsLocalizationCompiled()
	{
		var pathLocalization = "res://a_dialogues/localization.csv";
		
		if (!FileAccess.FileExists(pathLocalization)) return false;
		
		var checksum = new List<byte>();
		var dirPath = "res://a_dialogues/tsv/";
		var dir = DirAccess.Open(dirPath);
		
		foreach (var filePath in dir.GetFiles())
		{
			var fileAbsolutePath = dirPath + filePath;
			var fileTsv = FileAccess.Open(fileAbsolutePath, FileAccess.ModeFlags.Read);
			
			if (fileTsv == null)
			{
				PrintWarn($"{FileAccess.GetOpenError()} : {fileAbsolutePath}");
				return false;
			}
			
			var md5 = fileTsv.GetAsText().Md5Buffer();
			checksum.Capacity = checksum.Count + md5.Length;
			
			checksum.AddRange(md5);
			fileTsv.Close();
		}
		
		var currentSum = checksum.ToString().Md5Buffer();
		var fileSumPath = "res://a_dialogues/localization.md5";
				
		if (FileAccess.FileExists(fileSumPath))
		{
			var fileSumByte = FileAccess.GetFileAsBytes(fileSumPath);
						
			if (fileSumByte == currentSum) return true;
		}
		
		var fileSum = FileAccess.Open(fileSumPath, FileAccess.ModeFlags.Write);
		
		if (fileSum == null)
		{
			PrintWarn($"{FileAccess.GetOpenError()} : {fileSumPath}");
		}
		
		fileSum.StoreBuffer(currentSum);
		fileSum.Close();
		
		return false;
	}
*/
}
#endif
