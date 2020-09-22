var userDetail = {
    "Fn_UpsertUserDetail": function (id) {
        customAjax.Fn_CommanCreate("/UserDetail/UpsertUserDetail", "#divCommanModalPartial", "#IGLCommanModal", "Create User Detail", "#divUserDetailIndex")
    },
    "Fn_DeleteUserDetail": function (id) {
        customAjax.Fn_CommanDelete(id, "/UserDetail/Delete", userDetail.Fn_GetUserDetail, "#divUserDetailIndex")
    },
    "Fn_EditUserDetail": function (id) {
        customAjax.Fn_CommanEdit(id, "/UserDetail/UpsertUserDetail", "Edit User Detail", "#divUserDetailIndex");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        userDetail.Fn_GetUserDetail();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetUserDetail": function () {
        customAjax.Fn_CommanGet("/UserDetail/GetUserDetails", "#divUserDetailIndex", "#IGLDataTable")
    }
};

$(document).ready(function () {
    userDetail.Fn_GetUserDetail();
})


