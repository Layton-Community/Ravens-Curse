using System.Text.RegularExpressions;

namespace Com.LaytonCommunity.RavensCurse.Game;

[Tool]
[GlobalClass]
public partial class Arrow : TextureButton
{
	// Constants
	public const string GROUP = "arrow";
	
	// Enums
	public enum Direction { Up, Down, Left, Right, Back, BackLeft, BackRight, Hand }
	
	// Signal
	
	// Export variables
	[Export(PropertyHint.File, "*.tscn")] private string pathArrowScene;
	[Export] private Direction ArrowDirection
	{
		get { return arrowDirection; }
		set { SetDirection(value); }
	}

	[ExportGroup("Imports")]
	[Export] private ArrowDictionary arrowDictionary;
	
	// Member variables
	private Direction arrowDirection;
	
	public override void _Ready()
	{
		Pressed += On_Pressed;
	}

	private void On_Pressed()
	{
		if (string.IsNullOrEmpty(pathArrowScene)) return;
		
		GetTree().ChangeSceneToFile(pathArrowScene);
	}
	
	private void SetDirection(Direction newDirection)
	{
		arrowDirection = newDirection;
		
		// If the script is not in the engine, return
		if (!Engine.IsEditorHint() || arrowDictionary == null) return;
		
		if (TextureNormal != null)
		{
			string dirSnakeCase = newDirection.ToString().ToSnakeCase();
			string textureName = TextureNormal.ResourceFileName();
			
			// If the direction enum is the same as the texture, return
			if (textureName.Find(dirSnakeCase) != -1) return;
		}
		
		TextureNormal = arrowDictionary.GetValue(arrowDirection).Item1;
	}
}

