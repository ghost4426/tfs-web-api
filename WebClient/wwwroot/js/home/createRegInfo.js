(function ($) {
    "use strict";


    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('#createNewRegisterInfoBtn').on('click', function () {
        var check = true;
        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }
        if (check) {
            var username = $('#txtUsername').val();
            var password = $('#txtPassword').val();
            var fullName = $('#txtFullname').val();
            var email = $('#txtEmail').val();
            var phone = $('#txtPhone').val();
            var premisesName = $('#txtPremisesName').val();
            var premisesAddress = $('#txtPremisesAddress').val();
            var preTypeId = $('select[id="dllPremisesType"]').val();
            console.log(phone);
            callAjax(
                {
                    url: REGISTER_URI,
                    dataType: JSON_DATATYPE,
                    type: POST,
                }, JSON.stringify({
                    PremisesName: premisesName,
                    PremisesAddress: premisesAddress,
                    PremisesTypeId: preTypeId,
                    Username: username,
                    Password: password,
                    Fullname: fullName,
                    Email: email,
                    PhoneNo: phone
                }),
                function (result) {
                    window.location.href = '/kich-hoat-tai-khoan';
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