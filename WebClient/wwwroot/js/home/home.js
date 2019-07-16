$(document).ready(function () {
    // count transaction
    var count = callAjax(
        {
            url: COUNT_TRANSACTION_URI,
            dataType: JSON_DATATYPE,
            type: GET,
        }, "",
        function (result) {
            $("#count-trans").html(result);
        }
    )    
});

$("#view-trans").click(function () {
    window.location.href = '/Transaction';
});