namespace Com.LaytonCommunity.RavensCurse.Resources;

[GlobalClass]
public partial class RichTextEffectDialogue : RichTextEffect
{
	// Constants
	
	// Enums
	
	// Signal
	[Signal] public delegate void FinishedEffectEventHandler();
		
	// Export variables
	[Export] public int fadeLength;
	[Export] public double fadeSpeed;
	
	// Member variables
	public int textLength = 0; // IMPORTANT, TO BE SET WHEN INSTANTIATING THE EFFECT
	
	private bool isFinished = false;
	private string bbcode = "dialogue";
	
	public override bool _ProcessCustomFX(CharFXTransform charFX)
	{
		if (isFinished) return true;
		
		
		var time = Mathf.Floor(charFX.ElapsedTime * fadeSpeed);
		var distance = time - charFX.RelativeIndex;
		
		var color = charFX.Color;
		color.A = Mathf.Clamp((float)(distance / fadeLength), 0, 1);
		charFX.Color = color;
		
		
		if (!isFinished && textLength - 1 == charFX.RelativeIndex  && color.A == 1 )
		{
			isFinished = true;
			
			EmitSignal(SignalName.FinishedEffect);
		}
		
		return true;
	}
}
