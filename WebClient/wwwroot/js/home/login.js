$('#btnLogin').click(function () {
    var username = $('#txtUsername').val();
    var password = $('#txtPassword').val();

    callAjax(
        {
            url: LOGIN_URI,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify({
            Username: username,
            Password: password,
        }),
        function (result) {
            Cookies.set('token', result.token);
            window.location.replace("/");
        },
        function (result) {
            console.log(result);
        }
    )
})

