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
$('#createNewRegisterInfoBtn').click(function () {
    var username = $('#txtUsername').val();
    var fullName = $('#txtFullname').val();
    var email = $('#txtEmail').val();
    var phone = $('#txtPhone').val();
    var premisesName = $('#txtPremisesName').val();
    var premisesAddress = $('#txtPremisesAddress').val();
    var preTypeId = $('select[id="dllPremisesType"]').val();
    callAjax(
        {
            url: CREATE_NEW_PREMISES_URI,
            dataType: JSON_DATATYPE,
            type: POST,
        }, JSON.stringify({
            PremisesName: premisesName,
            PremisesAddress: premisesAddress,
            PremisesTypeId: preTypeId,
            Username: username,
            Fullname: fullName,
            Email: email,
            Phone: phone,
        }),
        function (result) {
            toastr.success('Bạn tạo mới cơ sở thành công. Vui lòng chờ kiểm duyệt', 'Tạo cơ sở thành công');
        },
        function (result) {
            toastr.error(result);
        }
    )
});