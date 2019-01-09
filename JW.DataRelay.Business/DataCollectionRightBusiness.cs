using JW.DataRelay.Entity;
using JW.DataRelay.Provider;
using System.Collections.Generic;

namespace JW.DataRelay.Business
{
    public class DataCollectionRightBusiness
    {
        public DataCollectionRightProvider DataCollectionRightProvider { get; set; }

        public DataCollectionRightBusiness() : base()
        {
            DataCollectionRightProvider = new DataCollectionRightProvider();
        }

        /// <summary>
        /// 根据AgentId和CollectionName获取权限列表
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public List<DataCollectionRight> GetListByAgentIdAndCollectionName(string agentId, string collectionName)
        {
            return DataCollectionRightProvider.GetList(row => row.AgentId == agentId && row.CollectionName == collectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataCollectionRight"></param>
        public void Add(DataCollectionRight dataCollectionRight)
        {
            DataCollectionRightProvider.Add(dataCollectionRight);
        }
    }
}
