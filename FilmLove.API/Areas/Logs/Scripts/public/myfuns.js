var msg = {
    warn: function (msg, ShowM) {
        layer.msg(msg, { icon: 7, time: 1000 }, ShowM);
    },
    success: function (msg, ShowM) {
        layer.msg(msg, { icon: 6, time: 1000 }, ShowM);
    },
    error: function (msg, ShowM) {
        layer.msg(msg, { icon: 5, time: 1000 }, ShowM);
    },
    errorhandler: function () {
        layer.msg("请求服务异常，请稍后重试或联系服务人员！", { icon: 5, time: 3500 });
    },
    modalOpen: function (options) {
        var defaults = {
            id: null,
            title: '系统窗口',
            width: "100px",
            height: "100px",
            url: '',
            shade: 0.3,
            btn: ['确认', '关闭'],
            btnclass: ['btn btn-primary', 'btn btn-danger'],
            callBack: null
        };
        options = $.extend(defaults, options);
        if (!isNaN(options.width)) {
            options.width = options.width + 'px';
        }
        if (!isNaN(options.height)) {
            options.height = options.height + 'px';
        }

        var _width = $(window).width() > parseInt(options.width.replace('px', '')) ? options.width : $(window).width() + 'px';
        var _height = $(window).height() > parseInt(options.height.replace('px', '')) ? options.height : $(window).height() + 'px';

        layerIdx = layer.open({
            id: options.id,
            type: 2,
            shade: options.shade,
            title: options.title,
            fix: false,
            area: [_width, _height],
            content: options.url,
            btn: options.btn,
            btnclass: options.btnclass,
            yes: function (index, layero) {
                if (options.callBack != null) {
                    var frm = layer.getChildFrame('body', index);
                    options.callBack(frm);
                }
            }, cancel: function () {
                return true;
            }
        });
    },
    modalConfirm: function (content, callback) {
        layer.confirm(
            content,
            {
                icon: "fa-exclamation-circle",
                title: "系统提示",
                btn: ['确认', '取消'],
                btnclass: ['btn btn-primary', 'btn btn-danger'],
            },
            function (index) {
                layer.close(index);
                callback(true);
            },
            function () {
                callback(false);
            }
        );
    },
    alert: function (content) {
        layer.alert(
            content
        );
    }
}


var pTab = {
    OpenNav: function (Navid, Url) {
        var obj = top.$("#sidebar-menu").find('a#' + Navid);
        if (Url != null) {
            obj.attr("href", Url);
        }
        obj.trigger("click");
    },

};

var curIfr = {
    Back: function () {
        var nowurl = document.URL;
        var fromurl = document.referrer;

        var frameId = window.frameElement && window.frameElement.id || '';

        if (frameId != '') {
            var curDoc = top.document.getElementById(frameId).contentWindow.document;
            if (curDoc.referrer.lastIndexOf("/WebHome/Index") < 0) {
                top.document.getElementById(frameId).contentWindow.history.back();
            }
        }
        else {
            history.back();
        }
    }
};


/**
 * 生成一个用不重复的ID
 */
//function GenNonDuplicateID(randomLength) {
//    let idStr = Date.now().toString(36)
//    idStr += Math.random().toString(36).substr(3, randomLength)
//    return idStr
//}

// GenNonDuplicateID(3) 将生成类似 ix49wl2978w 的ID
//GenNonDuplicateID(3)

/**
 * 创建新标签页
 * @param {any} name 新标签页的名称
 * @param {any} url 新标签页的地址
 */
function addNewTab(name, url) {
    var dataId = $.md5(url);
    dataId = dataId.toUpperCase();
    top.$.learuntab.addNewTab(name, url, dataId);
}

function addChildTab(name, url, dataId) {
    var newUrl = url + "/" + dataId
    var dataId = $.md5(url);
    dataId = dataId.toUpperCase();
    top.$.learuntab.addNewTab(name, newUrl, dataId);
}

//特殊情况 2017-07-09 12:01:44 zh
function addSpecialTab(name, content, url) {
    var dataId = $.md5(content);
    dataId = dataId.toUpperCase();
    top.$.learuntab.addNewTab(name, url, dataId);
}


var myfuns = {
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) {
            return unescape(r[2]);
        }
        return null;
    },
    toExport: function (url, obj) {
        var urlP = this.GetUrlParams(obj);
        if (urlP != '') {
            url = url + "?" + urlP
        }
        location.href = url;
    },
    GetUrlParams: function (obj) {
        var urlp = '';
        for (var key in obj) {
            if (urlp != '') {
                urlp += "&";
            }
            if (obj[key] != '') {
                urlp += key + "=" + obj[key];
            }
        }
        return urlp;
    },
    GetQueryInt: function (name) {
        var result = this.GetQueryString(name);
        if (result != null) {
            return result;
        }
        return 0;
    },
    LocationHref: function (url) {
        location.href = url;
    }
};


var ajaxHelper = {
    post: function (url, pd, callv, compv, prev) {
        $.ajax({
            url: url,
            type: 'POST',
            data: pd,
            dataType: 'JSON',
            beforeSend: function (xhr) {
                if (prev != undefined) {
                    prev();
                }
            },
            success: function (r, textStatus, jqXHR) {
                if (r.code == -1) {
                    msg.warn(r.msg, function () {
                        top.window.location.href = '/WebEntrance/Login';
                    });
                    return;
                }
                if (r.code != 0) {
                    msg.warn(r.msg);
                    return;
                }
                callv(r.data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 404) {
                    msg.warn("请求地址不存在");
                }
                if (XMLHttpRequest.status == 500) {
                    msg.warn(XMLHttpRequest.statusText);
                }
            },
            complete: function () {
                if (compv != undefined) {
                    compv();
                }
            }
        });
    }
};

var authHelper = {
    createLink: function (url, params) {
        var r = '';
        if (authPages[url]) {
            var auth = authPages[url];
            r = '<a href="' + url + '?' + params + '">' + auth.PageViewname + '</a>';
        }
        return r;
    },
    createOpenPage: function (title, url, params) {
        var r = '';
        if (authPages[url]) {
            var auth = authPages[url];
            r = '&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript: void (0); " onclick="openItemPage(\'' + title + "','" + url + '?' + params + '\');">' + auth.PageViewname + '</a>';
        }
        return r;
    }
};

function openItemPage(title, url) {
    msg.modalOpen({
        title: title,
        url: url,
        height: 500,
        width: 1100,
        btn: ['关闭'],
        callBack: function (frm) {
            layer.close(layerIdx);
        }
    });
};

