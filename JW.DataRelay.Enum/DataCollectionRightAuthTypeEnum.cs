using System;
using System.ComponentModel;

namespace JW.DataRelay.Enum
{
    /// <summary>
    /// 同步数据集合权限表权限类
    /// </summary>
    [Flags]
    public enum DataCollectionRightAuthTypeEnum
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
        Delete = 4,
        /// <summary>
        /// 获取
        /// </summary>
        [Description("获取")]
        Get = 8,
    }
}
