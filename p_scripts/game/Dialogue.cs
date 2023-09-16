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
	[Export] private Components.Logger logger;
	[Export] private Sprite2D spriteNameBox;
	[Export] private Label labelName;
	[Export] private LabelBox labelBox;
	[Export] private Button buttonNext;
	[Export] private Button buttonSkip;
	
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
		
		if (key.IsEmpty())
		{
			logger.Warn("The dialogue key is empty!");
			return;
		}
		
		if (npcName.IsEmpty())
		{
			spriteNameBox.Hide();
		}
		
		logger.Info($"Key: {key}");
		FindDialogs();
		
		if (dialogues.Count == 0)
		{
			// It is normal for a location to have no dialogue.
			if (key.Find(Type.ENTRY.ToString()) == -1)
			{
				logger.Warn($"There is no dialogues for the key: \"{key}\"!");
			}
			
			QueueFree();
			return;
		}
		
		buttonNext.Pressed += OnButtonNext_Pressed;
		buttonSkip.Pressed += OnButtonSkip_Pressed;
		labelBox.FinishedEffect += OnLabelBox_FinishedEffect;
		buttonNext.Visible = false;
		buttonSkip.Visible = false;
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
			
			buttonSkip.Visible = true;
			index += 1;
		}
		else
		{
			animations.Play(ANIM_FADE_OUT);
		}
	}

	private void OnButtonSkip_Pressed()
	{
		buttonSkip.Visible = false;
		
		labelBox.SkipDialogues();
	}
	
	private void OnLabelBox_FinishedEffect()
	{
		buttonNext.Visible = true;
		buttonSkip.Visible = false;
	}

	private void OnButtonNext_Pressed()
	{
		buttonNext.Visible = false;
		
		UpdateDialogueText();
	}
}
