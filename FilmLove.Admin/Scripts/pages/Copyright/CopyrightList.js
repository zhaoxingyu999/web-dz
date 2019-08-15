$(function () {
    itemJS.pageBind();
});
var itemJS = {
    pageBind: function () {
        itemJS.loadData();
    },
    loadData: function () {
        var Param = {};
        $("#infoPage").page({
            pageIndex: 1,
            pageSize: 10,
            searchparam: Param,
            url: '/Copyright/CopyrightListLoad',
            viewCallback: itemJS.outputData
        });
    },
    outputData: function (r, j) {
        var html = '';
        for (var i = 0; i < r.length; i++) {
            j++;
            var d = r[i];
            html += '<tr>\
                 <td class="text-center">' + j + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Tel) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Email) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Address) + '</td>\
                 <td class="text-center"><a href="/Copyright/CopyrightEdit/' + d.Id + '">编辑</a></td>\
            </tr>';
        }
        $("#infodata").html(html);
    }
};