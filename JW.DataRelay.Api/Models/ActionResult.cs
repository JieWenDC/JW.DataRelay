namespace JW.DataRelay.Api.Models
{
    public class ActionResult<TResult>
    {
        public ActionResult(TResult result)
        {
            Data = result;
            Status = true;
        }

        public ActionResult()
        {
            Status = true;
        }

        public ActionResult(bool status)
        {
            Status = status;
        }

        public ActionResult(ErrorInfo error)
        {
            Error = error;
            Status = false;
        }

        /// <summary>
        /// 指示结果的成功状态。
        /// 如果为false设置<see cref="Error"/>的值
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 异常详情 (当<see cref="Status"/> 为false时).
        /// </summary>
        public ErrorInfo Error { get; set; }

        /// <summary>
        /// Ajax请求的实际结果对象。
        /// 当<see cref="Status"/> 为true时
        /// </summary>
        public TResult Data { get; set; }
    }

    public class ActionResult : ActionResult<object>
    {

    }
}