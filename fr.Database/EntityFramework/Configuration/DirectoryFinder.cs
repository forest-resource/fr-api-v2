using System;
using System.IO;
using System.Linq;

namespace fr.Database.EntityFramework.Configuration
{
    public class DirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var appSettingsFile = @"appsettings.json";
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(InitDbModule).Assembly.Location);
            if (File.Exists(Path.Combine(coreAssemblyDirectoryPath, appSettingsFile)))
            {
                return coreAssemblyDirectoryPath;
            }

            if (coreAssemblyDirectoryPath == null)
            {
                throw new Exception("Could not find location of App.Core assembly!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "fr-service-app-v2.sln"))
            {
                directoryInfo = directoryInfo.Parent ?? throw new Exception("Could not find content root folder!");
            }

            var webHostFolder = Path.Combine(directoryInfo.FullName, "fr.AppServer");
            if (Directory.Exists(webHostFolder))
            {
                return webHostFolder;
            }

            throw new Exception("Could not find root folder of the web project!");
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
