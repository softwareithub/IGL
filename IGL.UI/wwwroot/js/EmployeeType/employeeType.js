var employeeType = {
    "Fn_CreateEmployeeType": function (id) {
        customAjax.Fn_CommanCreate("/EmployeeType/UpSertEmployeeType", "#divCommanModalPartial", "#IGLCommanModal", "Create Employee Type", "#divEmployeeType")
    },
    "Fn_DeleteEmployeeType": function (id) {
        customAjax.Fn_CommanDelete(id, "/EmployeeType/Delete", employeeType.Fn_GetEmployeeTypeDetails, "#divEmployeeType")
    },
    "Fn_EditEmployeeType": function (id) {
        customAjax.Fn_CommanEdit(id, "/EmployeeType/UpSertEmployeeType", "Edit Employee Type Master", "#divEmployeeType");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        employeeType.Fn_GetEmployeeTypeDetails();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetEmployeeTypeDetails": function () {
        customAjax.Fn_CommanGet("/EmployeeType/GetEmployeeTypeDetails", "#divEmployeeType   ", "#IGLDataTable")
    }
};

$(document).ready(function () {
    employeeType.Fn_GetEmployeeTypeDetails();
})


