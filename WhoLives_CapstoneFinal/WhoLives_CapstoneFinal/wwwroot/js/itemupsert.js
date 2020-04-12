
var dataTable;
var obj = {};
var queryString;
var urlParams;
var urlid;

$(document).ready(function () {
    queryString = window.location.search;
    urlParams = new URLSearchParams(queryString);
    urlid = urlParams.get('id');
    console.log(urlid);
    //loadAssemblyList();    
    loadPurchaseList();
});

function loadAssemblyList() {
    dataTable = $('#AllItem').dataTable({
        "ajax": {
            "url": "/api/itemUpsert/",
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
    obj = { input: "purchase", id: urlid};
    dataTable = $('#purchaseHistory').dataTable({
        "ajax": {
            "url": "/api/itemUpsert/",
            "data": obj,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "vendorName", "width": "20%" },
            {
                "data":  "purchaseOrderID", 
                "render": function (data) {
                    return `<a href=/PurchaseOrders/Upsert?id=${data} style = "cursor:pointer"> 
                            ${data}
                    </a> `
                }, "width": "50%" },
            { "data": "dateOrdered", "width": "20%" },
            { "data": "price", "width": "20%" }  
        ], "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"

    });
}


