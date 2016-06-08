$(function () {
    $("#top04_01>div").hover(function () {
        $(this).addClass("dh_bg");
    }, function () {
        $(this).removeClass("dh_bg");
    });

    $("#jwen_gz li:even").addClass("jwen_gzlieven");
    $("#jwen_gz li:odd").addClass("jwen_gzliodd");
    $(".jwen_yc01 li:even").addClass("jwen_yclieven");
    $(".jwen_yc01 li:odd").addClass("jwen_ycliodd");

    //$.nowrap("div[id^=jwen] a");
    $.nowrap("#jwen_gz a");
    $.nowrap(".jwen_yc01 a");
    $.nowrap(".wen_main_title a");
    $.nowraplist(".wen_main_np a", 90);
    $.nowraplist(".wen_main_par a", 60);

    $.post("/News/MenuAsync", { category: "@Model.CurrentCategory" }, function (data) {
        $("#t").html(data);
    });
});