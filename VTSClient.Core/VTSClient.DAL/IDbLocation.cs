using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTSClient.DAL
{
    public interface IDbLocation
    {
        string GetDatabasePath(string filename);
    }
}
