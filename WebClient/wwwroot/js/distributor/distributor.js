$(document).ready(function () {
    function getProduct() {
        $.ajax({
            type: 'get',
            url: 'https://localhost:4201/',
            dataType: 'json',


        }).done(function (data) {
            $('.dataex-html5-background').DataTable({
                data: data,
                ordering: false,
                columns: [
                    { data: 'Id' },
                    { data: 'Name' },
                    { data: 'Categories.Name' },
                    { data: 'Provider.Fullname' }
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
                            return '<button class="btn btn-grey" style="font-size:10px">Thông tin chi tiết<i class="icon-eye"></i></button>< button class="btn btn-info" style = "font-size:10px" > Thêm thông tin < i class="icon-pencil" ></i ></button >';
                        }
                    }
                ],
            });
        });
    }

});

