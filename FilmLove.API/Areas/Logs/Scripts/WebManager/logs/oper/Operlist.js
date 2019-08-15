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
            url: '/Logs/OperLogs/List',
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
                    <td align="center">' + itemData.ID + '</td>\
                    <td align="center"><div class="autocut" title="' + itemData.URL + '">' + tools.nullToEmptyString(itemData.URL) + '</div></td>\
                    <td align="center"><div onclick="msg.alert($(this).html())" class="autocut" title="' + itemData.Param + '">' + tools.nullToEmptyString(itemData.Param) + '</div></td>\
                    <td align="center"><div onclick="msg.alert($(this).html())" class="autocut" title=\'' + itemData.UserInfo + '\'>' + tools.nullToEmptyString(itemData.UserInfo) + '</div></td>\
                    <td align="center">' + tools.formatTime(itemData.CreateTime) + '</td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.IP) + '</td>\
                </tr>';
        });
        $('#accountbody').html(html);
    }
}