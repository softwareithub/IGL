var riserDetail = {
    "Fn_CreateRiserInfo": function (id) {
        customAjax.Fn_CommanCreate("/RiserMaster/CreateRiser", "#divCommanModalPartial", "#IGLCommanModal", "Create Riser Detail", "#divRiserIndex")
    },
    "Fn_DeleteRiserDetail": function (id) {
        customAjax.Fn_CommanDelete(id, "/RiserMaster/Delete", riserDetail.Fn_GetRiserList, "#divRiserIndex")
    },
    "Fn_EditRiserDetail": function (id) {
        customAjax.Fn_CommanEdit(id, "/RiserMaster/CreateRiser", "Riser Detail", "#divRiserIndex");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        riserDetail.Fn_GetRiserList();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetRiserList": function () {
        customAjax.Fn_CommanGet("/RiserMaster/GetRiserList", "#divRiserIndex", "#IGLDataTable")
    }
};

$(document).ready(function () {
    riserDetail.Fn_GetRiserList();
})


