namespace Com.LaytonCommunity.RavensCurse;

[Tool]
public partial class CharacterBase : Control
{
	// Constants
	private const string TALK = "talk";
	private const string FIRST_TALK = "first_talk";
	public const string GROUP = "NPCs";
	
	// Enums
	
	// Signa
	[Signal] public delegate void PressedNpcEventHandler(string NpcName);
	
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
		if (!Engine.IsEditorHint() || newTexture == null || button == null) return;
		
		texture = newTexture;
		button.TextureNormal = texture;
		button.Size = Vector2.Zero;
	}
	
	public override void _Ready()
	{
		if (Engine.IsEditorHint()) return;
		
		button.Pressed += OnButton_Pressed;
	}
	
	private void OnButton_Pressed()
	{
		animation.AnimationFinished += OnAnimation_AnimationFinished;
		
		animation.Play(hasSpoken ? TALK : FIRST_TALK);
		
		button.Disabled = true;
		hasSpoken = true;
	}

	private void OnAnimation_AnimationFinished(StringName animName)
	{
		button.Disabled = false;
		
		animation.AnimationFinished -= OnAnimation_AnimationFinished;
		
		if (button.TextureNormal == null)
		{
			Print.Warn(GetType().Name, "Npc has no texture!");
			return;
		}
		
		var fileName = button
			.TextureNormal.ResourceFileName()
			.Split('_')[0];
		
		EmitSignal(SignalName.PressedNpc, fileName);
	}
}
