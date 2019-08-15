Date.prototype.Format = function (fmt) {
    var o =
    {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
Date.prototype.addDays = function (d) {
    this.setDate(this.getDate() + d);
};
Date.prototype.addWeeks = function (w) {
    this.setDate(this.getDate() + w * 7);
};
Date.prototype.addMonth = function (m) {
    this.setMonth(this.getMonth() + m);
};
Date.prototype.addYears = function (y) {
    this.setFullYear(this.getFullYear() + y);
};

//NumCreateColor
var tools = {
    delHtmlTag: function (str) {
        return str.replace(/<[^>]+>/g, "");//去掉所有的html标记
    },
    trim: function (s) {
        s = this.ltrim(s);
        s = this.rtrim(s);
        return s;
    },
    ltrim: function (s) {
        return s.replace(/^\s*/, "");
    },
    rtrim: function (s) {
        return s.replace(/\s*$/, "");
    },
    gotop: function () {
        $("body,html").animate({ scrollTop: 0 }, 700);
    },
    colorRgb: function (hex) {
        hex = hex.toUpperCase();
        if (hex.charAt(0) == '#') {
            var rgb = parseInt(hex.substr(1, 6), 16);
            var r = rgb >> 16;
            var g = rgb >> 8 & 0xFF;
            var b = rgb & 0xFF;
            return "rgb(" + r + "," + g + "," + b + ")";
        }
        return "rgb(173, 173, 173)";
    },
    colorRgba: function (hex, alpha) {
        hex = hex.toUpperCase();

        if (hex.charAt(0) == '#') {
            var rgb = parseInt(hex.substr(1, 6), 16);
            var r = rgb >> 16;
            var g = rgb >> 8 & 0xFF;
            var b = rgb & 0xFF;
            var rgba = "rgba(" + r + "," + g + "," + b + ", " + alpha + ")";
            return rgba;
        }
        return "rgba(173, 173, 173, 0.25)";
    },
    getTimeStamp: function () {
        return parseInt(new Date().getTime()).toString();
    },
    toFixed2: function (obj) {
        return parseFloat(obj).toFixed(2);
    },
    getValueByKey: function (obj, key, df) {
        if (obj[key]) {
            return obj[key];
        }
        if (df) {
            return df;
        }
        return "";
    },
    formatTime: function (value) {//传入格式为  ：  /Date(1470819621000)/	
        if (value == '' || value == null)
            return '';
        value = eval('new ' + (value.replace(/\//g, '')));
        var year = value.getFullYear();
        var month = value.getMonth() + 1 < 10 ? "0" + (value.getMonth() + 1) : value.getMonth() + 1;
        var date = value.getDate() < 10 ? "0" + value.getDate() : value.getDate();
        var hour = value.getHours() < 10 ? "0" + value.getHours() : value.getHours();
        var minute = value.getMinutes() < 10 ? "0" + value.getMinutes() : value.getMinutes();
        var second = value.getSeconds() < 10 ? "0" + value.getSeconds() : value.getSeconds();
        return (year + "-" + month + "-" + date + " " + hour + ":" + minute + ":" + second);
    },
    formatterTime2: function (value) {//传入格式为  ：  /Date(1470819621000)/
        if (value == '' || value == null)
            return '';
        value = eval('new ' + (value.replace(/\//g, '')));
        var year = value.getFullYear();
        var month = value.getMonth() + 1 < 10 ? "0" + (value.getMonth() + 1) : value.getMonth() + 1;
        var date = value.getDate() < 10 ? "0" + value.getDate() : value.getDate();
        return (year + "-" + month + "-" + date);
    },
    formatterTimeDate: function (value) { //传入格式为  ：  1470819621000	
        value = new Date(value);
        var year = value.getFullYear();
        var month = value.getMonth() + 1 < 10 ? "0" + (value.getMonth() + 1) : value.getMonth() + 1;
        var date = value.getDate() < 10 ? "0" + value.getDate() : value.getDate();
        var hour = value.getHours() < 10 ? "0" + value.getHours() : value.getHours();
        var minute = value.getMinutes() < 10 ? "0" + value.getMinutes() : value.getMinutes();
        var second = value.getSeconds() < 10 ? "0" + value.getSeconds() : value.getSeconds();
        return (year + "-" + month + "-" + date + " " + hour + ":" + minute + ":" + second);
    },
    GetNowTime: function CurentTime() {
        var now = new Date();

        var year = now.getFullYear();       //年
        var month = now.getMonth() + 1;     //月
        var day = now.getDate();            //日

        var hh = now.getHours();            //时
        var mm = now.getMinutes();          //分
        var ss = now.getSeconds();         //秒
        var clock = year + "-";

        if (month < 10)
            clock += "0";

        clock += month + "-";

        if (day < 10)
            clock += "0";

        clock += day + " ";

        if (hh < 10)
            clock += "0";

        clock += hh + ":";

        if (mm < 10)
            clock += '0';
        clock += mm + ":";

        if (ss < 10)
            clock += '0';
        clock += ss;
        return (clock);
    },
    DateFormart: function (date, fmt) {
        var o = {
            "M+": date.getMonth() + 1, //月份 
            "d+": date.getDate(), //日 
            "h+": date.getHours(), //小时 
            "m+": date.getMinutes(), //分 
            "s+": date.getSeconds(), //秒 
            "q+": Math.floor((date.getMonth() + 3) / 3), //季度 
            "S": date.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    },
    getQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },
    nullToEmptyString: function (obj) {
        if(obj == null)
            return '';
        else{
            return obj;
        }
    },
    nullToDefaultString: function (obj,defaultvalue) {
        if (obj == null || obj == '')
            return defaultvalue;
        else {
            return obj;
        }
    },
    nullToZore: function (obj) {
        if (obj == null) {
            return 0;
        } else {
            return obj;
        }
    },
    setNullToImg: function (obj) {
        if (obj == null) {
            return "/Content/images/nopic.jpg";
        } else {
            return obj;
        }
    },
    numCreateColor:function(i)
    {
        var clrs = ['#36A4FB', '#36C4C6', '#729FDA', '#67C2EF', '#FF6B6B', '#99CCFF', '#FF6600',
            '#FF9900', '#99CC33', '#666699', '#CCCC33', '#339933', '#FF6666', '#CCCCFF', '#6699CC', '#9999CC'];

        var k = i % clrs.length;
        return clrs[k];
    },
    getImageHtml: function (src) {
        return "<img src=\"" + src + "\" onerror=\"this.src='/Content/images/nopic.jpg'\" />";
    }



}