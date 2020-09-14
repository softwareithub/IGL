var customer = {
    "Fn_UpsertCustomerMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/Customer/CreateCustomer","Create Material Master");
    },
    "Fn_DeleteCustomerMaster": function (id, propData) {
        customAjax.Fn_CommanDelete(id, "/Customer/Delete", propData);
    },
    "Fn_EditCustomerMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/Customer/CreateCustomer","Edit Material Master");
    }

};