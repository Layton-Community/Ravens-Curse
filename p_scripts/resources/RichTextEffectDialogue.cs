namespace Com.LaytonCommunity.RavensCurse.Resources;

[GlobalClass]
public partial class RichTextEffectDialogue : RichTextEffect
{
	// Constants
	
	// Enums
	
	// Signal
	
	// Export variables
	[Export] public int fadeLength;
	[Export] public double fadeSpeed;
	
	// Member variables
	private string bbcode = "dialogue";
	
	public override bool _ProcessCustomFX(CharFXTransform charFX)
	{
		var time = Mathf.Floor(charFX.ElapsedTime * fadeSpeed);
		var charPos = charFX.RelativeIndex;
		var distance = time - charPos;
		
		var color = charFX.Color;
		color.A = Mathf.Clamp((float)(distance / fadeLength), 0, 1);
		charFX.Color = color;
		
		return true;
	}
}
