using Harmony;
using spacechase0.MiniModLoader.Api.Mods;
using System.Reflection;

namespace $safeprojectname$
{
    public class Mod : IMod
    {
        public override void AfterModsLoaded()
        {
            var harmony = HarmonyInstance.Create("$username$.$safeprojectname$");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
