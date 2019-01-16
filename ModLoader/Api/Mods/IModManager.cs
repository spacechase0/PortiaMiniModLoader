namespace spacechase0.MiniModLoader.Api.Mods
{
    /// <summary>
    /// The mod manager.
    /// </summary>
    public abstract class IModManager
    {
        /// <summary>
        /// Load mod and stuff.
        /// </summary>
        internal abstract void Initialize();

        /// <summary>
        /// Check if a mod is loaded.
        /// </summary>
        /// <param name="id">The ID of the expected mod.</param>
        /// <returns>If the mod with ID id is loaded or not.</returns>
        public abstract bool IsLoaded(string id);
    }
}
