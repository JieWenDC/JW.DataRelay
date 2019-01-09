using System;

namespace JW.DataRelay.Api.Models
{
    public class ErrorInfo
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public ValueType Code { get; set; }

        /// <summary>
        ///错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ErrorInfo()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ErrorInfo(string message)
        {
            Message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public ErrorInfo(ValueType code)
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ErrorInfo(ValueType code, string message) : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public ErrorInfo(string message, string details) : this(message)
        {
            Details = details;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public ErrorInfo(ValueType code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }
    }
}