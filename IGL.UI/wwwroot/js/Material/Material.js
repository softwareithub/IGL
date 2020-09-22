var productDetail = {
    "Fn_CreateProduct": function (id) {
        customAjax.Fn_CommanCreate("/Materialmaster/CreateMaterial", "#divCommanModalPartial", "#IGLCommanModal", "Create Product Master", "#divProductDetail")
    },
    "Fn_DeleteProduct": function (id) {
        customAjax.Fn_CommanDelete(id, "/Materialmaster/Delete", productDetail.Fn_GetProductDetail, "#divProductDetail")
    },
    "Fn_EditProductMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/Materialmaster/CreateMaterial", "Edit Product Master", "#divProductDetail");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        productDetail.Fn_GetProductDetail();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetProductDetail": function () {
        customAjax.Fn_CommanGet("/Materialmaster/GetProductDetails", "#divProductDetail", "#IGLDataTable")
    }
};

$(document).ready(function () {
    productDetail.Fn_GetProductDetail();
})


