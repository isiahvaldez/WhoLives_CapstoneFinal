var dataTable;
var myInput = document.createElement("input");

$(document).ready(function () {
    loadInventoryList();
    //loadAssemblyList();
});

function loadInventoryList() {
    dataTable = $('#DT_load_inventory').DataTable({
        "ajax": {
            "url": "/api/drillAssembly/",
            //"data": { input: "INVENTORY" },
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data:": "inventoryItemID", "width": "25%", "defaultContent": "<i>Not set</i>"},
            { "data:": "isAssembly", "width": "25%", "defaultContent": "<i>Not set</i>" },
            { "data:": "name", "width": "25%", "defaultContent": "<i>Not set</i>" }, // change this to include items in assemblies later - IV 4/3/2020 
            { "data:": "description", "width": "25%", "defaultContent": "<i>Not set</i>" } // this needs to be the number required for the drill - IV 4/3/2020
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function loadAssemblyList() {
    dataTableAssembly = $('#DT_load_assembly').DataTable({
        "ajax": {
            "url": "/api/drillAssembly/",
            //"data": { input: "ASSEMBLY" },
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data:": "totalLooseQty", "width": "33%", "defaultContent": "<i>Not set</i>" }//,
            //{ "data:": "totalLooseQty", "width": "33%" },
           // { "data:": "retailCost", "width": "33%" } // this needs to be the number required for the drill - IV 4/3/2020
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}