$(function () {
    $("#menu li a").click(function () {
        $(this).next(".sub_menu").slideToggle();
    });
});