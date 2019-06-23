$(document).ready(function () {
    testAPI();
});

function testAPI() {
    var request = new XMLHttpRequest();

    request.open('GET', 'https://localhost:4201/getByProvider?providerID=2', true);
    request.onload = function () {
        var data = JSON.parse(this.response);

        if (request.status >= 200 && request.status < 400) {
            console.log(data);
        } else {
            console.log("error");
        }
    }
    request.send();
}