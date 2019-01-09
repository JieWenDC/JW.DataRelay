(function () {
    const logView = {
        /** 
      * 填充Html到指定的容器
      */
        LoadTable: function () {
            $("#LogTable").dataTable({
                "oLanguage": {
                    "sLengthMenu": "每页显示 _MENU_ 条记录",
                    "sZeroRecords": "抱歉， 没有找到",
                    "sInfo": "从 _START_ 到 _END_ /共 _TOTAL_ 条数据",
                    "sInfoEmpty": "没有数据",
                    "sInfoFiltered": "(从 _MAX_ 条数据中检索)",
                    "oPaginate": {
                        "sFirst": "首页",
                        "sPrevious": "前一页",
                        "sNext": "后一页",
                        "sLast": "尾页"
                    },
                    "sSearch": "搜索:"
                },
                "bProcessing": true,//数据加载时候提示
                "bPaginate": true, //翻页功能
                "bLengthChange": true, //改变每页显示数据数量
                "bFilter": true, //过滤功能
                "bSort": false, //排序功能
                "bInfo": true,//页脚信息
                "bAutoWidth": true,//自动宽度
                "bStateSave": true,//记录个性设置
                "bServerSide": true,//启动服务器数据导入
                ajax: {
                    //地址
                    url: "/ControlCenter/GetLogData",
                    type: "POST",
                    dataType: "json",
                    data: function (d) {
                        for (let key in d) {
                            if (d.hasOwnProperty(key)) {
                                if (key.indexOf("columns") === 0 ||
                                    key.indexOf("order") === 0) { //以columns开头的参数删除
                                    delete d[key];
                                }
                            }
                        }
                    },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    error: function (data, e) {
                        alert("系统繁忙，请稍后重试！");
                    },
                    dataSrc: function (json) {
                        json.recordsTotal = 200;
                        json.recordsFiltered = 100;
                        return json.Data;
                    }
                },
                "aoColumns": [
                    { "sTitle": "代理标识", "mDataProp": "AgentId", "sClass": "center" },
                    { "sTitle": "同步时间", "mDataProp": "CreateTime", "sClass": "center" },
                    { "sTitle": "同步数据条数", "mDataProp": "TotalCount", "sClass": "center" },
                    { "sTitle": "操作类型", "mDataProp": "Type", "sClass": "center" },
                    { "sTitle": "集合名称(数据类型)", "mDataProp": "CollectionName", "sClass": "center" },
                    { "sTitle": "最小ObjectId", "mDataProp": "MinId", "sClass": "center" },
                    { "sTitle": "最大ObjectId", "mDataProp": "MaxId", "sClass": "center" },
                    { "sTitle": "操作结果描述", "mDataProp": "Result", "sClass": "center" }
                ],
                "fnRowCallback": function(nRow, aData, iDisplayIndex) {}//行处理
            });
        }
    };
    window.LogView = logView;
})();
$(function () {
    LogView.LoadTable();
})