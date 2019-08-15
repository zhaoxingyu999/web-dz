$(function () {
    myaccount.pageBind();
});

var myaccount = {
    pageBind: function () {
        $('#btnSubmit').click(function () {
            myaccount.saveData();
        });
    },
    saveData: function () {
        var pd = {};
        pd.ManagerId = $('#ManagerId').val();
        pd.ManagerEmail = $('#ManagerEmail').val();
        pd.ManagerRealname = $('#ManagerRealname').val();
        pd.ManagerPwd = $('#ManagerPwd').val();
        pd.re_password = $('#re_password').val();
        pd.ManagerTel = $('#ManagerTel').val();

        if (pd.ManagerRealname == '') {
            msg.warn('请输入真实姓名');
            return;
        }

        if (pd.ManagerTel == '') {
            msg.warn('请输入联系电话');
            return;
        }

        if (pd.ManagerPwd != '') {
            if (pd.re_password == '') {
                msg.warn('请输入重复密码');
                return;
            }
            if (pd.ManagerPwd != pd.re_password) {
                msg.warn('两次输入的密码不一致');
                return;
            }
            pd.ManagerPwd = $.md5(pd.ManagerPwd);
            pd.re_password = $.md5(pd.re_password);
        }
        ajaxHelper.post('/WebSystem/MyAccountSave', pd, function (d) {
            msg.success('个人信息修改成功！', function () {
                location.href = '/WebSystem/MyAccount';
            });
        });
    },
};