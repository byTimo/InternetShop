$(document).ready(function() {
    $("#input-image").bind({
        change: function() {
            displayFiles(this.files[0]);
        }
    });

    function displayFiles(file) {

        if (!file.type.match(/image.*/)) {
            return;
        }
        var img = $("#image");
        var reader = new FileReader();
        reader.onload = (function(aImg) {
            return function(e) {
                aImg.attr('src', e.target.result);
            };
        })(img);
        reader.readAsDataURL(file);
    }
});
