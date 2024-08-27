using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace FasterEmployees
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource { get; private set; }

        private void Awake()
        {
            // Plugin startup logic
            LogSource = Logger;
            LogSource.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly);
        }
    }
}
