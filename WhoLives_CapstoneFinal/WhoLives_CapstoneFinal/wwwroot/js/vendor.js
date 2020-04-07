var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        responsive: { details: true },
        "ajax": {
            "url": "/api/vendor/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "vendorName", 
                "render": function (data, type, row) {
                    if (type == 'display') {
                        data = '<a href="/vendor/upsert?vendorID=' + row.vendorID + '">' + data + '</a>';
                    }
                    return data;
                },
                "width": "25%",
            },
            {
                "data": "vendorWebsite", "width": "25%",
                "render": function (data, type) {
                    if (type == 'display') {
                        if (data != null) {
                            data = '<a href="' + data + '">' + data + '</a>';
                        }
                        else {
                            data = "";
                        }
                    }
                    return data;
                }
            },
            { "data": "phoneNumber", "width": "25%"},
            {
                "data": "vendorID",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/vendor/upsert?vendorID=${data}" class="btn btn-primary text-white" style="cursor:pointer; width:40px;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:40px;" onclick=Delete('/api/vendor/'+${data})>
                                    <i class="far fa-trash-alt"></i>
                                </a>
                             </div>`
                },
                "width": "25%"
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