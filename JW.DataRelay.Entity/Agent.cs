using JW.DataRelay.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace JW.DataRelay.Entity
{
    /// <summary>
    /// 授权信息表
    /// </summary>
    public class Agent: BaseEntity
    {
        /// <summary>
        /// 应用标识
        /// </summary>
        [BsonElement("appKey")]
        public string AppKey { get; set; }

        /// <summary>
        /// 服务密钥
        /// </summary>
        [BsonElement("masterSecret")]
        public string MasterSecret { get; set; }


        /// <summary>
        /// 代理机构名称
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// RSA公钥
        /// </summary>
        [BsonElement("publickKey")]
        public string RSAPublickKey { get; set; }

        /// <summary>
        /// RSA私钥
        /// </summary>
        [BsonElement("privateKey")]
        public string RSAPrivateKey { get; set; }

        /// <summary>
        /// 最后请求时间
        /// </summary>
        [BsonElement("lastRequestTime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastRequestTime { get; set; }

        /// <summary>
        /// 同步最后一条数据的Id,每次同步通过该Id标识同步到哪里了
        /// </summary>
        [BsonElement("lastAsyncId")]
        public string LastSyncId { get; set; }

        /// <summary>
        /// 数据推送地址
        /// </summary>
        [BsonElement("pushUrl")]
        public string PushUrl { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        public AgentStatusEnum Status { get; set; }

    }
}
