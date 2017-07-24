using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VTSClient.DAL;

namespace VTSClient.Test
{
    public class DbLocation : IDbLocation
    {
        public string GetDatabasePath(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            return path;
        }
    }
}
