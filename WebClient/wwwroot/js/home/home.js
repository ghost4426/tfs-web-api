$(document).ready(function () {
    // count farm transaction
    var count = callAjax(
        {
            url: COUNT_FARM_TRANSACTION_URI,
            dataType: JSON_DATATYPE,
            type: GET,
        }, "",
        function (result) {
            $("#count-farm-trans").html(result);
        }
    ) 

    // count provider transaction
    var count = callAjax(
        {
            url: COUNT_PROVIDER_TRANSACTION_URI,
            dataType: JSON_DATATYPE,
            type: GET,
        }, "",
        function (result) {
            $("#count-provider-trans").html(result);
        }
    )
});

$("#view-farm-trans").click(function () {
    window.location.href = '/nong-trai/quan-li-giao-dich';
});

$("#view-provider-trans").click(function () {
    window.location.href = '/nha-cung-cap/quan-li-giao-dich';
});