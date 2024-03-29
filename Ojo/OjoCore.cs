using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using Burro;
using Ninject;
using Ojo.View;

namespace Ojo
{
    public class OjoCore
    {
        private readonly IKernel _kernel;
        private readonly IBurroCore _parser;

        private IMainWindow _mainWindow;

        public OjoCore(IKernel kernel, IBurroCore parser)
        {
            _kernel = kernel;
            _parser = parser;
        }

        public void Initialise()
        {
            var assembly = Assembly.GetEntryAssembly();
            var baseDir = ".";

            if (assembly != null)
            {
                baseDir = Path.GetDirectoryName(assembly.Location);
            }
            var configPath = baseDir + "/ojo.yml";

            EnsureConfigExists(configPath);

            Initialise(configPath);
        }

        public void Initialise(string configFile)
        {
            InitialiseParser(configFile);

            SetupMainWindow();
        }

        private void SetupMainWindow()
        {
            _mainWindow = _kernel.Get<IMainWindow>();
            _mainWindow.Initialise(_parser.BuildServers);
            _mainWindow.Show();
        }

        private void InitialiseParser(string configFile)
        {
            _parser.Initialise(configFile);

            _parser.StartMonitoring();
        }

        private void EnsureConfigExists(string configPath)
        {
            if (!File.Exists(configPath))
            {
                var resourceAssembly = Assembly.GetExecutingAssembly();
                var defaultConfig = resourceAssembly.GetManifestResourceStream("Ojo.Config.ojo.yml");
                WriteStreamToFile(defaultConfig, configPath);

                GiveWriteAccessToUsers(configPath);

                throw new FileLoadException("No config file found.  Put default at " + configPath);
            }
        }

        private void GiveWriteAccessToUsers(string configPath)
        {
            var fileSecurity = File.GetAccessControl(configPath);
            fileSecurity.AddAccessRule(new FileSystemAccessRule("BUILTIN\\Users", FileSystemRights.Write, AccessControlType.Allow));
            File.SetAccessControl(configPath, fileSecurity);
        }

        private void WriteStreamToFile(Stream stream, string fileName)
        {
            var outputFile = new FileStream(fileName, FileMode.Create);

            try
            {
                const int length = 256;
                var buffer = new Byte[length];

                var bytesRead = stream.Read(buffer, 0, length);
                while (bytesRead > 0)
                {
                    outputFile.Write(buffer, 0, bytesRead);
                    bytesRead = stream.Read(buffer, 0, length);
                }
            }
            finally
            {
                stream.Close();
                outputFile.Close();
            }
        }
    }
}