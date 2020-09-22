var roleMaster = {
    "Fn_UpsertRoleMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/RoleMaster/CreateRole", "Create Role Master");
    },
    "Fn_DeleteRoleMaster": function (id, propData) {
        customAjax.Fn_CommanDelete(id, "/RoleMaster/Delete", propData);
    },
    "Fn_EditRoleMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/RoleMaster/CreateRole", "Edit Role Master");
    }

};