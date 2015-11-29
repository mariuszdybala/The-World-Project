/// <reference path="../lib/angular/angular.min.js" />
/// <reference path="../lib/angular/angular.js" />


(function () {
    "use strict";
    angular.module("simpleControls", [])
    .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/views/waitCursor.html",


        }


    };

})()