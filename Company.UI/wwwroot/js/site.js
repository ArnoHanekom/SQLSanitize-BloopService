$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

function fnScrollToTop() {
    $("section.main-word-list").animate({
        scrollTop: "0"
    });
}

$(".modal[id^='edit-word-']").on('shown.bs.modal', function () {
    $(this).find("#WordDefinition").focus();
});

$("#add-word.modal").on('shown.bs.modal', function () {
    $(this).find("#WordDefinition").focus();
});

$(".main-word-list").on("scroll", function () {
    if ($(this).scrollTop() > 0)
        $(".scroll-to-top").addClass("show");
    else
        $(".scroll-to-top").removeClass("show");
});
