$(document).ready(function () {
    getProduct();
    
})

function getProduct() {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:4201/api/Staff/getProductMatched/1',
        dataType: 'JSON',
        success: function (data) {
            console.log(data);
            $('.dataex-html5-background').DataTable({
                data: data,
                ordering: false,
                columns: [
                    { data: 'Id' },
                    { data: 'Name' },
                    {
                        data: 'CreatedDate',
                        render: function (data, type, row) {
                            var d = new Date(data);
                            return d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
                        }
                    },
                    { data: 'Provider.Fullname' },
                    {
                        data: null,
                        render: function (o) {
                            return '<button class="btn btn-grey" style="width: 50%" data-target="#moreInfo" data-toggle="modal">Thông tin chi tiết<i class="icon-eye"></i></button><button class="btn btn-info" style="width: 50%" data-target="#AddMoreInfo" data-toggle="modal">Thêm thông tin<i class="icon-pencil"></i></button>';
                        }
                    }
                ],
            });
        },
        dom: 'Bfrtip'
    });
}

