using JW.DataRelay.Entity;
using LJ.UM.Provider.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Web;
using System.Web.SessionState;

namespace JW.DataRelay.Provider
{
    public class BaseProvider<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Session
        /// </summary>
        private static HttpSessionState _session = HttpContext.Current.Session;

        /// <summary>
        /// 数据库名称
        /// </summary>
        protected virtual string DatabaseName { get; set; }

        /// <summary>
        /// 排序定义器
        /// </summary>
        public SortDefinitionBuilder<TEntity> Sort { get { return Builders<TEntity>.Sort; } }

        /// <summary>
        /// 过滤定义器
        /// </summary>
        protected FilterDefinitionBuilder<TEntity> Filter { get { return Builders<TEntity>.Filter; } }

        /// <summary>
        /// 更新定义器
        /// </summary>
        protected UpdateDefinitionBuilder<TEntity> Updater { get { return Builders<TEntity>.Update; } }

        /// <summary>
        /// 索引操作器
        /// </summary>
        protected IndexKeysDefinitionBuilder<TEntity> IndexKeys { get { return Builders<TEntity>.IndexKeys; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="collectionName">集合名称</param>
        public BaseProvider(string databaseName)
        {
            this.DatabaseName = databaseName;
        }

        private static string _MongoDBConnectionString;
        private static string MongoDBConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_MongoDBConnectionString))
                {
                    var setting = ConfigurationManager.ConnectionStrings["MongoDBConnectionString"];
                    setting.CheckNull($"缺少连接配置MongoDBConnectionString");
                    _MongoDBConnectionString = setting.ConnectionString;
                }
                return _MongoDBConnectionString;
            }
        }

        /// <summary>
        /// Mongo 配置
        /// </summary>
        private static MongoClientSettings _setting = MongoClientSettings.FromUrl(new MongoUrl(MongoDBConnectionString));

        /// <summary>
        /// Mongo 客户端
        /// </summary>
        protected MongoClient Client
        {
            get
            {
                return new MongoClient(_setting);
            }
        }

        /// <summary>
        /// 数据库
        /// </summary>
        protected IMongoDatabase Database
        {
            get
            {
                return Client.GetDatabase(DatabaseName);
            }
        }

        /// <summary>
        /// 集合名称
        /// </summary>
        protected virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                return Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }


        #region 扩展方法

        public virtual void Add(TEntity entity)
        {
            ApplyConcepts(entity);
            Collection.InsertOne(entity);
        }

        public virtual void Add(List<TEntity> entitys)
        {
            entitys.ForEach(entity =>
            {
                ApplyConcepts(entity);
            });
            Collection.InsertMany(entitys);
        }

        public virtual void AddAsync(TEntity entity)
        {
            ApplyConcepts(entity);
            Collection.InsertOneAsync(entity);
        }

        public virtual void AddAsync(List<TEntity> entitys)
        {
            if (entitys.ExistsData())
            {
                entitys.ForEach(entity =>
                {
                    ApplyConcepts(entity);
                });
                Collection.InsertManyAsync(entitys);
            }
        }

        public virtual TEntity Get(ObjectId id)
        {
            return Collection.FindAsync(row => row.Id == id, new FindOptions<TEntity, TEntity>() { Limit = 1 }).GetAwaiter().GetResult().FirstOrDefaultAsync().GetAwaiter().GetResult();
        }

        public virtual TEntity Get(string id)
        {
            return Get(id.ToObjectId());
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            var filter = Filter.Where(expression);
            return Collection.FindAsync(filter, new FindOptions<TEntity, TEntity>()
            {
                Limit = 1
            }).GetAwaiter().GetResult().FirstOrDefaultAsync().GetAwaiter().GetResult();
        }

        public virtual List<TEntity> GetList(List<ObjectId> ids)
        {
            return Collection.FindAsync(row => ids.Contains(row.Id), new FindOptions<TEntity, TEntity>() { }).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
        }

        public virtual List<TEntity> GetList(List<string> ids)
        {
            return GetList(ids.ToObjectId());
        }

        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> expression)
        {
            var filter = Filter.Where(expression);
            return Collection.FindAsync(filter).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
        }

        public virtual void Delete(ObjectId id)
        {
            Collection.DeleteOneAsync(row => row.Id == id);
        }

        public virtual void Delete(string id)
        {
            Delete(id.ToObjectId());
        }

        public virtual void Delete(List<ObjectId> ids)
        {
            Collection.DeleteManyAsync(row => ids.Contains(row.Id));
        }

        public virtual void Delete(List<string> ids)
        {
            Delete(ids.ToObjectId());
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> expression)
        {
            var filter = Filter.Where(expression);
            Collection.DeleteManyAsync(filter).GetAwaiter().GetResult().IsSuccess();
        }

        public virtual void DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var filter = Filter.Where(expression);
            Collection.DeleteManyAsync(filter);
        }

        public virtual long Count(Expression<Func<TEntity, bool>> expression)
        {
            var filter = Filter.Where(expression);
            return Collection.CountAsync(filter).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 实现接口数据 自动初始化
        /// </summary>
        /// <param name="entity"></param>
        protected void ApplyConcepts(TEntity entity)
        {
            if (entity.Id == null || entity.Id == ObjectId.Empty)
            {
                entity.Id = ObjectId.GenerateNewId();
            }
            if (entity.CreateTime == default(DateTime))
            {
                entity.CreateTime = DateTime.Now;
            }
        }

        #endregion

    }
}
