var ids = [];
var totalCost = 0;

var materialTransaction = {
    "Fn_ReturnIssue": function (id) {
        customAjax.Fn_CommanCreate("/MaterialTransaction/ReturnIssue", "#divCommanModalPartial", "#IGLCommanModal", "Return Issue Material", "#divMaterialTransaction")
    },
    "Fn_DeleteProduct": function (id) {
        customAjax.Fn_CommanDelete(id, "/Materialmaster/Delete", productDetail.Fn_GetProductDetail, "#divProductDetail")
    },
    "Fn_EditProductMaster": function (id) {
        customAjax.Fn_CommanEdit(id, "/Materialmaster/CreateMaterial", "Edit Product Master", "#divProductDetail");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        materialTransaction.Fn_GetMaterialTransaction();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetMaterialTransaction": function () {
        customAjax.Fn_CommanGet("/MaterialTransaction/GetMaterialTransactionDetail", "#divMaterialTransaction", "#IGLDataTable")
    },
    "Fn_GetMaterialDetail": function () {
        $.get("/MaterialTransaction/GetMaterialDetails", function (data) {
            $("#divMaterilDetail").html(data);
            $("#MaterialModalPopUp").modal('show');
        });
    },
    "Fn_AddItemToIssueReturn": function () {
        var message = "Item Added !!!";
        $(".chk").each(function (e, data) {
            if (data.checked) {
                debugger;
                if ($("#tblMaterialReturnIssue tr > td:contains(" + data.getAttribute("data-name") + ")").length > 0) {
                    message = "Item already present, Please select another item."
                }
                else {
                    var cost = parseFloat(data.getAttribute("data-perunitCost"));
                    var html = "<tr>";
                    html += "<td><input type='hidden' name='matId' value=" + data.getAttribute("data-id") + "><input type='hidden' name='unitPrice' value=" + data.getAttribute("data-perunitCost") + ">";
                    html += "" + 1 + "</td>";
                    html += "<td>" + data.getAttribute("data-name") + "</td>";
                    html += "<td>" + data.getAttribute("data-code") + "</td>";
                    html += "<td>" + data.getAttribute("data-unit") + "</td>";
                    html += "<td><input type='number' name='qty' onblur='materialTransaction.fn_calculateCost(this," + cost + "," + data.getAttribute("data-id") + ")' class='form-control' name='quantity'/></td>";
                    html += "<td><span id=" + data.getAttribute("data-id") + "> </span></td>";
                    html += "<td><input type='text' class='form-control' name='remarks'/></td>";
                    html += "<td><a onclick='materialTransaction.fn_deleteItem(this)'>Delete</a></td>";
                    html += "</tr>";
                    $("#tblMaterialReturnIssue").append(html);
                    message = "Item Added !!!";
                }
            }
        });
        alertify.success(message);
        $("#MaterialModalPopUp").modal('hide');
    },
    "fn_deleteItem": function (eData) {
        debugger;
        $(eData).parent().parent().remove()
    },
    "fn_calculateCost": function (eData, cost, id) {
        debugger;
        $("#" + id).text(parseFloat($(eData).val()) * cost);
        return
    },
    "fn_GetMaterialSlip": function (id) {
        $.get("/MaterialTransaction/GetMaterialSlip", { id: id }, function (data) {
            $("#divCommanModalPartial").html(data);
            $("#IGLCommanModal").modal('show');
        })
    },
    "fn_PrintSlip": function () {
        kendo.drawing
            .drawDOM("#slipDiv",
                {
                    paperSize: "A4",
                    margin: { top: "1cm", bottom: "1cm" },
                    scale: 0.8,
                    height: 500,
                    landscape: true
                })
            .then(function (group) {
                kendo.drawing.pdf.saveAs(group, "Material Slip")
            });
    },
    "fn_CreateConfirm": function () {
        var transactionType = $("#TransactionType").val();
        alertify.confirm("Are you sure want to create " + transactionType, function () {
            return true
        }, function () { return false; })
    }
};

$(document).ready(function () {
    materialTransaction.Fn_GetMaterialTransaction();
})


