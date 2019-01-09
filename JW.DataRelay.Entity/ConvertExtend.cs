using MongoDB.Bson;
using System.Collections.Generic;

namespace System
{
    public static class ConvertExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ObjectId ToObjectId(this string id)
        {
            return ObjectId.Parse(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ObjectId> ToObjectId(this List<string> ids)
        {
            var ret_ids = new List<ObjectId>(ids.Count);
            ids.ForEach(id =>
            {
                ret_ids.Add(id.ToObjectId());
            });
            return ret_ids;
        }

        /// <summary>
        /// 获取ObjectId中的本地时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DateTime GetCreateTime(this ObjectId id)
        {
            return id.CreationTime.ToLocalTime();
        }

        /// <summary>
        /// 获取指定时间最大的ObjectId
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static ObjectId MaxObjectId(this DateTime _this)
        {
            return new ObjectId(_this, 16777215, short.MaxValue, 16777215);
        }

        /// <summary>
        /// 获取指定时间最小的ObjectId
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static ObjectId MinObjectId(this DateTime _this)
        {
            return new ObjectId(_this, 0, 0, 0);
        }

        public static ObjectId MinObjectId(this DateTime? _this)
        {
            return new ObjectId((DateTime)_this, 0, 0, 0);
        }

        /// <summary>
        /// 获取指定时间最大的ObjectId
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static ObjectId MaxObjectId(this DateTime? _this)
        {
            return ((DateTime)_this).MaxObjectId();
        }
    }
}
