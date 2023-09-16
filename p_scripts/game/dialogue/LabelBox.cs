using Com.LaytonCommunity.RavensCurse.Resources;

namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class LabelBox : RichTextLabel
{
	// Constants
	private const string EFFECT = "dialogue";
	
	// Enums
	
	// Signal
	[Signal] public delegate void FinishedEffectEventHandler();
	
	// Export variables
	[Export] private RichTextEffectDialogue textEffect;
	
	// Member variables
	private string currentText;
	
	
	
	public void AddDialogue(string newText)
	{
		currentText = newText;
		Text = $"[{EFFECT}]{currentText}[/{EFFECT}]";
		var effect = textEffect.Duplicate() as RichTextEffectDialogue;
		effect.textLength = newText.Length;
		effect.FinishedEffect += OnEffect_Finished;

		InstallEffect(effect);
	}
	
	public void SkipDialogues()
	{
		Text = currentText;
		
		OnEffect_Finished();
	}
	
	private void OnEffect_Finished()
	{
		CustomEffects.Clear();
		EmitSignal(SignalName.FinishedEffect);
	}
}

