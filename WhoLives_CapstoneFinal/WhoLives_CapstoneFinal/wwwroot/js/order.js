var dataTable;

$(document).ready(function () {
    //var scripts = document.getElementsByTagName('script');
    //var lastScript = scripts[scripts.length - 1];
    //let type = lastScript.getAttribute('page-type');
    //if (document.getElementsByTagName('page-type') == 'upsert') {
    //    loadUpsertList();
    //}
    //else {
        loadList();
    //}
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
                "data": "vendor.vendorName",
                "width": "25%"
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

function loadUpsertList() {
    dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "/api/order",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "item",
                "width": "35%"
            },
            {
                "data": "quantityOrdered",
                "width": "15%"
            },
            {
                "data": "status",
                "width": "35%",
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
                "data": "quantityReceived",
                "width": "15%"
            }
        ],
        "language": {
            "emptyTable": "No order items to display."
        },
        "width": "100%"
    });
}