
var dataTable;
var Qty_ID = {};
var reset = false;
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
            "data": { input: "ALL" },
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
            { "data": "reorderQty", "width": "25%" }

        ], "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"

    });
}
function loadOrderList() {
    dataTable = $('#ReOrderTable').DataTable({
        "ajax": {
            "url": "/api/inventoryItem/",
            "data": { input: "ORDER" },
            "type": "GET",
            "datatype": "json"
        },
        select: {
            style: "multi",
            items: "row"
        },
        "columns": [
            {
                "data": "inventoryItemID",
                "visible": false,
                "width": "5%"
            },
            { "data": "name", "width": "50%" },
            { "data": "totalLooseQty", "width": "10%" },
            { "data": "reorderQty", "width": "10%" },
            { "data": "vendorName", "width": "25%" }

        ], "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"

    }).on('click', 'tbody tr', function () {
        // Check to see if vendor was selected. 
        var select = document.getElementById('ItemAssemblyVendor_Vendor_VendorID');
        var selectedValue = select.options[select.selectedIndex].text;
        //if (selectedValue == "-Please Select a Vendor") {           
        //    reset = true;      
        //} 
        // Need to check if Vendor has been selected  belfore Selection is allowed 
        if ($('#ReOrderTable').DataTable().row(this, { selected: true }).any()) {
            //$('#ReOrderTable').DataTable().row(this).deselect();
            $('#ReOrderTable').DataTable().row(this, { selected: false });
        }
        else {
            $('#ReOrderTable').DataTable().row(this, { selected: true });
        }


        //if (reset == true) {
        //    swal( "Please Select a vendor", {
        //        icon: "warning"
        //    }).then((willDelete) => {
        //        if (willDelete) {
        //            $('#ReOrderTable').DataTable().row(this, { selected: false });
        //        } 
        //       // $('#ReOrderTable').DataTable().row(this, { selected: false });
        //    });
        //}

    });


}
// Filter the table on Selection
$("#ItemAssemblyVendor_Vendor_VendorID").on('change', function () {
    var select = document.getElementById('ItemAssemblyVendor_Vendor_VendorID');
    var selectedValue = select.options[select.selectedIndex].text;
    if (selectedValue != "-Please Select a Vendor") {
        $('#ReOrderTable').DataTable().column(4).search(selectedValue).draw();
    } else {
        //filter by selected value on Last column
        $('#ReOrderTable').DataTable().column(4).search("").draw();
    }
});
// Load Assemble diss assemble Table
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
                }, "width": "50%"
            },
            { "data": "totalLooseQty", "width": "20%" },
            { "data": "reorderQty", "width": "20%" },
            {
                "data": "inventoryItemID",

                "render": function (data) {
                    return ` <div class="text-center">
                               <input type="number" style="width:60px;" id ="asse${data}"/> <a class="btn btn-primary text-white" style="cursor:pointer; width:40px;" onClick="assemble(${data})">
                                    <i class="fas fa-wrench"></i>
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

function assemble(id) {
    //console.log('asse' + id);
    var qty = document.getElementById('asse' + id).value;
    // console.log("my Id is" + id + 'and I made ' + qty);
    Qty_ID = { QTY: qty, ITEMID: id, ASSEMBLE: true };
    if (parseInt(qty) > 0) {
        swal({
            title: 'ALERT!!!!',
            text: "Inventory did not meet recipe for a Item(s). Press confirm to override.",
            icon: 'warning',
            buttons: {
                confirm: { text: "Confirm", value: false, visible: true, className: "", closeModal: true },
                cancel: { text: "Cancel", value: true, visible: true, className: "", closeModal: true, }
            },
        }).then((result) => {
            if (result) {
                //Clicked Cancel
                swal({
                    title: 'Cancelled:',
                    text: 'Nothing was changed.'
                });
            }
            else {
                //Clicked confirm
                $.ajax({
                    url: '/api/inventoryItem/assemble' + '?QTY=' + qty + '&ITEMID=' + id + '&ASSEMBLE=true',
                    type: 'POST',
                    data: Qty_ID, //JSON.stringify(Qty_ID),
                    // contentType: 'application/json; charset=utf-16',
                    dataType: 'json',
                    success: function (data) {
                        if (data.success) {
                            swal(data.message, {
                                icon: "success"
                            }).then(function () {
                                location.reload();
                            });
                            //dataTable.ajax.reload();
                        } else {
                            swal(data.message, {
                                icon: "error"
                            });
                        }
                        //datatable.ajax.reload();
                    }
                });
            }
        });
    }
    else {
        swal({
            text: "Are you sure you want to dissassemble?",
            icon: 'warning',
            buttons: {
                confirm: { text: "Confirm", value: false, visible: true, className: "", closeModal: true },
                cancel: { text: "Cancel", value: true, visible: true, className: "", closeModal: true, }
            },
        }).then((result) => {
            if (result) {
                //Clicked Cancel
                swal({
                    title: 'Cancelled:',
                    text: 'Nothing was changed.'
                });
            }
            else {
                //Clicked confirm
                $.ajax({
                    url: '/api/inventoryItem/assemble' + '?QTY=' + qty + '&ITEMID=' + id + '&ASSEMBLE=false',
                    type: 'POST',
                    data: Qty_ID, //JSON.stringify(Qty_ID),
                    // contentType: 'application/json; charset=utf-16',
                    dataType: 'json',
                    success: function (data) {
                        if (data.success) {
                            swal(data.message, {
                                icon: "success"
                            }).then(function () {
                                location.reload();
                            });
                        } else {
                            swal(data.message, {
                                icon: "error"
                            });
                        }
                       //datatable.ajax.reload();
                    } 
                    
                });
            }
        });
    }
}
// Used when there was seperate Dis assemble button 
function disassemble(id) {
    //console.log('asse' + id);
    var qty = document.getElementById('dis' + id).value;
    // console.log("my Id is" + id + 'and I made ' + qty);
    Qty_ID = { QTY: qty, ITEMID: id, ASSEMBLE: false };
    $.ajax({
        url: '/api/inventoryItem/disassemble' + '?QTY=' + qty + '&ITEMID=' + id + '&ASSEMBLE=false',
        type: 'POST',
        data: Qty_ID, //JSON.stringify(Qty_ID),
        // contentType: 'application/json; charset=utf-16',
        dataType: 'json',
        success: function (data) {
            if (data.success) {
                swal(data.message, {
                    icon: "success"
                });
                //dataTable.ajax.reload();
            } else if (data.error) {
                swal(data.message, {
                    icon: "error"
                });
            }
            else {
                swal(data.message, {
                    icon: "warning"
                });

            }
        }
    });
}
// Handles Passing the selection to re order page
function PassSelection() {

    var SelectedRows = $('#ReOrderTable').DataTable().rows({ selected: true }).data();
    var count = $('#ReOrderTable').DataTable().rows({ selected: true }).count();
    var SelectedId = [];

    for (var i = 0; i < count; i++) {
        SelectedId.push(SelectedRows[i].inventoryItemID.toString());
    }
    var select = document.getElementById('ItemAssemblyVendor_Vendor_VendorID');
    var selectedValue = select.options[select.selectedIndex].value;

    if (select.selectedIndex > 0) {
        var data = { Vendor: selectedValue, Items: SelectedId };
        $.ajax({
            url: '/api/order/',
            type: 'POST',
            data: JSON.stringify({ "Vendor": selectedValue, "Items": SelectedId }),
            contentType: 'application/json',
            success: function (data) {
                //bad hard-code, find a html helper
                window.location.href = '../PurchaseOrders/Upsert?id=' + data;
            }
        });
    }
    else {
        var warning = document.getElementById("vendorWarning");
        warning.innerHTML = "Select a vendor";
    }

}
