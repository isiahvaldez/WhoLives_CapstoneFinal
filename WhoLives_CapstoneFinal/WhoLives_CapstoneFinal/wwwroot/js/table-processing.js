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