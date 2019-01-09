using Newtonsoft.Json.Linq;

namespace JW.DataRelay.Model.EntityMapModel
{
    public class SynchDataModel
    {
        /// <summary>
        /// 来源Id
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 同步数据(不固定格式)
        /// </summary>
        public JObject Data { get; set; }
    }
}
