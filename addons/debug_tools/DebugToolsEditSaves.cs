#if TOOLS
namespace Com.LaytonCommunity.RavensCurse.Addons;

[Tool]
public partial class DebugToolsEditSaves : ConfirmationDialog
{
	// Constants

	// Enums

	// Signal

	// Export variables
	[Export] private AcceptDialog popupChooseSaves;
		
	// Member variables
	public uint Id = uint.MaxValue;
	private List<Node> childrens;
	
	public override void _Ready()
	{
		var vbox = new VBoxContainer();
		
		AddChild(vbox);
		
		foreach (var field in typeof(Resources.PlayerSave).GetFields())
		{
			var hbox = new HBoxContainer();
			var label = new Label
			{
				Text = field.Name,
				SizeFlagsHorizontal = Control.SizeFlags.Expand
			};
			
			Control lineEdit;
			
			if (field.FieldType == typeof(string))
			{
				lineEdit = new LineEdit
				{
					Name = field.Name,
				};
			}
			else
			{
				lineEdit = new SpinBox
				{
					Name = field.Name,
					MinValue = 0,
					MaxValue = uint.MaxValue,
				};
				
				if (field.Name == "id")
				{
					(lineEdit as SpinBox).Editable = false;
				}
			}
			
			var lineEditSize = lineEdit.Size;
			lineEditSize.X = 200;
			lineEdit.CustomMinimumSize = lineEditSize;
			
			hbox.AddChild(label);
			hbox.AddChild(lineEdit);
			vbox.AddChild(hbox);
		}
		
		VisibilityChanged += On_VisibilityChanged;
		Confirmed += On_Confirmed;
		Canceled += On_Canceled;
		
		childrens = GetChild(0).GetChildren().ToList();
	}

	private void On_VisibilityChanged()
	{
		if (Id < 3 && Visible)
		{
			var save = Resources.PlayerSave.LoadFromDisk(Id);
			var saveType = save.GetType();

			childrens.ForEach((hbox) =>
			{
				var label = hbox.GetChild(0) as Label;
				var lineEdit = hbox.GetChild(1);
				var fieldInfo = saveType.GetField(label.Text);
				var stringValue = fieldInfo.GetValue(save).ToString();
				
				switch (fieldInfo.FieldType)
				{
					case var value when value == typeof(string):
						(lineEdit as LineEdit).Text = stringValue;
						break;
					default:
						(lineEdit as SpinBox).Value = double.Parse(stringValue);
						break;
				}
			});
		}
	}

	private void On_Confirmed()
	{
		var save = new Resources.PlayerSave();
		var saveType = save.GetType();

		childrens.ForEach((hbox) =>
		{
			var lineEdit = hbox.GetChild(1);
			var fieldInfo = saveType.GetField(lineEdit.Name);
			
			switch (fieldInfo.FieldType)
			{
				case var value when value == typeof(uint):
					fieldInfo.SetValue(save, (uint)(lineEdit as SpinBox).Value);
					break;
				case var value when value == typeof(double):
					fieldInfo.SetValue(save, (lineEdit as SpinBox).Value);
					break;
				default:
					fieldInfo.SetValue(save, (lineEdit as LineEdit).Text);
					break;
			}
		});
		
		save.PrintDbg();
		Resources.PlayerSave.StoreToDisk(save);
	}

	private void On_Canceled()
	{
		Hide();
		popupChooseSaves.Popup();
	}
}
#endif
