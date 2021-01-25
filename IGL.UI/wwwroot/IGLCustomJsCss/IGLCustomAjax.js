var customAjax = {
    "Fn_Success": function (response) {
        // alertify.success(response);
        customAjax.Fn_CommanAlert(response, 'IGL Success Alert !');
    },
    "Fn_Error": function (response) {
        // alertify.error(response);
        customAjax.Fn_CommanAlert(response, 'IGL Error Alert !');
    },
    "Fn_SubmitBegin": function (name) {
        $("#" + name.id).addClass('ajaxLoading');
    },
    "Fn_SubmitComplete": function (name) {
        $("#" + name.id).removeClass('ajaxLoading');
    },
    "Fn_SubmitSuccess": function (response) {
        customAjax.Fn_Success(response);
        $("#form")[0].reset();
    },
    "Fn_CommanDelete": function (id, deleteUrl, callback, loadingDiv) {
        $(loadingDiv).addClass('ajaxLoading');

        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure want to Delete !',
            buttons: {
                Ok: {
                    btnClass: 'btn-success',
                    text:'ok',
                    action: function () {
                        $.get(deleteUrl, { id: id }, function (response) {
                            alertify.success(response);
                        }).done(function () { callback(); $(loadingDiv).removeClass('ajaxLoading'); });
                    }
                },
                Cancel: {
                    text: 'Cancel',
                    btnClass:'btn-danger',
                    action: function () {
                        $(loadingDiv).removeClass('ajaxLoading');
                    }
                }
            }
        });
    },
    "Fn_CommanEdit": function (id, geturl, headerText, loadingDiv) {
        $(loadingDiv).addClass('ajaxLoading');
        $.get(geturl, { id, id }, function (data) {
            $("#modalTitle").text(headerText);
            $("#divCommanModalPartial").html(data);
            $("#IGLCommanModal").modal('show');

        }).done(function () {
            $(loadingDiv).removeClass('ajaxLoading');
        });
    },
    "Fn_CommanGet": function (getUrl, bindingDivId, bindingTableId) {
        $.get(getUrl, function (response) {
            debugger;
            $(bindingDivId).html(response)
        }).done(function () {
            $(bindingTableId).DataTable({
                "responsive": true,
                "autoWidth": false,
                "scrollX": true,
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ]
            });
        });
    },
    "Fn_CommanCreate": function (getUrl, dataBindingId, modalPopId, modalText, loadingDiv) {
        $(loadingDiv).addClass('ajaxLoading');
        $.get(getUrl, function (data) {
            $(dataBindingId).html(data);
        }).done(function () {
            $(modalPopId).modal('show');
            $("#modalTitle").text(modalText);
            $(loadingDiv).removeClass('ajaxLoading');
        })
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