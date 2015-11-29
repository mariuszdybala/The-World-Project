/// <reference path="../lib/angular/angular.min.js" />
/// <reference path="../lib/angular/angular.js" />

(function () {

    "use strict";

    //tutaj korzystam z danego modu³u (brak drugiego paramteru -> [])
    angular.module("app-trips").controller(
        "tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

        vm.name = "Mariusz";

        vm.trips = [];
        vm.newTrip = {};
        vm.errorMessage = "";
        vm.isBusy = true;


        vm.addTrip = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
            .then(
            function (response) {
                //success
                vm.trips.push(response.data);
                vm.newTrip = {};
                vm.isBusy = false;
            },
            function () {
                //faliture
                vm.errorMessage = "Failed to save new trip";
                vm.isBusy = false;
            }
            ).finally(
            function () {
                vm.isBusy = false;
            }
            );
        };

        $http.get("/api/trips")
        .then(function (response) {
            //on success
            angular.copy(response.data, vm.trips);
            vm.busy = false;
        },
        function (error) {
            vm.errorMessage = "Failed to load data" + error;
            vm.busy = false;
        }).finally(
        function()
        {
            vm.busy = false;
        }
        );
    }


})();