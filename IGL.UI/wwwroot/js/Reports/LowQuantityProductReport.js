$(document).ready(function () {

    var table = $('#IGLDataTable').DataTable();

    $("#btnExport").click(function (e) {
        table.page.len(-1).draw();
        window.open('data:application/vnd.ms-excel,' +
            encodeURIComponent($('#IGLDataTable').parent().html()));
        setTimeout(function () {
            table.page.len(10).draw();
        }, 1000)

    });
})