namespace JW.DataRelay.Web.Models
{
    /// <summary>
    /// 通用返回Json数据格式
    /// </summary>
    public class ResultData
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 重定向URL
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}