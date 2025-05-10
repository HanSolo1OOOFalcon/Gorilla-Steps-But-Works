using BepInEx;

namespace GorillaStepsButWorks
{
	// This is your mod's main class.
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
    {
	    void Start()
	    {
		    HarmonyPatches.ApplyHarmonyPatches();
	    }
    }
}
