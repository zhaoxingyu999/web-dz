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
            url: '/Product/ProductListLoad',
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
                 <td class="text-center">' + tools.nullToEmptyString(d.Name) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Type) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Title) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Content) + '</td>\
                 <td class="text-center"><a href="/Product/ProductEdit/' + d.Id + '">编辑</a></td>\
            </tr>';
        }
        $("#infodata").html(html);
    }
};