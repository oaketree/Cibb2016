$.extend({
    tab: function(tabp, tabul, active, normal, hasFirst) {   //去除第一个li的tab；
        //关掉所有的tab接着显示第一个tab
        $(tabp + ">div").hide();
        $(tabp + ">div:first").show();
        if (hasFirst) {
            $(tabul + " li:not(:first)").mouseover(function() {
                $(tabul + " li:not(:first)").removeClass().addClass(normal);  //所有tab li 非激活状态
                $(this).removeClass().addClass(active); //选中的tab li 激活


                $(tabp + ">div").hide(); //关闭所有的tab
                var tabid = $(this).find("a").attr("btag"); //显示选中的btab
                $(tabid).show();
                $.nowrap(tabid);
            })
        }
        else {
            $(tabul + " li").mouseover(function() {

                $(tabul + " li").removeClass().addClass(normal);
                $(this).removeClass().addClass(active);

                $(tabp + ">div").hide(); //关闭所有的tab
                var tabid = $(this).find("a").attr("btag"); //显示选中的btab
                $(tabid).show();
                $.nowrap(tabid);
            })
        }
        $(tabul + " li").click(function() {
            var href = $(this).find("a").attr("href")
            if (href != "#")
                window.open(href);
            return false;
        })       //链接不点击进去
    }, picture: function(pList, active, normal, t) {
        var pNum = $(pList + " a").length;
        var n = pNum;
        while (n) {
            $("<li>" + n + "</li>").addClass(normal).appendTo(pList + "title ul");   //设置默认的按钮
            n--
        }
        $(pList + "title ul>li:last-child").removeClass().addClass(active); //第一个按钮激活
        $(pList + "bg a").html($(pList + " a").eq(0).find("img").attr("btag")); //设置第一个透明层文字
        $(pList + "bg a").attr("href", $(pList + " a").eq(0).attr("href")); //设置第一个透明层链接
        $(pList + " a").eq(0).show(); //显示第一张图片


        var pidx = 0; //真ID号
        $(pList + "title ul li").mouseover(function() {
            var idx = $(this).index(); //index从0开始索引假ID
            pidx = pNum - idx - 1; //真ID号
            //e.stopPropagation();
            $(pList + " a").stop(true, true);
            showImg(pidx);
        });
        function showImg(n) {
            $(pList + "title ul li").removeClass().addClass(normal); //删除所有按钮效果
            $(pList + "title ul li").eq(pNum - n - 1).removeClass().addClass(active); //显示点击到的按钮效果

            $(pList + "bg a").html($(pList + " a").eq(n).find("img").attr("btag"));   // 设置透明层文字
            $(pList + "bg a").attr("href", $(pList + " a").eq(n).attr("href"));   //设置透明层链接
            $(pList + " a").hide().eq(n).show(); //显示图片
        }
        function showImg2(n, last) {
            $(pList + "bg a").html($(pList + " a").eq(n).find("img").attr("btag"));   // 设置透明层文字
            $(pList + "bg a").attr("href", $(pList + " a").eq(n).attr("href"));   //设置透明层链接

            $(pList + " a").eq(last).fadeOut(500, function() {
                $(pList + "title ul li").removeClass().addClass(normal); //删除所有按钮效果
                $(pList + "title ul li").eq(pNum - n - 1).removeClass().addClass(active); //显示点击到的按钮效果
                $(pList + " a").eq(n).fadeIn(500); //显示图片
            })
        }
        //var m = 0;
        function auto() {
            var last = pidx;
            pidx = pidx >= (pNum - 1) ? 0 : ++pidx;
            showImg2(pidx, last);
        }

        var interval = setInterval(auto, t);

        $(pList).parent().hover(function() {
            clearInterval(interval);
            $(pList + " a").stop(true, true);
        }, function() {
            interval = setInterval(auto, t);
        })
    }, clearDefaultText: function(id) {
        var txt = $(id).val();
        $(id).bind({
            focus: function() {
                if ($(id).val() == txt)
                    $(id).val("");
            }, blur: function() {
                if ($(id).val() == "")
                    $(id).val(txt);
            }
        });
    }, clearDefaultText2: function() {
        for (var n = 0; n < arguments.length; n++) {
            $.bindText($(arguments[n]));
        }
    }, bindText: function(a) {
        var txt = a.val();
        a.bind({
            focus: function() {
                a.val("");
            },
            blur: function() {
                a.val(txt);
            }
        })
    }, ad: function(aList, active, normal, t) {
        var adNum = $(aList + " a").length;
        var n = adNum;
        while (n) {
            $("<li></li>").addClass(normal).appendTo(aList + "title ul");   //设置默认的按钮
            n--
        }
        $(aList + "title ul>li:last-child").removeClass().addClass(active); //第一个按钮激活

        var aidx = 0; //真ID号
        $(aList + "title ul li").mouseover(function() {
            var idx = $(this).index(); //index从0开始索引,假ID
            aidx = adNum - idx - 1; //真ID号
            //e.stopPropagation();       
            showImg(aidx);

        });
        function showImg(n) {
            $(aList + " a").hide().eq(n).show(); //显示上图片
            $(aList + "small ul").hide().eq(n).show(); //显示下图片
            $(aList + "title ul li").removeClass().addClass(normal); //删除所有按钮效果
            $(aList + "title ul li").eq(adNum - n - 1).removeClass().addClass(active); //显示点击到的按钮效果
        }
        function auto() {
            aidx = aidx >= (adNum - 1) ? 0 : ++aidx;
            showImg(aidx);
        }
        var interval = setInterval(auto, t);
        $(aList).parent().hover(function() {
            clearInterval(interval);
        }, function() {
            interval = setInterval(auto, t);
        })
        $(aList).parent().next().hover(function() {
            clearInterval(interval);
        }, function() {
            interval = setInterval(auto, t);
        })
    }, roll: function(id, t) {
        var li = $(id + " ul li");
        var ul = $(id + " ul");
        //var maxLength = $(id)[0].scrollHeight;//div真实高度不同浏览器不一致不用
        var hgt = $(id).innerHeight(); //div显示高度
        var lh = li.outerHeight(); //层高
        var cs = hgt / lh; //层数
        var num = li.size();
        var maxheight = num * lh;
        var ys = maxheight % hgt; //多出来的格子
        //console.log(maxLength);

        if (ys != 0) {
            var bc = cs - (ys / lh); //不满要补充的格子
            for (var i = 0; i < bc; i++) {
                ul.append("<li>&nbsp;</li>");
            }
        }
        num = li.size();
        maxheight = num * lh;

        //ul.append(li.slice(0).clone());//复制


        var le = $(id).scrollTop();
        showImg(le); //初始化

        function showImg(n) {
            $(id).scrollTop(le);
        }
        function auto() {
            if (le >= maxheight - hgt)
                le = 0;
            else {
                var t = le % lh;
                le = le + hgt - t;

            }

            showImg(le);
        }
        var interval = setInterval(auto, t);


        $(id).hover(function() {
            clearInterval(interval);
            $(id).css("overflow-y", "scroll");
        }, function() {
            interval = setInterval(auto, t);
            $(id).css("overflow-y", "hidden");
        })
        $(id).scroll(function() {
            le = $(id).scrollTop();
        })

    }, fixwidth: function(id) {
        var ul = $(id + " ul");
        var li = $(id + " ul li");
        var max = li.width();
        li.each(function() {
            var text = $(this).text();
            $("#word-cal").text(text);
            var width = $("#word-cal").width();
            if (width > max) {
                var stop = Math.floor(text.length * max / width);
                var temp_str = text.substring(0, stop);
                $(this).children().text(temp_str);
            }
        })
    }, rollTop: function(id, t) {
        var lh = $(id).innerHeight();
        var h = $(id)[0].scrollHeight;

        var le = $(id).scrollTop();
        showImg(le); //初始化

        function showImg(n) {
            $(id).scrollTop(le);
        }
        function auto() {
            if (le >= h - lh)
                le = 0;
            else {
                var t = le % lh;
                le = le + lh - t;
            }
            showImg(le);
        }
        var interval = setInterval(auto, t);
        $(id).hover(function() {
            clearInterval(interval);
        }, function() {
            interval = setInterval(auto, t);
        })
        $(id).scroll(function() {
            le = $(id).scrollTop();
        })
    }, ad2014: function(id, callback) {
        callback(id);
        $(window).scroll(function() {
            callback(id);
        });
        $(window).resize(function() {
            callback(id);
        });
    }, showAd: function(id) {
        var txt = $(id).text();
        var adHight = $(id).offset().top;
        var scr = $(document).scrollTop();
        var h = $(window).height();
        var th = scr + h;
        var re = /^\d+$/;
        if (re.test(txt)) {
            if (adHight <= th) {
                $.post("WebHandler/AdHandler.ashx", { data: txt }, function(result) {
                    if (result == "")
                        $(id).text("广告不存在或已过期!");
                    else {
                        //console.log(result);
                        var r = $.parseJSON(result);
                        if (r.type == "flash") {
                            $(id).html("");
                            $(id).flash({
                                src: r.path + r.name + "?clickUrl=" + r.link,
                                width: r.width,
                                height: r.height
                            });
                        }
                        else {
                            $(id).adimg({
                                href: r.link,
                                src: r.path + r.name,
                                width: r.width,
                                height: r.height
                            });
                        }
                    }
                });
            }
        }
    }, showTopAd: function(id) {
        var txt = $(id).text();
        var adHight = $(id).offset().top;
        var scr = $(document).scrollTop();
        var h = $(window).height();
        var th = scr + h;
        var re = /^\d+$/;
        if (re.test(txt)) {
            if (adHight <= th) {
                $.post("WebHandler/AdHandler.ashx", { data: txt }, function(result) {
                    if (result == "")
                        $(id).text("广告不存在或已过期!");
                    else {
                        //console.log(result);
                        var r = $.parseJSON(result);
                        if (r.type == "flash") {
                            $(id).html("");
                            $(id).flash({
                                src: r.path + r.name + "?clickUrl=" + r.link,
                                width: r.width,
                                height: r.height
                            });
                        }
                        else {
                            $(id).adimg({
                                href: r.link,
                                src: r.path + r.name,
                                width: r.width,
                                height: r.height
                            });
                        }
                    }
                });
            }
        }

        function autoclose() {
            $(id).hide();
        }
        var tid = setTimeout(autoclose, 10000);
        $(id).hover(function() {
            clearTimeout(tid);
        }, function() {
            tid = setTimeout(autoclose, 10000);
        });
    }, showyoukuAd: function(id) {
        var adHight = $(id).offset().top;
        var scr = $(document).scrollTop();
        var h = $(window).height();
        var th = scr + h;
        var txt = $(id).text();
        var re = /^youku$/;
        if (re.test(txt)) {
            if (adHight <= th) {
                $(id).html("");
                $(id).flash({
                    src: "http://player.youku.com/player.php/sid/XNDg3NjM0MzQw/v.swf",
                    width: 287,
                    height: 202
                }); //优酷广告
            }
        }
    }, login: function(bid, n, p, s) {
        $(s + "001").click(function() {
            $(s).hide();
            $(s).prev().show();
            return false;
        })
        $(s + "003").click(function() {
            $.post("WebHandler/LoginOut.ashx", function(result) {
                if (result == true) {
                    $(s).prev().show();
                    $(s).hide();
                }
            });
        })
        $(bid).click(function() {
            var username = $(n).val();
            var password = $(p).val();

            $("#form1").submit(function() {
                return false;
            })
            $.post("WebHandler/LoginHandler.ashx", { username: username, password: password }, function(result) {
                if (result == "") {
                    $(s).prev().hide();
                    $(s).show();
                    $(s + "01").html("&nbsp; &nbsp; 您的用户名或密码错误！&nbsp;")
                    $(s + "001").show();
                    $(s + "002").hide();
                    $(s + "003").hide();
                } else {
                    $(s + "01").html("您好！" + result);
                    $(s).prev().hide();
                    $(s).show();
                    $(s + "001").hide();
                    $(s + "002").show();
                    $(s + "003").show();
                }
            });
        });
    }, checkSession: function(s) {
        $.post("WebHandler/CheckSession.ashx", function(result) {
            if (result != "") {
                $(s + "01").html("您好！" + result);
                $(s).prev().hide();
                $(s).show();
                $(s + "001").hide();
                $(s + "002").show();
                $(s + "003").show();
            }
        })
    }, sendMail: function(bid) {
        $("#ImageButton3,#ImageButton4").click(function() {
            $("#mid04right06_1").show();
            $("#mid04right07").show();
            $("#mid04right06_3").hide();
            $("#mid04right06_2").hide();
        })
        $(bid).click(function() {
            $("#form1").submit(function() {
                return false;
            })


            if ($("#upfile").val() != "") {
                var type = [".doc", ".docx"];
                var fileType = $("#upfile").val().substring($("#upfile").val().lastIndexOf(".")).toLocaleLowerCase();

                if ($.inArray(fileType, type) != -1) {

                    $("#form1").ajaxSubmit({
                        url: 'WebHandler/SendMail.ashx', /*设置post提交到的页面*/
                        type: "post", /*设置表单以post方法提交*/
                        dataType: "text", /*设置返回值类型为文本*/
                        success: function(result) {
                            if (result == "") {
                                $("#mid04right06_3").show();
                                $("#mid04right06_2").hide();
                                $("#mid04right07").show();
                                $("#mid04right06_1").hide();
                            }
                            else {
                                $("#mid04right06_3").hide();
                                $("#mid04right07").hide();
                                $("#mid04right06_2").show();
                                $("#mid04right06_1").hide();
                                $("#mid04right06_2 span").html("文章编号：T" + result);

                            }
                        }
                    });
                } else {
                    $("#mid04right06_3").show();
                    $("#mid04right06_2").hide();
                    $("#mid04right07").show();
                    $("#mid04right06_1").hide();
                }
            }

        });
    }, searchprogress: function(bid) {
        $("#ImageButton6").click(function() {
            $("#mid04right09").show();
            $("#mid04right09_2").hide();
        });
        $(bid).click(function() {
            $("#form1").submit(function() {
                return false;
            })
            $("#mid04right09_2").hide();
            var bh = $("#textfield4").val().trim();
            var na = $("#textfield5").val().trim();
            if (bh != "" && na != "") {
                $.post("WebHandler/SearchProgress.ashx", { bh: bh, na: na }, function(result) {
                    $("#mid04right09_2").show();
                    $("#mid04right09_2 span").html("审稿进度：" + result);
                    $("#mid04right09").hide();
                });
            }
        })
    }, standardsearch: function(bid) {
        $("#form1").submit(function() {
            return false;
        })
        $(bid).click(function() {
            var sel = $("#select").val();
            var te = $("#search").val().trim();
            if (te != "") {
                $.post("WebHandler/StandardSearch.ashx", { sel: sel, te: te }, function(result) {
                    if (result == "0") {
                        $("#search").val("");
                        $("#search").attr("placeholder", "暂无查询结果");
                    }
                    if (result == "1") {
                        $("#search").attr("placeholder", "");
                        window.open("/technology/tech_com.aspx?type=" + sel + "&q=" + te);
                        //window.showModalDialog("/technology/tech_com.aspx?type=" + sel + "&q=" + te);
                    }
                });
            }
        });
        $("#search").keyup(function() {
            if (event.keyCode == 13)
                $(bid).click();
        });
    }, nowrap: function(id) {
        var li = $(id + " li");
        var n = li.length;
        for (var i = 0; i < n; i++) {
            var text = $(id + " li:eq(" + i + ") a").html();
            //var text = $(id + " li a").eq(i).html();
            var tn = text.length;
            if (li.eq(i).height() > 26) {
                for (j = 1; j < tn; j++) {
                    var t = text.slice(0, -j);
                    //$(id + " li a").eq(i).html(t);
                    $(id + " li:eq(" + i + ") a").html(t);
                    if (li.eq(i).height() <= 26)
                        break;
                }
            }
        }
    }, nowrap2: function(id, h) {
        var text = $(id + " p span").text();
        var tn = text.length;
        if ($(id + " p").outerHeight() > h) {
            for (j = 1; j < tn; j++) {
                var t = text.slice(0, -j);
                $(id + " p span").html(t);
                if ($(id + " p").outerHeight() <= h)
                    break;
            }
        }
    }, nowrap3: function(id, h) {
        var span = $(id + " li span")
        var n = span.length;

        for (var i = 0; i < n; i++) {
            var text = span.eq(i).html();
            var tn = text.length;
            if ($(id + " span").eq(i).outerHeight() > h) {
                for (j = 1; j < tn; j++) {
                    var t = text.slice(0, -j);
                    span.eq(i).html(t);
                    if ($(id + " span").eq(i).outerHeight() <= h)
                        break;
                }
            }
        }
    }, nowrap4: function(id, h) {
        var text = $(id).text();
        var tn = text.length;
        if ($(id).outerHeight() > h) {
            for (j = 1; j < tn; j++) {
                var t = text.slice(0, -j);
                $(id).text(t);
                if ($(id).outerHeight() <= h)
                    break;
            }
        }
    }, duilian: function(id) {
        var menuYloc = parseInt(50);
        $(window).scroll(function() {
            var offset = menuYloc + $(document).scrollTop() + "px";
            $(id).animate({ top: offset }, { duration: 500, queue: false });
        })


        $.ad2014(id + "_img", $.showAd);

        $(id + "_close").click(autoclose);

        $(id + "_close").hover(function() {
            $(id + "_close").css({ "background": "#0066cc" });
        }, function() {
            $(id + "_close").css({ "background": "#999999" });
        });


        function autoclose() {
            $(id).hide();
        }

        //        var tid = setTimeout(autoclose, 60000);

        //        $(id).hover(function() {
        //            clearTimeout(tid);
        //        }, function() {
        //            tid = setTimeout(autoclose, 30000);
        //        });


    }, productad2014: function(id) {
        var txt = $(id + " li:eq(0)").text();
        var adHight = $(id).offset().top;
        var scr = $(document).scrollTop();
        var h = $(window).height();
        var th = scr + h;
        var re = /^\d+$/;
        if (re.test(txt)) {
            if (adHight <= th) {
                $.post("WebHandler/AdHandler.ashx", { data: txt }, function(result) {
                    if (result == "") {
                        $(id + " li:eq(0)").text("广告不存在或已过期!");
                        $(id + " li:eq(1)").text("广告不存在或已过期!");
                    }
                    else {
                        //console.log(result);
                        var r = $.parseJSON(result);
                        if (r.type == "flash") {
                            $(id + " li:eq(0)").html("");
                            $(id + " li:eq(0)").flash({
                                src: r.path + r.name + "?clickUrl=" + r.link,
                                width: r.width,
                                height: r.height
                            });
                            $(id + " li:eq(1)  a").attr({ href: r.link, target: "_blank" });
                            $(id + " li:eq(1)  a").text(r.title);
                        }
                        else {
                            $(id + " li:eq(0)").adimg({
                                href: r.link,
                                src: r.path + r.name,
                                width: r.width,
                                height: r.height
                            });
                            $(id + " li:eq(1)  a").attr({ href: r.link, target: "_blank" });
                            $(id + " li:eq(1)  a").text(r.title);
                        }
                    }
                });
            }
        }
    }
})



