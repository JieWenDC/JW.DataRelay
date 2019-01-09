using LJ.UM.Framework.Models.Input;
using System;
using System.Collections.Generic;

namespace JW.DataRelay.Model.Input.SynchData
{
    public class GetSynchDataListInput : IPagingInput, ICreateTimeInput
    {
        /// <summary>
        /// 返回条数
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 返回总数据条数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 上次同步最后一条Id
        /// </summary>
        public string LastSyncId { get; set; }
    }
}
