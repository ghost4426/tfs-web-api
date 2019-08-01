$(document).ready(function () {
    getProfile();
});

function getProfile() {
    callAjaxAuth(
        {
            type: GET,
            url: GET_PROFILE_URI,
            dataType: JSON_DATATYPE,
        },
        JSON.stringify({}),
        function (result) {
            $('#userId').text(result.data.UserId);
            $('#UserName').text(result.data.Username);
            $('#FullName').text(result.data.Fullname);
            $('#Email').text(result.data.Email);
            $('#Phone').text(result.data.PhoneNo);
            $('#txtRole').text(result.data.Role.Name);
            if (result.data.IsActive==true) {
                $('#txtStatus').append('<span class="btn btn-success btn-sm mr-1 mb-1 ladda-button"><b>Hiệu lực</b></span>');
                
            } else {
                $('#txtStatus').append('<span class="btn btn-danger btn-sm mr-1 mb-1 ladda-button"><b>Vô Hiệu lực</b></span>');
            }
            if (result.data.Premises != null) {
                $('#txtPremises').text(result.data.Premises.Name);
            } else {
                $('#txtPremises').text("Chưa có thông tin cơ sở");
            }
            
        },
        function (result) {
            toastr.error(result.UserId);
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