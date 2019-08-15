$(function () {
    rolelist.loadInfo();
});


var rolelist = {
    loadInfo: function () {
        var postData = {};
        ajaxHelper.post('/WebSystem/RoleLoadList', postData, function (d) {
            var html = '';
            $.each(d, function (i) {
                var itemData = d[i];
                html += '<tr>\
                        <td>' + itemData.RoleName + '</td>\
                        <td align="center">'+ (itemData.RoleStatus == 0 ? '<span class="label label-danger">禁用</span>' : '<span class="label label-primary">启用</span>') +'</td>\
                        <td align="center">' + tools.formatTime(itemData.CreateTime) + '</td>\
                        <td align="center">' + authHelper.createLink('/WebSystem/RoleEdit', 'roleid=' + itemData.RoleId) + '</td>\
                    </tr>';
            });
            $('#roletbody').html(html);
        });
    }
};