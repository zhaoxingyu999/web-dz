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
        d.Title = $('#TitleName').val();
        return d;
    },
    loadData: function () {
        var Param = {};
        Param.Title = $('#TitleName').val();
        $("#infoPage").page({
            url: '/CarouselPhoto/CarouselPhotoListLoad',
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
                <td class="text-center">' + tools.nullToEmptyString(d.ImgUrl) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Title) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Remark) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Description) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Author) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Headline) + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Time) + '</td>\
                 <td class="text-center"><a href="/CarouselPhoto/CarouselPhotoEdit/' + d.Id + '">编辑</a>&nbsp;<a href="/CarouselPhoto/Delete/' + d.Id + '">删除</a></td>\
            </tr>';
        }
        $("#infodata").html(html);
    }
};