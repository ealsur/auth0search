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
		.controller('UsersController', ['$http',
            function ($http) {
                var ctrl = this;
                /******** MODEL ************/
                ctrl.searching = false;
                ctrl.results = [];
                var lastSearch='';
                ctrl.filters=null;
                ctrl.searchText = '';
                ctrl.page = 1;
                /******** /MODEL ***********/
                ctrl.filter = function($event, filter, value){
                    $event.preventDefault();
                    if(ctrl.filters !== null && ctrl.filters.hasOwnProperty(filter)){
                        delete ctrl.filters[filter];
                    }
                    else{
                        if(ctrl.filters===null){
                            ctrl.filters={};
                        }
                        ctrl.filters[filter] = value;
                    }
                    ctrl.search($event);
                };

                
                ctrl.search = function($event){
                    $event.preventDefault();  
                    ctrl.searching=true;
                    ctrl.results=null;
                    if(lastSearch!==ctrl.searchText){
                        ctrl.page = 1;
                    }
                    $http({
                        method: 'POST',
                        url: '/search',
                        data: {
                            Text: ctrl.searchText,
                            Filters:ctrl.filters,
                            IncludeFacets:true,
                            Page:ctrl.page
                        }
                    }).then(function success(response) {
                        ctrl.results= response.data;
                        ctrl.searching=false;
                    });
                };
   
            }]);
})();