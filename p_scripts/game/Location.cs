namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class Location : Ui.UiBase
{
	// Constants
	public const string ANIM_TRUNK = "game_location/open_trunk";
	public const string ANIM_MOVEMENT = "game_location/open_movement";
	
	// Enums
	
	// Signal
	
	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneTrunk;
	[Export(PropertyHint.File,"*.tscn")] private string sceneDialogue;
	[Export] protected Texture2D textureTrunkDust;
	[Export] protected Texture2D textureMoveDust;
	[Export] protected TextureButton buttonTrunk;
	[Export] protected TextureButton buttonMove;
	[Export] protected Button buttonCursor;
	[Export] protected AudioStreamPlayer buttonTrunkSfx;
	[Export] protected AudioStreamPlayer buttonMoveSfx;
	[Export] protected TextureRect background;
	
	// Member variables
	private string locationName;
	
	private List<Arrow> arrows;
	private Cursor cursor;
	
	
	
	public override void _Ready()
	{
		base._Ready();
		
		buttonTrunk.Pressed += OnButtonTrunk_Pressed;
		buttonMove.Pressed += OnButtonMove_Pressed;
		buttonCursor.Pressed += OnButtonClick_Pressed;
		animations.AnimationFinished += On_ReadyAnimationFinished;
		
		cursor = AutoLoad.GetSingleton<Cursor>();
		animations.SpeedScale = 2;
		
		if (background.Texture == null) { return; }
		
		var npcs = GetTree().GetNodesInGroup(CharacterBase.GROUP).ToList<CharacterBase>();
		var coins = GetTree().GetNodesInGroup(HintCoin.GROUP).ToList<HintCoin>();
		var popups = GetTree().GetNodesInGroup(PopupZone.GROUP).ToList<PopupZone>();
		arrows = GetTree().GetNodesInGroup(Arrow.GROUP).ToList<Arrow>();
		
		npcs.ForEach((npc) => npc.PressedNpc += OnNpc_PressedNpc);
		coins.ForEach((coin) => coin.Collected += OnCoin_Collected);
		popups.ForEach((popup) => popup.ZonePressed += OnPopupZone_Pressed);
		arrows.ForEach((arrow) => arrow.Hide());
	}
	
	private void On_ReadyAnimationFinished(StringName _)
	{
		animations.AnimationFinished -= On_ReadyAnimationFinished;
		
		locationName = background
			.Texture.ResourceFileName()
			.Replace("bg_", "").ToUpper();
			
		AutoLoad.GetSingleton<DialogueFactory>()
			.Create(locationName, Dialogue.Type.ENTRY);
	}
	
	
	
	private void OnButtonClick_Pressed()
	{
		cursor.ClickEffect();
	}
	
	
	
	private void OnNpc_PressedNpc(string npcName)
	{
		npcName = npcName.ToUpper();
		
		AutoLoad.GetSingleton<DialogueFactory>()
			.Create(locationName, Dialogue.Type.NPC, npcName);
	}
	
	
	
	private void OnButtonTrunk_Pressed()
	{
		animations.Play(ANIM_TRUNK);
	}
	
	private void OnTrunk_AnimationFinished()
	{
		animations.AnimationFinished += (_) =>
		{
			ChangeSceneToFile(sceneTrunk, true);
		};
		
		animations.Play(ANIM_FADE_OUT);
	}
	
	
	
	private void OnButtonMove_Pressed()
	{
		animations.AnimationFinished += OnAnimationMove_Finished;
		
		animations.Play(ANIM_MOVEMENT);
	}
	
	private void OnAnimationMove_Finished(StringName _)
	{
		animations.AnimationFinished -= OnAnimationMove_Finished;
		
		arrows.ForEach((arrow) => arrow.OnLocation_ShowArrows(this));
	}
	
	public void OnArrow_Pressed(string pathArrow)
	{
		animations.AnimationFinished += (_) =>
		{
			ChangeSceneToFile(pathArrow, true);
		};
		
		animations.Play(ANIM_FADE_OUT);
	}
	
	
	
	private void OnCoin_Collected()
	{
		// save coin
	}
	
	
	
	public void OnPopupZone_Pressed(PackedScene popupScene)
	{
		AddSibling(popupScene.Instantiate());
	}
}
