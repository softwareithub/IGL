var poDetail = {
    "Fn_CreatePO": function (id) {
        customAjax.Fn_CommanCreate("/PurchaseOrder/CreatePO", "#divCommanModalPartial", "#IGLCommanModal", "Create Purchase Order", "#divPurchaseOrder")
    },
    "Fn_DeletePO": function (id) {
        customAjax.Fn_CommanDelete(id, "/PurchaseOrder/Delete", poDetail.Fn_GetPOList, "#divPurchaseOrder")
    },
    "Fn_EditPO": function (id) {
        customAjax.Fn_CommanEdit(id, "/PurchaseOrder/POPDF", "Purchase Order", "#divPurchaseOrder");
    },
    "Fn_Success": function (response) {
        $("#IGLCommanModal").modal('hide');
        poDetail.Fn_GetPOList();
        customAjax.Fn_SubmitSuccess(response);
    },
    "Fn_GetPOList": function () {
        customAjax.Fn_CommanGet("/PurchaseOrder/GetPOList", "#divPurchaseOrder", "#IGLDataTable")
    },
    "Fn_AddMaterialToPo": function () {
        var message = "Item Added !!!";
        debugger;
        $(".chk").each(function (e, data) {
            if (data.checked) {
                if ($("#tblMaterialReturnIssue tr > td:contains(" + data.getAttribute("data-name") + ")").length > 0) {
                    message="Item already present, Please select another item."
                }
                else {
                    var cost = parseFloat(data.getAttribute("data-perunitCost"));
                    var html = "<tr>";
                    html += "<td><input type='hidden' name='matId' value=" + data.getAttribute("data-id") + "><input type='hidden' name='unitPrice' value=" + data.getAttribute("data-perunitCost") + ">";
                    html += "" + 1 + "</td>";
                    html += "<td>" + data.getAttribute("data-name") + "</td>";
                    html += "<td>" + data.getAttribute("data-code") + "</td>";
                    html += "<td>" + data.getAttribute("data-unit") + "</td>";
                    html += "<td><input type='number' name='qty' onblur='poDetail.fn_calculateCost(this," + cost + "," + data.getAttribute("data-id") + ")' class='form-control' name='quantity'/></td>";
                    html += "<td><span id=" + data.getAttribute("data-id") + "> </span></td>";
                    html += "<td><input type='text' class='form-control' name='remarks'/></td>";
                    html += "<td><a onclick='poDetail.fn_deleteItem(this)'>Delete</a></td>";
                    html += "</tr>";
                    $("#tblMaterialReturnIssue").append(html);
                    message = "Item Added !!!";
                    $('#dvCreatePObtn').removeClass("hide");
                }
            }
        });
        alertify.success(message);
        $("#MaterialModalPopUp").modal('hide');
    },
    "fn_deleteItem": function (eData) {
        $(eData).parent().parent().remove();
        if ($('#dvCreatePObtn>tr').length > 0) {
            $('#dvCreatePObtn').removeClass("hide");
        }
        else {
            $('#dvCreatePObtn').addClass("hide");
        }
    },
    "fn_calculateCost": function (eData, cost, id) {
        $("#" + id).text(parseFloat($(eData).val()) * cost);
        return
    },
    "Fn_GetMaterialDetail": function () {
        $.get("/PurchaseOrder/MaterialForPo", function (data) {
            $("#divMaterilDetail").html(data);
            $("#MaterialModalPopUp").modal();
        }).done(function () {
            setTimeout(function () {
                debugger;
                $("#IGLMaterialTable").DataTable({
                    "responsive": true,
                    "autoWidth": false,
                    "paging": false,
                });
            }, 500)
        });
    },
    "fn_PrintSlip": function (poNumber) {
        kendo.drawing
            .drawDOM("#poDiv",
                {
                    paperSize: "A4",
                    margin: { top: "1cm", bottom: "1cm", right: "1cm", left: "1cm" },
                    scale: 0.8,
                    height: 500
                })
            .then(function (group) {
                kendo.drawing.pdf.saveAs(group, "Purchase Order  " + poNumber)
            });
    },
    "Fn_ApprovePO": function (id) {
        $.get("/PurchaseOrder/ApprovePo", { poId: id }, function (data) {
            $("#divCommanModalPartial").html(data);
            $("#IGLCommanModal").modal('show')
            $("#modalTitle").text("Approve Purchase Order");
        })
    },
    "Fn_UpdateTotalPrice": function (eData, unitPrice, id) {
        debugger;
        var totalPrice = parseFloat($(eData).val()) * parseFloat(unitPrice);
        $("#" + id).text(totalPrice);
    }
};

$(document).ready(function () {
    poDetail.Fn_GetPOList();
})
