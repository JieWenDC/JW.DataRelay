using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JW.DataRelay.Web.Models;

namespace JW.DataRelay.Web.Controllers
{
    public class ControlCenterController : Controller
    {
        // GET: ControlCenter
        /// <summary>
        /// 日志查看
        /// </summary>
        /// <returns>视图</returns>
        [HttpGet, Description("日志查看")]
        public  PartialViewResult LogView()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取日志记录
        /// </summary>
        /// <returns>json数据-日志记录</returns>
        [HttpPost,Description("获取日志记录")]
        public JsonResult GetLogData()
        {
            ResultData rd=new ResultData();
            LogInfo logInfo2=new LogInfo
            {
                Id = "2",
                AgentId = "MH",
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalCount = 200,
                Type = Behavior.Push,
                CollectionName = "测试2",
                MinId = "1",
                MaxId = "100",
                Result = "成功"
            };
            LogInfo logInfo = new LogInfo
            {
                Id = "1",
                AgentId = "PD",
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalCount = 100,
                Type = Behavior.Obtain,
                CollectionName = "测试",
                MinId = "1",
                MaxId = "100",
                Result = "成功"
            };
            List<LogInfo> list = new List<LogInfo> {logInfo, logInfo2};
            rd.Success = true;
            rd.Data = list;
            return Json(rd, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns>权限列表查看</returns>
        public PartialViewResult Authority()
        {
            return PartialView();
        }
    }

}