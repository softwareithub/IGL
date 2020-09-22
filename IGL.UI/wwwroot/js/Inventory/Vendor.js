var vendorDetail = {
    "Fn_CreateVendor": function (id) {
        customAjax.Fn_CommanCreate("/VendorMaster/CreateVendor", "#divCommanModalPartial", "#IGLCommanModal", "Create Vendor Detail", "#divVendorIndex")
    },
    "Fn_DeleteVendor": function (id) {
        customAjax.Fn_CommanDelete(id, "/VendorMaster/Delete", vendorDetail.Fn_GetVendorList, "#divVendorIndex")
    },
    "Fn_EditVendor": function (id) {
        customAjax.Fn_CommanEdit(id, "/VendorMaster/CreateVendor", "Vendor Customer Detail", "#divVendorIndex");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        vendorDetail.Fn_GetVendorList();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetVendorList": function () {
        customAjax.Fn_CommanGet("/VendorMaster/GetVendorList", "#divVendorIndex", "#IGLDataTable")
    }
};

$(document).ready(function () {
    vendorDetail.Fn_GetVendorList();
})


