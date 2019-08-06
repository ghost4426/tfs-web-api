$(document).ready(function () {
    getProfile();
});

function getProfile() {
    $('#txtStatus').empty();
    callAjaxAuth(
        {
            type: GET,
            url: GET_PROFILE_URI,
            dataType: JSON_DATATYPE,
        },
        JSON.stringify({}),
        function (result) {
            $('span.avatar > img').attr({ src: result.data.Image });
            $('#profile-ava').attr({src:result.data.Image});
            $('#userId').text(result.data.UserId);
            $('#txtUserId').val(result.data.UserId);
            $('#txtUserIdPass').val(result.data.UserId);
            $('#UserName').text(result.data.Username);
            $('#FullName').text(result.data.Fullname);
            $('#Email').text(result.data.Email);
            $('#Phone').text(result.data.PhoneNo);
            $('#txtRole').text(result.data.Role);
            if (result.data.IsActive==true) {
                $('#txtStatus').append('<span class="btn btn-success btn-sm mr-1 mb-1 ladda-button"><b>Hiệu lực</b></span>');
                
            } else {
                $('#txtStatus').append('<span class="btn btn-danger btn-sm mr-1 mb-1 ladda-button"><b>Vô Hiệu lực</b></span>');
            }
            if (result.data.PremisesName != null && result.data.PremisesType != null) {
                
                $('#h4Premises').css('visibility','visible');
                $('#PremisesName').css('visibility','visible');
                $('#PremisesType').css('visibility', 'visible');
                $('#txtPremisesName').text(result.data.PremisesName);
                $('#txtPremisesType').text(result.data.PremisesType);
                
            } else {
                $('#h4Premises').css('visibility', 'hidden');
                $('#PremisesName').css('visibility', 'hidden');
                $('#PremisesType').css('visibility', 'hidden');
                $('#txtPremisesName').text("");
                $('#txtPremisesType').text("");
                //$('#txtPremisesName').text("Chưa có thông tin cơ sở").css({ 'color': 'red', 'font-weight': 'bold' });
            }
        },
        function (result) {
            toastr.error(result.UserId);
        })
}
//Change pass
$('#changePassButton').click(function () {
    var userId = parseInt($('#txtUserIdPass').val());
    var oldPass = $('#txtOldPass').val();
    var newPass = $('#txtNewPass').val();
    var confirmNewPass = $('#txtConfirmNewPass').val();
    if (newPass != "" && oldPass != "" && confirmNewPass != "") {
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
                    $('#changePassModal').modal('hide');
                    getProfile();
                    /*$('#userTable').DataTable().ajax.reload();*/
                },
                function (result) {
                    toastr.error("Mật khẩu cũ không chính xác!");
                })
        } else {
            toastr.error("Xác nhận mật khẩu không chính xác!")
        }
    } else {
        toastr.error("Vui lòng điền đầy đủ thông tin!")
    }
    
});
//Load Image


$(".upload-button").on('click', function () {
    $(".file-upload").click();
});

$('#loadModal').click(function () {
    $('#txtFullName').val($('#FullName').text());
    $('#txtEmail').val($('#Email').text());
    $('#txtPhone').val($('#Phone').text());
});

//api imgur
$(".file-upload").on("change", function (event) {
    var img = this.files[0];
    var reader = new FileReader();

    reader.onload = function (e) {
        $('.profile-pic').attr('src', e.target.result);
    }
    reader.readAsDataURL(img);
    console.log(img);
    var form = new FormData();
    form.append("image", img);

    var settings = {
        "url": "https://api.imgur.com/3/image",
        "method": "POST",
        "dataType": JSON_DATATYPE,
        "timeout": 0,
        "headers": {
            "Authorization": "Client-ID 527d3d0ae851a83"
        },
        "beforeSend": showLoadingPage,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {
        console.log(response.data.link);
        saveImg(response.data.link);
    });
});
//save img link to db
function saveImg(imgDetails) {
    var userId = $('#userId').text();
    callAjax(
        {
            url: CHANGE_AVA_URI + userId,
            dataType: JSON_DATATYPE,
            type: PUT,
        },
        JSON.stringify({
            avaUrl: imgDetails,
        }),
        function (result) {
            toastr.success('Đổi ảnh đại diện thành công', 'Thành Công');
            //setTimeout("location.reload(true);", 2000);
            getProfile();
            /*$('#userTable').DataTable().ajax.reload();*/
        },
        function (result) {
            toastr.error("Đã có lỗi xảy ra!");
        })

}
//validate
function validate(input) {
    if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
        if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
            return false;
        }
    } else if ($(input).attr('name') == 'Phone') {
        if ($(input).val().trim().match(/^\d{10}/) == null) {
            return false;
        }
    }
    else {
        if ($(input).val() == '') {
            return false;
        }
    }
}
//Confirm save 
$('#confirmSaveButton').click(function () {
    var isValidated = true;
    var txtUserId = parseInt($('#txtUserId').val());
    var txtFullName = $('#txtFullName').val();
    var txtEmail = $('#txtEmail').val();
    var txtPhone = $('#txtPhone').val();

    if (validate('txtFullName') == false) {
        toastr.error('Vui lòng kiểm tra lại Tên');
        isValidated = false;
    } else if (validate('txtEmail') == false) {
        toastr.error('Vui lòng kiểm tra lại Email');
        isValidated = false;
    } else if (validate('txtPhone') == false) {
        toastr.error('Vui lòng kiểm tra lại Số điện thoại');
        isValidated = false;
    }else
    if (isValidated == true) {
        callAjax(
            {
                type: PUT,
                url: USER_UPDATE_URI + txtUserId,
                dataType: JSON_DATATYPE,
            },
            JSON.stringify({
                Fullname: txtFullName,
                Email: txtEmail,
                PhoneNo: txtPhone
            }),
            function (result) {
                toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
                //setTimeout("location.reload(true);", 2000);
                $('#confirm').modal('hide');
                getProfile();
                /*$('#userTable').DataTable().ajax.reload();*/
            },
            function (result) {
                toastr.error('Đã có lỗi xảy ra!');
            })
    }
    
});