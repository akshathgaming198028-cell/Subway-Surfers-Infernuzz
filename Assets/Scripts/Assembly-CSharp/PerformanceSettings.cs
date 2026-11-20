using UnityEngine;

// Runs automatically when the game starts (no scene wiring needed).
// Caps the frame rate at a steady 60 and turns off costly MSAA to save battery and heat.
public static class PerformanceSettings
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Apply()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;
		QualitySettings.antiAliasing = 0;
	}
}
