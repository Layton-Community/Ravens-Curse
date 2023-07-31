namespace Com.LaytonCommunity.RavensCurse.Components;

[Tool]
[GlobalClass]
public partial class AutoPivot : Component
{
	private const string EDITOR_DESCRIPTION = "Automatically centres the pivot of the parent control.";

	// Constants

	// Enums

	// Signal

	// Export variables

	// Member variables
	private Control parentControl;

	public override async void _Ready()
	{
		if (Engine.IsEditorHint())
		{
			EditorDescription = EDITOR_DESCRIPTION;
			
			await ToSignal(GetTree().CreateTimer(1), Timer.SignalName.Timeout);
			
			TreeEntered += On_TreeEntered;
			
			On_TreeEntered();	
		}
		else
		{
			QueueFree();
		}
	}
	
	private void On_TreeEntered()
	{
		if (parentControl != null)
		{
			if (parentControl.IsConnected(Control.SignalName.Resized, Callable.From(OnTarget_Resized)))
			{
				parentControl.Disconnect(Control.SignalName.Resized, Callable.From(OnTarget_Resized));
				
				Print.Info(GetType().Name, $"Removal of \"{parent.Name}\" as the target!");
			}
		}
		
		parent = FindComponentParent();
		parentControl = parent as Control;
		
		if (parentControl != null)
		{
			parentControl.Connect(Control.SignalName.Resized, Callable.From(OnTarget_Resized));
			
			OnTarget_Resized();
			Print.Info(GetType().Name, $"Setting \"{parent.Name}\" as the target!");
		}
		else
		{
			Print.Warn(GetType().Name, $"Parent \"{parent.Name}\" is not a Control node!");
		}
	}

	private void OnTarget_Resized()
	{
		parentControl.PivotOffset = parentControl.Size / 2;
	}
}

