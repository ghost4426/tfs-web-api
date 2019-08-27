$(document).ready(function () {

});
//Lấy thông tin hồ sơ
var premisesTable = $('#premisesTable').DataTable({
    ajax: {
        url: GET_PREMISES_URI,
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
        { data: 'Name' },
        {
            data: 'TypeName',
            render: function (data, type, row) {
                if (data == "Farm") {
                    return "Nông trại";
                } else if (data == "Provider") {
                    return "Nhà cung cấp";
                } else if (data == "Distributor") {
                    return "Nhà phân phối";
                }
            }
        },
        { data: 'Address' },
        {
            data: 'IsActive',
            render: function (data, type, row) {
                if (data == true) {
                    return "<span class='badge badge-glow badge-pill badge-success'>Kích hoạt</span>";
                }
                else {
                    return "<span class='badge badge-glow badge-pill badge-danger'>Vô hiệu hóa</span>";
                };
            }
        },
        {
            data: function (data, type, dataToSet) {
                if (data.IsActive) {
                    return '<center><button class="btn btn-danger btn-deactivate" title="Hủy kích hoạt"><i class="fa fa-times"></i></button></center>';
                } else {
                    return '<center><button class="btn btn-success btn-activate" title="Kích hoạt"><i class="fa fa-check"></i></button></center>';
                }
            }
        }
    ],
    language: registerInfoTable_vi_lang
});

$('#premisesTable').on('click', 'button.btn-deactivate', function () {
    var tr = $(this).closest('tr');
    var row = premisesTable.row(tr);
    var id = row.data().PremisesId;
    swal({
        title: "Hủy kích hoạt?",
        text: "Cơ sở này sẽ bị hủy kích hoạt!",
        icon: "warning",
        showCancelButton: true,
        buttons: {
            cancel: {
                text: "Không",
                visible: true,
                className: "btn-warning",
                closeModal: false,
            },
            confirm: {
                text: "Xác nhận!",
                visible: true,
                className: "",
                closeModal: false
            }
        }
    }).then(isConfirm => {
        if (isConfirm) {
            callAjaxAuth(
                {
                    url: UPDATE_PREMISES_STATUS_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Hủy thành công!", "Cơ sở đã được hủy kích hoạt.", "success");
                    $("#premisesTable").DataTable().ajax.reload();
                },
                function (result) {
                    toastr.error(result);
                }
            );
        } else {
            swal("", "Bạn đã hủy hành động này", "error");
        }
    });
});

$('#premisesTable').on('click', 'button.btn-activate', function () {
    var tr = $(this).closest('tr');
    var row = premisesTable.row(tr);
    var id = row.data().PremisesId;
    swal({
        title: "Kích hoạt?",
        text: "Cơ sở này sẽ được kích hoạt!",
        icon: "warning",
        showCancelButton: true,
        buttons: {
            cancel: {
                text: "Không",
                visible: true,
                className: "btn-warning",
                closeModal: false,
            },
            confirm: {
                text: "Xác nhận!",
                visible: true,
                className: "",
                closeModal: false
            }
        }
    }).then(isConfirm => {
        if (isConfirm) {
            callAjaxAuth(
                {
                    url: UPDATE_PREMISES_STATUS_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Kích hoạt thành công!", "Cơ sở đã được kích hoạt.", "success");
                    $("#premisesTable").DataTable().ajax.reload();
                },
                function (result) {
                    toastr.error(result);
                }
            );
        } else {
            swal("", "Bạn đã hủy hành động này", "error");
        }
    });
});