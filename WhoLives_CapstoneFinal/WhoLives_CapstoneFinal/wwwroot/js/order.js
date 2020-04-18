var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').dataTable({
        initComplete: function () {
            this.api().columns([2]).every(function () {
                var column = this;
                var select = $('<select><option value="">--Select All--</option></select>')
                    .appendTo($(column.header()))
                    .on('change',
                    function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    }
                );
                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        },
        responsive: { details: true },
        "ajax": {
            "url": "/api/order",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "vendor.vendorName",
                "render": function (data, type, row) {
                    if (type == 'display') {
                        data = '<a href="/purchaseorders/upsert?id=' + row.purchaseOrderID + '">' + data + '</a>';
                    }
                    return data;
                },
                "width": "20%"
            },
            {
                "data": "po",
                "render": function (data, type, row) {
                    if (type == 'display') {
                        data = '<a href="/purchaseorders/upsert?id=' + row.purchaseOrderID + '">' + data + '</a>';
                    }
                    return data;
                },
                "width": "20%"
            },
            {
                "data": "status.name",
                "width": "20%"
            },
            {
                "data": "purchaseOrderID",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/purchaseorders/upsert?id=${data}" class="btn btn-primary" style="cursor:pointer; width: 100px">
                                <i class="far fa-edit"></i>
                            </a>
                            <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/order/'+${data})>
                                <i class="far fa-trash-alt"></i>
                            </a>
                        </div>`;
                },
                "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No purchase orders to display."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data! Any items marked received will remain in the inventory.",
        icon: "warning",
        buttons: true,
        dangerMode: true,
        confirmButtonColor: '#428bca'
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        window.location.href = '../PurchaseOrders/Index'
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}