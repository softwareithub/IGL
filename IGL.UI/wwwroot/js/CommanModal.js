var modalPop = {
    "fn_close": function (id) {

        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure want Leave, and close the PopUp.',
            buttons: {
                Ok: {
                    title: 'Ok',
                    btnClass:'btn-success',
                    action: function () {
                        $("#" + id).modal('hide');
                    },
                    btnClass: 'btn-primary',
                  
                },
                Cancel: {
                    text: 'Cancel',
                    btnClass: 'btn-warning',
                    action: function () {
                    }
                }
            }
        });
    }
}