using Harmony;
using Pathea.ArchiveNs;
using spacechase0.MiniModLoader.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Patches
{
	[HarmonyPatch(typeof(Archive))]
    [HarmonyPatch("SaveArchive")]
	public static class SaveHook
    {
        public static void Prefix(string filePath)
        {
            spacechase0.MiniModLoader.Api.Util.InvokeEvent(nameof(Events.BeforeSave), Events.BeforeSave?.GetInvocationList(), null, new BeforeSaveEventArgs(filePath));
        }

        public static void Postfix(string filePath)
        {
            spacechase0.MiniModLoader.Api.Util.InvokeEvent(nameof(Events.AfterSave), Events.AfterSave?.GetInvocationList(), null, new AfterSaveEventArgs(filePath));
        }
    }
    [HarmonyPatch(typeof(Archive))]
    [HarmonyPatch("Load")]
    [HarmonyPatch(new Type[] { typeof(string) } )]
    public static class Loadhook
    {
        public static void Postfix(string filePath)
        {
            spacechase0.MiniModLoader.Api.Util.InvokeEvent(nameof(Events.AfterLoad), Events.AfterLoad?.GetInvocationList(), null, new AfterLoadEventArgs(filePath));
        }
    }
}
