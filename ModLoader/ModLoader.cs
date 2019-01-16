using Harmony;
using spacechase0.MiniModLoader.Api;
using spacechase0.MiniModLoader.Api.Mods;
using spacechase0.MiniModLoader.ApiImpl.Mods;
using System.Reflection;

namespace spacechase0.MiniModLoader
{
    public class ModLoader : IModLoader
    {
        internal static ModLoader instance;

        public IModManager Mods { get; internal set; }
        
        private ModLoader()
        {
            Log.Info("Starting...");
            Mods = new ModManager(this);

            var harmony = HarmonyInstance.Create("spacechase0.MiniModLoader");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Mods.Initialize();
        }

        /// <summary>
        /// Initialize the modloader. Only intended to be called once at the start of the game.
        /// </summary>
        public static void Initialize()
        {
            if (instance == null)
                instance = new ModLoader();
        }
    }
}
