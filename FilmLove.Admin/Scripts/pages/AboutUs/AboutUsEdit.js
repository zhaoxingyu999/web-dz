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
        pd.Content = $('#Content').val();  
        pd.Name = $('#Name').val();  
        pd.Type = $('#Type').val();  
        ajaxHelper.post('/AboutUS/AboutUSListSaveData', pd, function (d) {
            msg.success('保存成功！', function () {
                location.href = '/AboutUS/AboutUSList';
            });
        });
    }
};