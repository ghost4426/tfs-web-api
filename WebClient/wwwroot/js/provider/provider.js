$(document).ready(function () {
    testAPI();
});

function testAPI() {
    var request = new XMLHttpRequest();

    request.open('GET', 'http://localhost/api/food', true);
    request.onload = function () {
        var data = JSON.parse(this.response);

        if (request.status >= 200 && request.status < 400) {
            console.log(data[0].id);
        } else {
            console.log("error");
        }
    }
    request.send();
}