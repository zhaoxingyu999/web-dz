$(function () {
    itemJS.pageBind();
});
var itemJS = {
    pageBind: function () {
        $('#SearchBtn').click(function () {
            itemJS.loadData();
        });
        itemJS.loadData();
    },
    getSearchParams: function () {
        var d = {};
        d.Title = $('#jobName').val();
        return d;
    },
    loadData: function () {
        var Param = {};
        Param.Name = $('#jobName').val();
        $("#infoPage").page({
            url: '/JobOffers/JobOffersListLoad',
            pageSize: 10,
            searchparam: Param,
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
                 <td class="text-center">' + tools.nullToEmptyString(d.Count) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Position) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Time) + '</td>\
                <td class="text-center"><a href="/JobOffers/JobOffersEdit/' + d.Id + '">编辑</a>&nbsp;<a href="/JobOffers/Delete/' + d.Id + '">删除</a></td>\
            </tr>';
        }
        $("#infodata").html(html);
    }
};