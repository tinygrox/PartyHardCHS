using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PartyHardCHS
{
    [BepInPlugin("tinygrox.CHS.PartyHard", "Party Hard 1 Chinese Translation", "1.0.0")]
    [BepInProcess("PartyHardGame.exe")]
    public class PluginMain : BaseUnityPlugin
    {
        Harmony HarmonyInstance;
        private void Awake()
        {
            Logger.LogInfo("Plugin Party Hard Chinese Translation is loaded!");
            HarmonyInstance = new Harmony("tinygrox.CHS.PartyHard");
            HarmonyInstance.PatchAll();
        }
    }
}
