using JW.DataRelay.Api.App_Start.Attribute;
using JW.DataRelay.Business;
using JW.DataRelay.Enum;
using JW.DataRelay.Model.EntityMapModel;
using JW.DataRelay.Model.Input.SynchData;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using JW.DataRelay.Framework.Json;
using Newtonsoft.Json.Linq;

namespace JW.DataRelay.Api.Controllers
{
    public class SyncDataController : ApiController
    {
        public SynchDataBusiness synchDataBusiness { get; set; }

        private string _CollectionName;
        public string CollectionName
        {
            get
            {
                if (string.IsNullOrEmpty(_CollectionName))
                {

                    _CollectionName = System.Web.HttpContext.Current.Request.Headers["CollectionName"];
                }
                return _CollectionName;
            }
        }

        public SyncDataController()
        {
            synchDataBusiness = new SynchDataBusiness(CollectionName);
        }

        [HttpPost]
        [AuthType(DataCollectionRightAuthTypeEnum.Get)]
        /// <summary>
        /// 获取同步数据列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object GetList(GetSynchDataListInput param)
        {
            if (param == null)
            {
                param = new GetSynchDataListInput();
            }
            var lists = synchDataBusiness.GetSynchDataList(param);
            var results = new List<JObject>();
            lists.ForEach(item =>
            {
                results.Add(JsonHelper.DeserializeObject<JObject>(item.Data.ToString()));
            });
            return new
            {
                Data = results,
                Total = param.Total,
            };
        }

        [HttpPost]
        [AuthType(DataCollectionRightAuthTypeEnum.Get)]
        /// <summary>
        /// 根据sourceId获取一个
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public object GetListBySourceId(List<string> sourceIds)
        {
            return synchDataBusiness.GetListBySourceId(sourceIds);
        }

        [HttpPost]
        [AuthType(DataCollectionRightAuthTypeEnum.Add | DataCollectionRightAuthTypeEnum.Edit | DataCollectionRightAuthTypeEnum.Delete)]
        /// <summary>
        /// 推送单条数据
        /// </summary>
        /// <returns></returns>
        public void PushOne(SynchDataModel param)
        {
            if (param == null)
            {
                throw new ArgumentException("无数据");
            }
            param.SourceId.CheckNull("param.SourceId不能为空");
            param.Data.CheckNull("param.Data不能为空");
            synchDataBusiness.PushOne(param);
        }

        [HttpPost]
        [AuthType(DataCollectionRightAuthTypeEnum.Add | DataCollectionRightAuthTypeEnum.Edit | DataCollectionRightAuthTypeEnum.Delete)]
        /// <summary>
        /// 推送多条数据
        /// </summary>
        /// <returns></returns>
        public int PushMany(List<SynchDataModel> param)
        {
            if (param == null)
            {
                throw new ArgumentException("无数据");
            }
            param.ForEach(item =>
            {
                var index = param.IndexOf(item);
                item.SourceId.CheckNull($"param[{index}].SourceId不能为空");
                item.Data.CheckNull($"param[{index}].Data不能为空");
            });
            return synchDataBusiness.PushMany(param);
        }
    }
}
