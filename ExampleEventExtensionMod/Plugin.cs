using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using ExampleEventExtensionMod.events;
using ExampleEventExtensionMod.items;
using HarmonyLib;
using LethalLib.Modules;
using LuckyDice.custom.events;
using LuckyDice.Patches;
using UnityEngine;

namespace ExampleEventExtensionMod
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(LuckyDice.Plugin.GUID, LuckyDice.Plugin.VERSION)]
    [BepInDependency(LethalLib.Plugin.ModGUID, LethalLib.Plugin.ModVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(GUID);

        private const string GUID = "oe.tweaks.luckydice.extension.example";
        private const string NAME = "Lucky Dice Extension Example";
        private const string VERSION = "0.0.1";

        internal static Plugin Instance;

        internal static ManualLogSource Log;

        public static AssetBundle ab;
        
        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"'{NAME}' is loading...");

            if (Instance == null)
                Instance = this;
            
            ab = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames()[0]));
            if (ab == null)
            {
                Log.LogError("Failed to load asset bundle");
            }
            
            RegisterItem("assets/templates/monkey/TestItem.asset", 50, Levels.LevelTypes.All);
            
            harmony.PatchAll();

            Log.LogInfo($"'{NAME}' loaded!");
        }
        
        private Item RegisterItem(string path, int rarity, Levels.LevelTypes levelTypes)
        {
            Item item = ab.LoadAsset<Item>(path);
            if (item == null)
                Log.LogError($"Failed to load item: {path}");
            else
            {
                Items.RegisterScrap(item, rarity, levelTypes);
                NetworkPrefabs.RegisterNetworkPrefab(item.spawnPrefab);
            }

            return item;
        }
        
        internal static void RegisterEventsAndItems()
        {
            // Register your custom events and items here
            string testItemPool = EventRegistry.RegisterItem<TestItem>();
            EventRegistry.RegisterEvent<TestEvent>(testItemPool);
        }
    }
}