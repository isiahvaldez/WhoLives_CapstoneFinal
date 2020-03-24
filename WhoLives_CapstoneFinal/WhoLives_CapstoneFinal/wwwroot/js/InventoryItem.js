var dataTable; 
$(document).ready(function () {
    loadInventoryList();
});

function loadInventoryList() {
    dataTable = $('#AllItem').dataTable({
        "ajax": {
            "url": "/api/inventoryItem",
            "type": "GET",
            "datatype": "json"
        }
        "columns": [
            {
            "data":"Name"}
        ]

        
    });
}