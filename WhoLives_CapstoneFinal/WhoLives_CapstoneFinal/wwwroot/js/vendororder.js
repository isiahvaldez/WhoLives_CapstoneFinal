var dataTable;

// IMPORTANT: URLSearchParams is not supported by Internet Explorer or Opera right now - 03/30/2020 IV 
var queryString = window.location.search;
var urlParams = new URLSearchParams(queryString);

var id = urlParams.get("vendorID");

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "/api/vendororder/" + id,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "po",
                "width": "18%"
            },
            {
                "data": "dateOrdered",
                "render": function (data) {
                    var dateString = new Date(data);
                    var month = ("0" + (dateString.getMonth() + 1)).slice(-2);
                    var day = ("0" + (dateString.getDate())).slice(-2);
                    var year = dateString.getFullYear();
                    //var newDate = Date(month, day, year);
                    return `<div class="text-center">` + month + `/` + day + `/` + year + `</div>`
                },
                "width": "25%"
            },
            {
                "data": "status.name",
                "width": "20%"
            },
            {
                "data": "totalPrice",
                "render": function (data) {
                    return `<div class="text-center">\$${data}</div>`;
                },
                "width": "18%"
            },
            {
                "data": "purchaseOrderID",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/purchaseorders/upsert?id=${data}" class="btn btn-primary" style="cursor:pointer; width: 40px">
                                <i class="far fa-edit"></i>
                            </a>
                        </div>`;
                },
                "width": "19%"
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
                        window.location.href = "./Index";
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}