using System.Collections.Generic;
using System.Linq;
using BepInEx;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace RainbowCompany;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, "6.9.0")]
public class Rainbow : BaseUnityPlugin
{
    private const string PLUGIN_GUID = "giosuel.rainbowcompany";
    private const string PLUGIN_NAME = "Rainbow Company";

    internal static List<Material> sceneMaterials = [];
    
    private void Awake()
    {
        new Harmony(PLUGIN_GUID).PatchAll();
        Logger.LogInfo("R A I N B O W");
    }

    public static void RefreshMaterials()
    {
        sceneMaterials = FindObjectsOfType<MeshRenderer>()
            .SelectMany(renderer => renderer.materials)
            .Where(material => material.HasProperty("_Color") && material.color.a != 0f)
            .ToList();
    }
}


[HarmonyPatch(typeof(PlayerControllerB))]
public abstract class PlayerControllerBPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("ConnectClientToPlayerObject")]
    private static void ConnectClientToPlayerObjectPatch(PlayerControllerB __instance)
    {
        Rainbow.RefreshMaterials();
        new GameObject().AddComponent<Company>();
    }
}

[HarmonyPatch(typeof(RoundManager))]
public abstract class RoundManagerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("FinishGeneratingNewLevelClientRpc")]
    private static void FinishGeneratingNewLevelClientRpcPostfixPatch() => Rainbow.RefreshMaterials();
}