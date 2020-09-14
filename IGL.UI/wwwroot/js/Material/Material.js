var material = {
    "Fn_UpsertMaterialMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/MaterialMaster/CreateMaterial");
    },
    "Fn_DeleteMaterialMaster": function (id, propData) {
        debugger;
        customAjax.Fn_CommanDelete(id, "/MaterialMaster/Delete", propData);
    },
    "Fn_EditMaterialMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/MaterialMaster/CreateMaterial");
    }

};