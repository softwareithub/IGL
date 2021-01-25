function PoDetail(id, status) {
    $.get("/VendorWisePoStatusReport/GetPODetailReport", { vendorId: id, status: status }, function (data) {
        $("#divCommanModalPartial").html(data);
        $("#IGLCommanModal").modal('show');

    }).done(function () {
        $("#IGLPOStatusDetailTable").DataTable({ "scrollX": true});
    });
}

$(document).ready(function () {

    $("#IGLDataTable").DataTable({ "scrollX": true});
})