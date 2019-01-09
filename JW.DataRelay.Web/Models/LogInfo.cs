using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JW.DataRelay.Web.Models
{
    /// <summary>
    /// 日志实体
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 代理标识
        /// </summary>
        public string AgentId { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public string  CreateTime { get; set; }

        /// <summary>
        /// 同步数据条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public Behavior Type { get; set; }

        /// <summary>
        /// 集合名称(数据类型)
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// 最小ObjectId，开始数据标识
        /// </summary>
        public string MinId { get; set; }

        /// <summary>
        /// 最大ObjectId，结束数据标识
        /// </summary>
        public string MaxId { get; set; }

        /// <summary>
        /// 操作结果描述
        /// </summary>
        public string Result { get; set; }
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum Behavior
    {
        /// <summary>
        /// 推送
        /// </summary>
        Push=1,
        
        /// <summary>
        /// 获取
        /// </summary>
        Obtain=2,

        /// <summary>
        /// 接收
        /// </summary>
        Receive=4
    }
}