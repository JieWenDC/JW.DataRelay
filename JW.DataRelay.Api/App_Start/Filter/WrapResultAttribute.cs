using System;

namespace JW.DataRelay.Api.App_Start.Filter
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute :System.Attribute
    {
        /// <summary>
        /// 成功执行后包装
        /// </summary>
        public bool WrapOnSuccess { get; set; }

        /// <summary>
        /// 发送错误后包装
        /// </summary>
        public bool WrapOnError { get; set; }

        /// <summary>
        /// 记录日志
        /// Default: true.
        /// </summary>
        public bool LogError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapOnSuccess"></param>
        /// <param name="wrapOnError"></param>
        public WrapResultAttribute(bool wrapOnSuccess = true, bool wrapOnError = true)
        {
            WrapOnSuccess = wrapOnSuccess;
            WrapOnError = wrapOnError;
            LogError = true;
        }
    }
}