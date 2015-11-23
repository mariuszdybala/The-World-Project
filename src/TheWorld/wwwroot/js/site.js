/// <reference path="../lib/jquery/dist/jquery.min.js" />

(function () {

    //var ele = $("#username");
    //ele.text("Mariusz");

    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.css("background-color","#888");
    //});

    //main.on("mouseleave", function () {
    //    main.css("background-color","");
    //});

    //var mainItems = $("ul.menu li  a");
    //mainItems.on("click", function () {
    //    var cur = $(this);
    //    alert(cur.text());
    //});

    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#siderbarToggle i.fa");

    $("#siderbarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");

        if($sidebarAndWrapper.hasClass('hide-sidebar'))
        {
            // $(this).text("Show SideBar");
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }
        else
        {
            //  $(this).text("Hide SideBar");
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }

    });



})();


