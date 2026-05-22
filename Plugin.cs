using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Butterfingers.Patches;
namespace Butterfingers
{
    [BepInPlugin("Butterfingers.j08044", "Butterfingers", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        public static Plugin Instance;
        public static ConfigEntry<float> DropChance;

        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");
            Instance = this;
            DropChance = Config.Bind(
                "General",        // section
                "Gun Drop Chance", // key
                0.25f,            // default value
                new ConfigDescription(
                    "Chance of your gun dropping on malfunction (0.0 - 1.0) Ex: 0.25 = 25% chance of gun dropping",
                    new AcceptableValueRange<float>(0f, 1f)
                )
            );
            
            new Patches.Patches.ButterfingersP().Enable();
        }
    }
}
