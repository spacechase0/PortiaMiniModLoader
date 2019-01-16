namespace spacechase0.MiniModLoader.Api.Mods
{
    /// <summary>
    /// The common mod manifest attributes.
    /// </summary>
    public interface IManifest
    {
        /// <summary>
        /// The ID of this mod.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The user-friendly name of this mod.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The description of this mod.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The author of this mod.
        /// </summary>
        string Author { get; }
        
        // TODO: Semantic version? Or something other than just a string
        /// <summary>
        /// The version of this mod.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// The mod type.
        /// </summary>
        string Type { get; }
    }
}
