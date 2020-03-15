var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "/api/order",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "vendor",
                "width": "25%",
                "render": function (data, type, row, meta) {
                    var index = sdata.indexOf(row);
                    var select = $("<select id='" + index + "'>" +
                                        "<option value='backorder'>Backorder</option>" +
                                        "<option value='ordered'>Ordered</option>" +
                                        "<option value='shipping'>Shipping</option>" +
                                        "<option value='received'>Received</option>" +
                                        "<option value='partiallyreceived'>Partially Received</option>" +
                                        "<option value='overdue'>Overdue</option>" +
                                        "<option value='pending'>Pending</option>" +
                                    "</select>");
                }
            },
            {
                "data": "po",
                "width": "15%"
            },
            {
                "data": "status",
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/order/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width: 100px">
                                <i class="far fa-edit"></i> Edit
                            </a>
                            <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/order/'+${data})>
                                <i class="far fa-trash-alt"></i> Delete
                            </a>
                        </div>`;
                },
                "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No purchase orders to display."
        },
        "width": "100%"
    });
}