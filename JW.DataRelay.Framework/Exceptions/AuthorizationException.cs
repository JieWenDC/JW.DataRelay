using System;

namespace JW.DataRelay.Framework.Exceptions
{
    /// <summary>
    /// 授权异常
    /// </summary>
    public class AuthorizationException : System.Exception
    {
        public AuthorizationException(ValueType code, string message) : base(message)
        {
            if (code != null)
            {
                this.HResult = (int)code;
            }
        }
    }
}
