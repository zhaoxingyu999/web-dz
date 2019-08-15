var Validate = {
    //是否正整数
    isPInteger: function (s) {
        s = Validate.trim(s);
        var p = /^\d+$/;
        return p.test(s);
    },
    //是否负整数
    isNInteger: function (s) {
        s = Validate.trim(s);
        var p = /^-\d+$/;
        return p.test(s);
    },
    //是否整数(含正负)
    isInteger: function (s) {
        s = Validate.trim(s);
        var p = /^-?\d+$/;
        return p.test(s);
    },
    //是否正小数
    isPDecimal: function (s) {
        s = Validate.trim(s);
        var p = /^\d+\.\d+$/;
        return p.test(s);
    },
    //是否负小数
    isNDecimal: function (s) {
        s = Validate.trim(s);
        var p = /^\d+\.\d+$/;
        return p.test(s);
    },
    //是否小数（含正负）
    isDecimal: function (s) {
        s = Validate.trim(s);
        var p = /^-\d+\.\d+$/;
        return p.test(s);
    },
    //是否实数（含正负）
    isRealNumber: function (s) {
        s = Validate.trim(s);
        var p = /^-?\d+\.?\d*$/;
        return p.test(s);
    },
    //是否保留1位小数
    isFloat1: function (s) {
        s = Validate.trim(s);
        var p = /^-?\d+\.?\d{0,1}$/;
        return p.test(s);
    },
    //是否保留2位小数
    isFloat2: function (s) {
        s = Validate.trim(s);
        var p = /^-?\d+\.?\d{0,2}$/;
        return p.test(s);
    },
    //是否保留3位小数
    isFloat3: function (s) {
        s = Validate.trim(s);
        var p = /^-?\d+\.?\d{0,3}$/;
        return p.test(s);
    },
    //是否电话
    isPhone: function (s) {
        var re = /^0\d{2,3}-?\d{7,8}$/;
        return re.test(s);
    },
    //是否邮编
    isZipcode: function (s) {
        if (s.length != 6) {
            return false;
        }
        return this.isPInteger(s)
    },
    //是否手机号码
    isMobile: function (s) {
        var regu = /^(1[3,5,8][0-9])\d{8}$/;
        return regu.test(s);
    },
    //检查是否邮箱
    isEmail: function (s) {
        var myReg = /^([-_A-Za-z0-9\.]+)@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/;
        return myReg.test(s);
    },


};

//字符帮助
var strHelper = {
    trim: function (s) {
        return Validate.rtrim(Validate.ltrim(s));
    },
    ltrim: function (s) {
        return s.replace(/^\s*/, "");
    },
    rtrim: function (s) {
        return s.replace(/\s*$/, "");
    },
    //去掉所有的html标记
    delHtmlTag: function (str) {
        return str.replace(/<[^>]+>/g, "");
    },
};

//格式化帮助
var formatHelper = {
    formatToN2: function (s) {
        if (!/\d+(\.\d+)?/.test(s)) return "0.00";
        if (/^\d+$/.test(s)) return (s + '').replace(/\B(?=(\d{3})+$)/g, ',') + '.00';
        if (/^(\d+)\.(\d+)$/.test(s)) {
            s = s.toFixed(2);
            var v = s.split('.');
            var f = (v[0] + '').replace(/\B(?=(\d{3})+$)/g, ',');
            return f + '.' + v[1]
        }
        return "0.00";
    },
    //保留小数点后两位
    toFixed2: function (obj) {
        if (obj == null)
            return '0.00';
        return parseFloat(obj).toFixed(2);
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
    //时间格式化
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
    AdTypeFormart: function (value) {
        if (value == 1)
            return " 弹窗广告";
        if (value == 2)
            return "banner";
        if (value == 3)
            return "今日推荐";
        if (value == 4)
            return "有好货";
        if (value == 5)
            return "热销爆品";
        if (value == 6)
            return "滚动条";


    },
    AdStautsFormart: function (value) {
        if (value == 0)
            return " 未启用";
        if (value == 1)
            return " 启用";

    },

};

var pageHelper = {
    //移至页面顶部
    gotop: function () {
        $("body,html").animate({ scrollTop: 0 }, 700);
    },
};

var colorHelper = {
    //颜色编码转RGB
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
    //颜色编码转rgba
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
};

var myhelper = {
    getEnumLabel: function (enumX, id) {
        if (enumX[id]) {
            var r = '<span class="' + enumX[id].cssname + '">' + enumX[id].name + '</span>';;
            return r;
        }
        return '';
    },
    getEnumOption: function (enumX) {
        var id = 0;
        var enInfo = "";
        $.each(enumX, function (i) {
            enInfo += '<option value="' + enumX[i].id + '">' + enumX[i].name + '</option>';
        })
        return enInfo;
    

    }
};