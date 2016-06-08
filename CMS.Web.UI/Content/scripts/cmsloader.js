$(function () {

    $.startHeartBeat();
    $("#navmenu a").click(function () {
        if (typeof ($(this).attr("data-options")) == "undefined")
            return true;
        var _optstr = "{" + $(this).attr("data-options") + "}";
        _optstr = _optstr.replace(/'/g, "\"");
        var _opts = $.parseJSON(_optstr);
       $. addTab(_opts.title, _opts.href, _opts.icons)
        return false;
    })

    $("#mainTabs").tabs({
        fit: true,
        border: false
    })

})