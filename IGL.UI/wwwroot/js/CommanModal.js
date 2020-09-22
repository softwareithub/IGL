var modalPop = {
    "fn_close": function (id) {
        if (confirm("Are you sure want to Close the Pop Up")) {
            $("#" + id).modal('hide');
        }
    }
}