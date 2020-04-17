
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
    var itemName = document.getElementById("TempOrderItem_ItemID").options[document.getElementById("TempOrderItem_ItemID").selectedIndex].text;
    var qtyreceived = document.getElementById("TempOrderItem_QuantityReceived").value;
    var qtyordered = document.getElementById("TempOrderItem_QuantityOrdered").value;
    var price = document.getElementById("TempOrderItem_Price").value;

    for (var i = 0; i < currTable.rows.length; i++) {
        if (currTable.rows[i].cells[0].innerHTML == itemid) {
            currTable.rows[i].cells[1].innerHTML = qtyordered;
            currTable.rows[i].cells[2].innerHTML = price;
            currTable.rows[i].cells[3].innerHTML = qtyreceived;
            return;
        }
    }

    var newRow = currTable.insertRow(-1); // add row at next index
    newRow.innerHTML = "<td class='px-3'>" + itemName + "</td>" +
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

function RemoveItemFromTable() {
    var nodeToDelete = this.parentNode; // cell
    var parent = nodeToDelete.parentNode;
    parent.parentNode.removeChild(parent);
}

function AddMissingEvents() {
    var myTable = $("#iTable > TBODY")[0];
    var item;
    for (var i = 1; i < myTable.rows.length; i++) {
        myTable.rows[i].cells[4].firstChild.addEventListener("click", RemoveItemFromTable);
        myTable.rows[i].cells[4].firstChild.addEventListener("click", Edit);
        item = myTable.rows[i].cells[0].innerHTML;
        item = item.replace(/[^0-9]/g, "");
        myTable.rows[i].cells[1].addEventListener("click", function (item) { return function () { SetSelectedItem('itemList', item); } }(item));
    }
    document.getElementById("itemList").selectedIndex = 0;
}


function SetSelectedItem(dropdownID, itemID) {
    console.log("setting item to " + itemID)
    $("#" + dropdownID).val(itemID);
}