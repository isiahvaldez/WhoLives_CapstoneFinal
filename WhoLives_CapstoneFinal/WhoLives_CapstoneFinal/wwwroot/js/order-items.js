var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        responsive: { details: true },
        "ajax": {
            "url": "/api/order/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "item",
                "width": "25%",
            },
            {
                "data": "quantityOrdered",
                "width": "20%"
            },
            {
                "data": "price",
                "width": "25%"
            },
            {
                "data": "quantityReceived",
                "width": "20%"
            },
            {
                "data": "orderItemID",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:40px;" onclick=Delete('/api/vendor/'+${data})>
                                    <i class="far fa-trash-alt"></i>
                                </a>
                             </div>`
                },
                "width": "10%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

$("#itemdialog").dialog({ autoOpen: false });

$("#add").click(function (e) {
    $("#itemdialog").dialog("open");
});

$("#save").click(function (e) {
    e.preventDefault();
    e.stopPropagation();
    $.ajax({
        url: "/PurchaseOrders/Upsert?id=",
        data: {
            //name: function () { $("#id").val(); }
        },
        success: function () {
            $("#itemdialog").dialog("close");
            dataTable.fnDraw();
        }
    });
});