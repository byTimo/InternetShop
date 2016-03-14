$(document).ready(function() {
    $(".edit-product").click(function() {
        var url = "/Admin/EditProduct";
        var id = $(this).attr("data-id");
        $.get(url + "?productId=" + id, function(data) {
            $("#edit-product-modal-content").html(data);
            $("#edit-product-modal").modal("show");
        });
    });
});