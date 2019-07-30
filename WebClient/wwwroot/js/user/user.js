$(document).ready(function () {
    getProfile();
});

function getProfile(userId) {
    callAjax(
        {
            type: GET,
            url: GET_PROFILE_URI,
            dataType: JSON_DATATYPE,

        },
        JSON.stringify({
        }),
        function (user, result) {
            $('#userId').val(user.data.UserId);
            $('#FullName').val(user.data.Fullname);
            $('#Email').val(user.data.Email);
            $('#Phone').val(user.data.PhoneNo);
            $('#txtRole').val(user.data.Role.Name);
            $('#txtPremises').val(user.data.Premises.Name);
        },
        function (result) {
            toastr.error(result);
        })
}
//Change pass
$('#changePassButton').click(function () {
    var userId = parseInt($('#userId').val());
    var oldPass = $('#txtOldPass').val();
    var newPass = $('#txtNewPass').val();
    var confirmNewPass = $('#txtConfirmNewPass').val();
    if (confirmNewPass == newPass) {
        callAjax(
            {
                url: USER_PASS_CHANGE_URI + userId,
                dataType: JSON_DATATYPE,
                type: PUT,
            },
            JSON.stringify({
                newPass: newPass,
                oldPass: oldPass,
            }),
            function (result) {
                toastr.success('Đổi mật khẩu thành công', 'Thành Công');
                //setTimeout("location.reload(true);", 2000);
                $('#changePass').modal('hide');
                /*$('#userTable').DataTable().ajax.reload();*/
            },
            function (result) {
                toastr.error("Mật khẩu cũ không chính xác!");
            })
    } else {
        toastr.error("Xác nhận mật khẩu không chính xác!")
    }
});
//Load Image
$(".file-upload").on('change', function () {
    readURL(this);
});

$(".upload-button").on('click', function () {
    $(".file-upload").click();
});

var readURL = function (input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('.profile-pic').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
//Confirm save 
$('#confirmSaveButton').click(function () {
    var userId = parseInt($('#userId').val());
    var FullName = $('#FullName').val();
    var Email = $('#Email').val();
    var Phone = $('#Phone').val();
    callAjax(
        {
            type: PUT,
            url: USER_UPDATE_URI + userId,
        },
        JSON.stringify({
            fullName: FullName,
            Email: Email,
            phone: Phone
        }),
        function (result) {
            toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
            //setTimeout("location.reload(true);", 2000);
            $('#confirm').modal('hide');
            /*$('#userTable').DataTable().ajax.reload();*/
        },
        function (result) {
            toastr.error(result);
        })
});