using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalLib.Modules;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using UnityEngine;

namespace JackAshScrapsNShit;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("BMX.LobbyCompatibility", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency(LethalLib.Plugin.ModGUID)]
[LobbyCompatibility(CompatibilityLevel.ClientOnly, VersionStrictness.None)]
public class JackAshScrapsNShit : BaseUnityPlugin
{
    public static JackAshScrapsNShit Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    public static AssetBundle? JackAshScrapsNShitBundle;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        JackAshScrapsNShitBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "jackashscrapsnshit"));
        if (JackAshScrapsNShitBundle == null) {
            Logger.LogError("Failed to load custom assets."); // ManualLogSource for your plugin
            return;
        }

        int rarity = 5;
        Item giantGummyBear = JackAshScrapsNShitBundle.LoadAsset<Item>("Assets/GiantGummyBear.asset");
        Utilities.FixMixerGroups(giantGummyBear.spawnPrefab);
        NetworkPrefabs.RegisterNetworkPrefab(giantGummyBear.spawnPrefab);
        Items.RegisterScrap(giantGummyBear, rarity, Levels.LevelTypes.All);
        
        rarity = 10;
        Item metalChair = JackAshScrapsNShitBundle.LoadAsset<Item>("Assets/MetalChair.asset");
        Utilities.FixMixerGroups(metalChair.spawnPrefab);
        NetworkPrefabs.RegisterNetworkPrefab(metalChair.spawnPrefab);
        Items.RegisterScrap(metalChair, rarity, Levels.LevelTypes.All);
        
        rarity = 7;
        Item glasses = JackAshScrapsNShitBundle.LoadAsset<Item>("Assets/Glasses.asset");
        Utilities.FixMixerGroups(glasses.spawnPrefab);
        NetworkPrefabs.RegisterNetworkPrefab(glasses.spawnPrefab);
        Items.RegisterScrap(glasses, rarity, Levels.LevelTypes.All);
        
        rarity = 9;
        Item trafficCone = JackAshScrapsNShitBundle.LoadAsset<Item>("Assets/TrafficCone.asset");
        Utilities.FixMixerGroups(trafficCone.spawnPrefab);
        NetworkPrefabs.RegisterNetworkPrefab(trafficCone.spawnPrefab);
        Items.RegisterScrap(trafficCone, rarity, Levels.LevelTypes.All);
        
        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}