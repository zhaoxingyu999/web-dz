var Validate = {
    isPhone: function (s) {
        var re = /^0\d{2,3}-?\d{7,8}$/;
        return re.test(s);
    },
    isZipcode: function (s) {
        if (!Validate.isInteger(s)) {
            return false;
        }
        if (s.length != 6) {
            return false;
        }
        return true;
    },
    //检查是否手机号码
    isMobile: function (s) {
        var regu = /^(1[3,5,8][0-9])\d{8}$/;
        return regu.test(s);
    },
    //检查是否邮箱
    isEmail: function (s) {
        var myReg = /^([-_A-Za-z0-9\.]+)@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/;
        return myReg.test(s);
    },
    //验证是否数字
    isInteger: function (s) {
        s = Validate.trim(s);
        var p = /^\d+$/;
        return p.test(s);
    },
    isFloat: function (s) {
        s = Validate.trim(s);
        var p = /^\d+.{0,1}\d+$/;
        return p.test(s);
    },
    isTwoPositiveFloat: function (s) {
        s = Validate.trim(s);
        if (s.substring(0, 2) == '00') {
            return false;
        }
        var p = /^\d+.{0,1}\d{0,2}$/;
        return p.test(s);
    },
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
    trim: function (s) {
        return Validate.rtrim(Validate.ltrim(s));
    },
    ltrim: function (s) {
        return s.replace(/^\s*/, "");
    },
    rtrim: function (s) {
        return s.replace(/\s*$/, "");
    }

}