$.extend({
    addTab: function (title, herf, icons) {
        var _tabs = $("#mainTabs");
        if (_tabs.tabs('exists', title)) {
            _tabs.tabs('select', title)
        }
        else {
            _tabs.tabs('add', {
                title: title,
                closable: true,
                content: '<iframe frameborder="0"  src="' + herf + '" style="width:100%;height:100%;"></iframe>',
                iconCls: icons || 'icon-blank'
            });
        }
    }, addSubTab: function (title, herf, icons) {
        var jq = top.jQuery;
        var _tabs=jq("#mainTabs");
        if (_tabs.tabs('exists', title)) {
            _tabs.tabs('select', title)
        }
        else {
            _tabs.tabs('add', {
                title: title,
                closable: true,
                content: '<iframe frameborder="0"  src="' + herf + '" style="width:100%;height:100%;"></iframe>',
                iconCls: icons || 'icon-blank'
            });
        }
    }, SetCurrentTabUrl: function (url) {
        var curtab = $('#mainTabs').tabs('getSelected');
        if (curtab && curtab.find("iframe").length > 0) {
            var ifream = curtab.find("iframe")[0];
            ifream.contentWindow.location.href = url;
        }
    }, jsonDateFormat: function (jsonDate) {
        try {
            var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            var hours = date.getHours();
            var minutes = date.getMinutes();
            var seconds = date.getSeconds();
            return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
        } catch (ex) {
            return "";
        }
    },
    startHeartBeat: function () {
        var HeartBeatTimer;
        // pulse every 10 分钟
        if (HeartBeatTimer == null)
            HeartBeatTimer = setInterval($.heartBeat, 1080000);
    },
    heartBeat: function () {
        $.post("/Admin/Home/PokePage", function () {
            console.log("heartbeat!")
        });
    }
})