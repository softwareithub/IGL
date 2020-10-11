function PoDetail(id, status) {
    $.get("/VendorWisePoStatusReport/GetPODetailReport", { vendorId: id, status: status }, function (data) {
        $("#divCommanModalPartial").html(data);
        $("#IGLCommanModal").modal('show');

    }).done(function () {
        $("#IGLPOStatusDetailTable").DataTable();
    });
}

$(document).ready(function () {

    $("#IGLDataTable").DataTable();
})