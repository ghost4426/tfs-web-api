$(document).ready(function () {
    updateStatus();
});
//Lấy thông tin hồ sơ
var registerInfoTable = $('#registerInfoTable').DataTable({
    ajax: {
        url: GET_PREMISES_URI,
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    columns: [
        { data: 'RegisterId' },
        { data: 'PremisesName' },
        { data: 'PremisesType.Name' },
        { data: 'Username' },
        { data: 'Email' },
        {
            data: 'CreatedDate', width: '20%',
            render: function (data, type, row) {
                return $.format.date(data, "dd-MM-yyyy HH:mm")
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                if (data.IsConfirm == true) {
                    var btnChange = '<button class="btn btn-success btn-sm mr-1 mb-1 ladda-button" data-style="expand-right" data-size="s"><span class="ladda-label">Duyệt</span></button>';
                } else
                if (data.IsConfirm == null) {
                    var btnChange = '<button onclick="getReg(' + data.RegisterId + ')" class="btn btn-secondary btn-sm mr-1 mb-1 ladda-button" data-toggle="modal" data-target="#changeStatus" data-style="expand-right" data-size="s"><span class="ladda-label">Đang chờ</span></button>';
                } else {
                    var btnChange = '<button class="btn btn-danger btn-sm mr-1 mb-1 ladda-button" data-style="expand-right" data-size="s"><span class="ladda-label">Từ chối</span></button>';
                };
                return btnChange;
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                var btnView = '<button class="btn btn-grey btn-sm btn-view-detail" title="Chi tiết"><i class="icon-eye"></i ></button >';
                return btnView;
            }
        },
    ],
    language: registerInfoTable_vi_lang
});

var preId = 0;
//View details
$('#registerInfoTable').on('click', 'button.btn-view-detail', function () {
    var tr = $(this).closest('tr');
    var row = registerInfoTable.row(tr);
    var id = row.data().RegisterId;
    $('#txtPreAddress').val(row.data().PremisesAddress);
    $('#txtFullname').val(row.data().Fullname);
    $('#txtPhone').val(row.data().Phone);
    $('#view-reg-data').modal('show');
});

//Get info for modal
function getReg(regId) {
    $("#dllStatus").empty();
    $('#txtRegId').val(regId);
    $("#dllStatus").append($("<option></option>").val(1).html('Duyệt'));
    $("#dllStatus").append($("<option></option>").val(2).html('Đang chờ'));
    $("#dllStatus").append($("<option></option>").val(3).html('Từ chối'));
}
/*$('#updateRegisterInfoButton').click(function () {
    var regId = parseInt($('#txtRegId').val());
    var isConfirm = $('select[id="dllStatus"]').val();
    callAjax({
        type: PUT,
        url: UPDATE_PREMISES_STATUS_URI + regId,
        contentType: JSON_DATATYPE,
        headers: {
            //'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8'
        },
        data: isConfirm,
        success: function () {
            toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
            $('#changeRole').modal('hide');
            $('#userTable').DataTable().ajax.reload();
        },
        error: function () {
            toastr.error('Xin hãy kiểm tra lại', 'Cập nhật thất bại');
        }
    })
});*/

function updateStatus() {
    $('#updateRegisterInfoButton').click(function () {
        var regId = parseInt($('#txtRegId').val());
        var isConfirm = $('select[id="dllStatus"]').val();
        $.ajax({
            type: PUT,
            url: UPDATE_PREMISES_STATUS_URI + regId,
            contentType: JSON_DATATYPE,
            headers: {
                //'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: isConfirm,
            success: function () {
                toastr.success('Cập nhật thông tin hồ sơ thành công', 'Thành Công');
                $('#changeStatus').modal('hide');
                $('#registerInfoTable').DataTable().ajax.reload();
            },
            error: function () {
                toastr.error('Xin hãy kiểm tra lại', 'Cập nhật thất bại');
            }
        })
    })
}
//Create New Info
/*$('#createNewRegisterInfoBtn').click(function () {
    var username = $('#txtUsername').val();
    var fullName = $('#txtFullname').val();
    var email = $('#txtEmail').val();
    var phone = $('#txtPhone').val();
    var premisesName = $('#txtPremisesName').val();
    var premisesAddress = $('#txtPremisesAddress').val();
    var preTypeId = $('select[id="dllPremisesType"]').val();
    callAjax(
        {
            url: CREATE_NEW_PREMISES_URI,
            dataType: JSON_DATATYPE,
            type: POST,
        }, JSON.stringify({
            PremisesName: premisesName,
            PremisesAddress: premisesAddress,
            PremisesTypeId: preTypeId,
            Username: username,
            Fullname: fullName,
            Email: email,
            Phone: phone,
        }),
        function (result) {
            toastr.success('Bạn tạo mới cơ sở thành công. Vui lòng chờ kiểm duyệt', 'Tạo cơ sở thành công');
        },
        function (result) {
            toastr.error(result);
        }
    )
});
*/