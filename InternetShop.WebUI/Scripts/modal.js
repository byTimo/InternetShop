function showInfo(url) {
    $.get(url, function(data) {
        $(".modal-content").html(data);
        $(".modal").modal("show");
    });
}