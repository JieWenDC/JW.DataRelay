using JW.DataRelay.Entity;
using JW.DataRelay.Provider;

namespace JW.DataRelay.Business
{
    public class AgentBusiness
    {
        private AgentProvider AgentProvider { get; set; }

        public AgentBusiness()
        {
            AgentProvider = new AgentProvider();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public Agent GetByAppKey(string appKey)
        {
            return AgentProvider.Get(row => row.AppKey == appKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        public void Add(Agent agent)
        {
            AgentProvider.Add(agent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        public void Update(Agent agent)
        {
            AgentProvider.Update(agent);
        }
    }
}
