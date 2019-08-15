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
        $("#infoPage").page({
            url: '/Recommend/RecommendListLoad/',
            pageSize: 10,
            searchparam: Param,
            viewCallback: itemJS.outList
        });
    },
    delete: function (id) {
        var pd = {};
        pd.IDD = id;
        var r = window.confirm("你确定要删除吗？");
        if (r == true) {
            ajaxHelper.post('/Recommend/DeleteRecommend', pd, function (d) {
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
                <td class="text-center">' + tools.nullToEmptyString(d.VideoId) + '</td>\
               <td class="text-center"><video src="'+ tools.nullToEmptyString(d.Url) + '" controls="controls" poster="' + tools.nullToEmptyString(d.Cover) + '">您的浏览器不支持 video 标签。</video ></td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Title) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Type) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Description) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.WatchCount) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Label) + '</td>\
                <td class="text-center">' + tools.nullToEmptyString(d.Sort) + '</td>\
                <td class="text-center"><a href="javascript:void(0);" onclick="itemJS.delete(' + d.Id+')">删除</a></td>\
                </tr > ';
        }
        $("#infodata").html(html);
    },
};