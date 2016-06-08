$.extend({
    nowrap: function (id) {
        $(id).each(function () {
            var objLength = $(this).text().length;
            var objString = $(this).text();
            var objHeight = $(this).height();
            var lineHeight = $(id).css("line-height").slice(0, -2);
            while (objHeight > lineHeight) {
                objLength = objLength - 1;
                objString = objString.substring(0, objLength);
                $(this).text(objString);
                objHeight = $(this).height();
            }
        })
    }, nowraplist: function (id,lh) {
        $(id).each(function () {
            var objLength = $(this).text().length;
            var objString = $(this).text();
            var objHeight = $(this).height();
            while (objHeight >lh) {
                objLength = objLength - 1;
                objString = objString.substring(0, objLength);
                $(this).text(objString);
                objHeight = $(this).height();
            }
        })
    }, zzsc: function () {
        n = {
            start: function (num) {
                for (i = 1; i <= num; i++) {
                    $("<b>" +i + "</b>").appendTo("#focus_image .controler");
                }//初始化按钮列表
                $("#focus_image .controler b").eq(0).addClass("down");//初始化第一个按钮
            }, showImg: function (before, idx) {
                if (before == idx)
                    return;
                $("#focus_image div[data-scro='list'] .item").eq(before).animate({ left: -$("#focus_image div[data-scro='list']").width() }, "slow", function () {
                    $(this).removeAttr("style");//动作结束后移除style
                });//图片移动位置
                $("#focus_image div[data-scro='controler'] b").removeClass("down").eq(idx).addClass("down");
                $("#focus_image div[data-scro='list'] .item").eq(idx).animate({ left: "0px" }, "slow");//下张图移动到0px
                //$("#focus_image div[data-scro='list'] .item").eq(before).removeAttr("style");
            }
        }
        var num = $("#focus_image .pages a").size();
        n.start(num);
        var idx = 0;
        $("#focus_image div[data-scro='controler'] b").mouseover(function () {
            var before = idx;
            idx = $(this).index(); //index从0开始索引
            $("#focus_image div[data-scro='list'] .item").stop(true, true);
            n.showImg(before, idx);
            
        })
        function auto() {
            var before = idx;
            idx = idx >= (num - 1) ? 0 : ++idx;
            n.showImg(before,idx);
        }
        var interval = setInterval(auto, 5000);

        $("#bd").hover(function () {
            clearInterval(interval);
            $("#focus_image div[data-scro='list'] .item").stop(true, true);
        }, function () {
            interval = setInterval(auto, 5000);
        })


        $("#focus_image a").click(function () {
            var href = $(this).attr("href")
            if (href != "#")
                window.open(href);
            return false;
        })       //链接不点击进去


    }, staticLeft: function (id) {
        //var menuYloc = parseInt(20);
        $(window).scroll(function () {
            if ($(document).scrollTop() > 380) {
                //var height = menuYloc + $(document).scrollTop() - 380 + "px"
                //$(id).css({ "padding-top": height });
                $(id).css({ "position": "fixed","top":"0px"});

            } else {
                $(id).css({ "position": "static" });
                //$(id).css({ "padding-top": "20px" });

            }
            
        })
    }
});