(function () {
    const index = {
        /** 
       * 填充Html到指定的容器
       * @param {Object}  url Html地址
       * @param {Object}  id 指定容器的ID
       */
        AppendHtml: (url, id) => {
            $.get(url, {}, function (html) {
                $(`#${id}`).html(html);
            }, "text");
        },
    };
    window.Index = index;
})();