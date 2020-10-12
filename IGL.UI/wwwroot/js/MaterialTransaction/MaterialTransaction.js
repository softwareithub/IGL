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
        var html = "<tr>";
        html += "<td><select name='matId' class='form-control'  onchange='materialTransaction.fn_ValidateProduct(this)'><option>" + GetProductDetails() + "</option></select></td>";
        html += "<td><select name='prodNumber'  class='form-control'></select></td>";
        html += "<td><input type='number' onblur='materialTransaction.fn_ValidateQuantity(this)' name='qty' class='form-control' name='quantity'/></td>";
        html += "<td><input type='text' class='form-control' name='remarks'/></td>";
        html += "<td><a onclick='materialTransaction.fn_deleteItem(this)'>Delete</a></td>";
        html += "</tr>";
        $("#tblMaterialReturnIssue").append(html);
    },
    "fn_deleteItem": function (eData) {
        $(eData).parent().parent().remove()
        if ($('#tblMaterialReturnIssue>tr').length > 0) {
            $('#dvMatIssueReturnSaveBtn').removeClass("hide");
        }
        else {
            $('#dvMatIssueReturnSaveBtn').addClass("hide");
        }
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
    },
    "fn_ValidateProduct": function (eData) {
        debugger;
        if ($(eData.selectedOptions).attr('data-isunique') == 1) {
            $(eData.parentElement.nextElementSibling.firstElementChild).removeAttr('readOnly')
            $(eData.parentElement.nextElementSibling.nextElementSibling.firstElementChild).val('1');
            $(eData.parentElement.nextElementSibling.nextElementSibling.firstElementChild).attr('readOnly', 'true');
            $.get("/MaterialTransaction/GetProductNumber", { prodId: $(eData.selectedOptions).val() }, function (data) {
                debugger;
                var html = '';
                for (var i = 0; i < data.length; i++) {
                    html += '<option value=' + data[i].id + '>' + data[i].itemNumber + '</option>'
                }
                $(eData.parentElement.nextElementSibling.firstElementChild).empty().append(html);
            });
        }
        else {
            $(eData.parentElement.nextElementSibling.firstElementChild).attr('readOnly', 'true')
            $(eData.parentElement.nextElementSibling.nextElementSibling.firstElementChild).val('0');
            $(eData.parentElement.nextElementSibling.nextElementSibling.firstElementChild).removeAttr('readOnly');
            $(eData.parentElement.nextElementSibling.firstElementChild).empty().append('<option value="0"> select</option>');
        }
    },

    "fn_ValidateQuantity": function (eData) {
        $(eData.parentElement.previousElementSibling.previousElementSibling.firstElementChild).val()
        $.get("/MaterialTransaction/ValidateProductQuantity",
            { prodId: $(eData.parentElement.previousElementSibling.previousElementSibling.firstElementChild).val(), qty: eData.value }, function (data) {
                if (data == "-1") {
                    alertify.confirm("Product do not have suffcient quantity to Issue. Do you want to continue?", function () {
                    }, function () {
                            $(eData).val('0')
                    });
                }
                if (data == "-2") {
                    alertify.confirm("After Product Issue the quantity will become 0. Do you want to continue?", function () { }, function () {
                        $(eData).val('0')
                    });
                }

            })
    }
};

$(document).ready(function () {
    materialTransaction.Fn_GetMaterialTransaction();

})


