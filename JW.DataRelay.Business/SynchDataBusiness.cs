using JW.DataRelay.Entity;
using JW.DataRelay.Model.EntityMapModel;
using JW.DataRelay.Model.Input.SynchData;
using JW.DataRelay.Provider;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using JW.DataRelay.Enum;
using JW.DataRelay.Framework.Web;

namespace JW.DataRelay.Business
{
    public class SynchDataBusiness
    {
        protected SynchDataProvider SynchDataProvider { get; set; }

        protected SynchLogBusiness SynchLogBusiness { get; set; }

        protected AgentBusiness AgentBusiness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        public SynchDataBusiness(string collectionName) : base()
        {
            SynchDataProvider = new SynchDataProvider(collectionName);
            SynchLogBusiness = new SynchLogBusiness();
            AgentBusiness = new AgentBusiness();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(SynchData entity)
        {
            SynchDataProvider.Add(entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(List<SynchData> entitys)
        {
            SynchDataProvider.Add(entitys);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateBySourceId(SynchData entity)
        {
            SynchDataProvider.UpdateBySourceId(entity);
        }

        #region 获取推送数据

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public List<SynchData> GetListBySourceId(List<string> sourceIds)
        {
            return SynchDataProvider.GetList(row => sourceIds.Contains(row.SourceId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<SynchData> GetSynchDataList(GetSynchDataListInput param)
        {
            var agent = AgentBusiness.GetByAppKey(HttpContext.Current.Request.Headers["AppKey"]);
            agent.LastRequestTime = DateTime.Now;
            param.LastSyncId = agent.LastSyncId;
            var results = SynchDataProvider.GetSynchDataList(param);
            string minId = null;
            string maxId = null;
            if (results.ExistsData())
            {
                var item = results.LastOrDefault();
                if (param.StartTime == null && param.EndTime == null)
                {
                    agent.LastSyncId = item.Id.ToString();
                }
                minId = results.Min(row => row.Id).ToString();
                maxId = results.Max(row => row.Id).ToString();
            }
            AgentBusiness.Update(agent);
            SynchLogBusiness.Add(new SynchLog()
            {
                AgentId = agent.Id.ToString(),
                CollectionName = HttpContext.Current.Request.Headers["CollectionName"],
                MinId = minId,
                MaxId = maxId,
                Result = "",
                TotalCount = results.Count(),
                Type = SynchLogTypeEnum.Get,
                Ip = HttpHelper.IP,
                Token = HttpContext.Current.Request.Headers["Token"]
            });
            return results;
        }

        #endregion

        #region 推送数据

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void PushOne(SynchDataModel param)
        {
            var agent = AgentBusiness.GetByAppKey(HttpContext.Current.Request.Headers["AppKey"]);
            agent.LastRequestTime = DateTime.Now;
            var syncData = new SynchData()
            {
                SourceId = param.SourceId,
                Data = BsonDocument.Parse(param.Data.ToString()),
            };
            Add(syncData);
            AgentBusiness.Update(agent);
            SynchLogBusiness.Add(new SynchLog()
            {
                AgentId = agent.Id.ToString(),
                CollectionName = HttpContext.Current.Request.Headers["CollectionName"],
                MinId = syncData.Id.ToString(),
                MaxId = syncData.Id.ToString(),
                Result = "",
                TotalCount = 1,
                Type = SynchLogTypeEnum.Add,
                Ip = HttpHelper.IP,
                Token = HttpContext.Current.Request.Headers["Token"]
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PushMany(List<SynchDataModel> param)
        {
            var agent = AgentBusiness.GetByAppKey(HttpContext.Current.Request.Headers["AppKey"]);
            agent.LastRequestTime = DateTime.Now;

            var entitys = new List<SynchData>();
            param.ForEach(row =>
            {
                entitys.Add(new SynchData()
                {
                    Data = BsonDocument.Parse(row.Data.ToString()),
                    SourceId = row.SourceId,
                });
            });
            Add(entitys);

            AgentBusiness.Update(agent);
            SynchLogBusiness.Add(new SynchLog()
            {
                AgentId = agent.Id.ToString(),
                CollectionName = HttpContext.Current.Request.Headers["CollectionName"],
                MinId = entitys.Min(row => row.Id).ToString(),
                MaxId = entitys.Max(row => row.Id).ToString(),
                Result = "",
                TotalCount = entitys.Count,
                Type = SynchLogTypeEnum.Add,
                Ip = HttpHelper.IP,
                Token = HttpContext.Current.Request.Headers["Token"]
            });
            return entitys.Count();
        }

        #endregion
    }
}
