using spacechase0.MiniModLoader.Api.Mods;

namespace spacechase0.MiniModLoader.ApiImpl.Mods
{
    internal class AssemblyManifest : IManifest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }

        public string Type => "assembly";

        public class AssemblyData
        {
            public string Dll { get; set; }
            public string Class { get; set; }
        }
        public AssemblyData Assembly { get; set; } = new AssemblyData();
    }
}
