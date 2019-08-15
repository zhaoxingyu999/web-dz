$(function () {
    var MenuId = myfuns.GetQueryInt('menuid');
    menuedit.loadInfo(MenuId);
    menuedit.pageBind();

});

var menuedit = {
    pageBind: function () {
        $('#btn_save').click(function () {
            menuedit.menuModify();
        });
        $('#btn_cancel').click(function () {
            history.go(-1);
        });
    },
    loadInfo: function (MenuId) {
        var postData = {
            menuid: MenuId
        };
        ajaxHelper.post('/WebSystem/MenuLoadInfo', postData, function (d) {
            var menuData = d.menu;
            var topMenus = d.topMenus;
            $('#MenuId').val(menuData.MenuId);
            $('#MenuUrl').val(menuData.MenuUrl);

            var menuParentHtml = '<option value="0" selected="selected">顶级菜单</option>';
            $.each(topMenus, function (i) {
                menuParentHtml += '<option value="' + topMenus[i].MenuId + '">' + topMenus[i].MenuName + '</option>';
            });
            $('#MenuPid').html(menuParentHtml);
            $('#MenuPid').val(menuData.MenuPid);

            $('#menu_remark').val(menuData.menu_remark);
            $('#MenuSort').val(menuData.MenuSort);
            $('#MenuName').val(menuData.MenuName);
            $('#MenuIcon').val(menuData.MenuIcon);
            $('#MenuStatus').val(menuData.MenuStatus);
            var statusObjs = $('.MenuStatus');
            statusObjs.removeProp('checked');
            $.each(statusObjs, function (i) {
                if (statusObjs.eq(i).val() == menuData.MenuStatus) {
                    statusObjs.eq(i).prop('checked', true);
                }
            });
        });
    },
    menuModify: function () {
        var url = $("#btn_save").data('url');
        var MenuStatus = 0;
        if ($('.MenuStatus:checked').length > 0) {
            MenuStatus = $('.MenuStatus:checked').eq(0).val();
        }
        if ($('#MenuPid').val() == "" || $('#MenuPid').val() == null) {
            msg.warn("请选择上级导航！");
            return;
        }
        if ($('#MenuName').val() == "") {
            msg.warn("请输入标题！");
            return;
        }
        if ($('#MenuUrl').val() == "" && $('#MenuPid').val() != '0') {
            msg.warn("请输入链接！");
            return;
        }
        var saveObj = {
            MenuId: $('#MenuId').val(),
            MenuPid: $('#MenuPid').val(),
            MenuIcon: $('#MenuIcon').val(),
            MenuUrl: $('#MenuUrl').val(),
            MenuSort: $('#MenuSort').val(),
            MenuStatus: MenuStatus,
            MenuName: $('#MenuName').val()
        };

        ajaxHelper.post(url, saveObj, function (d) {
            msg.success("菜单保存成功！", function () {
                history.go(-1);
            });
        });

    }
};