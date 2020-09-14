
var customAjax = {
    "Fn_Success": function (response) {
        var Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000
        });
        Toast.fire({
            icon: 'success',
            title: response
        })
    },
    "Fn_Error": function (response) {
        var Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000
        });
        Toast.fire({
            icon: 'error',
            title: response
        })
        setTimeout(function () {
            customAjax.Fn_ReloadPage();
        }, 3000);
    },
    "Fn_SubmitBegin": function () {
        $("#divCreate").addClass('ajaxLoading');
    },
    "Fn_SubmitComplete": function () {
        $("#divCreate").removeClass('ajaxLoading');
    },
    "Fn_SubmitSuccess": function (response) {
        customAjax.Fn_Success(response);
        $("#form")[0].reset();
    },
    "Fn_ReloadPage": function () {
        window.location.reload();
    },
    "Fn_CommanDelete": function (id, deleteUrl, propData) {
        Swal.fire({
            title: 'Are you sure want to delete ' + propData+'? ',
            text: "Deleted data will be archived ,You can enable it by the help of Admin !",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.get(deleteUrl, { id, id }, function (response) {
                    Swal.fire({
                        title: 'Deleted',
                        text: response,
                        icon: 'success',
                        onClose: () => {
                            location.reload();
                        }
                    })
                });
            }
        })
    },
    "Fn_CommanEdit": function (id, geturl,headerText) {
        $.get(geturl, { id, id }, function (data) {
            $("#modalTitle").text(headerText);
            $("#divCommanModalPartial").html(data);
            $("#IGLCommanModal").modal('show');
        });
    }

};