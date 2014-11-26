using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.AssemblyData
{
    public class AssemblyDataHelper
    {
        private string _dataPath;

        public AssemblyDataHelper(string[] assemblyPath)
        {
            string root = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            string[] paths=string.Concat(root,",", string.Join(",", assemblyPath)).Split(',');
            _dataPath = Path.Combine(paths);
        }

        public string GetFilePath(string fileName)
        {
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }

            return Path.Combine(_dataPath, fileName);
        }
    }
}
