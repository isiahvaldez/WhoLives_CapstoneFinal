var dataTable; 
$(document).ready(function () {
    loadInventoryList();
    loadOrderList();
});

function loadInventoryList() {
    dataTable = $('#AllItem').dataTable({
        "ajax": {
            "url": "/api/inventoryItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "name", "width": "50%" },
            { "data": "totalLooseQty", "width": "25%" },
            {"data":"reorderQty","width":"25%"}

        ], "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
        
    });
}
function loadOrderList() {
    dataTable = $('#ReOrderTable').dataTable({
        "ajax": {
            "url": "/api/inventoryItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "totalLooseQty", "width": "10%" },
            { "data": "reorderQty", "width": "10%" },
            { "data": "vendor", "width": "20%"},
            {"data": "inventoryItemID",
                "render": function (data) {
                    return ` <div class="text-center">
                               <input type="checkbox" />
                             </div>`
                },
                "width": "10%"
            }

        ], "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"

    });
}