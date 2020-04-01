
var dataTable;
var name = document.createElement("input");
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
            {
                "data": { ItemId: "inventoryItemID", name: "name" },
                "render": function (data) {
                    return `<a href=/Inventory/Upsert?id=${data.inventoryItemID} style = "cursor:pointer"> 
                            ${data.name}
                    </a> `
                }
            },
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
            { 
                "data": { ItemId: "inventoryItemID", name: "name" },
                "render": function (data) {
                    return `<a href=/Inventory/Upsert?id=${data.inventoryItemID} style = "cursor:pointer"> 
                            ${data.name}
                    </a> `
                }, "width": "50%"
            },
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
            {
                "data": { ItemId: "inventoryItemID", name: "name" },
                "render": function (data) {
                    return `<a href=/Inventory/Upsert?id=${data.inventoryItemID} style = "cursor:pointer"> 
                            ${data.name}
                    </a> `
                }, "width": "50%" },
            { "data": "totalLooseQty", "width": "20%" },
            { "data": "reorderQty", "width": "20%" },
            {
                "data": "inventoryItemID",
          
                "render": function (data) {
                    return ` <div class="text-center">
                               <input type="number" style="width:15%;" id ="asse${data}"/> <a class="btn btn-primary text-white" style="cursor:pointer; width:50%;" onClick="assemble('/api/inventoryItem/'+${data})">
                                    <i class="far fa-edit">Assemble</i>
                                </a>
                            </div>
                            <div class="text-center">
                               <input type="number" style="width:15%;"/> <a class="btn btn-danger text-white" style="cursor:pointer; width:50%;onClick="disassebmle(${data})" >
                                    <i class="far fa-trash-alt">Disassemble</i>
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
//function assemble(id) {
//    console.log('asse' + id);
//    var qty = document.getElementById('asse' + id).value;
//    console.log("my Id is" + id + 'and I made ' + qty);
//    $.ajax({
//        url: '/api/inventoryItem',
//        type: 'POST',       
//        data: JSON.stringify(id),
//        contentType: 'application/json',
//        success: function (data) {
//            if (data.success) {
//                console.log('Finally');
//            }
//            else {
//                console.log('Error');
//            }
//        }
//    });
//}
function assemble(url) {
    //console.log('asse' + id);
    console.log(url);
    //var qty = document.getElementById('asse' + id).value;
    //console.log("my Id is" + id + 'and I made ' + qty);
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            if (data.success) {
                console.log('Finally');
            }
            else {
                console.log('Error');
            }
        }
    });
}
function disassebmle(id) {
    var qty = document.getElementById('asse' + id).value;
}