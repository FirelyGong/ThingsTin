using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.License
{
    internal class LicenseStorageUtil
    {

        private const string VerificationInfo = "{0}_VI.txt";
        private const string LicenseInfo = "{0}_LI.txt";

        private string _verificationInfoFile;
        private string _licenseInfoFile;
        private string _folderName;// = "F313D99C-6EE2-4190-BD79-2DC1D1547129";
        internal LicenseStorageUtil(Guid appId, string assemblyCode)
        {
            _folderName = appId.ToString();
            _verificationInfoFile = GetFilePath(string.Format(CultureInfo.InvariantCulture, VerificationInfo, assemblyCode));
            _licenseInfoFile = GetFilePath(string.Format(CultureInfo.InvariantCulture, LicenseInfo, assemblyCode));
        }

        internal string LoadLicenseInformation()
        {
            return LoadFile(_licenseInfoFile);
        }

        internal void WriteLicenseInformation(string content)
        {
            WriteFile(content, _licenseInfoFile);
        }

        internal string LoadVerificationInformation()
        {           
            return LoadFile(_verificationInfoFile);
        }

        internal void WriteVerificationInformation(string content)
        {
            WriteFile(content, _verificationInfoFile);
        }

        private void WriteFile(string content, string fileName)
        {
            FileMode mode = FileMode.Create;
            if (File.Exists(fileName))
            {
                mode = FileMode.Truncate;
            }

            using (var stream = new FileStream(fileName, mode, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(content);
                }
            }
        }

        private static string LoadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (var stream = new FileStream(fileName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        return content;
                    }
                }
            }

            return string.Empty;
        }

        private string GetFilePath(string fileName)
        {
            string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), _folderName);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            return Path.Combine(root, fileName);
        }
    }
}
