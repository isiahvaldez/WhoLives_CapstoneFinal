
var dataTable;
var Qty_ID = {};
var reset = false;
var name = document.createElement("input");
$(document).ready(function () {
    loadAssemblyList();    
    loadPurchaseList();
});

function loadAssemblyList() {
    dataTable = $('#AllItem').dataTable({
        "ajax": {
            "url": "/api/inventoryItem/",
            "data": { input:"ALL"},
            "type": "GET",
            "datatype": "json"

        },
        "columns": [
            {
                "data": { ItemId: "inventoryItemID", name: "name" },
                "render": function (data) {
                    return `<a href=/Inventory/Upsert?id=${data.inventoryItemID} style = "cursor:pointer"> 
                            ${data.name}
                    </a> `
                }, "width": "50%" 
            },
            { "data": "totalLooseQty", "width": "25%" },
            {"data":"reorderQty","width":"25%"}

        ], "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
        
    });
}

function loadPurchaseList() {
    dataTable = $('#purchaseHistory').dataTable({
        "ajax": {
            "url": "/api/itemupsert/",
            "data": { input: "purchase"},
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Vendor", "width": "20%" },
            {
                "data":  "PurchaseOrderID", 
                "render": function (data) {
                    return `<a href=/Inventory/Upsert?id=${data.PurchaseOrderID} style = "cursor:pointer"> 
                            ${data.PurchaseOrderID}
                    </a> `
                }, "width": "50%" },
            { "data": "DateOrdered", "width": "20%" },
            { "data": "Purchased Cost", "width": "20%" }  
        ], "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"

    });
}


