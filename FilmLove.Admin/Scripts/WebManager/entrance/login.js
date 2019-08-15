$(function () {
    login.pageBind();
});

var login = {
    pageBind: function () {
        var mttop = ($(window).height() - $('.frontHome.page').height() - 20) / 2
        if (mttop < 0) { mttop = 0 }
        //登录框效果
        $('#id_username').focus(function () {
            $(this).parent('.controls').addClass('focus');
        }).blur(function () {
            $(this).parent('.controls').removeClass('focus');
        });

        $('#id_password').focus(function () {
            $(this).parent('.controls').addClass('focus');
        }).blur(function () {
            $(this).parent('.controls').removeClass('focus');
        });


        $('#id_vercode').focus(function () {
            $(this).parent('.controls').addClass('focus');
        }).blur(function () {
            $(this).parent('.controls').removeClass('focus');
        })

        $(".no-upie").click(function () {
            $("#noie-content").hide();
            $("#loginbox").show();
        })

        $('.js_login_btn').click(function () {
            login.doLogin()
        });

        $('#vercodeImg').click(function () {
            login.changeVerCode();
        });

        $('.textInput').keypress(function (event) {
            if (event.keyCode == "13") {
                login.doLogin();
            }
        });
        login.changeVerCode();
    },
    doLogin: function () {
        var postData = {};
        postData.LoginName = $('#id_username').val();
        postData.Password = $('#id_password').val();
        postData.VerCode = $('#id_vercode').val();
        if (postData.LoginName == '') {
            msg.warn("请输入登录名！", function () {
                $('#id_username').focus();
            });
            this.changeVerCode();
            return;
        }

        if (postData.Password == '') {
            msg.warn("请输入密码！", function () {
                $('#id_password').focus();
            });
            this.changeVerCode();
            return;
        }

        if (postData.VerCode == '') {
            msg.warn("请输入验证码！", function () {
                $('#id_vercode').focus();
            });
            this.changeVerCode();
            return;
        }
        $('.js_login_btn').html("登 录 中 . .");
        $('.js_login_btn').attr('disabled', true);
        postData.Password = $.md5(postData.Password);

        $.post('/WebEntrance/DoLogin', postData, function (result) {
            $('.js_login_btn').attr('disabled', false);
            if (result.code != 0) {
            $('.js_login_btn').html("登    录");
                msg.warn(result.msg);
                login.changeVerCode();
                return;
            }
            $('.js_login_btn').html("登录成功");
            location.href = "/WebHome/Index";
        });
    },
    //更换验证码
    changeVerCode: function () {
        $("#vercodeImg").attr("src", "/WebEntrance/ValidateCode" + "?r=" + Math.random());
    },
}