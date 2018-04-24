(function ($) {
    $.extend(Array.prototype, {
        contains: function (needle) {
            return $.inArray(needle, this) >= 0;
        }
    });

    $.extend(String.prototype, {
        trim: function () {
            // 用正则表达式将前后空格  
            // 用空字符串替代。  
            return this.replace(/(^\s*)|(\s*$)/g, "");
        },
        isNaN: function () {
            return isNaN(this);
        },
        isEmpty: function () {
            return this == "";
        }
    });

    if ($.blockUI) {
        $.extend($.blockUI.defaults, {
            message: $("<img src='/images/ajax-loader.gif'/>"),
            css: {
                width: '32px',
                border: 'none',
                background: 'none'
            },
            overlayCSS: { backgroundColor: '#FFF', opacity: 0.8 }
        });
    }
    $.QueryString = (function(a) {
        if (a == "") return { };
        var b = { };
        for (var i = 0; i < a.length; ++i) {
            var p = a[i].split('=');
            if (p.length != 2) continue;
            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
        return b;
    })(window.location.search.substr(1).split('&'));

})(jQuery);