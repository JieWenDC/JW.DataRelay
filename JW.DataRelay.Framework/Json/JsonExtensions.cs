using JW.DataRelay.Framework.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace System
{
    public static class JsonExtensions
    {
        /// <summary>
        /// 转换JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T source, JsonSerializerSettings settings = null)
        {
            if (source == null)
            {
                return string.Empty;
            }
            if (source == null)
            {
                return string.Empty;
            }
            if (settings != null)
            {
                return JsonConvert.SerializeObject(source, settings);
            }
            else
            {
                return JsonConvert.SerializeObject(source);
            }
        }

        /// <summary>
        /// JSON反序列化为T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            return JsonHelper.DeserializeObject<T>(json);
        }

        public static T ToObject<T>(this object json)
        {
            if (json == null)
            {
                return default(T);
            }
            if (json is string)
            {
                return JsonHelper.DeserializeObject<T>(json.ToString());
            }
            if (json is T)
            {
                return (T)json;
            }
            if (typeof(T).FullName == "System.String")
            {
                return JsonHelper.DeserializeObject<T>(JsonHelper.SerializeObject(json));
            }
            if (json.IsNumber())
            {
                return json.ToNumber<T>();
            }
            throw new Exception(string.Format("{0}和{1}类型不匹配", json.GetType().FullName, typeof(T).FullName));
        }
    }
}
