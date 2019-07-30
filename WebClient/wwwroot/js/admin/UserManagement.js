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
                    return '<span class="btn btn-success btn-sm mr-1 mb-1 ladda-button"><b>Hiệu lực</b></span>';
                }
                else {
                    return '<span class="btn btn-danger btn-sm mr-1 mb-1 ladda-button"><b>Vô Hiệu lực</b></span>';
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


