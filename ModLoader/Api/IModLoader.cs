using spacechase0.MiniModLoader.Api.Mods;

namespace spacechase0.MiniModLoader.Api
{
    /// <summary>
    /// The mod loader.
    /// </summary>
    public interface IModLoader
    {
        /// <summary>
        /// The mod manager.
        /// </summary>
        IModManager Mods { get; }
    }
}
