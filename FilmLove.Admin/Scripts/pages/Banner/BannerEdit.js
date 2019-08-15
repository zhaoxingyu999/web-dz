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
        pd.Title = $('#Title').val();
        pd.Content = $('#Content').val();
        pd.Url = $('#Url').val();
        pd.BeginDate = $('#BeginDate').val();
        pd.EndDate = $('#EndDate').val();
        pd.JumpType = $('#JumpType').val();
        pd.AdType = $('#AdType').val();
        pd.ImgType = $('#ImgType').val();
        pd.Duration = $('#Duration').val();
        pd.Status = $('#Status').val();
        pd.Sort = $('#Sort').val();

        ajaxHelper.post('/Banner/Save', pd, function (d) {
            msg.success('保存成功！', function () {
                location.href = '/Banner/BannerList';
            });
        });
    }
};