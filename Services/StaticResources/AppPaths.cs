using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheJobOrganizationApp.Services.StaticResources
{
    static class AppPaths
    {
        public readonly static string settingsFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Settings");
        public readonly static string IconsFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Icons");
        public readonly static string assemblyFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "assembly.json");
    }
}
