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
	[Export] protected AudioStreamPlayer buttonTrunkSfx;
	[Export] protected AudioStreamPlayer buttonMoveSfx;
	[Export] protected TextureRect background;
	
	// Member variables
	private string locationName;
	
	public override void _Ready()
	{
		base._Ready();

		buttonTrunk.Pressed += OnButtonTrunk_Pressed;
		buttonMove.Pressed += OnButtonMove_Pressed;

		animations.SpeedScale = 2;
		var npcs = GetTree().GetNodesInGroup(CharacterBase.GROUP).ToList<CharacterBase>();
		var coins = GetTree().GetNodesInGroup(HintCoins.GROUP).ToList<HintCoins>();
		
		npcs.ForEach((npc) => npc.PressedNpc += OnNpc_PressedNpc);
		coins.ForEach((coin) => coin.Collected += OnCoin_Collected);
		
		locationName = background
			.Texture.ResourceFileName()
			.Replace("bg_", "").ToUpper();
			
		AutoLoad.GetSingleton<DialogueFactory>()
			.Create(locationName, Dialogue.Type.ENTRY);
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
		animations.Play(ANIM_MOVEMENT);
	}
	
	private void OnMove_AnimationFinished()
	{
		// Show arrows 
	}
	
	private void OnNpc_PressedNpc(string npcName)
	{
		if (background.Texture == null)
		{
			Print.Warn(GetType().Name, "The background has no texture!");
			return;
		}
		
		npcName = npcName.ToUpper();
		
		AutoLoad.GetSingleton<DialogueFactory>()
			.Create(locationName, Dialogue.Type.NPC, npcName);
	}

	private void OnCoin_Collected()
	{
		// save coin
	}
}
