using Harmony;
using Pathea;
using spacechase0.MiniModLoader.Api.Events;

namespace spacechase0.MiniModLoader.Patches
{
    /// <summary>
    /// 
    /// </summary>
    [HarmonyPatch(typeof(SleepModule))]
    [HarmonyPatch("GetOffBed")]
    public static class BedHook
    {
        public static void Postfix()
        {
            // TODO: Make sender the player?
            spacechase0.MiniModLoader.Api.Util.InvokeEvent(nameof(Events.PlayerLeavesBed), Events.PlayerLeavesBed?.GetInvocationList(), null);
        }
    }
}
