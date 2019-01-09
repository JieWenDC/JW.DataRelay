using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JW.DataRelay.Web.Models
{
    public class AuthorityInfo
    {
        public string Id { get; set; }
        public string AgentId { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }

        private Auth Auth { get; set; }
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum Auth
    {
        /// <summary>
        /// 推送
        /// </summary>
        Push = 1,

        /// <summary>
        /// 获取
        /// </summary>
        Obtain = 2,

        /// <summary>
        /// 接收
        /// </summary>
        Receive = 4
    }
}