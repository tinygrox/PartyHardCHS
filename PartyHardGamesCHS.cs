using App.View;
using App.View.Interface;
using Assets.Scripts.Controllers.Engine;
using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Core.GameObjectManager;
using Assets.Scripts.Core.Localization;
using Assets.Scripts.View.Menu;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using UnityEngine;

namespace PartyHardCHS
{
    [HarmonyPatch]
    public static class PartyHardGamesCHS
    {
        private static string dll_Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string directory = Path.Combine(dll_Location, "Lang");
        static string filename = "lang_smch";

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Localisator), "FillDict")]
        public static void Patch_FillDict(ref XmlDocument ____xmldoc)
        {
            if (Localisator.Localisation == Localisator.LocalisationE.SimplifiedChinese)
            {
                string xml = File.ReadAllText(Path.Combine(directory, filename));
                ____xmldoc.LoadXml(xml);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(LevelManager), nameof(LevelManager.LoadLevel), new[] { typeof(LevelManager.Levels) })]
        public static void Patch_LevelManager()
        {
            LevelManager.LevelMessage = Lang.Transl("Objective: kill them all!");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TKText), MethodType.Constructor, new[] { typeof(GameObject), typeof(bool) })]
        static void Patch_TKText(ref tk2dTextMesh ____textMesh, GameObject container)
        {
            if (GameOptionsController.Instance.Language.Equals("smch") && container != null && ____textMesh != null)
            {
                ____textMesh.font = FontAssets.CheckMyFontData();
                ____textMesh.Commit();
            }
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WorkshopMainScreen), "InitChilds")]
        static void Patch_WorkshopMainScreen(UIPanel ____mainScreenPanel)
        {
            var childByName = GOManager.GetChildByName(____mainScreenPanel.gameObject, "InfoSigns");
            var childByName2 = GOManager.GetChildByName(childByName, "InfoSignSelect");
            tk2dTextMesh component = GOManager.GetChildByName(childByName2, "Title").GetComponent<tk2dTextMesh>();
            component.font = FontAssets.CheckMyFontData();
            GameObject childByName3 = GOManager.GetChildByName(childByName, "InfoSignBack");
            tk2dTextMesh component2 = GOManager.GetChildByName(childByName3, "Title").GetComponent<tk2dTextMesh>();
            component2.font = FontAssets.CheckMyFontData();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.ShowOptions))]
        static void Patch_TitleScreen(GameObject ___optionBg)
        {
            if (GameOptionsController.Instance.Language.Equals("smch"))
            {
                tk2dTextMesh component = GOManager.GetChildByName(___optionBg, "Label").GetComponent<tk2dTextMesh>();
                component.font = FontAssets.CheckMyFontData();
                component.Commit();
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WordsGuiManager), "InitStatuses")]
        public static void Patch_WordsGuiManager(ref Dictionary<int, string[]> ____statusDict)
        {
            if (GameOptionsController.Instance.Language.Equals("smch"))
            {
                ____statusDict[0] = new string[] {
                    Lang.Transl("moppet"),
                    Lang.Transl("noob"),
                    Lang.Transl("easy bot"),
                    Lang.Transl("kitty"),
                    Lang.Transl("chicken"),
                    Lang.Transl("loser"),
                    Lang.Transl("try tutorial"),
                    Lang.Transl("pussy")
                };
                ____statusDict[20] = new string[] {
                    Lang.Transl("pacifist"),
                    Lang.Transl("tiny crusher"),
                    Lang.Transl("next time try better"),
                    Lang.Transl("bot AI"),
                    Lang.Transl("shot-knee"),
                    Lang.Transl("peacewalker"),
                    Lang.Transl("spectator"),
                    Lang.Transl("support")
                };
                ____statusDict[60] = new string[] {
                    Lang.Transl("weak reaper"),
                    Lang.Transl("not casual"),
                    Lang.Transl("partymaker"),
                    Lang.Transl("benefactor"),
                    Lang.Transl("beginner killer"),
                    Lang.Transl("trainee"),
                    Lang.Transl("typical guy"),
                    Lang.Transl("curious deadman")
                };
                ____statusDict[80] = new string[] {
                    Lang.Transl("beginner warrior"),
                    Lang.Transl("almost assassin"),
                    Lang.Transl("impudent biker"),
                    Lang.Transl("local maniac"),
                    Lang.Transl("bad aimbot"),
                    Lang.Transl("failed punisher"),
                    Lang.Transl("wrestler friend"),
                    Lang.Transl("onion knight")
                };
                ____statusDict[95] = new string[] {
                    Lang.Transl("human bean"),
                    Lang.Transl("brute"),
                    Lang.Transl("chaos champion"),
                    Lang.Transl("savage gladiator"),
                    Lang.Transl("party devil"),
                    Lang.Transl("party animal"),
                    Lang.Transl("ninja style"),
                    Lang.Transl("dead can dance")
                };
            }

        }
    }
}
