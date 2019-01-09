using System.ComponentModel;

namespace JW.DataRelay.Enum
{
    /// <summary>
    /// 授权信息状态
    /// </summary>
    public enum AgentStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 测试
        /// </summary>
        [Description("测试")]
        Test = 2,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 3,
    }
}
