$(document).ready(function () {
    loadUser();
    updateUser();
    changeRole();
});

function loadUser() {
    $.ajax({
        method: "GET",
        crossDomain: true,
        url: "https://localhost:4200/api/Admin/Users",
        dataType: 'json',
        success: function (data) {
            $('#userTable').DataTable({
                bDestroy: true,
                data: data,
                ordering: true,
                columns: [
                    { data: 'UserId' },
                    { data: 'Username' },
                    { data: 'Fullname' },
                    { data: 'RoleId' },
                    { data: 'PhoneNo' },
                    { data: 'Email' },
                    { data: 'IsActive' },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<button onclick="statusChange(' + data.UserId + ')" class="btn btn-grey" tittle="Đổi trạng thái"><i class="fa fa-repeat"></i ></button >' +
                                '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-info" data-toggle="modal" data-target="#updateInfo" title="Cập Nhật Thông Tin Người Dùng"><i class="icon-pencil"></i ></button >' +
                                '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-primary" data-toggle="modal" data-target="#changeRole" title="Đổi vai trò"><i class="fa fa-odnoklassniki"></i ></button >';

                        }
                    },
                ],
            })
        }
    });
}
function statusChange(userId) {
    $.ajax({
        url: 'https://localhost:4200/api/Admin/User/Deactive/' + userId,
        type: 'PUT',
        success: function () {
            toastr.success('Bạn đã thay đổi trạng thái thành công. Làm mới trong 2s', 'Thay đổi thành công');
            setTimeout("location.reload(true);", 2000);
        },
        error: function () {
            toastr.error('Xin hãy kiểm tra lại', 'Thay đổi thất bại');
        }
    })
}
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
            $('input[name="Role2"]').val(data.RoleId);
        },
        error: function () {
            toastr.error('Xin hãy kiểm tra lại', 'Thất bại');
        }
    })
}
function updateUser() {
    $('#updateUser').click(function () {
        var userId = parseInt($('input[name="userId"]').val());
        var FullName = $('input[name="FullName"]').val();
        var Email = $('input[name="Email"]').val();
        var Phone = $('input[name="Phone"]').val();
        $.ajax({
            type: 'PUT',
            url: 'https://localhost:4200/api/Admin/Users/Update/' + userId,
            contentType: 'json',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
            },
            data: JSON.stringify({
                fullName: FullName,
                Email: Email,
                phone: Phone
            }),
            success: function () {
                toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
                //setTimeout("location.reload(true);", 2000);
                loadUser();
            },
            error: function () {
                toastr.error('Xin hãy kiểm tra lại', 'Cập nhật thất bại');
            }
        })

    })

}

function changeRole() {
    $('#changeRoleButton').click(function () {
        var userId = parseInt($('input[name="UserId2"]').val());
        var roleId = $('input[name="Role2"]').val();
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
                loadUser();
            },
            error: function () {
                toastr.error('Xin hãy kiểm tra lại', 'Cập nhật thất bại');
            }
        })

    })
}
