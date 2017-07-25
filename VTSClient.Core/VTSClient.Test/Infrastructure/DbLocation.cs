using System.IO;
using VTSClient.DAL.Infrastructure;

namespace VTSClient.Test.Infrastructure
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
