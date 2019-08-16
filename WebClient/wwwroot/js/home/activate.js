$(document).ready(function () {

    var activationCode = GetURLParameter('ActivationCode');
    ActivateAccount(activationCode);
})

function GetURLParameter(sParam){
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++)
    {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam)
        {
            return sParameterName[1];
        }
    }
}

function ActivateAccount(activationCode) {
    $.ajax({
        url: ACTIVATE_URI + activationCode,
        method: PUT,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8',

        },
    });
}