var menuid;
$(function () {
    menuid = myfuns.GetQueryInt('menuid');
    menupagelist.pageBind();
    menupagelist.loadInfo();
})

var menupagelist = {
    pageBind: function () {
        $('#btnAdd').click(function () {
            menupagelist.showEditBox(0);
        });
    },
    loadInfo: function () {
        var pd = {};
        pd.menuid = menuid;
        ajaxHelper.post('/WebSystem/MenuPageDataList', pd, function (d) {
            var html = '';
            $.each(d, function (i) {
                var cd = d[i];
                html += '<tr>\
                    <td class="text-center">'  + cd.PageName + '</td>\
                    <td class="text-center">' + (cd.PageType == 1 ? '页面':'ajax请求') + '</td>\
                    <td class="text-center">' + cd.PageViewname + '</td>\
                    <td class="text-center">' + cd.PageBtnname + '</td>\
                    <td class="text-left">' + cd.PageUrl + '</td>\
                    <td class="text-center">' + (cd.MainStatus == 1 ? '主页面' : '') + '</td>\
                    <td class="text-center">' + (cd.PageStatus == 1 ? '启用':'禁用') + '</td>\
                    <td class="text-center">' + (cd.MainStatus != 1 ? '<a href="javascript:void(0);" onclick="menupagelist.showEditBox(' + cd.PageId + ');">编辑</a>':'') + '</td>\
                </tr>';
            });
            $('#data_body').html(html);
        });
    },
    showEditBox: function (pageid) {
        msg.modalOpen({
            title: "子页面编辑",
            url: "/WebSystem/MenuPageEdit?menuid=" + menuid + '&pageid=' + pageid,
            height: 317,
            width: 700,
            callBack: function (frm) {
                menupagelist.saveInfo(frm);
                layer.close(layerIdx);
            }
        });
    },
    saveInfo: function (frm) {
        var pd = {};
        pd.PageId = frm.find('#PageId').val();
        pd.MenuId = frm.find('#MenuId').val();
        pd.PageName = frm.find('#PageName').val();
        pd.PageViewname = frm.find('#PageViewname').val();
        pd.PageBtnname = frm.find('#PageBtnname').val();
        pd.PageType = frm.find('.PageType:checked').val();
        pd.PageStatus = frm.find('.PageStatus:checked').val();
        pd.PageUrl = frm.find('#PageUrl').val();
        if (pd.PageName == '') {
            msg.warn('请输入页面名称');
            return;
        }
        if (pd.PageViewname == '') {
            msg.warn('请输入显示名称');
            return;
        }
        ajaxHelper.post('/WebSystem/SaveMenuPage', pd, function (d) {
            window.location.reload();
        });
    },
};