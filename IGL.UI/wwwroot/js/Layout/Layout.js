var layout = {
    "fn_changePassword": function () {
        $.get("/Account/ChangePassword", function (data) {
            $("#divChangePasswordPartial").html(data);
            $("#IGLChangePassword").modal('show');
        });
    },
    "Fn_Success": function (response) {
        //alertify.success(response);
        layout.Fn_CommanAlert(response,'IGL Alert !')
    },
    "Fn_MasterSearch": function () {
        $("#IGLMasterSearch").modal('show');
    },
    "Fn_VendorSearch": function () {
        location.href = "/VendorMaster/GetVendorList?vendorName=" + $("#txtVendorSearch").val();
    },
    "Fn_CommanAlert": function (response, title) {
        jQuery.alert({
            title: title,
            buttons: {
                ok: {
                    text: 'Ok',
                    btnClass: 'btn-blue',
                    keys: ['enter'],
                }
            },
            content: response,
            draggable: false
        });
    },

};