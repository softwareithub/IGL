function GetProductIssueDetail(id) {
    $.get("/MaterialReturn/GetProductIssueDetail", { id: $("#ddlSlipNumber").val()}, function (data) {
        $("#divIssueDetailPartial").html(data);
    })
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