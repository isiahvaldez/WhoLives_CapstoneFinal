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
            { "data": "name", "width": "25%", "defaultContent": "<i>Not set</i>"},
            { "data": "looseQty", "width": "25%", "defaultContent": "<i>Not set</i>" },
            { "data": "totalQty", "width": "25%", "defaultContent": "<i>Not set</i>" }, // change this to include items in assemblies later - IV 4/3/2020 
            { "data": "requiredQty", "width": "25%", "defaultContent": "<i>Not set</i>" } // this needs to be the number required for the drill - IV 4/3/2020
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}