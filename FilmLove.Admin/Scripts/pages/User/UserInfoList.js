$(function () {
    itemJS.pageBind();
    itemJS.loadData();
})

var itemJS = {
    pageBind: function () {
        laydate.skin('yalan');
        if ($('#BeginDate').length > 0) {
            var start = {
                elem: '#BeginDate',
                max: laydate.now(), //最大日期
                istoday: true,
                isclear: true, //是否显示清空
                choose: function (datas) {
                    end.min = datas; //开始日选好后，重置结束日的最小日期
                    end.start = datas //将结束日的初始值设定为开始日
                }
            };
            laydate(start);
        }
        if ($('#EndDate').length > 0) {
            var end = {
                elem: '#EndDate',
                max: laydate.now(),
                isclear: true, //是否显示清空
                istoday: true,
                choose: function (datas) {
                    start.max = datas; //结束日选好后，重置开始日的最大日期
                }
            };
            laydate(end);
        }
        $('#SearchBtn').click(function () {
            itemJS.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.Account = $('#Account').val();
        Param.BeginDate = $('#BeginDate').val();
        Param.EndDate = $('#EndDate').val();
        $("#infoPage").page({
            url: '/User/UserInfoListLoad',
            pageSize: 10,
            searchparam: Param,
            viewCallback: itemJS.outList
        });
    },
    status: function (id, aStatus) {
        var r = window.confirm("你确定要修改状态吗？");
        if (r == false)
            return;
        var pd = {};
        pd.UserID = id;

        if (aStatus == 0)
            pd.DisState = 1;
        else
            pd.DisState = 0;

        ajaxHelper.post('/User/ChangeStatus', pd, function (d) {
            msg.success('修改成功！', function () {
                if (d == "错误")
                    alert("错误");
                else
                    itemJS.loadData();
            });
        });
    },
    outList: function (r, j) {
        var html = '';
        for (var i = 0; i < r.length; i++) {
            j++;
            var d = r[i];
            html += '<tr>\
                <td class="text-center">' + j + '</td>\
                <td class="text-center"><img src="' + tools.nullToEmptyString(d.Photo) + '" /></td>\
                <td class="text-center"><img src="' + tools.nullToEmptyString(d.BackgroundPic) + '" /></td>\
                <td class="text-center">' + tools.nullToEmptyString(d.CertificationLevel) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Number) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Phone) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.NickName) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Gender) + '</td>\
                <td class="text-center" > ' + formatHelper.formatterTime2(d.Birthday) + '</td >\
                <td class="text-center">' + formatHelper.formatTime(d.CreatTime) + '</td>\
                <td class="text-center">' + formatHelper.formatTime(d.RecentlyTime) + '</td>\
                <td class="text-center"><a href="/User/UserInfoEdit/' + d.UserId + '">查看</a>&nbsp&nbsp<a href="javascript:void(0);" onclick="itemJS.status(' + d.UserId + ',' + tools.nullToEmptyString(d.Status) + ')">' + (tools.nullToEmptyString(d.Status) == 0 ? "禁用" : "启用") + '</a></td>\
                </tr > ';
        }
        $("#infodata").html(html);
    },
};