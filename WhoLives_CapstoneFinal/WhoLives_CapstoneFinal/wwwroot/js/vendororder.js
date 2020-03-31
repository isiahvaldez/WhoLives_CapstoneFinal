﻿var dataTable;

// IMPORTANT: URLSearchParams is not supported by Internet Explorer or Opera right now - 03/30/2020 IV 
var queryString = window.location.search;
var urlParams = new URLSearchParams(queryString);

var id = urlParams.get("vendorID");

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "/api/vendororder/" + id,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "po",
                "width": "20%"
            },
            {
                "data": "dateOrdered",
                "width": "20%"
            },
            {
                "data": "statusChangeDate",
                "width": "20%"
            },
            {
                "data": "totalPrice",
                "width": "20%"
            },
            {
                "data": "purchaseOrderID",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/purchaseorders/upsert?id=${data}" class="btn btn-primary" style="cursor:pointer; width: 75px">
                                Details
                            </a>
                        </div>`;
                },
                "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No purchase orders to display."
        },
        "width": "100%"
    });
}