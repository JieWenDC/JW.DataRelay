using LJ.UM.Framework.Models.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LJ.UM.Provider.Mongo
{
    public static class ExtendProvider
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="_this"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IFindFluent<TDocument, TProjection> Paging<TDocument, TProjection>(this IFindFluent<TDocument, TProjection> _this, IPagingInput param)
        {
            if (param.Limit > 0)
            {
                if (param.Limit > 0)
                {
                    _this = _this.Skip((param.Page - 1) * param.Limit);
                }
                _this = _this.Limit(param.Limit);
            }
            return _this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjectio"></typeparam>
        /// <param name="options"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static FindOptions<TDocument, TProjectio> Paging<TDocument, TProjectio>(this FindOptions<TDocument, TProjectio> options, IPagingInput param)
        {
            if (param.Limit > 0)
            {
                options.Limit = param.Limit;
                if (param.Page > 0)
                {
                    options.Skip = (param.Page - 1) * param.Limit;
                }
            }
            return options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static FilterDefinition<TDocument> FilterCreateTimeRanges<TDocument>(this FilterDefinitionBuilder<TDocument> filter, ICreateTimeInput param)
        {
            var filter_bson = new BsonDocument();
            if (param.StartTime != null && param.EndTime != null)
            {
                filter_bson.Add("_id", new BsonDocument(new Dictionary<string, ObjectId> {
                    { "$gt",param.StartTime.MinObjectId() },
                    { "$lt", param.EndTime.MaxObjectId() } }));
            }
            else if (param.StartTime != null)
            {
                filter_bson.Add("_id", new BsonDocument("$gt", param.StartTime.MinObjectId()));
            }
            else if (param.EndTime != null)
            {
                filter_bson.Add("_id", new BsonDocument("$lt", param.EndTime.MaxObjectId()));
            }
            return filter_bson;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="_this"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static IFindFluent<TDocument, TDocument> Search<TDocument>(this IMongoCollection<TDocument> _this, string keywords, Expression<Func<TDocument, bool>> filter)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                return _this.Find(row => 1 == 1);
            }
            else
            {
                return _this.Find(filter);
            }
        }

        /// <summary>
        /// 判断修改结果是否为成功  不会抛出异常
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static bool IsSuccess(this UpdateResult _this)
        {
            if (_this != null)
            {
                if (_this.ModifiedCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static bool IsSuccess(this Task<UpdateResult> _this)
        {
            var thant = _this.GetAwaiter().GetResult();
            if (thant != null)
            {
                if (thant.ModifiedCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断修改结果是否为成功 会抛出异常
        /// 成功不返回任何数据
        /// </summary>
        /// <param name="_this"></param>
        public static void IsSuccessThrow(this UpdateResult _this)
        {
            if (_this == null)
            {
                throw new NullReferenceException();
            }
            if (_this.MatchedCount <= 0)
            {
                throw new Exception("未匹配到任何数据");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_this"></param>
        public static void IsSuccessThrow(this Task<UpdateResult> _this)
        {
            UpdateResult thant = _this.GetAwaiter().GetResult();
            if (thant == null)
            {
                throw new NullReferenceException();
            }
            if (thant.MatchedCount <= 0)
            {
                throw new ArgumentOutOfRangeException("未匹配到任何数据");
            }
        }

        /// <summary>
        /// 判断修删除是否为成功  不会抛出异常
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static bool IsSuccess(this DeleteResult _this)
        {
            if (_this != null)
            {
                if (_this.DeletedCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static bool IsSuccess(this Task<DeleteResult> _this)
        {
            var thant = _this.GetAwaiter().GetResult();
            if (_this != null)
            {
                if (thant.DeletedCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断删除是否为成功 会抛出异常
        /// 成功不返回任何数据
        /// </summary>
        /// <param name="_this"></param>
        public static void IsSuccessThrow(this DeleteResult _this)
        {
            if (_this == null)
            {
                throw new NullReferenceException();
            }
            if (_this.DeletedCount <= 0)
            {
                throw new ArgumentOutOfRangeException("未匹配到任何数据");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_this"></param>
        public static void IsSuccessThrow(this Task<DeleteResult> _this)
        {
            var thant = _this.GetAwaiter().GetResult();
            if (thant == null)
            {
                throw new NullReferenceException();
            }
            if (thant.DeletedCount <= 0)
            {
                throw new ArgumentOutOfRangeException("未匹配到任何数据");
            }
        }

    }
}
