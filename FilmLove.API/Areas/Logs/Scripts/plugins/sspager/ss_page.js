/*
参数config
url：请求URL地址
searchparam：查询参数
pageSize：每页显示数据条数
viewCallback：数据显示方法
maxButton:页码按钮数目
showCustom:是否能手动输入页码
------------------------------------
viewCallback:数据显示方法
参数：(data,j)
data:显示数据JSON
j:起始行序号
*/

(function ($) {
    $.fn.page = function (config) {
        if (this.length != 1) {
            throw "k_page:如有多个page请调用多次!";
        }

        var defaults = { url:null,pageSize: 10, maxButton: 4, showCustom: true, searchparam: null, pageIndex: 1 ,viewCallback:null }
        config = $.extend(defaults, config);


        if (config.maxButton <= 1) config.maxButton = 2;
        if (config.pageSize < 1) config.pageSize = 1;
        //按钮数目需偶数
        if (config.maxButton % 2 != 0)
            config.maxButton++;

        
        

        var searchparam = config.searchparam;
        
        var pageIndex = 1, pageCount, move_kf;

        if (config.pageIndex) {
            pageIndex = config.pageIndex;
        }






        var pagekey = this.attr('id') + window.location.href + $('#pagehd').val() + JSON.stringify(config);
        pagekey = $.md5(pagekey);

        if ($.cookie(pagekey)) {
            pageIndex = $.cookie(pagekey);
        }
        else {
            $.cookie(pagekey, pageIndex)
        }

        pageIndex = parseInt(pageIndex);

        
        var pageSize = config.pageSize;

        var totalCount;

        var prev = "<div class='k_p_prev'>上一页</div>";
        var next = "<div class='k_p_next'>下一页</div>";
        var pbody = $("<span style='display:block;float:left;overflow:hidden;'></span>");
        var pcustom = $("<span class='k_custom hidden-xs'>到第&nbsp;<span></span>&nbsp;页&nbsp;&nbsp;&nbsp; </span><div class='k_btn hidden-xs'>确定</div>");
        var cl = "<div class='k_cl'></div>";
        var pipt = $("<input class='k_ipt'>");
        var pmark = $('<label class="col-sm-4 mypdl0 ss_page_mark"></label>');
        var pmain = $('<div></div>');

        this.empty();
        var itemObj = this;
        pmain.addClass("ss_page").addClass("myfr").append(prev);

        pipt.keypress(function (e) {
            if (e.which == 13) {
                topage("确定");
                return false;
            }
        }).appendTo(pcustom.children());

        //跳转页码
        function topage(text) {

            switch (text) {
                case "上一页":
                    if (pageIndex - 1 < 1) {
                        return;
                    }
                    pageIndex--;
                    move_kf = "sc_r";
                    break;
                case "下一页":
                    if (pageIndex + 1 > pageCount) {
                        return;
                    }
                    pageIndex++;
                    move_kf = "sc_l";
                    break;
                case "确定":

                    if (!/^\d+$/.test(pipt.val())) {
                        pipt.val("");
                        return;
                    }
                    text = parseInt(pipt.val());
                    if (text < 1 || text > pageCount) {
                        pipt.val("");
                        return;
                    }

                default:
                    var _pindex = parseInt(text);
                    if (pageIndex == _pindex)
                        return;
                    move_kf = pageIndex < _pindex ? "sc_l" : "sc_r";
                    pageIndex = _pindex;
                    break;
            }

            gopageChange();
        }

        //页变更事件
        function gopageChange() {
            if (isNaN(pageIndex)) {
                return;
            }

            $.cookie(pagekey, pageIndex)
            searchparam.PageIndex = pageIndex;
            searchparam.PageSize = pageSize;

            ajaxHelper.post(config.url, searchparam, function (data) {
                if (config.viewCallback) {
                    config.viewCallback(data.dataList, (pageIndex - 1) * pageSize);
                }
                totalCount = data.TotalCount;
                pageCount = totalCount % config.pageSize == 0 ? totalCount / config.pageSize : parseInt(totalCount / config.pageSize) + 1;
                initpage();
                itemObj.unbind("click").bind("click", function (e) {
                    var _t = $(e.target);
                    if (_t[0].tagName == "DIV" && _t[0].className != "ss_page") {

                        topage(_t.text());
                    }
                });
            });

        }

        //添加页码
        function initpage() {
            pmark.empty();
            var startRow = pageSize * (pageIndex - 1) + 1;
            var endRow = pageSize * pageIndex;
            endRow = endRow > totalCount ? totalCount : endRow;
            //pmark.append('&nbsp;第 ' + pageIndex + ' 页， &nbsp;&nbsp;数据显示 ' + startRow + ' 到 ' + endRow + ' 项，共 ' + totalCount + ' 项');
            pmark.append('&nbsp;共' + totalCount + '条记录');

            pbody.empty();
            if (totalCount > 0) {
                var _t_maxb = config.maxButton / 2;
                //前后页码集合
                var _t_listp = [], _t_listn = [];
                var _min = 0, _max = pageCount;

                for (var i = 1; i <= _t_maxb; i++) {
                    var _t_prev = pageIndex - i, _t_next = pageIndex + i;
                    //当前页码之前的页
                    if (_t_prev > 0) {
                        _t_listp.push("<div class='k_p_page hidden-xs'>" + _t_prev + "</div>");
                        if (i == _t_maxb) _min = _t_prev;
                    }
                    
                    //当前页码之后的页
                    if (_t_next <= pageCount) {
                        _t_listn.push("<div class='k_p_page hidden-xs'>" + _t_next + "</div>");
                        if (i == _t_maxb) _max = _t_next;
                    }
                }
                //显示第一页
                if (_min > 1) pbody.append("<div class='k_p_page hidden-xs'>1</div>");
                //显示前 ……
                if (_min - 1 > 1) pbody.append("<em class='hidden-xs'>...</em>");

                for (var i = _t_listp.length; i >= 0; i--) {
                    pbody.append(_t_listp[i]);
                }
                pbody.append("<div class='k_p_page k_p_current hidden-xs'>" + pageIndex + "</div>");
                for (var i = 0; i < _t_listn.length; i++) {
                    pbody.append(_t_listn[i]);
                }
                //显示后 ……
                if (pageCount - _max > 1) pbody.append("<em class='hidden-xs'>...</em>");
                //显示最后一页
                if (_max < pageCount) pbody.append("<div class='k_p_page hidden-xs'>" + pageCount + "</div>");
            }
        }

        gopageChange();
        pmain.append(pbody).append(next);
        if (config.showCustom)
            pmain.append(pcustom);
        pmain.append(cl);
        this.append(pmark);
        this.append(pmain);
    }
})(jQuery)