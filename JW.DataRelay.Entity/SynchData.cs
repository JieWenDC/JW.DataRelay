using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JW.DataRelay.Entity
{
    /// <summary>
    /// 同步数据表
    /// </summary>
    public class SynchData: BaseEntity
    {
        /// <summary>
        /// 来源Id
        /// </summary>
        [BsonElement("sid")]
        public string SourceId { get; set; }

        /// <summary>
        /// 同步数据(不固定格式)
        /// </summary>
        [BsonElement("data")]
        public BsonDocument Data { get; set; }
    }
}
