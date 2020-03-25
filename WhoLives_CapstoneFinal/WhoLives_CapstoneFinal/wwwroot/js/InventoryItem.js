var dataTable;
$(document).ready(function () {    
    loadInventoryList();
    loadOrderList();
    loadAssemblyList();
});

function loadInventoryList() {
    dataTable = $('#AllItem').dataTable({
        "ajax": {
            "url": "/api/inventoryItem/",
            "data": { input:"ALL"},
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
        "url": "/api/inventoryItem/",
        "data": { input:"ORDER" },
        "type": "GET",
        "datatype": "json"
        },
        "columns" : [
            { "data": "name", "width": "50%" },
            { "data": "totalLooseQty", "width": "10%" },
            { "data": "reorderQty", "width": "10%" },
            { "data": "vendorItems", "width": "20%"},
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
function loadAssemblyList() {
    dataTable = $('#AssembleDisassemble').dataTable({
        "ajax": {
            "url": "/api/inventoryItem/",
            "data": { input: "Assemble" },
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "totalLooseQty", "width": "10%" },
            { "data": "reorderQty", "width": "10%" },
            {
                "data": "inventoryItemID",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a  class="btn btn-primary text-white" style="cursor:pointer; width:40px;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:40px;" >
                                    <i class="far fa-trash-alt"></i>
                                </a>
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