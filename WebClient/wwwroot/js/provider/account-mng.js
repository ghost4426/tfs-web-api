var providerAccountTable = $('#provider-account-mng').DataTable({
    ajax: {
        url: MANAGER_GET_ACCOUNT_URI,
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
    ordering: false,
    columns: [
        { data: 'UserId', width: '10%' },
        { data: 'Fullname', width: '25%' },
        { data: 'Email', width: '25%' },
        { data: 'PhoneNo', width: '15%' },
        {
            data: function (data, type, dataToSet) {
                if (data.IsActive) {
                    return "<span class='badge badge-glow badge-pill badge-success'>Kích hoạt</span>";
                } else {
                    return "<span class='badge badge-glow badge-pill badge-danger'>Không kích hoạt</span>";
                }
            }, width: '15%'
        },
        {
            data: function (data, type, dataToSet) {
                if (data.IsActive) {
                    return '<center><button class="btn btn-danger btn-deactivate" title="Hủy kích hoạt"><i class="fa fa-times"></i></button></center>';
                } else {
                    return '<center><button class="btn btn-success btn-activate" title="Kích hoạt"><i class="fa fa-check"></i></button></center>';
                }
            }, width: '10%'
        }
    ],
    dom: '<"row" <"col-sm-12"Bf>>'
        + '<"row" <"col-sm-12"i>>'
        + '<"row" <"col-sm-12"tr>>'
        + '<"row"<"col-sm-5"l><"col-sm-7"p>>',
    buttons: [],
    language: table_vi_lang
});

$('#provider-account-mng').on('click', 'button.btn-deactivate', function () {
    var tr = $(this).closest('tr');
    var row = providerAccountTable.row(tr);
    var id = row.data().UserId;
    swal({
        title: "Hủy kích hoạt?",
        text: "Tài khoản này sẽ bị hủy kích hoạt!",
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
                    url: MANAGER_UPDATE_ACCOUNT_STATUS_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Xóa thành công!", "Tài khoản đã được hủy kích hoạt.", "success");
                    $("#provider-account-mng").DataTable().ajax.reload();
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

$('#provider-account-mng').on('click', 'button.btn-activate', function () {
    var tr = $(this).closest('tr');
    var row = providerAccountTable.row(tr);
    var id = row.data().UserId;
    swal({
        title: "Kích hoạt?",
        text: "Tài khoản này sẽ được kích hoạt!",
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
                    url: MANAGER_UPDATE_ACCOUNT_STATUS_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Xóa thành công!", "Tài khoản đã được kích hoạt.", "success");
                    $("#provider-account-mng").DataTable().ajax.reload();
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