namespace VTSClient.DAL.Infrastructure
{
    public interface IDbLocation
    {
        string GetDatabasePath(string filename);
    }
}
