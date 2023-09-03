using System.Text.RegularExpressions;

namespace Com.LaytonCommunity.RavensCurse.Game;

[Tool]
[GlobalClass]
public partial class Arrow : TextureButton
{
	// Constants
	public const string GROUP = "arrow";
	
	private const string FILE_PREFIX = "arrow";
	private const string FILE_IMPORT = ".import";
	
	// Enums
	enum Direction { Up, Down, Left, Right, Back, BackLeft, BackRight, Hand }
	
	// Signal
	
	// Export variables
	[Export(PropertyHint.File, "*.tscn")] private string pathArrowScene;
	[Export] private Direction ArrowDirection
	{
		get { return arrowDirection; }
		set { SetDirection(value); }
	}

	[ExportGroup("Imports")]
	[Export(PropertyHint.Dir)] private string pathArrowSprites;
	
	// Member variables
	private Direction arrowDirection;
	private Dictionary<Direction, Texture2D> directionTextures;
	
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
		
		string dirSnakeCase = newDirection.ToString().ToSnakeCase();
		string textureName = TextureNormal.ResourceFileName();
		
		// If the script is not in the engine or the direction enum is the same as the texture, return
		if (!Engine.IsEditorHint() || textureName.Find(dirSnakeCase) != -1) return;
		
		if (directionTextures == null)
		{
			var error = InitDirectionTextures();
			
			if (error != Error.Ok) return;
		}
		
		TextureNormal = directionTextures[arrowDirection];
	}

	private Error InitDirectionTextures()
	{
		if (pathArrowSprites == null) return Error.FileBadPath;
		
		var directions = (Enum.GetValues(typeof(Direction)) as Direction[]).ToList();
		var files = DirAccess.Open(pathArrowSprites)
			.GetFiles()
			.ToList()
			.FindAll(str => str.Find(FILE_PREFIX) != -1)
			.FindAll(str => str.Find(FILE_IMPORT) == -1)
			.FindAll(str => str.Find("0") != -1);

		var names = new List<string>(files)
			.Select(str => str.Replace($"{FILE_PREFIX}_", string.Empty))
			.Select(str => str.Remove(str.LastIndexOf('_')))
			.Select(str => string.Join(string.Empty, str.Split('_').Select(str => str.ToPascalCase())))
			.ToList();

		directionTextures = new();

		for (int i = 0; i < files.Count; i++)
		{
			directionTextures.Add(
				directions.Find(str => str.ToString() == names[i]),
				GD.Load<Texture2D>($"{pathArrowSprites}/{files[i]}")
			);
		}
		
		return Error.Ok;
	}
}

