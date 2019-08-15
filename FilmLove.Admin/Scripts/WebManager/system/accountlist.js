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
            url: '/WebSystem/AccountLoadList',
            pageSize: 10,
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
                    <td align="center">' + j + '</td>\
                    <td align="center"><a href="/WebSystem/AccountEdit?accountid=' + itemData.ManagerId + '">' + tools.nullToEmptyString(itemData.ManagerName) + '</a></td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.ManagerRealname) + '</td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.ManagerTel) + '</td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.ManagerEmail) + '</td>\
                    <td align="center">' + tools.formatTime(itemData.CreateTime) + '</td>\
                    <td align="center">' + tools.formatTime(itemData.LastLoginTime) + '</td>\
                    <td align="center">' + (itemData.ManagerStatus == 1 ? '<span class="label label-primary">启用</span>' : '<span class="label label-danger">禁用</span>') + '</td>\
                    <td align="center">' + authHelper.createLink('/WebSystem/AccountEdit','accountid=' + itemData.ManagerId) + '</td>\
                </tr>';
        });
        $('#accountbody').html(html);
    }
}