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
            { "data": "ratio", "width": "20%", "defaultContent": "<i>Not set</i>" },
            {
                "data": { "id": "id", "name": "name"},
                "render": function (data) {
                    return `<a href=/Inventory/Upsert?id=${data.id} style = "cursor:pointer"> 
                            ${data.name}
                    </a> `
                }, "width": "35%", "defaultContent": "<i>Not set</i>"},
            { "data": "looseQty", "width": "15%", "defaultContent": "<i>Not set</i>" },
            { "data": "assembledQty", "width": "15%", "defaultContent": "<i>Not set</i>" },  
            { "data": "requiredQty", "width": "15%", "defaultContent": "<i>Not set</i>" }
            
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}