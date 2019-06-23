$(document).ready(function () {
    getProduct();
    loadCategory();
    insertProduct();
});

function getProduct() {
    $.ajax({
        type: 'get',
        url: 'https://localhost:4201/testgetByProvider',
        dataType: 'json',
        success: function (data) {
            $('.dataex-html5-background').DataTable({
                data: data,
                ordering: false,
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
                            return '<button class="btn btn-grey" data-toggle="modal" data-target="#getinfo" title="Chi tiết"><i class="icon-eye"></i ></button >&nbsp;<button class="btn btn-info" data-toggle="modal" data-target="#addinfo" title="Thêm thông tin"><i class="icon-pencil"></i></button>';
                        }
                    }
                ],
                dom: 'Bfrtip',
                buttons: [{
                    extend: 'excelHtml5',
                    customize: function (xlsx) {
                        var sheet = xlsx.xl.worksheets['sheet1.xml'];

                        // Loop over the cells in column `F`
                        $('row c[r^="F"]', sheet).each(function () {
                            // Get the value and strip the non numeric characters
                            if ($('is t', this).text().replace(/[^\d]/g, '') * 1 >= 500000) {
                                $(this).attr('s', '20');
                            }
                        });
                    }
                }]
            });
        }
    });
}

function loadCategory() {
    $.ajax({
        type: 'get',
        url: 'https://localhost:4201/getAllCategory',
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
        console.log(cate);
        $.ajax({
            type: 'post',
            url: 'https://localhost:4201/createProduct',
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
                location.reload();
            }
        })
    });
}