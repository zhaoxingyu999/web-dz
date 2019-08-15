var ue = UE.getEditor('Task', { initialFrameWidth: "100%", initialFrameHeight: 300, autoHeightEnabled: false });

$(function () {
    itemJS.pageBind();
    $('.form_date').datetimepicker({
        language: 'zh-CN',
        format: 'yyyy-mm-dd',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
    });
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
        pd.Count = $('#Count').val();
        pd.Position = $('#Position').val();
        pd.Task = ue.getContent();
        pd.Time = $('#Time').val();
        ajaxHelper.post('/JobOffers/JobOffersListSave', pd, function (d) {
            msg.success('保存成功！', function () {
                location.href = '/JobOffers/JobOffersList';
            });
        });
    }
};


