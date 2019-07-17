$(document).ready(function () {
   
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