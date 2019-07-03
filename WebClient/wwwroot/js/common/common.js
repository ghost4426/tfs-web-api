function callAjaxAuth(input) {
    $.ajax({
        url: input.url,
        data: input.data,
        type: input.type,
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('token') },
        success: function (result) {
            if (input.success) {
                input.success(result);
            }
        },
        error: function (error) {
            if (input.error) {
                input.error(error);
            }
        }
    })
}

