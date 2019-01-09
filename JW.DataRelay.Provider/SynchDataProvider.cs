using JW.DataRelay.Entity;
using JW.DataRelay.Model.Input.SynchData;
using LJ.UM.Provider.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JW.DataRelay.Provider
{
    public class SynchDataProvider : BaseProvider<SynchData>
    {
        private string CollectionName { get; set; }
        public SynchDataProvider(string collectionName) : base("DataSync_Data")
        {
            CollectionName = collectionName;
        }
        protected override IMongoCollection<SynchData> Collection => Database.GetCollection<SynchData>(CollectionName);

        /// <summary>
        /// 获取同步数据集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<SynchData> GetSynchDataList(GetSynchDataListInput param)
        {
            FilterDefinition<SynchData> filter = Filter.Empty;
            if (param.StartTime == null && param.EndTime == null && !string.IsNullOrEmpty(param.LastSyncId))
            {
                filter = filter & Filter.Where(row => row.Id > ObjectId.Parse(param.LastSyncId));
            }
            else
            {
                if (param.StartTime != null)
                {
                    filter = filter & Filter.Gte(entity => entity.CreateTime, param.StartTime);
                }
                if (param.EndTime != null)
                {
                    filter = filter & Filter.Lte(entity => entity.CreateTime, param.EndTime);
                }
            }

            var options = new FindOptions<SynchData, SynchData>().Paging(param);
            param.Total = Collection.Count(filter);
            return Collection.FindAsync(filter, options).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateBySourceId(SynchData entity)
        {
            var filter = Filter.Where(row => row.SourceId == entity.SourceId);
            var update = Updater.Set(row => row.Data, entity.Data)
                .Set(row => row.SourceId, entity.SourceId);
            Collection.UpdateOne(filter, update, new UpdateOptions() { IsUpsert = false, BypassDocumentValidation = false });
        }

    }
}
