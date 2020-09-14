var unitMaster = {
    "Fn_UpsertUnitMaster": function (id) {
        $.get("/UnitMaster/CreateUnit", { id:id }, function (response) {
            $("#divCommanModalPartial").html(response)
        }).done(function () {
            $("#IGLCommanModal").modal('show');
            $("#modalTitle").text("Create Unit Master");
        })
    },
    "Fn_EditUnitMaster": function (id) {

    },
    "Fn_DeleteUnitMaster": function (id, propData) {
        customAjax.Fn_CommanDelete(id, "/UnitMaster/Delete", propData);
    },
    "Fn_EditUnitMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/UnitMaster/CreateUnit");
    }
};


