function GetProductIssueDetail(id) {
    $("#divReturnPartial").addClass("ajaxLoading");
    $("#divIssueDetailPartial").html('');
    $.get("/MaterialReturn/GetProductIssueDetail", { id: $("#ddlSlipNumber").val() }, function (data) {
        $("#divIssueDetailPartial").html(data);
    }).catch(function (error) {
        console.log(error.responseText);
        debugger;
        $("#divReturnPartial").removeClass('ajaxLoading');
    }).done(function () {
        $("#divReturnPartial").removeClass('ajaxLoading');
    });
}
function CheckNo(sender,maxValue) {
    if (!isNaN(sender.value)) {
        if (sender.value > maxValue)
            sender.value = maxValue;
        if (sender.value < 0)
            sender.value = 0;
    } else {
        sender.value = 0;
    }
}