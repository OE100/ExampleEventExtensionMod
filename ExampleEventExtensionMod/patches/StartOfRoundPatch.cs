using HarmonyLib;

namespace ExampleEventExtensionMod.patches
{
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Awake))]
    public class StartOfRoundPatch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            // This is where you would add your custom event to the event pool - it just needs to happen after the network handler is registered
            Plugin.RegisterEventsAndItems();
        }
    }
}