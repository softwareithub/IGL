var itemNumner = [];

function Fn_Success(response) {
    alertify.success(response);
}
function Fn_ValidateItemNumber(eval) {
    debugger;
    $.get("/SIVIGLProduct/IsItemExists", { itemNumber: $(eval).val() }, function (data) {
        if (data == 1) {
            alert("Item with code " + $(eval).val() + "already exists.Please assign another item number.");
            $(eval).attr("style", "border-color:red")
            $(eval).val('');
        }
    })
}
function CreateItems() {
    var number = parseInt($("#quantity").val());
    $("#divItemNumber").html('')
    debugger;
    for (let i = 0; i < number; i++) {
        var html = '<div class="col-md-4 form-group">';
        html += '  <label class="control-label">Item Number</label>';
        html += ' <input type="text" class="form-control" onchange="ValidateItemNumber(this)" name="ItemNumber"/>';
        html += '</div>';
        $("#divItemNumber").append(html);
    }

}
function ValidateItemNumber(eval) {
    debugger;
    if (itemNumner.length == 0) {
        itemNumner.push($(eval).val().toLowerCase().trim());
    } else {
        if (itemNumner.find(x => x == $(eval).val().toLowerCase().trim())) {
            alert('Item with item number ' + $(eval).val() + " already exists");
            $(eval).val('')
        }
        else {
            itemNumner.push($(eval).val());
        }
    }
}
function AjaxSuccess(response) {
    alert("SIV For IGL product created.")
    setTimeout(function () {
        location.reload();
    }, 200)
}

function GetPoWiseProduct()
{
    $.get("/SIVIGLProduct/GetPoWiseIGLProduct", function (data) {
        $("#divCommanModalPartial").html(data);
        $("#IGLCommanModal").modal('show');
    })
}
function ExpandCollapse(eData) {
   // $("#" + eData)
}