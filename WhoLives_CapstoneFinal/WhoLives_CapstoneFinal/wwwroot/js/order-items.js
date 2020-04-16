//var dataTable;
//var id;
//$(document).ready(function () {
//    id = document.getElementById("poid").value; 
//    loadList();
//});

//function loadList() {
//    dataTable = $('#DT_load').DataTable({
//        //responsive: { details: true },
//        "ajax": {
//            "url": "/api/order/",
//            "data": { id: parseInt(id,10) },
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": [
//            {
//                "data": "item.name",
//                "width": "25%"
//            },
//            {
//                "data": "quantityOrdered",
//                "width": "20%"
//            },
//            {
//                "data": "price",
//                "width": "25%"
//            },
//            {
//                "data": "quantityReceived",
//                "width": "20%"
//            },
//            {
//                "data": "orderItemID",
//                "render": function (data) {
//                    return ` <div class="text-center">
//                                <button type="button" class="btn btn-primary" style="cursor:pointer; width: 40px" onclick=Edit(${data})>
//                                    <i class="far fa-edit"></i>
//                                </button>
//                                <a class="btn btn-danger text-white" style="cursor:pointer; width:40px;" onclick=Delete('/api/order/item?='+${data})>
//                                    <i class="far fa-trash-alt"></i>
//                                </a>
//                             </div>`
//                },
//                "width": "10%"
//            }
//        ],
//        "language": {
//            "emptyTable": "no data found."
//        },
//        "width": "100%"
//    });
//}

//function Delete(url) {
//    swal({
//        title: "Are you sure you want to delete?",
//        text: "You will not be able to restore the data!",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true
//    }).then((willDelete) => {
//        if (willDelete) {
//            $.ajax({
//                type: 'DELETE',
//                url: url,
//                success: function (data) {
//                    if (data.success) {
//                        toastr.success(data.message);
//                        dataTable.ajax.reload();
//                    }
//                    else {
//                        toastr.error(data.message);
//                    }
//                }
//            });
//        }
//    });
//}
//function Edit(id) {
//    document.getElementById("PurchaseOrderVM_tempOrderItem_ItemID").value = id.toString();
//    $("#itemdialog").dialog("open");
//}
$("#itemdialog").dialog({ autoOpen: false });

$("#add").click(function (e) {

    $("#itemdialog").dialog("open");
});

function Edit(btn) {
    $("#itemdialog").dialog('option','title','Edit order item');
    var id = document.getElementById("poid").value;
    var row = btn.parentNode.parentNode.rowIndex - 1;
    var currTable = $("#iTable > TBODY")[0];
    var itemid = currTable.rows[row].cells[0].innerHTML;
    var qtyordered = currTable.rows[row].cells[1].innerHTML;
    var price = currTable.rows[row].cells[2].innerHTML;
    var qtyreceived = currTable.rows[row].cells[3].innerHTML;
    //document.getElementById("PurchaseOrderVM_tempOrderItem_ItemID").value = id.toString();
    $("#TempOrderItem_ItemID").replaceWith('<input id="TempOrderItem_ItemID" value="' +
        itemid.replace('"', '&quot;') +
        '" class="form-control" readonly />');
    //document.getElementById("TempOrderItem_ItemID").value = itemid;
    document.getElementById("TempOrderItem_QuantityReceived").value = qtyreceived;
    document.getElementById("TempOrderItem_QuantityOrdered").value = qtyordered;
    document.getElementById("TempOrderItem_Price").value = price;

    $("#itemdialog").dialog("open");
}


function AddItemToTable() {
    $("#itemdialog").dialog("close");

    var currTable = $("#iTable > TBODY")[0];
    var itemid = document.getElementById("TempOrderItem_ItemID").value;
    var qtyreceived = document.getElementById("TempOrderItem_QuantityReceived").value;
    var qtyordered = document.getElementById("TempOrderItem_QuantityOrdered").value;
    var price = document.getElementById("TempOrderItem_Price").value;

    console.log("there are " + currTable.rows.length + "rows");
    // before adding a new item, search the table for duplicates
    for (var i = 0; i < currTable.rows.length; i++) {
        if (currTable.rows[i].cells[0].innerHTML == itemid) {
            currTable.rows[i].cells[1].innerHTML = qtyordered;
            currTable.rows[i].cells[2].innerHTML = price;
            currTable.rows[i].cells[3].innerHTML = qtyreceived;
            return;
        }
    }

    var newRow = currTable.insertRow(-1); // add row at next index
    newRow.innerHTML = "<td style='display:none'>" + itemid + "</td>" +
        "<td class='px-3'>" + qtyordered + "</td>" +
        "<td class='px-3'>" + price + "</td>" +
        "<td class='px-3'>" + qtyreceived + "</td>" +
        "<td class='text-center'>" +
        "<a style='width:40px' class='btn'><i class='far fa-trash-alt'></i></a>" +
        "<a style='width:40px' class='btn'><i class='far fa-edit'></i></a>" +
        "</td >";
    newRow.cells[4].firstChild.addEventListener("click", RemoveItemFromTable); // add listener to the button inside the cell
    newRow.cells[4].firstChild.addEventListener("click", Edit); // add listener to the button inside the cell
}
//$("#save").click(function (e) {
//    var qtyOrdered = document.getElementsByName("QuantityOrdered")[0].value;
//    var qtyReceived = document.getElementsByName("QuantityReceived")[0].value;
//    var price = document.getElementsByName("Price")[0].value;
//    var itemId = document.getElementById("PurchaseOrderVM_tempOrderItem_ItemID").value;
//    e.preventDefault();
//    e.stopPropagation();
//    $.ajax({
//        url: "/api/order/save",
//        data: {
//            id: parseInt(id, 10),
//            itemId: parseInt(itemId, 10),
//            qtyOrdered: parseInt(qtyOrdered, 10),
//            price: parseInt(price, 10),
//            qtyReceived: parseInt(qtyReceived, 10),
//            //name: function () { $("#id").val(); }
//        },
//        success: function () {
//            document.getElementsByName("QuantityOrdered")[0].value = "0";
//            document.getElementsByName("QuantityReceived")[0].value = "0";
//            document.getElementsByName("Price")[0].value = "0.00";
//            document.getElementById("PurchaseOrderVM_tempOrderItem_ItemID").value = "0";
//            $("#itemdialog").dialog("close");
//            dataTable.ajax.reload();
//        }
//    });
//});



function RemoveItemFromTable() {
    var nodeToDelete = this.parentNode; // cell
    var parent = nodeToDelete.parentNode;
    parent.parentNode.removeChild(parent);
}
