
$("#itemdialog").dialog({ autoOpen: false });

$("#add").click(function (e) {
    document.getElementById("error").innerHTML = "";
    $("#itemdialog").dialog('option', 'title', 'New order item');
    $("#TempOrderItem_ItemID").replaceWith($('#ListTemplate').html());
    document.getElementById("TempOrderItem_QuantityReceived").value = 0;
    document.getElementById("TempOrderItem_QuantityOrdered").value = 0;
    document.getElementById("TempOrderItem_Price").value = 0.00;
    $("#itemdialog").dialog("open");
    $(".js-example-basic-single").select2();
    //$("#itemdialog").val(null).trigger("change");
});

function Edit(node) {
    document.getElementById("error").innerHTML = "";
    $("#itemdialog").dialog('option', 'title', 'Edit order item');
    var id = document.getElementById("poid") != null ? document.getElementById("poid").value : 0;
    var row = node.parentNode.parentNode.rowIndex - 1;
    var currTable = $("#iTable > TBODY")[0];
    var itemid = currTable.rows[row].cells[0].innerHTML;
    var qtyordered = currTable.rows[row].cells[1].innerHTML;
    var price = currTable.rows[row].cells[2].innerHTML;
    var qtyreceived = currTable.rows[row].cells[3].innerHTML;
    //document.getElementById("PurchaseOrderVM_tempOrderItem_ItemID").value = id.toString();
    $("#TempOrderItem_ItemID").replaceWith('<input id="TempOrderItem_ItemID" value="' +
        itemid.replace('"', '&quot;') +
        '" class="form-control js-example-basic-single" readonly />');
    //document.getElementById("TempOrderItem_ItemID").value = itemid;
    document.getElementById("TempOrderItem_QuantityReceived").value = qtyreceived;
    document.getElementById("TempOrderItem_QuantityOrdered").value = qtyordered;
    document.getElementById("TempOrderItem_Price").value = parseFloat(price).toFixed(2);
    

    $("#itemdialog").dialog("open");
    $(".js-example-basic-single").select2();
}


function AddItemToTable() {

    var currTable = $("#iTable > TBODY")[0];
    var itemid = document.getElementById("TempOrderItem_ItemID").value;
    var itemName = document.getElementById("TempOrderItem_ItemID").tagName == 'INPUT' ?
         itemid : document.getElementById("TempOrderItem_ItemID").options[document.getElementById("TempOrderItem_ItemID").selectedIndex].text;
    var qtyreceived = document.getElementById("TempOrderItem_QuantityReceived").value;
    var qtyordered = document.getElementById("TempOrderItem_QuantityOrdered").value;
    var price = parseFloat(document.getElementById("TempOrderItem_Price").value).toFixed(2);
    if (parseInt(qtyreceived,10) > parseInt(qtyordered,10)) {
        document.getElementById("error").innerHTML = "Received quantity cannot be greater than ordered quantity. ";
    }
    if (price == "NaN") {
        document.getElementById("error").innerHTML += "Price must be a decimal value.";
    }
    else {
        $("#itemdialog").dialog("close");
        for (var i = 0; i < currTable.rows.length; i++) {
            if (currTable.rows[i].cells[0].innerHTML == itemid) {
                currTable.rows[i].cells[1].innerHTML = qtyordered;
                currTable.rows[i].cells[2].innerHTML = price;
                currTable.rows[i].cells[3].innerHTML = qtyreceived;
                updateTotalPrice();
                return;
            }
        }

        var newRow = currTable.insertRow(-1); // add row at next index
        newRow.innerHTML = "<td class='px-3'>" + itemName + "</td>" +
            "<td class='px-3'>" + qtyordered + "</td>" +
            "<td class='px-3'>" + price + "</td>" +
            "<td class='px-3'>" + qtyreceived + "</td>" +
            "<td class='text-center'>" +
            "<a style='width:40px' class='btn btn-primary text-white'><i class='far fa-edit'></i></a>" +
            "<a style='width:40px' class='btn btn-danger twxt-white'><i class='far fa-trash-alt'></i></a>" +
            "</td >";
        newRow.cells[4].firstChild.addEventListener("click", function () { RemoveItemFromTable(newRow.cells[4].children[0])}, false); // add listener to the button inside the cell
        newRow.cells[4].children[1].addEventListener("click", function () { Edit(newRow.cells[4].children[1]) }, false); // add listener to the button inside the cell
        updateTotalPrice();
    }
}

function RemoveItemFromTable(node) {
    var nodeToDelete = node.parentNode; // cell
    var parent = nodeToDelete.parentNode;
    parent.parentNode.removeChild(parent);
    updateTotalPrice();
}

function AddMissingEvents() {
    var myTable = $("#iTable > TBODY")[0];
    for (var i = 1; i < myTable.rows.length; i++) {
        myTable.rows[i].cells[4].firstChild.addEventListener("click", function () { RemoveItemFromTable(myTable.rows[i].cells[4].children[0]) }, false);
        myTable.rows[i].cells[4].children[1].addEventListener("click", function () { Edit(myTable.rows[i].cells[4].children[1]) }, false);
    }
}

function updateTotalPrice() {
    var myTable = $("#iTable > TBODY")[0];
    var totalCost = 0;
    for (var i = 0; i < myTable.rows.length; i++) {
        var numItems = parseInt(myTable.rows[i].cells[1].innerHTML, 10);
        var itemprice = parseFloat(myTable.rows[i].cells[2].innerHTML);
        totalCost += (itemprice * numItems);
    }
    document.getElementById("PurchaseOrderVM_OrderInfo_TotalPrice").value = totalCost.toFixed(2);
}