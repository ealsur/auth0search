'use strict';

(function () {
    angular.module('auth0App', ['ui.bootstrap'])
        .config(['$httpProvider', function ($httpProvider) {
		    /*CONFIGURO EL PROVIDER PARA NUNCA CACHEAR RESPUESTAS DE GET*/
		    if (!$httpProvider.defaults.headers.get) {
		        $httpProvider.defaults.headers.get = {};
		    }
		    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
		    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
		    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
		    $httpProvider.defaults.headers.get['X-Requested-With'] = 'XMLHttpRequest';
        }])
		.controller('SuggestionsController', ['$http',
            function ($http) {
                var ctrl = this;
                /******** MODEL ************/
                ctrl.noResults=false;
                ctrl.loading=false;
                ctrl.fuzzy = 'true';
                ctrl.asyncSelected=null;
                ctrl.getSuggestions = function(text){
                    return $http({
                        method: 'GET',
                        url: '/search/suggest?term='+text
                    }).then(function (response) {
                        var map= response.data.results.map(function(item){
                            return item.text;
                        });
                        return map;
                    });
                };
            }]);
})();