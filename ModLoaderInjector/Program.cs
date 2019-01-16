using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace spacechase0.MiniModLoader.Injector
{
    class Program
    {
        public const string TARGET_VERSION = "Final 1.0";
        public const string MAIN_ASSEMBLY = "Assembly-CSharp.dll";
        public const string TARGET_ASSEMBLY = "Assembly-CSharp-firstpass.dll";
        public const string WRITE_SUFFIX = "$MEOW";

        static void Main(string[] args)
        {
            try
            {
                string mainAsmPath = Path.Combine("Portia_Data", "Managed", MAIN_ASSEMBLY);
                string targetAsmPath = Path.Combine("Portia_Data", "Managed", TARGET_ASSEMBLY);
                if (!File.Exists(mainAsmPath) || !File.Exists(targetAsmPath))
                {
                    Console.WriteLine($"Failed to find {MAIN_ASSEMBLY} or {TARGET_ASSEMBLY} in " + Path.GetDirectoryName(mainAsmPath));
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Reading assembly...");
                var mainAsm = AssemblyDefinition.ReadAssembly(mainAsmPath);
                var targetAsm = AssemblyDefinition.ReadAssembly(targetAsmPath);

                Console.WriteLine("Checking version...");
                string ver = CollectVersion(mainAsm);
                if (ver != TARGET_VERSION)
                {
                    Console.WriteLine($"Looking for version '{TARGET_VERSION}', found '{ver}'!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Making backup..");
                string backupPath = Path.Combine("Portia_data", "Managed", $"{TARGET_ASSEMBLY}.{ver}.backup");
                if (File.Exists(backupPath))
                {
                    Console.WriteLine("\tBackup already exists.");
                }
                else
                {
                    File.Copy(targetAsmPath, backupPath);
                }

                Console.WriteLine("Injecting...");
                //DoWork1(mainAsm);
                DoWork2(targetAsm);

                Console.WriteLine("Writing output...");
                targetAsm.Write(targetAsmPath + WRITE_SUFFIX);
                targetAsm.Dispose();
                File.Delete(targetAsmPath);
                File.Move(targetAsmPath + WRITE_SUFFIX, targetAsmPath);

            }
            catch ( Exception e )
            {
                if ( e.Message == "Already installed" )
                    Console.WriteLine("MiniModLoader already installed");
                else
                    Console.WriteLine("Exception: " + e);
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static string CollectVersion(AssemblyDefinition asm)
        {
            var module = asm.MainModule;
            var type = module.GetType("Pathea.Version");
            var method = type.Methods.First(p => p.Name == ".cctor");

            string title = "";
            int major = 0, minor = 0;

            Object obj = null;
            foreach ( var insn in method.Body.Instructions )
            {
                if (insn.OpCode.Code == Mono.Cecil.Cil.Code.Ldstr || insn.OpCode.Code == Mono.Cecil.Cil.Code.Ldc_I4)
                    obj = insn.Operand;
                else if (insn.OpCode.Code == Mono.Cecil.Cil.Code.Ldc_I4_S)
                    obj = (int)(sbyte)insn.Operand;
                else if (insn.OpCode.Code >= Mono.Cecil.Cil.Code.Ldc_I4_M1 && insn.OpCode.Code <= Mono.Cecil.Cil.Code.Ldc_I4_8)
                    obj = insn.OpCode.Code - Mono.Cecil.Cil.Code.Ldc_I4_0;
                else if (insn.OpCode.Code == Mono.Cecil.Cil.Code.Stsfld)
                {
                    var field = insn.Operand as FieldDefinition;
                    if (field.Name == "Title")
                        title = (string)obj;
                    else if (field.Name == "major")
                        major = (int)obj;
                    else if (field.Name == "minor")
                        minor = (int)obj;
                }
            }

            return $"{title} {major}.{minor}";
        }

        // https://github.com/spaar/besiege-modloader/blob/master/ModLoader%20Injector/Injector.cs
        // TODO: Use this instead of Asssembly-CSharp-firstpass.dll & DoWork2 once I figure out why Cecil hates us
        /*
        private static void DoWork1(AssemblyDefinition asm)
        {
            var module = asm.MainModule;
            var type = module.GetType("Ccc.CccModuleLoader");
            var method = type.Methods.First(p => p.Name == "Start");

            var insns = method.Body.Instructions;
            var proc = method.Body.GetILProcessor();
            var spot = 0;

            // Assembly.GetExecutingAssembly().Location
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.Reflection.Assembly", "GetExecutingAssembly")));
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.Reflection.Assembly", "get_Location")));
            
            // Path.GetDirectoryName( ^ )
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.IO.Path", "GetDirectoryName")));

            insns.Insert(spot++, proc.Create(OpCodes.Ldstr, "../../MiniModLoader.dll"));

            // Path.Combine( ^^, ^ )
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.IO.Path", "Combine", new Type[] { typeof(string), typeof(string) })));

            // Assembly.LoadFrom( ^ )
            insns.Insert( spot++, proc.Create( OpCodes.Call, ImportSystemMethod(module, "System.Reflection.Assembly", "LoadFrom", new Type[] { typeof(string) })));

            // ^.GetType( "spacechase0.MiniModLoader.ModLoader" )
            insns.Insert(spot++, proc.Create(OpCodes.Ldstr, "spacechase0.MiniModLoader.ModLoader"));
            insns.Insert(spot++, proc.Create(OpCodes.Callvirt, ImportSystemMethod(module, "System.Reflection.Assembly", "GetType", new Type[] { typeof(string) })));
            
            // ^.GetMethod( "Initialize" )
            insns.Insert(spot++, proc.Create(OpCodes.Ldstr, "Initialize"));
            insns.Insert(spot++, proc.Create(OpCodes.Callvirt, ImportSystemMethod(module, "System.Type", "GetMethod", new Type[] { typeof(string) })));
            
            // ^.Invoke(null, null)
            insns.Insert(spot++, proc.Create(OpCodes.Ldnull));
            insns.Insert(spot++, proc.Create(OpCodes.Ldnull));
            insns.Insert(spot++, proc.Create(OpCodes.Callvirt, ImportSystemMethod(module, "System.Reflection.MethodBase", "Invoke", new Type[] { typeof(object), typeof(object[]) })));

            // Clear the return value (which is on the stack) from that call.
            insns.Insert(spot++, proc.Create(OpCodes.Pop));
        }
        */

        private static void DoWork2(AssemblyDefinition asm)
        {
            var module = asm.MainModule;
            var type = module.GetType("Steamworks.SteamAPI");
            var method = type.Methods.First(p => p.Name == "Init");

            var insns = method.Body.Instructions;
            var proc = method.Body.GetILProcessor();
            var spot = 0;

            // First, check if it is already installed
            foreach ( var insn in insns )
            {
                if (insn.OpCode == OpCodes.Ldstr && (string) insn.Operand == "spacechase0.MiniModLoader.ModLoader")
                {
                    throw new Exception("Already installed");
                }
            }


            // Assembly.GetExecutingAssembly().Location
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.Reflection.Assembly", "GetExecutingAssembly")));
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.Reflection.Assembly", "get_Location")));

            // Path.GetDirectoryName( ^ )
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.IO.Path", "GetDirectoryName")));

            insns.Insert(spot++, proc.Create(OpCodes.Ldstr, "../../MiniModLoader.dll"));

            // Path.Combine( ^^, ^ )
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.IO.Path", "Combine", new Type[] { typeof(string), typeof(string) })));

            // Assembly.LoadFrom( ^ )
            insns.Insert(spot++, proc.Create(OpCodes.Call, ImportSystemMethod(module, "System.Reflection.Assembly", "LoadFrom", new Type[] { typeof(string) })));

            // ^.GetType( "spacechase0.MiniModLoader.ModLoader" )
            insns.Insert(spot++, proc.Create(OpCodes.Ldstr, "spacechase0.MiniModLoader.ModLoader"));
            insns.Insert(spot++, proc.Create(OpCodes.Callvirt, ImportSystemMethod(module, "System.Reflection.Assembly", "GetType", new Type[] { typeof(string) })));

            // ^.GetMethod( "Initialize" )
            insns.Insert(spot++, proc.Create(OpCodes.Ldstr, "Initialize"));
            insns.Insert(spot++, proc.Create(OpCodes.Callvirt, ImportSystemMethod(module, "System.Type", "GetMethod", new Type[] { typeof(string) })));

            // ^.Invoke(null, null)
            insns.Insert(spot++, proc.Create(OpCodes.Ldnull));
            insns.Insert(spot++, proc.Create(OpCodes.Ldnull));
            insns.Insert(spot++, proc.Create(OpCodes.Callvirt, ImportSystemMethod(module, "System.Reflection.MethodBase", "Invoke", new Type[] { typeof(object), typeof(object[]) })));

            // Clear the return value (which is on the stack) from that call.
            insns.Insert(spot++, proc.Create(OpCodes.Pop));
        }

        private static MethodReference ImportSystemMethod(ModuleDefinition module, string typeName, string methName, Type[] types = null)
        {
            var type = Type.GetType(typeName);
            MethodInfo meth;
            if ( types == null )
            {
                meth = type.GetMethod(methName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            }
            else
            {
                meth = type.GetMethod(methName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, types, null);
            }
            return module.ImportReference(meth);
        }

        private static MethodReference ImportGameMethod(ModuleDefinition module, string typeName, string methName)
        {
            var type = module.Types.First(p => p.Name == typeName);
            var meth = type.Resolve().Methods.First(p => p.Name == methName);
            return module.ImportReference(meth);
        }
    }
}
