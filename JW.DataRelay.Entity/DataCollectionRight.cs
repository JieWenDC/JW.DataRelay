using JW.DataRelay.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JW.DataRelay.Entity
{
    /// <summary>
    /// 同步数据集合权限表
    /// </summary>
    public class DataCollectionRight: BaseEntity
    {
        /// <summary>
        /// 代理机构Id
        /// </summary>
        [BsonElement("agentId")]
        public string AgentId { get; set; }

        /// <summary>
        /// 集合名称
        /// </summary>
        [BsonElement("name")]
        public string CollectionName { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [BsonElement("auth")]
        public DataCollectionRightAuthTypeEnum Auth { get; set; }
    }
}
