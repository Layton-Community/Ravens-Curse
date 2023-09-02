namespace Com.LaytonCommunity.RavensCurse;

public partial class AutoLoad : Singleton
{
	// Statics
	private static bool instanciated = false;
	private static Dictionary<Type, Node> singletons = new();
	
	public static T GetSingleton<T>()
	where
		T : Node
	{
		return singletons.GetValueOrDefault(typeof(T), null) as T;
	}
}

public partial class AutoLoad : Singleton
{
	// Constants
	
	// Enums
	
	// Signals
	
	// Export variables
	[Export] private bool isLogging = false;
	
	// Member variables
	
	public override void _Ready()
	{
		base._Ready();
		
		if (instanciated)
		{
			logger.Error("Cannot be instanciated more than once!");
			QueueFree();
			
			return;
		}
		
		instanciated = true;
		logger.enabled = isLogging;
		
		GetChildren().ToList().ForEach((node) =>
		{
			if (node is Singleton)
			{
				singletons.Add(node.GetType(), node);
				logger.Info($"Found singleton: {node.Name}");
			}
		});
	}
}

