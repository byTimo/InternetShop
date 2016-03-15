$(document).ready(function () {
    var value = $(".combobox :selected").val();
    if (value === "0")
        $("#audio-parameters").removeClass('hidden');
    else
        $("#video-parameters").removeClass("hidden");

    $(".combobox").change(function () {
        var value = $(".combobox :selected").val();
        if (value === "0") {
            $("#audio-parameters").removeClass('hidden');
            $("#video-parameters").addClass("hidden");
        } else {
            $("#video-parameters").removeClass("hidden");
            $("#audio-parameters").addClass("hidden");
        }
    });
});