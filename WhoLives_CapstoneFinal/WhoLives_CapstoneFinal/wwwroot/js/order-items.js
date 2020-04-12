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
//$("#itemdialog").dialog({ autoOpen: false });

//$("#add").click(function (e) {

//    $("#itemdialog").dialog("open");
//});

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


