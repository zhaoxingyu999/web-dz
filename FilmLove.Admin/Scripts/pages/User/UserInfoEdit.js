$(function () {
    KeyWord.pageBind();
});

var KeyWord = {
    pageBind: function () {
        $('#btnSubmit').click(function () {
            if ($("#UserId").val() == "") {
                alert("不能为空");
                return;
            }
            if ($("#Sort").val() == "") {
                alert("不能为空");
                return;
            }
            KeyWord.saveData();
        });
    },
    saveData: function () {
        var pd = {};
        pd.HeadImage = $("#HeadImage").val();
        pd.Number = $('#Number').val();
        pd.Phone = $('#Phone').val();
        pd.NickName = $('#NickName').val();
        pd.Gender = $('#Gender').val();
        pd.BirthDay = $('#BirthDay').val();
        pd.CreateTime = $('#CreateTime').val();
        pd.LastLoginTime = $('#LastLoginTime').val();
        
        ajaxHelper.post('/User/Save', pd, function (d) {
            msg.success('保存成功！', function () {
                location.href = '/User/UserInfoList';
            });
        });
    }
};