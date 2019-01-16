using spacechase0.MiniModLoader.Api.Mods;
using System;

namespace spacechase0.MiniModLoader.ApiImpl.Mods
{
    internal class BlankManifest : IManifest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }

        public string Type { get; set; }
    }
}
