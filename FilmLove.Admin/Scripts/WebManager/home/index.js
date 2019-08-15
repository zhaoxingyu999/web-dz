/**
* 本地搜索菜单
*/
function search_menu() {
    //要搜索的值
    var text = $('input[name=q]').val();

    var $ul = $('.sidebar-menu');
    $ul.find("a.nav-link").each(function () {
        var $a = $(this).css("border", "");
        //判断是否含有要搜索的字符串
        if ($a.children("span.menu-text").text().indexOf(text) >= 0) {

            //如果a标签的父级是隐藏的就展开
            $ul = $a.parents("ul");

            if ($ul.is(":hidden")) {
                $a.parents("ul").prev().click();
            }
            //点击该菜单
            $a.click().css("border", "1px solid");
            //                return false;
        }
    });
}

$(function () {
    indexJs.loadMenu();
    indexJs.pageBind();
});


var indexJs = {
    pageBind: function () {
        addTabs({
            id: '10008',
            title: '首页',
            close: false,
            url: '/WebHome/DashBoard',
            urlType: "relative"
        });
        App.fixIframeCotent();
        if ($('#_editpassword').length > 0) {
            $('#_editpassword').click(function () {
                msg.modalOpen({
                    title: "修改密码",
                    url: "/WebSystem/UpdatePassword",
                    height: 271,
                    width: 500,
                    callBack: function (frm) {
                        indexJs.updatePassWordDo(frm);
                    }
                });
            });
        }
    },
    updatePassWordDo: function (frm) {
        var pd = {};
        pd.PassWord = frm.find('#PassWord').val();
        pd.NewPassWord = frm.find('#NewPassWord').val();
        pd.RePassWord = frm.find('#RePassWord').val();
        if (pd.PassWord == '') {
            msg.warn('请输入原密码');
            return;
        }
        if (pd.NewPassWord == '') {
            msg.warn('请输入新密码');
            return;
        }
        if (pd.RePassWord == '') {
            msg.warn('请输入确认密码');
            return;
        }
        if (pd.NewPassWord != pd.RePassWord) {
            msg.warn('新密码与确认密码不一致');
            return;
        }
        pd.PassWord = $.md5(pd.PassWord);
        pd.NewPassWord = $.md5(pd.NewPassWord);
        pd.RePassWord = $.md5(pd.RePassWord);
        ajaxHelper.post('/WebSystem/UpdatePasswordDo', pd, function (d) {
            msg.success('修改密码成功');
            layer.close(layerIdx);
        });
    },
    loadMenu: function () {
        var pd = {};
        ajaxHelper.post('/WebHome/LoadMenu', pd, function (d) {
            $('.sidebar-menu').sidebarMenu({ data: d });
        });
    }
}