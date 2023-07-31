namespace Com.LaytonCommunity.RavensCurse;

[Tool]
public partial class CharacterBase : Control
{
	// Constants
	private const string TALK = "talk";
	private const string FIRST_TALK = "first_talk";
	
	// Enums
	
	// Signal
	
	// Export variables
	[Export] private Texture2D Texture 
	{
		get { return texture; }
		set { SetTexture(value); }
	}
	
	[ExportGroup("Imports")]
	[Export] private TextureButton button;
	[Export] private AnimationPlayer animation;
	
	// Member variables
	private Texture2D texture;
	private bool hasSpoken;
	
	private void SetTexture(Texture2D newTexture)
	{
		if (!Engine.IsEditorHint() || button == null) return;
		
		texture = newTexture;
		button.TextureNormal = texture;
		button.Size = Vector2.Zero;
	}
	
	public override void _Ready()
	{
		button.Pressed += OnButton_Pressed;
		animation.AnimationFinished += (_) => button.Pressed += OnButton_Pressed;
	}
	
	private void OnButton_Pressed()
	{
		button.Pressed -= OnButton_Pressed;
		
		animation.Play(hasSpoken ? TALK : FIRST_TALK);
		
		hasSpoken = true;
	}
}