$(document).ready(function () {
    changeRole();
});

var userTable = $('#userTable').DataTable({
    ajax: {
        url: 'https://localhost:4200/api/Admin/Users',
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
                    '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-info"  title="Cập Nhật Thông Tin Người Dùng"><i class="icon-pencil"></i ></button >' +
                    '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-primary" data-toggle="modal" data-target="#changeRole" title="Đổi vai trò"><i class="fa fa-odnoklassniki"></i ></button >';

            }
        },
    ],
});

$('#confirmButton').click(function () {
    var userId = parseInt($('input[name="UserId3"]').val());
    callAjax(
        {
        url: 'https://localhost:4200/api/Admin/User/Deactive/' + userId,
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
    $.ajax({
        url: 'https://localhost:4200/api/Admin/User/' + userId,
        type: 'GET',
        success: function (data) {
            $('input[name="userId"]').val(data.UserId);
            $('input[name="FullName"]').val(data.Fullname);
            $('input[name="Email"]').val(data.Email);
            $('input[name="Phone"]').val(data.PhoneNo);
            $('input[name="UserId2"]').val(data.UserId);
            $('div#RoleOption select').val(data.RoleId).change();
            $('input[name="UserId3"]').val(data.UserId);
            if (data.IsActive == true) {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái sang Deactive không?";
            } else {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái sang Active không?";
            }
            $('input[name="status"]').val(data.IsActive);
        },
        error: function () {
            toastr.error('Xin hãy kiểm tra lại', 'Thất bại');
        }
    })
    getRole();
}

function getRole() {
    $("#Role2").empty();
    $.ajax({
        url: 'https://localhost:4200/api/Admin/Role/',
        type: 'GET',
        success: function (data) {
            $.each(data, function (key, value) {
                $("#Role2").append($("<option></option>").val(value.RoleId).html(value.Name));
            });
        },
        error: function () {
            toastr.error('Xin hãy kiểm tra lại', 'Thất bại');
        }
    })
}

$('#updateUser').click(function () {
    var userId = parseInt($('input[name="userId"]').val());
    var FullName = $('input[name="FullName"]').val();
    var Email = $('input[name="Email"]').val();
    var Phone = $('input[name="Phone"]').val();
    callAjax(
        {
            type: PUT,
            url: 'https://localhost:4200/api/Admin/Users/Update/' + userId,
            dataType: JSON_DATATYPE,
        },
        JSON.stringify({
            fullName: FullName,
            Email: Email,
            phone: Phone
        }),
        function (result) {
            toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
            //setTimeout("location.reload(true);", 2000);
            $('#updateInfo').modal('hide');
            $('#userTable').DataTable().ajax.reload();
        },
        function (result) {
            toastr.error('Xin hãy kiểm tra lại', result);
        })
});

function changeRole() {
    $('#changeRoleButton').click(function () {
        var userId = parseInt($('input[name="UserId2"]').val());
        var roleId = $('select[name="Role2"]').val();
        $.ajax({
            type: 'PUT',    
            url: 'https://localhost:4200/api/Admin/User/Role/' + userId,
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
