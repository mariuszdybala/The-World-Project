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

    $("#siderbarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");

        if($sidebarAndWrapper.hasClass('hide-sidebar'))
        {
            $(this).text("Show SideBar");
        }
        else
        {
            $(this).text("Hide SideBar");
        }

    });



})();


