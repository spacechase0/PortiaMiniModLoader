namespace spacechase0.MiniModLoader.Api.Mods
{
    /// <summary>
    /// The base mod.
    /// </summary>
    public abstract class IMod
    {
        /// <summary>
        /// The mod loader.
        /// </summary>
        public IModLoader ModLoader { get; internal set; }

        /// <summary>
        /// The mod's manifest.
        /// </summary>
        public IManifest Manifest { get; internal set; }

        /// <summary>
        /// The path the mod was loaded from.
        /// </summary>
        public string DirectoryPath { get; internal set; }

        /// <summary>
        /// Ran after every mod is loaded.
        /// </summary>
        public abstract void AfterModsLoaded();
    }
}
