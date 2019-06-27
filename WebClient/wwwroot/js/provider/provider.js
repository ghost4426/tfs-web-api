$(document).ready(function () {
    getProduct();
    loadCategory();
    insertProduct();
});

function getProduct() {
    $.ajax({
        type: 'get',
        url: 'https://localhost:4201/api/Product/testgetByProvider',
        dataType: 'json',
        success: function (data) {
            $('.file-export').DataTable({
                data: data,
                ordering: false,
                destroy: true,
                columns: [
                    { data: 'Name' },
                    { data: 'Categories.Name' },
                    {
                        data: 'CreatedDate',
                        render: function (data, type, row) {
                            var d = new Date(data);
                            return d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
                        }
                    },
                    {
                        data: null,
                        render: function (o) {
                            return '<button class="btn btn-grey" data-toggle="modal" data-target="#getinfo" title="Chi tiết"><i class="icon-eye"></i ></button >&nbsp;<button class="btn btn-info" data-toggle="modal" data-target="#addinfo" title="Thêm thông tin"><i class="icon-pencil"></i></button>&nbsp;<button class="btn btn-success" data-toggle="modal" data-target="#addDistributor" title="Bán sản phẩm"><i class="icon-basket"></i></button>';
                        }
                    }
                ],
                dom: 'Bfrtip',
                buttons: [
                    {
                        text: 'Thêm mới',
                        className: 'btn-addNew'
                    },
                    {
                        extend: 'excel',
                        text: 'Tải báo cáo'
                    }
                ],
                language: {
                    "decimal": "",
                    "emptyTable": "Không có giá trị",
                    "info": "Hiển thị _START_ đến _END_ trong tổng số _TOTAL_ sản phẩm",
                    "infoEmpty": "Hiển thị 0 đến 0 trong tổng số 0 sản phẩm",
                    "infoFiltered": "(Lọc từ _MAX_ tổng sản phẩm)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "loadingRecords": "Loading...",
                    "processing": "Đang tiến hành...",
                    "search": "Tìm kiếm:",
                    "zeroRecords": "Không tìm thấy sản phẩm phù hợp",
                    "paginate": {
                        "first": "Đầu",
                        "last": "Cuối",
                        "next": "Sau",
                        "previous": "Trước"
                    },
                    "aria": {
                        "sortAscending": ": Lọc từ thấp đến cao",
                        "sortDescending": ": Lọc từ cao đến thấp"
                    }
                }
            });
            $('.buttons-excel, .btn-addNew').addClass('btn btn-primary mr-1');
            $('.btn-addNew').attr({ 'data-toggle': 'modal', 'data-target': "#default" });
        }
    });
}

function loadCategory() {
    $.ajax({
        type: 'get',
        url: 'https://localhost:4201/api/Product/getAllCategory',
        dataType: 'json',
        success: function (data) {
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += "<option value='" + data[i].Id + "'>" + data[i].Name + "</option>";
            }
            document.getElementById("NewCategory").innerHTML = option;
        }
    });
}
function insertProduct() {
    $('#btnAddProduct').click(function () {
        var name = $('input[name="NewName"]').val();
        var cate = parseInt($('select[name="NewCategory"]').val());
        $.ajax({
            type: 'post',
            url: 'https://localhost:4201/api/Product/createProduct',
            dataType: 'json',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: JSON.stringify({
                Name: name,
                CategoriesId: cate,
                ProviderUserId: 3
            }),
            success: function (data) {
                toastr.success('Bạn đã thêm ' + name + ' vào danh sách sản phẩm', 'Thêm thành công');
                getProduct();
                $('#default').modal('hide');
                $('input[name="NewName"]').val("");
                $('select[name="NewCategory"]').val("1");
            }
        })
    });
}