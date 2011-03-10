initializeCommandBar = function () {
    $(".validation-summary-errors").addClass("ui-state-error-text");
    $("#commandBar a, input[type=submit], button").button();
    $("#commandBar").addClass("ui-widget-header ui-corner-all");
    $(".editCommand").prepend('<span class="ui-icon ui-icon-pencil" />');
}

setFocus = function () {
    $("input[type=text]").first().focus();
    $(".input-validation-error").first().focus();
}

$(function () {
    initializeCommandBar();
    setFocus();
});

