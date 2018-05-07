using System.Linq;
using System.Reflection;
using Queris.ExceptionNotifier.Common.Enums;
using Queris.ExceptionNotifier.Common.Loggers;

namespace Queris.ExceptionNotifier.Common.Helpers
{
    public static class UtilHelper
    {
        public static bool IsAppRunning(ILogger logger)
        {
            var ret = false;

            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                for (var x = 0; x < System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length; x++)
                {
                    if (System.Diagnostics.Process.GetProcessesByName(
                        System.Diagnostics.Process.GetCurrentProcess().ProcessName)[x].Id == System.Diagnostics.Process.GetCurrentProcess().Id) continue;
                    logger?.Error("Another program instance is already running, proccess id: " +
                                  $"{System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName)[x].Id}");
                    System.Diagnostics.Process.GetCurrentProcess().Close();
                }
                ret = true;
            }

            return ret;
        }

        public static string GetAssemblyName<T>(AssemblyValue value)
        {
            var avt = typeof(T).Assembly.GetTypes().FirstOrDefault(type => type == typeof(T));
            return (avt != null)
                       ? (value.Equals(AssemblyValue.FullName))
                             ? avt.FullName
                             : avt.Name
                       : "!AssemblyUnknown!";

        }

        public static string GetMethodName(MethodBase methodBase)
        {
            return (methodBase != null && methodBase.DeclaringType != null)
                ? $"{methodBase.DeclaringType.FullName}::{methodBase.Name}"
                : "!MethodUnknown!";
        }
    }
}