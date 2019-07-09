﻿$(document).ready(function () {
    getProduct();
    loadCategory();
    insertProduct();
    loadProvider();
    insertProvider();
});

function getProduct() {
    $.ajax({
        type: 'get',
        url: 'https://localhost:4201/api/Farmer/getByFarmer',
        dataType: 'json',
        success: function (data) {
            $('.file-export').DataTable({
                data: data,
                ordering: false,
                destroy: true,
                responsive: true,
                columns: [
                    { data: 'Categories.Name' },
                    { data : 'Breed'},
                    {
                        data: 'CreatedDate',
                        render: function (data, type, row) {
                            var d = new Date(data);
                            return d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
                        }
                    },
                    {
                        data: 'FoodId',
                        render: function (data, type, row) {
                            return '<button class="btn btn-grey" data-toggle="modal" data-target="#getinfo" title="Chi tiết"><i class="icon-eye"></i ></button >&nbsp;<button class="btn btn-info" data-toggle="modal" data-target="#addinfo" title="Thêm thông tin"><i class="icon-pencil"></i></button>&nbsp;<button class="btn btn-success" onclick="addProvider(' + data + ')" data-toggle="modal" data-target="#addDistributor" title="Bán sản phẩm"><i class="icon-basket"></i></button>';
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
        url: 'https://localhost:4201/api/Farmer/getAllCategory',
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
        var cate = parseInt($('select[name="NewCategory"]').val());
        var breed = $('input[name="Breed"]').val();
        $.ajax({
            type: 'post',
            url: 'https://localhost:4201/api/Farmer/createFood',
            dataType: 'json',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: JSON.stringify({
                CategoriesId: cate,
                Breed: breed,
                FarmerId: 2
            }),
            success: function (data) {
                toastr.success('Bạn đã thêm ' + name + ' vào danh sách sản phẩm', 'Thêm thành công');
                getProduct();
                $('#default').modal('hide');
                $('select[name="NewCategory"]').val("1");
                $('input[name="Breed"]').val("");
            }
        })
    });
}

function loadProvider() {
    $('.select2-placeholder').css('width', '100%');
    $(".select2-placeholder").select2({
        placeholder: "Chọn nhà cung cấp",
        allowClear: true
    });
    $.ajax({
        type: 'get',
        url: 'https://localhost:4201/api/Farmer/getAllProvider',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += "<option value='" + data[i].PremisesId + "'>" + data[i].Name + "</option>";
            }
            document.getElementById("single-placeholder").innerHTML = option;
        }
    });
}

function addProvider(foodId) {
    $('input[name="foodId"]').val(foodId);
}

function insertProvider() {
    $('#btn-addProvider').click(function () {
        var providerId = $('#single-placeholder').val();
        var foodId = $('input[name="foodId"]').val();
        $.ajax({
            type: 'post',
            url: 'https://localhost:4201/api/Farmer/createTransaction',
            dataType: 'json',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: JSON.stringify({
                FarmerId: 2,
                ProviderId: providerId,
                FoodId: foodId,
                StatusId: 1
            }),
            success: function (data) {
                toastr.success('Sản phẩm đang được giao dịch, vui lòng chờ nhà cung cấp xác nhận', 'Giao dịch thành công');
                getProduct();
                $('#addDistributor').modal('hide');
                $('select[name="providerID"]').val("1");
            }
        })
    });    
}