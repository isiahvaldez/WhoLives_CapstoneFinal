function process() {
    //Loop through the Table rows and build a JSON array.
    var OrderItems = new Array();
    $("#iTable TBODY TR").each(function () {
        var row = $(this);
        var OrderItem = {};
        OrderItem.iName = row.find("TD").eq(0).html();
        OrderItem.iQty = row.find("TD").eq(1).html();
        OrderItem.iPrice = row.find("TD").eq(2).html();
        OrderItems.push(OrderItem);
    });
    //Send the JSON array to Controller using AJAX.
    console.log(OrderItems);
    $.ajax({
        type: "POST",
        url: @Url.Page("PurchaseOrders/Upsert", "SaveOrder"),
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: JSON.stringify({
            OrderItems
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
        },
        failure: function (response) {
            alert(response);
        }
    });

}

function RemoveRow(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    //Get the reference of the Table.
    var table = $("#iTable")[0];
    //Delete the Table row using it's Index.
    table.deleteRow(row[0].rowIndex);
}
function AddRow() {
    //Get the reference of the Table.
    var table = $("#iTable")[0];
    var item = new {
        ItemID = 0,
        QuantityOrdered = 0,
        Price = 0,
        QuantityReceived = 0,
        DateDelivered = DateTime.Now,
        PurchaseOrderID = Model.PurchaseOrderVM.OrderInfo.PurchaseOrderID,
        ItemReceived = false,
        LastModifiedBy = "upsert",
        LastModifiedDate = DateTime.Now
    };
    Model.PurchaseOrderVM.OrderInfo.OrderItems.Add(new
        {
            ItemID = 0,
            QuantityOrdered = 0,
            Price = 0,
            QuantityReceived = 0,
            DateDelivered = DateTime.Now,
            PurchaseOrderID = Model.PurchaseOrderVM.OrderInfo.PurchaseOrderID,
            ItemReceived = false,
            LastModifiedBy = "upsert",
            LastModifiedDate = DateTime.Now
        });
    table.insertRow(1);
}
function RemoveRow(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    //Get the reference of the Table.
    var table = $("#order")[0];
    //Delete the Table row using it's Index.
    table.deleteRow(row[0].rowIndex);
}
//function AddRow() {
//    //Get the reference of the Table.
//    var table = document.getElementById("order");
//    var item = new {
//        ItemID = 0,
//        QuantityOrdered = 0,
//        Price = 0,
//        QuantityReceived = 0,
//        DateDelivered = DateTime.Now,
//        PurchaseOrderID = Model.PurchaseOrderVM.OrderInfo.PurchaseOrderID,
//        ItemReceived = false,
//        LastModifiedBy = "upsert",
//        LastModifiedDate = DateTime.Now
//    };
//    Model.PurchaseOrderVM.OrderInfo.OrderItems.Add(new
//        {
//            ItemID = 0,
//            QuantityOrdered = 0,
//            Price = 0,
//            QuantityReceived = 0,
//            DateDelivered = DateTime.Now,
//            PurchaseOrderID = Model.PurchaseOrderVM.OrderInfo.PurchaseOrderID,
//            ItemReceived = false,
//            LastModifiedBy = "upsert",
//            LastModifiedDate = DateTime.Now
//        });
//    var rowCount = table.rows.length;
//    var row = table.insertRow(rowCount);

//    //var ItemCell = row.insertCell(0);
//    //var eDropDown = document.createElement("select");
//    //eDropDown.type = "select";
//    //eDropDown.name = "itemlist";
//    //ItemCell.appendChild(eDropDown);

//    //var OrderQtyCell = row.insertCell(0);
//    //var eOrderQty = document.createElement("input");
//    //eOrderQty.type = "text";
//    //eOrderQty.name = "orderqty";
//    //OrderQtyCell.appendChild(eOrderQty);

//    //var PriceCell = row.insertCell(0);
//    //var ePrice = document.createElement("input");
//    //ePrice.type = "text";
//    //ePrice.name = "price";
//    //PriceCell.appendChild(ePrice);

//    //var RQtyCell = row.insertCell(0);
//    //var eRQty = document.createElement("input");
//    //eRQty.type = "text";
//    //eRQty.name = "receivedqty";
//    //RQtyCell.appendChild(eRQty);
//}