var dataTable;

$(document).ready(function () {
    loadInventoryList();
});

function loadInventoryList() {
    dataTable = $('#DT_load_inventory').DataTable({
        "ajax": {
            "url": "/api/drillAssembly/",
            "type": "GET",
            "datatype": "json",
            "contentType": "application/json; charset=utf-8",
            "dataSrc": ""
        },
        "columns": [
            { "data": "name", "width": "20%", "defaultContent": "<i>Not set</i>"},
            { "data": "looseQty", "width": "20%", "defaultContent": "<i>Not set</i>" },
            { "data": "totalQty", "width": "20%", "defaultContent": "<i>Not set</i>" },  
            { "data": "requiredQty", "width": "20%", "defaultContent": "<i>Not set</i>" },
            { "data": "ratio", "width": "20%", "defaultContent": "<i>Not set</i>"}
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}