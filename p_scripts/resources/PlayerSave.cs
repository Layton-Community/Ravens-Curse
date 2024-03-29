namespace Com.LaytonCommunity.RavensCurse.Resources;

[Tool]
public partial class PlayerSave : Resource
{
	// Constants
	private const string PATH_USER = "user://";
	private const string PATH_SAVE_DIR = PATH_USER + "save/";
	private const string PATH_SAVE_EXT = ".tres";
	
	// Statics
	// REFACTOR TO AUTOLOAD
	public static PlayerSave Singleton { get; private set; } = null;
	
	// Enums
	
	public static bool Exist(uint id)
	{
		var directory = DirAccess.Open(PATH_SAVE_DIR);
		var pathRessource = PATH_SAVE_DIR + id.ToString() + PATH_SAVE_EXT;
		
		if (directory == null)
		{
			DirAccess.MakeDirAbsolute(ProjectSettings.GlobalizePath(PATH_SAVE_DIR));
			directory = DirAccess.Open(PATH_SAVE_DIR);
		}
		
		return directory.FileExists(pathRessource);
	}
	
	public static PlayerSave LoadFromDisk(uint id)
	{
		if (id > 2)
		{
			throw new ApplicationException($"Cannot load a save with the id: {id}! Id must be inferior to 3");
		}
		
		if (Exist(id))
		{
			var pathRessource = PATH_SAVE_DIR + id.ToString() + PATH_SAVE_EXT;
			var save = ResourceLoader.Load<PlayerSave>(pathRessource, null, ResourceLoader.CacheMode.Ignore);
			
			return save;
		}
		else
		{
			Print.Warn(nameof(PlayerSave), $"Save with id: {id} does not exist! Returning an empty save!");
			
			return new PlayerSave()
			{
				id = id
			};
		}
	}
	
	public static void StoreToDisk(PlayerSave save)
	{
		if (save == null)
		{
			throw new Exception($"Cannot StoreToDisk() the save is null!");
		}
		
		var directory = DirAccess.Open(PATH_SAVE_DIR);
		
		if (directory == null)
		{
			directory.MakeDir(PATH_SAVE_DIR);
		}
		
		var pathRessource = PATH_SAVE_DIR + save.id.ToString() + PATH_SAVE_EXT;
		var error = ResourceSaver.Save(save, pathRessource);
		
		if (error != Error.Ok)
		{
			Print.Error(nameof(PlayerSave), $"Could not save user save: {error}");
		}
	}
		
	public static void LoadToSingleton(uint id)
	{
		Singleton = LoadFromDisk(id);
	}
	
	public static void StoreSingleton()
	{
		StoreToDisk(Singleton);
		
		typeof(PlayerSave).GetFields().ToList().ForEach((e) =>
		{
			Print.Info(nameof(PlayerSave), string.Format("{0,-20}{1}", e.Name + ':', e.GetValue(Singleton)));
		});
	}
}

public partial class PlayerSave : Resource
{
	// Enums
	
	// Signal
	
	// Export variables
	[Export] public uint id;
	[Export] public string username;
	[Export] public string locationCurrent;
	[Export] public string locationGoto;
	[Export] public double timePlayed;
	[Export] public uint picarats;
	[Export] public uint puzzleCompleted;
	[Export] public uint puzzleFound;
	[Export] public uint coinsCurrent;
	[Export] public uint coinsFound;
	
	// Member variables
	
	public PlayerSave() : this(0, "", "", "", 0, 0, 0, 0, 0, 0) {}

	public PlayerSave(uint id, string username, string locationCurrent, string locationGoto, double timePlayed, uint picarats, uint puzzleCompleted, uint puzzleFound, uint coinsCurrent, uint coinsFound)
	{
		this.id = id;
		this.username = username;
		this.locationCurrent = locationCurrent;
		this.locationGoto = locationGoto;
		this.timePlayed = timePlayed;
		this.picarats = picarats;
		this.puzzleCompleted = puzzleCompleted;
		this.puzzleFound = puzzleFound;
		this.coinsCurrent = coinsCurrent;
		this.coinsFound = coinsFound;
	}
	
	public void PrintDbg()
	{
		foreach (var fieldInfo in typeof(PlayerSave).GetFields())
		{
			Print.Info(nameof(PlayerSave), string.Format("{0,-20}{1}", $"{fieldInfo.Name}:", fieldInfo.GetValue(this)));
		}
		
		GD.Print();
	}
}

