#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class DebugToolsChooseSave : AcceptDialog
{
	// Constants

	// Enums

	// Signal

	// Export variables
	[Export] private AcceptDialog popupEditSaves;
	[Export] private VBoxContainer containerButtonSaves;
	[Export] private Button[] saveButtons;
	[Export] private Label[] saveLabels;
		
	// Member variables
	private List<DebugToolsChooseSaveButton> saveButtonList;
	
	public override void _Ready()
	{
		Assert._(saveButtons.Length == saveLabels.Length);
		
		saveButtonList = saveButtons.ToList<DebugToolsChooseSaveButton>();
		
		saveButtonList.ForEach((buttonSave) => buttonSave.PressedId += OnButtonSave_Pressed);
	}
	
	public void UpdateSaveLabel()
	{
		for (int i = 0; i < saveButtonList.Count; i++)
		{
			var buttonSave = saveButtonList[i];
			var label = saveLabels[i];
			
			label.Text = $"Save {buttonSave.Id} ";
			
			if (Resources.PlayerSave.Exist(buttonSave.Id))
			{
				var save = Resources.PlayerSave.LoadFromDisk(buttonSave.Id);
				label.Text += $"({save.username})";
			}
			else
			{
				label.Text += "(empty)";
			}
		}
	}
	
	private void OnButtonSave_Pressed(uint id)
	{
		(popupEditSaves as DebugToolsEditSaves).Id = id;
		
		Hide();
		popupEditSaves.Popup();
	}
}
#endif
