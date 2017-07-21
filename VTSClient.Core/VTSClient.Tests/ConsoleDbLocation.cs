using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTSClient.Tests
{
    public static class ConsoleDbLocation
    {
        public static string GetDatabasePath(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsPath, fileName);

            return path;
        }
    }
}
