var layout = {
    "fn_changePassword": function () {
        $.get("/Account/ChangePassword", function (data) {
            $("#divChangePasswordPartial").html(data);
            $("#IGLChangePassword").modal('show');
        });
    },
    "Fn_Success": function (response) {
        alertify.success(response);
    },
    "Fn_MasterSearch": function () {
        $("#IGLMasterSearch").modal('show');
    }

};