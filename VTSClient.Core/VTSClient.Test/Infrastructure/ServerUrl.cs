using VTSClient.DAL.Infrastructure;

namespace VTSClient.Test.Infrastructure
{
    public class ServerUrl : IServerUrl
    {
        public string GetServerUrl()
        {
           const string hostUrl = "http://localhost:5000";

            const string vacationUrl = hostUrl + "/vts/workflow";

            return vacationUrl;
        }
    }
}
