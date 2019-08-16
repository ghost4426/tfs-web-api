$(document).ready(function () {
    changeRole();
});

var userTable = $('#userTable').DataTable({
    ajax: {
        url: GET_USER_URI,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8',
            "Authorization": 'Bearer ' + Cookies.get('token')
        },
        statusCode: {
            401: function () {
                window.location.replace("/dang-nhap");
            },
        },
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    columns: [
        { data: 'UserId' },
        { data: 'Username' },
        { data: 'Fullname' },
        { data: 'Role.Name' },
        { data: 'PhoneNo' },
        { data: 'Email' },
        {
            data: 'IsActive',
            render: function (data, type, row) {
                if (data == true) {
                    return '<span class="badge badge-success"><b>Hiệu lực</b></span>';
                }
                else {
                    return '<span class="badge badge-danger"><b>Vô Hiệu lực</b></span>';
                };
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                return '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-grey" data-toggle="modal" data-target="#confirm" tittle="Đổi trạng thái"><i class="fa fa-repeat"></i ></button >';

            }
        },
    ],
    language: userTable_vi_lang
});

$('#confirmButton').click(function () {
    var userId = parseInt($('#txtUserIdActive').val());
    callAjax(
        {
            url: DEACTIVE_USER_URI + 16,
            dataType: JSON_DATATYPE,
            type: PUT,
        }, JSON.stringify(),
        function (result) {
            toastr.success('Bạn đã thay đổi trạng thái thành công', 'Thay đổi thành công');
            $('#confirm').modal('hide');
            $('#userTable').DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result);
        }
    )
});

function getUserInfo(userId) {

    callAjax(
        {
            type: GET,
            url: GET_USER_DETAILS_URI + 16,
            dataType: JSON_DATATYPE,

        },
        JSON.stringify({
        }),
        function (user, result) {
            getRole();
            $('#userId').val(user.data.UserId);
            $('#FullName').val(user.data.Fullname);
            $('#Email').val(user.data.Email);
            $('#Phone').val(user.data.PhoneNo);
            $('#txtUserIdRole').val(user.data.UserId);
            $('div#RoleOption select').val(user.data.RoleId).change();
            $('#txtUserIdActive').val(user.data.UserId);
            if (user.data.IsActive == true) {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái sang Vô hiệu lực không?";
            } else {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái sang Hiệu lực không?";
            }
            $('#status').val(user.data.IsActive);
        },
        function (result) {
            toastr.error(result);
        })
    getRole();
}
//Load Image
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#avatar')
                .attr('src', e.target.result)
                .width(150)
                .height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}
function getRole() {
    $("#dllRole").empty();
    $.ajax({
        url: GET_ROLE_URI,
        type: GET,
        success: function (data) {

            $.each(data, function (key, value) {
                $("#dllRole").append($("<option></option>").val(value.RoleId).html(value.Name));
            });
        },
        error: function () {
            toastr.error('Xin hãy kiểm tra lại', 'Thất bại');
        }
    })
}
//Change pass
$('#changePassButton').click(function () {
    var userId = parseInt($('#txtUserIdPass').val());
    var oldPass = $('#txtOldPass').val();
    var newPass = $('#txtNewPass').val();
    var confirmNewPass = $('#txtConfirmNewPass').val();
    if (confirmNewPass == newPass) {
        callAjax(
            {
                url: USER_PASS_CHANGE_URI + 16,
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
//Confirm save 
$('#confirmSaveButton').click(function () {
    var userId = parseInt($('#userId').val());
    var FullName = $('#FullName').val();
    var Email = $('#Email').val();
    var Phone = $('#Phone').val();
    callAjax(
        {
            type: PUT,
            url: USER_UPDATE_URI + 16,
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
//Change Role
function changeRole() {
    $('#changeRoleButton').click(function () {
        var userId = parseInt($('#txtUserIdRole').val());
        var roleId = $('select[id="dllRole"]').val();
        $.ajax({
            type: 'PUT',
            url: CHANGE_ROLE_URI + 16,
            contentType: 'json',
            headers: {
                //'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: roleId,
            success: function () {
                toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
                $('#changeRole').modal('hide');
                $('#userTable').DataTable().ajax.reload();
            },
            error: function () {
                toastr.error('Xin hãy kiểm tra lại', 'Cập nhật thất bại');
            }
        })
    })
}

/*$('#changeRoleButton').click(function () {
    var userId = parseInt($('input[name="UserId2"]').val());
    var roleId = $('select[name="Role2"]').val();
    callAjax({
        type: PUT,
        url: 'https://localhost:4200/api/Admin/User/Role/' + userId,
        dataType: JSON_DATATYPE,
        data: roleId,
    }, JSON.stringify(),
        function (result) {
            toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
            $('#changeRole').modal('hide');
            $('#userTable').DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result);
        }
    )
});*/
