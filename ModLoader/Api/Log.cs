using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace spacechase0.MiniModLoader.Api
{
    /// <summary>
    /// Class for logging.
    /// Debug logs include the full type name of the caller, while others include only the assembly name.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Log at debug level.
        /// </summary>
        /// <param name="str">The message to log.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Debug(object str)
        {
            System.Console.WriteLine($"<{new StackFrame(1).GetMethod().DeclaringType.FullName}> [DEBUG] {str}");
            //LogManager.GetLogger(new StackFrame(1).GetMethod().DeclaringType.FullName).Debug(str);
        }

        /// <summary>
        /// Log at info level.
        /// </summary>
        /// <param name="str">The message to log.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Info(object str)
        {
            System.Console.WriteLine($"<{Assembly.GetCallingAssembly().GetName().Name}> [INFO] {str}");
            //LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name).Info(str);
        }

        /// <summary>
        /// Log at warning level.
        /// </summary>
        /// <param name="str">The message to log.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Warn(object str)
        {
            System.Console.WriteLine($"<{Assembly.GetCallingAssembly().GetName().Name}> [WARN] {str}");
            //LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name).Warn(str);
        }

        /// <summary>
        /// Log at error level.
        /// </summary>
        /// <param name="str">The message to log.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Error(object str)
        {
            System.Console.WriteLine($"<{Assembly.GetCallingAssembly().GetName().Name}> [ERROR] {str}");
            //LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name).Error(str);
        }

        /// <summary>
        /// Log at fatal level.
        /// </summary>
        /// <param name="str">The message to log.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Fatal(object str)
        {
            System.Console.WriteLine($"<{Assembly.GetCallingAssembly().GetName().Name}> [FATAL] {str}");
            //LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name).Fatal(str);
        }
    }
}
