$(document).ready(function () {
    
});


//根据页面内容生成右侧滚动条，并绑定相关事件
function InitFixedNaviBar() {
    var container = $('#fixed_navi_bar').html("");
    var itemsSelector = container.attr('itemsselector');
    var beginContainer = container.attr("begincontainer");
    var index = 0;
    var collaspeId = 1;
    var content = "";
    function createHtml(items,level) {
        var childrenPanel = items.children(".panel-default");
        if (childrenPanel.length == 0) {
            childrenPanel = items.find(".panel-group>.panel-default");
        }
        if (childrenPanel.length == 0) {
            childrenPanel = items.find(".panel-default");
        }

        var s="";

        if (level == 0) {
            s = " menu-first ";
        }
        
        for (var j = 0; j < childrenPanel.length; j++) {
            var panelBody = $(childrenPanel[j]);
            var headPanel = panelBody.children(".panel-heading");
            headPanel.attr("navi_id", index);
            
            if ($(panelBody).find(".panel-default:visible").length > 0) {
                content += '<li class="nav nav-list"><div href="#collaspe' + collaspeId + '" class="nav-header' + s + ' " navi_id="' + (index++)
                    + '"><i class="icon-user-md icon-large"></i>' + headPanel.text() + '</div><ul id="collaspe' + collaspeId + '" class="nav nav-list menu-second collapse">';
                collaspeId++;
                createHtml(panelBody, level + 1);
                content += "</ul></li>";
            } else {
                content += '<li class="nav nav-list"  navi_id="' + (index++) + '"><div class="' + s + '"><i class="icon-list"></i>' + headPanel.text() + '</div></li>';
            }
        }
    }
    var items = $(itemsSelector + ':visible');
    createHtml($(beginContainer), 0);
    container.append(content);
    
    //页面滚动时,绑定对应的自动hover事件
    $(window).scroll(function () {
        var divs = $("div.panel-heading[navi_id]");
        var mark = document.body.scrollTop;
        var curShow = $(divs[0]).attr("navi_id");
        for (var i = 0; i < divs.length; i++) {
            var tempDiv = $(divs[i]);
            if (tempDiv.position().top - mark >= 0) {
                curShow = tempDiv.attr("navi_id");
                tempDiv.removeClass("in");
                break;
            }
        }
        $("#fixed_navi_bar").find("[navi_id]").each(function() {
            $(this).removeClass("hover");
        });

        $("#fixed_navi_bar").find(".collapse").removeClass("in");

        var cur = $("#fixed_navi_bar").find("[navi_id=" + curShow + "]");
        cur.addClass("hover");
        var parent = cur.parents("ul.collapse");
        if (parent.length>0) {
            parent.addClass("in");
        } else {
            $(cur.attr("href")).addClass("in");
        }
    });

    //当对应导航按钮点击时,自动定位到相应的内容块
    container.find('[navi_id]').click(function () {
        var top = $(itemsSelector).filter('[navi_id="' + $(this).attr('navi_id') + '"]').position().top;
        document.documentElement.scrollTop = top;//IE
        document.body.scrollTop = top;//Chrome
        return false;
    });
   
}