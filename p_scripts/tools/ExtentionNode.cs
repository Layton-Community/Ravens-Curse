namespace Com.LaytonCommunity.RavensCurse;

public static class ExtentionNode
{
	public static void CallDeferred(this Node node, Action callback)
	{
		if (callback != null)
		{
			node.ToSignal(node.GetTree(), SceneTree.SignalName.ProcessFrame).OnCompleted(callback);
		}
	}
}


