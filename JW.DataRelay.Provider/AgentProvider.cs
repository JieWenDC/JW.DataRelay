using JW.DataRelay.Entity;
using MongoDB.Driver;

namespace JW.DataRelay.Provider
{
    public class AgentProvider : BaseProvider<Agent>
    {
        public AgentProvider() : base("DataSync_Auth") { }

        public void Update(Agent agent)
        {
            var filter = Filter.Eq(row => row.Id, agent.Id);
            var update = Updater.Set(row => row.LastRequestTime, agent.LastRequestTime)
                .Set(row => row.LastSyncId, agent.LastSyncId)
                .Set(row => row.MasterSecret, agent.MasterSecret)
                .Set(row => row.Name, agent.Name)
                .Set(row => row.PushUrl, agent.PushUrl)
                .Set(row => row.RSAPrivateKey, agent.RSAPrivateKey)
                .Set(row => row.RSAPublickKey, agent.RSAPublickKey)
                .Set(row => row.Status, agent.Status);
            Collection.UpdateOne(filter, update, new UpdateOptions() { IsUpsert = false, BypassDocumentValidation = false });
        }
    }
}
