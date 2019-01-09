using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace JW.DataRelay.Entity
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("ctime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
    }
}
