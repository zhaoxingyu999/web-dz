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
        pd.id = $("#UserId").val();
        pd.Sort = $('#Sort').val();
        pd.HotName = $('#HotName').val();
        ajaxHelper.post('/KeyWord/Save', pd, function (d) {
            msg.success('保存成功！', function () {
                location.href = '/KeyWord/KeyWordList';
            });
        });
    }
};