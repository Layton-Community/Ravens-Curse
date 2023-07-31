namespace Com.LaytonCommunity.RavensCurse;

public static class ExtentionTween
{
	public static void TweenProperty(this Tween tween, GodotObject godotObject, NodePath property, Variant finalVal, double duration, Tween.TransitionType transition, Tween.EaseType ease, double delay = 0) => 
		tween.TweenProperty(godotObject, property, finalVal, duration)
			.SetTrans(transition)
			.SetEase(ease)
			.SetDelay(delay);
}

