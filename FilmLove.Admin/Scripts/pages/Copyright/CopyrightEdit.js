$(function () {
    itemJS.pageBind();
});
var itemJS = {
    pageBind: function () {
        $('#btnSubmit').click(function () {
            itemJS.saveData();
        });
    },
    saveData: function () {
        var pd = {};
          pd.Id = $('#Id').val();
          pd.Tel = $('#Tel').val();
          pd.Email = $('#Email').val();
          pd.Address = $('#Address').val();
        ajaxHelper.post('/Copyright/CopyrightListSave', pd, function (d) {
            msg.success('保存成功！', function () {
                location.href = '/Copyright/CopyrightList';
            });
        });
    }
};