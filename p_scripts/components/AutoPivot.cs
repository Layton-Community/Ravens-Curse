namespace Com.LaytonCommunity.RavensCurse.Components;
	
[Tool]
[GlobalClass]
public partial class AutoPivot : Component
{
	
	// Constants
	private const string EDITOR_DESCRIPTION = "Automatically centres the pivot of the parent control.\nYou cannot change the value of the exported string Target, its purpose is to show you which target the AutoPivot is working on.";

	// Enums

	// Signal

	// Export variables
	[Export] private string target;

	// Member variables
	private Control parentControl;
	private Callable callback;
	private bool isPrinting = false;

	public override void _Ready()
	{
		if (Engine.IsEditorHint())
		{
			EditorDescription = EDITOR_DESCRIPTION;
			callback = Callable.From(OnTarget_Resized);
			
			base._Ready();
		}
		else
		{
			QueueFree();
		}
	}

	protected override void ReadyComponent()
	{
		TreeEntered += On_TreeEntered;
		
		On_TreeEntered();
	}

	private void On_TreeEntered()
	{
		var signalResized = Control.SignalName.Resized;
		
		if (parentControl != null && IsInstanceValid(parentControl) && parentControl.IsInsideTree())
		{
			if (parentControl.IsConnected(signalResized, callback))
			{
				parentControl.Disconnect(signalResized, callback);
				PrintInfo($"Removal of \"{parent.Name}\" as the target!");
			}
		}
		
		parent = FindComponentParent();
		parentControl = parent as Control;
		
		if (parentControl != null)
		{
			target = parent.Name;
			
			OnTarget_Resized();
			parentControl.Connect(signalResized, callback);
			PrintInfo($"Setting \"{parent.Name}\" as the target!");
		}
		else
		{
			PrintWarn($"Parent \"{parent.Name}\" is not a Control node!");
		}
	}

	private void OnTarget_Resized()
	{
		parentControl.PivotOffset = parentControl.Size / 2;
	}
	
	private void PrintInfo(string msg)
	{
		if (isPrinting) Print.Info(GetType().Name, msg);
	}
	
	private void PrintWarn(string msg)
	{
		if (isPrinting) Print.Warn(GetType().Name, msg);
	}
}

