namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Dialogue : Ui.UiBase
{
	// Constants
	public const string ANIM_LOOP = "game_dialogue/loop";
	
	// Enums
	public enum Type { ENTRY, NPC, POPUP }

	// Signal

	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneDialogue;
	[Export] private Components.Logger logger;
	[Export] private Label labelName;
	[Export] private LabelBox labelBox;
	[Export] private Button buttonNext;
	
	// Member variables
	public string key = string.Empty;
	public string npcName = string.Empty;
	
	private int index = 0;
	private int indexMax = 0;
	private string text;
	private List<string> dialogues = new();

	public override void _Ready()
	{
		base._Ready();
		
		if (key.IsEmpty() || npcName.IsEmpty())
		{
			logger.Warn("The dialogue key or name is empty!");
			return;
		}
		
		logger.Info($"Key: {key}");
		FindDialogs();
		
		if (dialogues.Count == 0)
		{
			logger.Warn($"There is no dialogues for the key: \"{key}\"!");
			return;
		}
		
		buttonNext.Pressed += OnButtonNext_Pressed;
		labelBox.FinishedEffect += OnLabelBox_FinishedEffect;
		buttonNext.Visible = false;
		labelName.Text = npcName;
		
		UpdateDialogueText();
	}

	private void FindDialogs(int index = 0)
	{
		var keyWithIndex = $"{key}_{index}";
		var text = Tr(keyWithIndex);
		
		if (text != keyWithIndex)
		{
			logger.Info($"Found dialogue with the key: {keyWithIndex}");
			dialogues.Add(text);
			
			FindDialogs(index + 1);
		}
		else
		{
			indexMax = index;
			
			logger.Info($"Stopping at index: {indexMax - 1}:");
		}
	}
	
	private void UpdateDialogueText()
	{
		if (index < indexMax)
		{
			labelBox.AddDialogue(dialogues[index]);
			
			index += 1;
		}
		else
		{
			animations.Play(ANIM_FADE_OUT);
		}
	}
	
	private void OnLabelBox_FinishedEffect()
	{
		buttonNext.Visible = true;
	}

	private void OnButtonNext_Pressed()
	{
		buttonNext.Visible = false;
		
		UpdateDialogueText();
	}
}
