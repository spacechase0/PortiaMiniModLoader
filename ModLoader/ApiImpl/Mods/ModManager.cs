using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using spacechase0.MiniModLoader.Api;
using spacechase0.MiniModLoader.Api.Mods;

namespace spacechase0.MiniModLoader.ApiImpl.Mods
{
    internal class ModManager : IModManager
    {
        private ModLoader modLoader;

        internal ModManager(ModLoader theModLoader)
        {
            modLoader = theModLoader;
        }

        internal override void Initialize()
        {
            Log.Info("Initializing mod manager...");

            var modDir = Path.Combine(Environment.CurrentDirectory, "Mods");
            if (!Directory.Exists(modDir))
                Directory.CreateDirectory(modDir);

            Log.Debug("Processing mods directory...");
            // TODO: Allow command line argument for path
            foreach (var dir in Directory.GetDirectories(modDir))
            {
                try
                {
                    string json = File.ReadAllText(Path.Combine(dir, "manifest.json"));
                    
                    IManifest manifest = Json.FromString<BlankManifest>(json);
                    Log.Info($"Loading mod '{manifest.Id}' from {dir}...");

                    if (manifest.Type == "assembly")
                    {
                        var manAsm = Json.FromString<AssemblyManifest>(json);

                        Log.Debug($"Loading assembly: {manAsm.Assembly.Dll}");

                        Assembly asm = Assembly.LoadFrom(Path.Combine(dir, manAsm.Assembly.Dll));

                        Log.Debug($"Creating mod: {manAsm.Assembly.Class}");
                        Type modType = asm.GetType(manAsm.Assembly.Class);
                        var module = (IMod)Activator.CreateInstance(modType);
                        module.ModLoader = modLoader;
                        module.Manifest = manAsm;
                        module.DirectoryPath = dir;
                        
                        assemblies[manifest.Id] = asm;
                        mods[manifest.Id] = module;
                        asmModuleMap.Add(asm, mods[manifest.Id]);
                    }
                    else
                    {
                        Log.Warn($"Mod type {manifest.Type} not supported!");
                    }
                }
                catch ( Exception e )
                {
                    Log.Error($"Exception while processing mod {dir}: {e}");
                }
            }

            Log.Debug("Notifying mods that loading has completed...");
            foreach ( var mod in mods )
            {
                try
                {
                    mod.Value.AfterModsLoaded();
                }
                catch ( Exception e )
                {
                    Log.Error($"Exception while notifying {mod.Key}:");
                    Log.Error(e.ToString());
                }
            }
        }

        public override bool IsLoaded(string id)
        {
            return mods.ContainsKey(id);
        }

        internal IMod Get( string id )
        {
            return mods[id];
        }

        private IDictionary<string, byte[]> assemblyData = new Dictionary<string, byte[]>();
        private IDictionary<string, IMod> mods = new Dictionary<string, IMod>();
        private IDictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
        internal IDictionary<Assembly, IMod> asmModuleMap = new Dictionary<Assembly, IMod>();
    }
}
