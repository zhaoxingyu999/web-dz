$(function () {
    accountlist.pageBind();
    accountlist.loadData();
});

var accountlist = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            accountlist.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.UserName = $('#KeyWord').val();
        $("#infoPage").page({
            url: '/Logs/ErrorLogs/List',
            pageSize: 20,
            searchparam: Param,
            viewCallback: accountlist.outList
        });
    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center" >' + itemData.ID + '</td>\
                    <td align="center" ><div class="autocut" title="' + itemData.ErrorType + '">' + (itemData.ErrorType) + '</div></td>\
                    <td align="center" ><div class="autocut" title="' + itemData.URL + '">' + (itemData.URL) + '</div></td>\
                    <td align="center" ><div onclick="msg.alert($(this).html())" class="autocut" title="' + itemData.MoreTxt + '">' + (itemData.MoreTxt) + '</div></td>\
                    <td align="center" ><div onclick="msg.alert($(this).html())" class="autocut" title="' + itemData.ErrorTxt + '">' + (itemData.ErrorTxt) + '</div></td>\
                    <td align="center" >' + tools.formatTime(itemData.CreateTime) + '</td>\
                    <td align="center" >' + (itemData.IP) + '</td>\
                </tr>';
        });
        $('#accountbody').html(html);
    }
}