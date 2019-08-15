var ue = UE.getEditor('Content', { initialFrameWidth: "100%", initialFrameHeight: 300, autoHeightEnabled: false });

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
        pd.Name = $('#Name').val();
        pd.Type = $('#Type').val();
        pd.Title = $('#Title').val();
        pd.Content = ue.getContent();
        ajaxHelper.post('/Product/ProductListSave', pd, function (d) {
            msg.success('保存成功！', function () {
                location.href = '/Product/ProductList';
            });
        });
    }
};