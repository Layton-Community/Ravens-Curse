namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Dialogue : Ui.UiBase
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	
	// Member variables
	public string key = string.Empty;
	
	public override void _Ready()
	{
		base._Ready();
		
		var type = GetType().Name;
		
		if (string.IsNullOrEmpty(key))
		{
			Print.Warn(type, "The dialogue key is empty!");
			return;
		}
		
		var index = 0;
		var newKey = $"{key}_{index}";
		var text = Tr(newKey);
		var dialogues = new List<string>();
		
		do {
			dialogues.Add(text);
			
			index += 1;
			newKey = $"{key}_{index}";
			text = Tr(newKey);
		}
		while (text != newKey);
		
		Print.Info(type, $"{key}:");
		foreach (var line in dialogues)
		{
			Print.Info(type, line);
		}
	}
}

