using Harmony;
using Pathea.UISystemNs;
using spacechase0.MiniModLoader.ApiImpl.Mods;
using spacechase0.MiniModLoader.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace spacechase0.MiniModLoader.Patches
{
    /// <summary>
    /// This hook just shows how many mods are loaded on the main menu.
    /// </summary>
    [HarmonyPatch(typeof(LaunchUICtr))]
    [HarmonyPatch("Awake")]
    public static class MainMenuAwakeHook
    {
        public static void Postfix(LaunchUICtr __instance, TextMeshProUGUI ___versionText)
        {
            var modCount = (ModLoader.instance.Mods as ModManager).asmModuleMap.Count;
            ___versionText.text = $"MiniModLoader v1.0.0\n{modCount} mods loaded\nGame {___versionText.text}";

            var obj = new GameObject();
            obj.AddComponent<GlobalObjectComponent>();
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("Game"));
        }
    }
}
