var store = {
    "Fn_CreateStore": function (id) {
        customAjax.Fn_CommanCreate("/StoreDetail/CreateStoreDetail", "#divCommanModalPartial", "#IGLCommanModal", "Create Store Details", "#divStoreIndex")
    },
    "Fn_DeleteStore": function (id) {
        customAjax.Fn_CommanDelete(id, "/StoreDetail/DeleteStoreDetail", store.Fn_GetStoredetail, "#divStoreIndex")
    },
    "Fn_EditStore": function (id) {
        customAjax.Fn_CommanEdit(id, "/StoreDetail/CreateStoreDetail", "Edit Store Details", "#divStoreIndex");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        store.Fn_GetStoredetail();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetStoredetail": function () {
        customAjax.Fn_CommanGet("/StoreDetail/GetStoreDetails", "#divStoreIndex", "#IGLDataTable")
    }
};

$(document).ready(function () {
    store.Fn_GetStoredetail();
})


