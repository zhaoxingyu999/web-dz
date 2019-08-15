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
        $('#AddBtn').click(function () {
            location.href = '/Banner/BannerEdit';
        });
    },
    loadData: function () {
        var Param = {};
        Param.Account = $('#Title').val();
        Param.BeginDate = $('#BeginDate').val();
        Param.EndDate = $('#EndDate').val();
        $("#infoPage").page({
            url: '/Banner/BannerListLoad',
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
        pd.id = id;

        if (aStatus == 0)
            pd.status = 1;
        else
            pd.status = 0;

        ajaxHelper.post('/Banner/ChangeStatus', pd, function (d) {
            msg.success('修改成功！', function () {
                if (d == "错误")
                    alert("错误");
                else
                    itemJS.loadData();
            });
        });
    },
    delete: function (id) {
        var pd = {};
        pd.id = id;
        var r = window.confirm("你确定要删除吗？");
        if (r == true) {
            ajaxHelper.post('/Banner/DeleteBanner', pd, function (d) {
                msg.success('删除成功！', function () {
                    if (d == "错误")
                        alert("错误");
                    else
                        itemJS.loadData();
                });
            });
        }
        else {
            return false;
        }
    },
    outList: function (r, j) {
        var html = '';
        for (var i = 0; i < r.length; i++) {
            j++;
            var d = r[i];
            html += '<tr>\
                <td class="text-center">' + j + '</td>\
                 <td class="text-center">' + tools.nullToEmptyString(d.Title) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Content) + '</td>\
                <td class="text-center"><img src="' + tools.nullToEmptyString(d.Url) + '" /></td>\
                <td class="text-center">' + formatHelper.formatTime(d.BeginDate) + '</td>\
                <td class="text-center" > ' + formatHelper.formatTime(d.EndDate) + '</td >\
                <td class="text-center">' + tools.nullToEmptyString(d.ManagerId) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.ManagerName) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Sort) + '</td>\
                <td class="text-center"><a href="/Banner/BannerEdit/' + d.Id + '">编辑</a>&nbsp&nbsp<a href="javascript:void(0);" onclick="itemJS.status(' + d.Id + ',' + tools.nullToEmptyString(d.Status) + ')">' + (tools.nullToEmptyString(d.Status) == 0 ? "禁用" : "启用") + '</a>\
                <a href="javascript:void(0);" onclick="itemJS.delete(' + d.Id +')">删除</a></td>\
                </tr > ';
        }
        $("#infodata").html(html);
    },
};