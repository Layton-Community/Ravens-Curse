namespace Com.LaytonCommunity.RavensCurse.Components;

[Tool]
[GlobalClass]
public partial class AutoPivot : Component
{

	// Constants
	private const string EDITOR_DESCRIPTION = "Automatically centres the pivot of the parent control.";

	// Enums

	// Signal

	// Export variables
	[Export] private string target;

	// Member variables
	private Control parentControl;
	private bool isPrinting = false;

	public override void _Ready()
	{
		if (Engine.IsEditorHint())
		{
			EditorDescription = EDITOR_DESCRIPTION;
			
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
		if (parentControl != null)
		{
			if (parentControl.IsConnected(Control.SignalName.Resized, Callable.From(OnTarget_Resized)))
			{
				parentControl.Disconnect(Control.SignalName.Resized, Callable.From(OnTarget_Resized));
				PrintInfo($"Removal of \"{parent.Name}\" as the target!");
			}
		}
		
		parent = FindComponentParent();
		parentControl = parent as Control;
		
		if (parentControl != null)
		{
			target = parent.Name;
			
			OnTarget_Resized();
			parentControl.Connect(Control.SignalName.Resized, Callable.From(OnTarget_Resized));
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

