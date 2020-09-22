var unitMaster = {
    "Fn_UpsertUnitMaster": function (id) {
        customAjax.Fn_CommanCreate("/UnitMaster/CreateUnit", "#divCommanModalPartial", "#IGLCommanModal", "Create Unit Master", "#divUnitList")
    },
    "Fn_DeleteUnitMaster": function (id) {
        customAjax.Fn_CommanDelete(id, "/UnitMaster/Delete", unitMaster.Fn_GetUnitDetail, "#divUnitList")
    },
    "Fn_EditUnitMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/UnitMaster/CreateUnit", "Edit Unit Master","#divUnitList");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        unitMaster.Fn_GetUnitDetail();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetUnitDetail": function () {
        customAjax.Fn_CommanGet("/UnitMaster/GetUnitList", "#divUnitList", "#IGLDataTable")
    }
};

$(document).ready(function () {
    unitMaster.Fn_GetUnitDetail();
})


