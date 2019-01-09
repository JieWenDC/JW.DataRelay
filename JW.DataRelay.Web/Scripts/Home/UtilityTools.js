(function() {
    const utilityTools = {
        LoadJqueryDataTable: function(id,url) {
            $(`#${id}`).dataTable();
        }
    };
    window.UtilityTools = utilityTools;
})();