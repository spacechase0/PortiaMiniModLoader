using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacechase0.MiniModLoader.Injector
{
    // https://stackoverflow.com/questions/44524414/mono-cecil-add-missing-assembly
    internal class CustomAssemblyResolver : BaseAssemblyResolver
    {
        private DefaultAssemblyResolver _defaultResolver;

        public CustomAssemblyResolver(string sourceDir)
        {
            _defaultResolver = new DefaultAssemblyResolver();
            _defaultResolver.AddSearchDirectory(sourceDir);
        }

        public override AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            AssemblyDefinition assembly;
            assembly = _defaultResolver.Resolve(name);
            return assembly;
        }
    }
}
