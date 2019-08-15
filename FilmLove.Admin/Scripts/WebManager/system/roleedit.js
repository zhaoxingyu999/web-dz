$(function () {
    var roleid = myfuns.GetQueryInt('roleid');
    roleedit.loadInfo(roleid);
    roleedit.pageBind();
});

var roleedit = {
    pageBind: function () {
        $('#btn_save').click(function () {
            roleedit.saveRole();
        });
        $('#btn_cancel').click(function () {
            history.go(-1);
        });
    },
    loadInfo: function (roleid) {
        var postData = {
            roleid: roleid
        };
        ajaxHelper.post('/WebSystem/RoleLoadInfo', postData, function (d) {
            $('#RoleId').val(d.RoleId);
            $('#RoleName').val(d.RoleName);
            $('#RoleRemark').val(d.RoleRemark);
            $.each($('.RoleStatus'), function (i) {
                if (d.RoleStatus == $('.RoleStatus').eq(i).val()) {
                    $('.RoleStatus').eq(i).prop('checked', true);
                }
            });
            var menuhtml = '';
            $.each(d.RoleMenus, function (i) {
                var itemMenu = d.RoleMenus[i];
                menuhtml += '<div class="role-menu">';
                menuhtml += '<div class="role-menu-main">';
                menuhtml += '<label style="font-weight:bold;"><input type="checkbox" class="rolemenu mainmenu mmenu_' + itemMenu.MenuID + '" data-menuid="' + itemMenu.MenuID + '" data-parentid="' + itemMenu.MenuParentID + '" ' + (itemMenu.Checked == 1 ? ' checked="checked"' : '') + ' value="' + itemMenu.MenuID + '"/> ' + itemMenu.MenuTitle + '</label>';
                menuhtml += '</div>';
                menuhtml += '<div class="role-menu-sub">';
                var subMenus = itemMenu.ChildMenus;
                $.each(subMenus, function (j) {
                    var subMenu = subMenus[j];
                    menuhtml += '<div>';
                    menuhtml += '<label style="font-weight:bold;"><input type="checkbox" class="rolemenu submenu smenu_' + subMenu.MenuParentID + '" data-menuid="' + subMenu.MenuID + '" data-parentid="' + subMenu.MenuParentID + '" ' + (subMenu.Checked == 1 ? ' checked="checked"' : '') + ' value="' + subMenu.MenuID + '"/> ' + subMenu.MenuTitle + '</label>';
                    menuhtml += '</div>';
                    menuhtml += '<div style="padding-left:25px;">';
                    var itemPages = subMenu.ItemPages;
                    for (var i = 0; i < itemPages.length; i++) {
                        var curPage = itemPages[i];
                        menuhtml += '<label><input type="checkbox" class="subpage omenu_' + curPage.MenuID + '" data-pageid="' + curPage.PageID + '" data-menuid="' + curPage.MenuID + '" ' + (curPage.Checked == 1 ? ' checked="checked"' : '') + ' value="' + curPage.PageID + '"/> ' + curPage.PageName + '</label>';
                    }
                    menuhtml += '</div>';
                });
                menuhtml += '</div>';
                menuhtml += '<div class="mycl"></div>';
                menuhtml += '</div>';
            });
            $('#div_menus').html(menuhtml);


            $('.submenu').click(function () {
                var pid = $(this).attr('data-parentid');
                var scobjs = $('.smenu_' + pid + ':checked');
                if (scobjs.length > 0) {
                    $('.mmenu_' + pid).prop('checked', true);
                }
                else {
                    $('.mmenu_' + pid).prop('checked', false);
                }
            });

            $('.mainmenu').click(function () {
                var mid = $(this).attr('data-menuid');
                if ($(this).prop('checked')) {
                    $('.smenu_' + mid).prop('checked', true);
                }
                else {
                    $('.smenu_' + mid).prop('checked', false);
                }
            });
        });
    },
    saveRole: function () {
        var url = $("#btn_save").data('url');
        var postData = {};
        postData.RoleId = $('#RoleId').val();
        postData.RoleName = $('#RoleName').val();
        postData.RoleStatus = 0;
        if ($('.RoleStatus:checked').length > 0) {
            postData.RoleStatus = $('.RoleStatus:checked').eq(0).val();
        }
        postData.RoleRemark = $('#RoleRemark').val();
        var menuObjs = $('.rolemenu:checked');
        if (postData.RoleName == '') {
            msg.warn("请输入角色名称！");
            return;
        }
        if (menuObjs.length == 0) {
            msg.warn("请选择管理权限！");
            return;
        }

        $.each(menuObjs, function (i) {
            var curMenuID = menuObjs.eq(i).val();
            postData['RoleMenus[' + i + '].MenuID'] = curMenuID;
            var curPageObjs = $('.omenu_' + curMenuID + ':checked');
            for (var j = 0; j < curPageObjs.length; j++) {
                postData['RoleMenus[' + i + '].ItemPages[' + j + '].PageID'] = curPageObjs.eq(j).val();
            }
        });
        ajaxHelper.post(url, postData, function (d) {
            msg.success('角色保存成功！', function () {
                history.go(-1);
            });
        });
    }
};