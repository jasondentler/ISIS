initializeCommandBar = function () {
    $("#commandBar a, input[type=submit], button").button();
    $("#commandBar").addClass("ui-widget-header ui-corner-all");
}

setFocus = function () {
    $("input[type=text]").first().focus();
    $(".input-validation-error").first().focus();
}

$(function () {
    initializeCommandBar();
    setFocus();
});

