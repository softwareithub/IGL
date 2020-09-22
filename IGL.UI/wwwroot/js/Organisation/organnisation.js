var organisation = {
    "Fn_CreateOrgan": function (id) {
        customAjax.Fn_CommanCreate("/Organisation/CreateDetail", "#divCommanModalPartial", "#IGLCommanModal", "Create Organisation Detail", "#divOrganisation")
    },
    "Fn_DeleteOrgan": function (id) {
        customAjax.Fn_CommanDelete(id, "/Organisation/Delete", organisation.Fn_GetOrganDetail, "#divOrganisation")
    },
    "Fn_EditOrgan": function (id) {
        customAjax.Fn_CommanEdit(id, "/Organisation/CreateDetail", "Edit Product Master", "#divOrganisation");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        organisation.Fn_GetOrganDetail();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetOrganDetail": function () {
        customAjax.Fn_CommanGet("/Organisation/GetDetails", "#divOrganisation", "#IGLDataTable")
    }
};

$(document).ready(function () {
    organisation.Fn_GetOrganDetail();
})