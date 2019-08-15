$(function () {
    menulist.loadInfo();
    menulist.pageBind();
})


var menulist = {
    pageBind: function () {
        $('#btn_makemenu').click(function () {
            menulist.menuMake();
        });
    },
    loadInfo: function () {
        var postData = {};
        ajaxHelper.post('/WebSystem/MenuLoadList', postData, function (d) {
            var html = '<thead style="display: table-header-group;">\
                    <tr>\
                        <th width="20%">菜单标题</th>\
                        <th width="40%">菜单连接地址</th>\
                        <th width="20%">状态</th>\
                        <th width="20%">操作</th>\
                    </tr>\
                </thead>';
            html += '<tbody>';
            $.each(d, function (i) {
                var itemData = d[i];
                html += '<tr>\
                        <td><span style="display: inline-block; width:0px;"></span><span class="folder-open"></span>' + itemData.MenuName + '</td>\
                        <td></td>\
                        <td align="center">'+ (itemData.MenuStatus == 0 ? '<span class="label label-danger">禁用</span>' : '<span class="label label-primary">启用</span>') +'</td>\
                        <td align="center">'+ authHelper.createLink('/WebSystem/MenuEdit', 'menuid=' + itemData.MenuID)+'</td>\
                    </tr>';
                $.each(itemData.ChildMenus, function (j) {
                    var itemCData = itemData.ChildMenus[j];
                    html += '<tr>\
                        <td><span style="display: inline-block; width:20px;"></span><span class="folder-line"></span><span class="folder-open"></span>' + itemCData.MenuName + '</td>\
                        <td>' + itemCData.MenuLink + '</td>\
                        <td align="center">'+ (itemCData.MenuStatus == 0 ? '<span class="label label-danger">禁用</span>' :'<span class="label label-primary">启用</span>')+'</td>\
                        <td align="center">'+ authHelper.createLink('/WebSystem/MenuEdit', 'menuid=' + itemCData.MenuID) + authHelper.createOpenPage('子页面编辑', "/WebSystem/MenuPageList","menuid=" + itemCData.MenuID) +'</td>\
                    </tr>';
                });
            });
            html += '</tbody>';
            $('#menutable').html(html);
        });
    },
    menuMake: function () {
        var url = $("#btn_makemenu").data('url');
        ajaxHelper.post(url, "", function (d) {
            msg.success("更新菜单成功！新增功能" + d, function () {
                if (d > 0)
                    window.location.reload(true);
                });
        });
    }
};