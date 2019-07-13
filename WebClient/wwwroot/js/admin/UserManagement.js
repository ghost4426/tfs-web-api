$(document).ready(function () {
    changeRole();
});

var userTable = $('#userTable').DataTable({
    ajax: {
        url: GET_USER_URI,
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
                    return '<font color="green">Active</font>';
                }
                else {
                    return '<font color="red">Deactive</font>';
                };
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                return '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-grey" data-toggle="modal" data-target="#confirm" tittle="Đổi trạng thái"><i class="fa fa-repeat"></i ></button >' +
                    '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-primary" data-toggle="modal" data-target="#changeRole" title="Đổi vai trò"><i class="fa fa-odnoklassniki"></i ></button >';

            }
        },
    ],
});

$('#confirmButton').click(function () {
    var userId = parseInt($('#txtUserIdActive').val());
    callAjax(
        {
            url: DEACTIVE_USER_URI + userId,
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
            url: GET_USER_DETAILS_URI + userId,
            dataType: JSON_DATATYPE,
            
        },
        JSON.stringify({
            
        }),
        function (result) {
            $('#userId').val(data.UserId);
            $('#FullName').val(data.Fullname);
            $('#Email').val(data.Email);
            $('#Phone').val(data.PhoneNo);
            $('#txtUserIdRole').val(data.UserId);
            $('div#RoleOption select').val(data.RoleId).change();
            $('#txtUserIdActive').val(data.UserId);
            if (data.IsActive == true) {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái sang Deactive không?";
            } else {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái sang Active không?";
            }
            $('#status').val(data.IsActive);
        },
        function (result) {
            toastr.error(result);
        })
    getRole();
}

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
    $("#Role2").empty();
    $.ajax({
        url: 'https://localhost:4200/api/Admin/role/',
        type: 'GET',
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
    var userId = $('#txtUserIdPass').val();
    var oldPass = $('#txtOldPass').val();
    var newPass = $('#txtNewPass').val();
    callAjax(
        {
            type: PUT,
            url: USER_PASS_CHANGE_URI + userId,
            dataType: JSON_DATATYPE,
        },
        JSON.stringify({
            password: newPass,
            oldPass: oldPass
        }),
        function (result) {
            toastr.success('Đổi mật khẩu thành công', 'Thành Công');
            //setTimeout("location.reload(true);", 2000);
            $('#changePass').modal('hide');
            /*$('#userTable').DataTable().ajax.reload();*/
        },
        function (result) {
            toastr.error(result);
        })
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
//Change Role
function changeRole() {
    $('#changeRoleButton').click(function () {
        var userId = parseInt($('#txtUserIdRole').val());
        var roleId = $('select[id="dllRole"]').val();
        $.ajax({
            type: 'PUT',    
            url: CHANGE_ROLE_URI + userId,
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
