$(document).ready(function(){
    classer();

    $("#listtable tfoot").remove();
    $.ajaxSetup({ cache: false }); //no cache
    //$("table#commListSummary").wrap('<div class="table_holder"></div>');

    $("#nav ul li:nth-child(4)").addClass("lastMenu");
});

$(window).load(function () {
    $("table#commListSummary").wrap('<div class="table_holder"></div>');
    $("table#commListDetail").wrap('<div class="table_holder"></div>');
    $("table#usersTable").wrap('<div class="table_holder"></div>');
    $("table#channelDetailsTable").wrap('<div class="table_holder"></div>');
    $("table#channelTable").wrap('<div class="table_holder"></div>');
    $("table#bannerAdsTable").wrap('<div class="table_holder"></div>');
    $("table#appStatuslisttableSummary").wrap('<div class="table_holder"></div>');
    $("table#extensionlisttableDetail").wrap('<div class="table_holder"></div>');
    $("table#extensionlisttableSummary").wrap('<div class="table_holder"></div>');
    $("table#channelTargetTable").wrap('<div class="table_holder"></div>');
    $("table#branchTable").wrap('<div class="table_holder"></div>');
    $("table#districtTable").wrap('<div class="table_holder"></div>');

    $(".contentHolder > button").click(function () {
       
        //$(".ui-state-default > div").trigger("click");
    });
   
    //$(".table_holder").bind("scroll", function () {
    //    console.log("yes")
    //    /*$("#channelDetailsTable tr th").click(function () {
    //        $(this).css({"fontSize": "12px"});
    //    });*/
    //});
});

function classer() {
    var loc = location.href;
    var url = loc.split('/');
    var base = url[2];
    var id1 = url[3];
    var id2 = url[4];
    var id3 = url[5];
    var id4 = url[6];
    var homepage = "homepage";

    if (id3 == undefined) { id3 = "" }; if (id4 == undefined) { id4 = "" };

    if (id1 == "") {
        $('body').addClass(homepage);
    } else {
        $('body').addClass(id1);
        $('body').addClass(id2);
        $('body').addClass(id3);
        $('body').addClass(id4);
    };
};

var maxHeight = 300;

$(function () {

    $(".dropdown > li").hover(function () {

        var $container = $(this),
            $list = $container.find("ul"),
            $anchor = $container.find("a"),
            height = $list.height() * 1.1,       // make sure there is enough room at the bottom
            multiplier = height / maxHeight;     // needs to move faster if list is taller

        // need to save height here so it can revert on mouseout            
        $container.data("origHeight", $container.height());

        // so it can retain it's rollover color all the while the dropdown is open
        $anchor.addClass("hover");

        // make sure dropdown appears directly below parent list item    
        $list
            .show()
            .css({
                paddingTop: $container.data("origHeight")
            });

        // don't do any animation if list shorter than max
        if (multiplier > 1) {
            $container
                .css({
                    height: maxHeight,
                    overflow: "hidden"
                })
                .mousemove(function (e) {
                    var offset = $container.offset();
                    var relativeY = ((e.pageY - offset.top) * multiplier) - ($container.data("origHeight") * multiplier);
                    if (relativeY > $container.data("origHeight")) {
                        $list.css("top", -relativeY + $container.data("origHeight"));
                    };
                });
        }

    }, function () {

        var $el = $(this);

        // put things back to normal
        $el
            .height($(this).data("origHeight"))
            .find("ul")
            .css({ top: 0 })
            .hide()
            .end()
            .find("a")
            .removeClass("hover");

    });

    //// Add down arrow only to menu items with submenus
    //$(".dropdown > li:has('ul')").each(function () {
    //    $(this).find("a:first").append("<img src='images/down-arrow.png' />");
    //});


});



