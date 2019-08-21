(function ($) {
    "use strict";


    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('#btn-Send').on('click', function () {
        var check = true;
        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }
        if (check) {
            var email = $('#txtEmail').val();
            callAjax(
                {
                    url: FORGET_PASSWORD_URI,
                    dataType: JSON_DATATYPE,
                    type: PUT,
                },
                JSON.stringify({
                    Email: email
                }),
                function (result) {
                    $('#mess').empty();
                    $('#mess').append('<span>Mật khẩu khôi phục thành công</span><br/><span>Kiểm tra mật khẩu mới trong email</span>');
                    $('#btn-Send').remove();
                    $('#email-form').remove();
                },
                function (result) {
                    toastr.error(result.responseJSON.message);
                }
            );
        } else {
            return false;
        }
    });


    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {
        console.log($(input).attr('type'));
        if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if ($(input).val() == '' || $(input).val() == null) {
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }
})(jQuery);