var sivDetails = {
    "Fn_CreateSiv": function () {
        $.get("/SIVMaster/CreateSIV", function (data) {
            $("#divSIVDetails").html(data);
        })
    },
    "Fn_GetPoDetailInfo": function () {
        $.get("/SIVMaster/GetPODetail", { id: $("#PoId").val() }, function (data) {
            debugger;
            if (data.isApprove) {
                $("#poDate").val(data.poDate);
                $("#vendorName").val(data.vendorName);
                $("#InvoiceNumber").val(data.invoiceNumber);
                $("#InvoiceDate").val(data.invoiceDate);
                $("#btnSave").attr("style", 'display:none');
                $("#btnApprove").removeAttr('style')
            }
            else {
                $("#poDate").val(data.poDate);
                $("#vendorName").val(data.vendorName)
                $("#btnApprove").attr("style", 'display:none');
                $("#btnSave").removeAttr('style')
            }
            if (data.poApproved == "SIVApproved") {
                $("#divButtons").hide();
            }
            else {
                $("#divButtons").show();
            }

        });
        $.get("/SIVMaster/GetPOItems", { id: $("#PoId").val() }, function (data) {
            $("#divPoItems").html(data);
        });
    },

    "Fn_Success": function (response) {
        alertify.success(response);
        sivDetails.Fn_CreateSiv();
    },
    "Fn_DeleteSIVItem": function (eData) {
        alertify.confirm("Are you sure want to remove the Item ??", function ()
        {
            $(eData).parent().parent().remove()
            alertify.success("SIV Item deleted successfully.");
        },
            function () {
                alertify.success("You cancel the SIV Item Deletion.")
        })
      
    },
    "Fn_UpdateTotalPrice": function (eData, unitPrice, id) {
        var totalPrice = parseFloat($(eData).val()) * parseFloat(unitPrice);
        $("#" + id).text(totalPrice);
    }

};

$(document).ready(function () {
    sivDetails.Fn_CreateSiv();
})


