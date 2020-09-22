var customerDetail = {
    "Fn_CreateCustomer": function (id) {
        customAjax.Fn_CommanCreate("/Customer/CreateCustomer", "#divCommanModalPartial", "#IGLCommanModal", "Create Customer Detail", "#divCustomerIndex")
    },
    "Fn_DeleteCustomer": function (id) {
        customAjax.Fn_CommanDelete(id, "/Customer/Delete", customerDetail.Fn_GetCustomerDetails, "#divCustomerIndex")
    },
    "Fn_EditCustomer": function (id) {
        customAjax.Fn_CommanEdit(id, "/Customer/CreateCustomer", "Edit Customer Detail", "#divCustomerIndex");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        customerDetail.Fn_GetCustomerDetails();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetCustomerDetails": function () {
        customAjax.Fn_CommanGet("/Customer/GetCustomerList", "#divCustomerIndex", "#IGLDataTable")
    }
};

$(document).ready(function () {
    customerDetail.Fn_GetCustomerDetails();
})


