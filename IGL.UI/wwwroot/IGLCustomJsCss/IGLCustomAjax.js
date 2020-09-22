var customAjax = {
    "Fn_Success": function (response) {
        alertify.success(response);
    },
    "Fn_Error": function (response) {
        alertify.error(response);
    },
    "Fn_SubmitBegin": function (name) {
        $("#"+name.id).addClass('ajaxLoading');
    },
    "Fn_SubmitComplete": function (name) {
        $("#"+ name.id).removeClass('ajaxLoading');
    },
    "Fn_SubmitSuccess": function (response) {
        customAjax.Fn_Success(response);
        $("#form")[0].reset();
    },
    "Fn_CommanDelete": function (id, deleteUrl, callback,loadingDiv) {
        $(loadingDiv).addClass('ajaxLoading');
        alertify.confirm('IGL Confirm', 'Are you sure to delete?', function () {
            $.get(deleteUrl, { id: id }, function (response) {
                alertify.success(response);
            }).done(function () { callback(); $(loadingDiv).removeClass('ajaxLoading'); });
        }, function () {
            alertify.error("Delete record canceled.")
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
    }

};