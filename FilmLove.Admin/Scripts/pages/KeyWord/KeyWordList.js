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
            location.href = '/KeyWord/KeyWordEdit';
        });
    },
    loadData: function () {
        var Param = {};
        Param.BeginDate = $('#BeginDate').val();
        Param.EndDate = $('#EndDate').val();
        $("#infoPage").page({
            url: '/KeyWord/KeyWordListLoad/',
            pageSize: 10,
            searchparam: Param,
            viewCallback: itemJS.outList
        });
    },
    outList: function (r, j) {
        var html = '';
        for (var i = 0; i < r.length; i++) {
            j++;
            var d = r[i];
            html += '<tr>\
                <td class="text-center">' + j + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.HotName) + '</td>\
                <td class="text-center">' + formatHelper.formatTime(d.CreateTime) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Sort) + '</td>\
                <td class="text-center"><a href="/KeyWord/KeyWordEdit/' + d.Id + '">编辑</a></td>\
                </tr > ';
        }
        $("#infodata").html(html);
    },
};