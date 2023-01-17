using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace InstantRandom;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static Plugin Instance;
    public static ManualLogSource Log;

    private void Awake()
    {
        Instance = this;
        Log = Logger;
        new Harmony(PluginInfo.PLUGIN_GUID).PatchAll();
    }
}

[HarmonyPatch(typeof(LevelSelectController), nameof(LevelSelectController.clickRandomTrack))]
public class LevelSelectControllerClickRandomTrackPatch : MonoBehaviour
{
    static bool Prefix(LevelSelectController __instance)
    {
        __instance.advanceSongs(Random.Range(1, __instance.alltrackslist.Count), true);
        __instance.Invoke("doneRandomizing", 0f);
        return false;
    }
}