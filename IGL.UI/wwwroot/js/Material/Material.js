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
    },
    "Fn_AsssignNumber": function (prdId,prdCount) {
        $.get("/Materialmaster/AssignProductNumber", { ProductId: prdId, count: prdCount }, function (data) {
            $("#divCommanModalPartial").html(data);
            $("#IGLCommanModal").modal('show');
            $("#modalTitle").text("Assign Product Number To Each Product.")
        })
    },
    "Fn_GetProductCostDetail": function (productId,productName) {
        $.get("/MaterialMaster/GetProductPriceDetail", { productId: productId }, function (data) {
            $("#IGLCommanModal").modal('show');
            $("#divCommanModalPartial").html(data);
            $("#modalTitle").html("Price Detail for Product (" + productName + ")");
        })                      
    },
    "Fn_IsProductExists": function (eval) {
        if ($("#Id").val() == "") {
            $.get("/MaterialMaster/IsProductExists", { productName: $(eval).val() }, function (data) {
                if (data == 1) {
                    alert("Product with name " + $(eval).val() + "   is already exists.")
                    $(eval).val('');
                }
            });
        }
       
    }
};

$(document).ready(function () {
    productDetail.Fn_GetProductDetail();
})


