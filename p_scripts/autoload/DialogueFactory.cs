namespace Com.LaytonCommunity.RavensCurse.Game;

public partial class DialogueFactory : Singleton
{
	// Constants

	// Enums

	// Signal

	// Export variables
	[ExportGroup("Imports")]
	[Export(PropertyHint.File,"*.tscn")] private string sceneDialogue;
	
	// Member variables
	
	public void Create(string locationName, Dialogue.Type type, string npcName, string version = "")
	{
		var instance = sceneDialogue.InstantiateFromPath<Dialogue>();
		instance.key = $"{locationName}_{type}_{npcName}";
		instance.npcName = npcName;
		
		GetTree().Root.AddChild(instance);
	}
}
