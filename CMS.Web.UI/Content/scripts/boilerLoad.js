$(function() {
    $.tab("#mid01left02", "#tabul01", "mid01left02tab02", "mid01left02tab01", true); //脱销板块
    $.tab("#mid02left04", "#tabul02", "mid02left04tab02", "mid02left04tab01", true);
    $.tab("#mid04left01", "#tabul03", "mid04left01tab02", "mid04left01tab01", false);
    $.tab("#mid04left08", "#tabul04", "mid04left08tab02", "mid04left08tab01", false);
    $.tab("#mid04right02", "#tabul05", "mid04right02tab02", "mid04right02tab01", false);
    $.tab("#mid04right26", "#tabul06", "mid04right26tab02", "mid04right26tab01", false);
    $.tab("#mid04right03", "#tabul07", "mid04right03tab02", "mid04right03tab01", true);
    $.picture("#newstu1", "news101", "news102", 4000); //图片新闻

    //$.clearDefaultText2("#textfield3", "#textfield4", "#textfield5");
    //$.clearDefaultText("#textfield4");
    //$.clearDefaultText("#textfield5");
    $.login("#ImageButton1", "#textfield", "#textfield2", "#youxiangchong"); //登录
    $.checkSession("#youxiangchong"); //检测session是否为空
    $.sendMail("#ImageButton2");
    $.searchprogress("#ImageButton5"); //查看文章进度
    $.standardsearch("#Image1"); //标准搜索
    $.ad("#newstu2", "newstu2_2", "newstu2_1", 2000); //下方广告
    $("#upfile").change(function() {
        if ($(this).val() != "")
            $("#upfileResult").html($(this).val());
        else
            $("#upfileResult").html("投稿仅支持word格式。");
    })
    $("#favorites").click(function() {
        try {
            window.external.addFavorite(window.location.host, "中国锅炉网");
        }
        catch (e) {
            try {
                window.sidebar.addPanel("中国锅炉网", window.location.host, "");
            }
            catch (e) {
                alert("加入收藏失败,请请使用Ctrl+D进行添加!");
            }
        }

    }); //加入收藏夹





    $.roll("#mid04right14", 2000); //向上滚动
    $.roll("#mid04right16", 2000); //向上滚动
    $.roll("#tabs-gao1", 2000); //向上滚动
    //$.fixwidth("#mid04right24");//固定文字宽度

    $.nowrap2("#mid01center0202", 50); //不换行(2行)首条新闻
    $.nowrap3("#mid02center08", 50); //不换行(2行)协会会员
    $.nowrap4("#HyperLink1", 30); //不换行(1行)首条新闻
    $.nowrap("#tabs-tabul032"); //不换行
    $.nowrap("#mid04right24"); //不换行
    $.nowrap("#mid02center04"); //不换行
    $.nowrap("#mid04center12"); //不换行
    $.nowrap("#mid04center17"); //不换行
    $.nowrap("#tabs-103"); //脱销--不换行
    $.nowrap("#mid02right02"); //行业活动会议
    $.nowrap("#mid02right03"); //行业活动会展
    $.nowrap("#mid02right04"); //行业活动会展
    $.nowrap("#mid04center09"); //产品展示
    $.nowrap("#tabs-tabul061"); //供应信息--不换行
    $.nowrap("#mid04left1802"); //招聘信息--不换行
    $.nowrap("#tabs-tabul022"); //工锅所信息--不换行
    $.nowrap("#mid04left16suo01"); //展会信息--不换行
    $.nowrap("#tabs-gao1"); //优秀企业家--不换行
    $.nowrap("#tabs-tabul041"); //论坛第三届--不换行
    $.nowrap("#mid01center0204"); //最新新闻--不换行
    $.nowrap("#mid02center06"); //企业新闻--不换行
    $.nowrap("#mid04center05"); //标准新闻--不换行
    $.nowrap("#mid04center15"); //行业动态--不换行
    $.nowrap("#mid04center21"); //热点聚焦--不换行

    $.rollTop("#bottom03", 3000); //底部滚动
    $.ad2014("#topguanggao01zuo", $.showAd); //顶部左广告
    $.ad2014("#topguanggao01you", $.showAd); //顶部右广告
    $.ad2014("#topguanggao", $.showTopAd); //通栏一广告
    $.ad2014("#topguanggao02", $.showAd); //通栏二广告
    $.ad2014("#mid01center01", $.showAd); //中一广告
    $.ad2014("#mid01center03", $.showAd); //中二广告
    $.ad2014("#mid04center10", $.showAd); //中三广告
    $.ad2014("#mid01right01", $.showAd); //右一广告
    $.ad2014("#mid01right02", $.showAd); //右二广告
    $.ad2014("#mid01right03", $.showAd); //右三广告
    $.ad2014("#mid01right04", $.showAd); //右四广告
    $.ad2014("#mid02right05", $.showAd); //右五广告

    $.ad2014("#mid02left01", $.showAd); //左一广告
    $.ad2014("#mid02left05", $.showAd); //左二广告
    $.ad2014("#mid04left03", $.showAd); //左三广告
    $.ad2014("#mid04left04", $.showAd); //左四广告
    $.ad2014("#mid04left19", $.showAd); //左五广告


    $.ad2014("#mid03left", $.showAd); //页中一广告
    $.ad2014("#mid03center01", $.showAd); //页中二广告
    $.ad2014("#mid03center02", $.showAd); //页中三广告
    $.ad2014("#mid03right", $.showAd); //页中四广告

    $.ad2014("#bottom0101", $.showAd); //底部广告
    $.ad2014("#bottom0102", $.showAd); //底部广告
    $.ad2014("#bottom0103", $.showAd); //底部广告
    $.ad2014("#bottom0104", $.showAd); //底部广告
    $.ad2014("#bottom0105", $.showAd); //底部广告
    $.ad2014("#bottom0106", $.showAd); //底部广告
    $.ad2014("#bottom0107", $.showAd); //底部广告



    $.ad2014("#mid04center0601", $.productad2014); //产品一广告
    $.ad2014("#mid04center0602", $.productad2014); //产品二广告
    $.ad2014("#mid04center0603", $.productad2014); //产品三广告
    $.ad2014("#mid04center0604", $.productad2014); //产品四广告


    $.duilian("#duilian_left"); //左对联广告
    $.duilian("#duilian_right"); //右对联广告

    $.ad2014("#mid04left15", $.showyoukuAd); //优酷广告

    var _hmt = _hmt || []; //百度统计
    var hm = document.createElement("script");
    hm.src = "//hm.baidu.com/hm.js?0b1de7c78fc7937b1051e9795fac222a";
    var s = document.getElementsByTagName("script")[0];
    s.parentNode.insertBefore(hm, s);


})
