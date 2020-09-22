var employeeDetail = {
    "Fn_CreateEmployee": function (id) {
        customAjax.Fn_CommanCreate("/EmployeeDetail/UpsertEmployeeDetail", "#divCommanModalPartial", "#IGLCommanModal", "Create Employee Detail", "#divEmployeeDetail")
    },
    "Fn_DeleteEmployee": function (id) {
        customAjax.Fn_CommanDelete(id, "/EmployeeDetail/Delete", employeeDetail.Fn_GetEmployeeDetails, "#divEmployeeDetail")
    },
    "Fn_EditEmployee": function (id) {
        customAjax.Fn_CommanEdit(id, "/EmployeeDetail/UpsertEmployeeDetail", "Edit Employee Detail", "#divEmployeeDetail");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        employeeDetail.Fn_GetEmployeeDetails();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetEmployeeDetails": function () {
        customAjax.Fn_CommanGet("/EmployeeDetail/GetEmployeeDetails", "#divEmployeeDetail   ", "#IGLDataTable")
    }
};

$(document).ready(function () {
    employeeDetail.Fn_GetEmployeeDetails();
})


