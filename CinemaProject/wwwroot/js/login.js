var Login_Module = (function () {
    function Init() {
        InitializeLogin();
        LoginHandler();
    }
    function InitializeLogin() {
        $("#login-message-error").hide();
        $("#input-email").focusin(function () {
            $("#login-message-error").hide();

        });
        $("#input-password").focusin(function () {
            $("#login-message-error").hide();

        });
        $('#login-trigger').click(function () {
            $(this).next('#login-content').slideToggle();
            $(this).toggleClass('active');

            if ($(this).hasClass('active')) $(this).find('span').html('&#x25B2;')
            else $(this).find('span').html('&#x25BC;')
        })

    }

    function LoginHandler() {
        $("#login-button").on("click", function () {
            var email = $("#input-email").val();
            var password = $("#input-password").val();
            var rememberMe = $("#remember-me").is(':checked');
            var loginViewModel = {
                Email: email, Password: password, RememberMe: rememberMe
            };
            $.ajax({
                type: "POST",
                url: "/Account/Login",
                data: JSON.stringify(loginViewModel),
                contentType: "application/json",
                success: function (result) {
                    location.reload();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $("#login-message-error").show();
                }
            });

        });
    }


    return {
        Init: function () {
            Init();
        },
    };
})();
