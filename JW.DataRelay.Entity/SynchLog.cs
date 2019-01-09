using JW.DataRelay.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JW.DataRelay.Entity
{
    /// <summary>
    /// 同步记录表
    /// </summary>
    public class SynchLog : BaseEntity
    {
        /// <summary>
        /// 代理标识
        /// </summary>
        [BsonElement("agentId")]
        public string AgentId { get; set; }

        /// <summary>
        /// 同步数据条数
        /// </summary>
        [BsonElement("totalCount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [BsonElement("type")]
        public SynchLogTypeEnum Type { get; set; }

        /// <summary>
        /// 集合名称（数据类型）
        /// </summary>
        [BsonElement("collectionName")]
        public string CollectionName { get; set; }

        /// <summary>
        /// 最小ObjectId,开始数据标识
        /// </summary>
        [BsonElement("minId")]
        public string MinId { get; set; }

        /// <summary>
        /// 最大ObjectId,结束数据标识
        /// </summary>
        [BsonElement("maxId")]
        public string MaxId { get; set; }

        /// <summary>
        /// 操作结果描述
        /// </summary>
        [BsonElement("ret")]
        public string Result { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 使用的令牌
        /// </summary>
        public string Token { get; set; }
    }
}
