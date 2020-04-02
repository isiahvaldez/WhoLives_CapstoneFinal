﻿var dataTable;

$(document).ready(function () {
    if (document.location.pathname.toLowerCase().includes('upsert')) {
        loadUpsertList();
    }
    else {
        loadList();
    }
});

function loadList() {
    dataTable = $('#DT_load').dataTable({
        responsive: { details: true },
        "ajax": {
            "url": "/api/order",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "vendor.vendorName",
                "width": "20%",
                "render": function (data, type, row) {
                    if (type == 'display') {
                        data = '<a href="/purchaseorders/upsert?id=' + row.purchaseOrderID + '">' + data + '</a>';
                    }
                    return data;
                },
            },
            {
                "data": "po",
                "width": "20%",
                "render": function (data, type, row) {
                    if (type == 'display') {
                        data = '<a href="/purchaseorders/upsert?id=' + row.purchaseOrderID + '">' + data + '</a>';
                    }
                    return data;
                },
            },
            {
                "data": "status",
                "width": "20%"
            },
            {
                "data": "purchaseOrderID",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/purchaseorders/upsert?id=${data}" class="btn btn-primary" style="cursor:pointer; width: 100px">
                                <i class="far fa-edit"></i>
                            </a>
                            <a class="btn btn-danger" style="cursor:pointer; width:100px;" onclick=Delete('/api/purchaseorders/'+${data})>
                                <i class="far fa-trash-alt"></i>
                            </a>
                        </div>`;
                },
                "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No purchase orders to display."
        },
        "width": "100%"
    });
}

//function loadUpsertList() {
//    dataTable = $('#DT_load').dataTable({
//        "ajax": {
//            "url": "/api/order",
//            "data": { input: "upsert" },
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": [
//            {
//                "data": "item",
//                "width": "35%"
//            },
//            {
//                "data": "quantityOrdered",
//                "width": "15%"
//            },
//            {
//                "data": "status",
//                "width": "35%",
//                //"render": function (data, type, row, meta) {
//                //    var index = data.indexOf(row);
//                //    var select = $("<select id='" + index + "'>" +
//                //        "<option value='backorder'>Backorder</option>" +
//                //        "<option value='ordered'>Ordered</option>" +
//                //        "<option value='shipping'>Shipping</option>" +
//                //        "<option value='received'>Received</option>" +
//                //        "<option value='partiallyreceived'>Partially Received</option>" +
//                //        "<option value='overdue'>Overdue</option>" +
//                //        "<option value='pending'>Pending</option>" +
//                //        "</select>");
//                //}
//            },
//            {
//                "data": "quantityReceived",
//                "width": "15%"
//            }
//        ],
//        "language": {
//            "emptyTable": "No order items to display."
//        },
//        "width": "100%"
//    });
//}