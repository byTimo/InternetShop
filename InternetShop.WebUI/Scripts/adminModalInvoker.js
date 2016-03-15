$(document).ready(function() {
    $(".edit-product").click(function() {
        var url = "/Admin/EditProduct";
        var id = $(this).attr("data-id");
        invokeModal(url + "?productId=" + id);
    });
});