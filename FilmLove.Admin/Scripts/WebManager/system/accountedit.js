$(function () {
    var accountid = myfuns.GetQueryInt('accountid');
    accountedit.loadInfo(accountid);
    accountedit.pageBind();

});

var accountedit = {
    pageBind: function () {
        $('#btn_save').click(function () {
            accountedit.saveInfo();
        });
        $('#btn_cancel').click(function () {
            history.go(-1);
        });
    },
    loadInfo: function (accountid) {
        var postData = {
            accountid: accountid
        };
        ajaxHelper.post('/WebSystem/AccountLoadInfo', postData, function (d) {
            $('#ManagerId').val(d.ManagerId);
            $('#ManagerEmail').val(d.ManagerEmail);
            $('#ManagerRealname').val(d.ManagerRealname);

            $.each($('.ManagerStatus'), function (i) {
                if ($('.ManagerStatus').eq(i).val() == d.ManagerStatus) {
                    $('.ManagerStatus').eq(i).prop('checked', true);
                }
            });

            $.each($('.IsSupper'), function (i) {
                if ($('.IsSupper').eq(i).val() == d.IsSupper) {
                    $('.IsSupper').eq(i).prop('checked', true);
                }
            });

            $('#ManagerTel').val(d.ManagerTel);
            $('#ManagerName').val(d.ManagerName);
            if (d.ManagerId > 0) {
                $('#ManagerName').attr('disabled', 'disabled');
            }
            $('#Password').val(d.Password);
            $('#RePassword').val(d.RePassword);
            var roleHtml = '';
            $.each(d.Roles, function (i) {
                var curRole = d.Roles[i];
                roleHtml += '<label><input type="checkbox" class="roleitem" ' + (curRole.Checked == 1 ? ' checked="checked"' : '') + ' value="' + curRole.RoleID + '"/> ' + curRole.RoleName + '</label>';
            });
            $('#div_roles').html(roleHtml);
        });
    },
    saveInfo: function () {
        var postData = {};
        postData.ManagerId = $('#ManagerId').val();
        postData.ManagerName = $('#ManagerName').val();
        postData.ManagerRealname = $('#ManagerRealname').val();
        postData.ManagerEmail = $('#ManagerEmail').val();
        postData.ManagerTel = $('#ManagerTel').val();
        var url = $("#btn_save").data('url');
        postData.ManagerStatus = 0;
        if ($('.ManagerStatus:checked').length > 0) {
            postData.ManagerStatus = $('.ManagerStatus:checked').eq(0).val();
        }
        postData.IsSupper = 0;
        if ($('.IsSupper:checked').length > 0) {
            postData.IsSupper = $('.IsSupper:checked').eq(0).val();
        }
        $.each($('.roleitem:checked'), function (i) {
            postData['Roles[' + i + '].RoleID'] = $('.roleitem:checked').eq(i).val();
        });
        ajaxHelper.post(url, postData, function (d) {
            msg.success('账号保存成功！', function () {
                history.go(-1);
            });
        });
    }
};