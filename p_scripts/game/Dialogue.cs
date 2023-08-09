namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Dialogue : Ui.UiBase
{
	// Constants
	public const string ANIM_LOOP = "game_dialogue/loop";
	
	private const string BB_FADE_START = "[dialogue]";
	private const string BB_FADE_END = "[/dialogue]";

	// Enums

	// Signal

	// Export variables
	[ExportGroup("Imports")]
	[Export] private Resources.RichTextEffectDialogue textEffect;
	[Export] private Components.Logger logger;
	[Export] private Label labelName;
	[Export] private RichTextLabel labelText;
	[Export] private Button buttonNext;
	
	// Member variables
	public string key = string.Empty;
	public string name = string.Empty;
	private int dialogueIndex = 0;
	private int dialogueIndexMax = 0;
	private int dialogueFadeIndex = 0;
	private int dialogueFadeIndexMax = 0;
	private string dialogueText;
	private List<string> dialogues = new();
	private SceneTree tree;

	public override void _Ready()
	{
		base._Ready();
		logger.Info(nameof(_Ready));
		
		var type = GetType().Name;
		
		if (string.IsNullOrEmpty(key))
		{
			Print.Warn(type, "The dialogue key is empty!");
			return;
		}
		
		var index = 0;
		var newKey = $"{key}_{index}";
		var text = Tr(newKey);
		
		while (text != newKey)
		{
			dialogues.Add(text);
			
			index += 1;
			newKey = $"{key}_{index}";
			text = Tr(newKey);
		}
		
		if (dialogues.Count == 0)
		{
			Print.Warn(type, $"There is no dialogues for the key: \"{key}\"!");
			return;
		}
		
		buttonNext.Pressed += OnButtonNext_Pressed;
		
		dialogueIndexMax = index;
		tree = GetTree();
		
		labelName.Text = key.Split('_')[^1].ToPascalCase();
		labelText.Text = dialogues[dialogueIndex];
		
		StartAnimationText();
		
		// logger.Info($"{key}:");
		// foreach (var line in dialogues) logger.Info(line);
	}
	
	private void StartAnimationText()
	{
		logger.Info(nameof(StartAnimationText));
		
		if (dialogueIndex < dialogueIndexMax)
		{
			labelText.Text = BB_FADE_START;
			labelText.Text += dialogues[dialogueIndex];
			labelText.Text += BB_FADE_END;
			dialogueIndex += 1;
			
			double effectDuration = labelText.Text.Length / textEffect.fadeSpeed - .5;
			
			tree.CreateTimer(effectDuration).Timeout += OnLabelText_EndEffect;

			logger.Info(dialogueIndex.ToString());
			labelText.InstallEffect(textEffect.Duplicate());
		}
		else
		{
			logger.Info(ANIM_FADE_OUT);
			animations.Play(ANIM_FADE_OUT);
		}
	}
	
	private void OnLabelText_EndEffect()
	{
		buttonNext.Visible = true;
		
		labelText.CustomEffects.Clear();
		logger.Info("End Anim");
	}

	private void OnButtonNext_Pressed()
	{
		buttonNext.Visible = false;
		
		StartAnimationText();
		logger.Info(nameof(OnButtonNext_Pressed));
	}
}
