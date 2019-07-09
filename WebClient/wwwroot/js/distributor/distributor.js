

$(document).ready(function () {
    getProduct();

})

function getProduct() {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:4201/api/Distributor/getProductMatched/4',
        dataType: 'JSON',
        success: function (data) {
            console.log(data);
            $('.dataex-html5-background').DataTable({
                data: data,
                ordering: false,
                columns: [
                    { data: 'FoodId' },
                    { data: 'Breed' },
                    {
                        data: 'CreatedDate',
                        render: function (data, type, row) {
                            var d = new Date(data);
                            return d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
                        }
                    },
                    { data: 'Provider.Name' },
                    {
                        data: 'FoodId',
                        render: function (data) {
                            return '<button class="btn btn-grey" style="width: 25%" data-target="#moreInfo" data-toggle="modal">Chi tiết<i class="icon-eye"></i></button><button class="btn btn-info" style="width: 50%" data-target="#AddMoreInfo" data-toggle="modal">Thêm thông tin<i class="icon-pencil"></i></button><button class="btn btn-success" style="width: 25%" data-target="#GetQRCode" onclick="makeCode(' + data + ')" data-toggle="modal">Mã QR</button>';
                        }
                    }
                ],
            });
        },
        dom: 'Bfrtip'
    });
}

//var qrcode = new QRCode(document.getElementById("qrcode"));
function makeCode(id) {
    //qrcode.clear();
    //qrcode.makeCode(id + "");
    //JsBarcode("#barcode", "abc" , {
    //    format: "pharmacode",
    //    lineColor: "#0aa",
    //    width: 4,
    //    height: 80,
    //    displayValue: false
    //});
    JsBarcode("#barcode", "" + id);
}

var myCustomScrollbar = document.querySelector('.my-custom-scrollbar');
var ps = new PerfectScrollbar(myCustomScrollbar);

var scrollbarY = myCustomScrollbar.querySelector('.ps.ps--active-y>.ps__scrollbar-y-rail');

myCustomScrollbar.onscroll = function () {
    scrollbarY.style.cssText = `top: ${this.scrollTop}px!important; height: 400px; right: ${-this.scrollLeft}px`;
}

function exportPfd() {
    var doc = new jsPDF();
    doc.fromHTML($('#printJs-form').get(0), 20, 20, {
        'width': 500
    });
    doc.save('barcode.pdf');
}

