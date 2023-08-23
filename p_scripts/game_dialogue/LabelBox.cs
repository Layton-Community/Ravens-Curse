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
	
	public void AddDialogue(string newText)
	{
		Text = $"[{EFFECT}]{newText}[/{EFFECT}]";
		var effect = textEffect.Duplicate() as RichTextEffectDialogue;
		effect.textLength = newText.Length;
		effect.FinishedEffect += () =>
		{
			CustomEffects.Clear();
			EmitSignal(SignalName.FinishedEffect);
		};
		
		InstallEffect(effect);
	}
}

