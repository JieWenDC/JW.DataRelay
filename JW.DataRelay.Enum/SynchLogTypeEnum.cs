using System.ComponentModel;

namespace JW.DataRelay.Enum
{
    /// <summary>
    /// 同步类型
    /// </summary>
    [Description("同步类型")]
    public enum SynchLogTypeEnum
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Edit = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 3,
        /// <summary>
        /// 获取
        /// </summary>
        [Description("获取")]
        Get = 4,
    }
}
