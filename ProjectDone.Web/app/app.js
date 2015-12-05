
var app = angular.module('App', ['ui.router', 'ngResource', 'ngFileUpload', 'App.valuesControllers', 'App.valuesServices', 'App.projectsControllers', 'App.projectsServices', 'LocalStorageModule', 'ngMaterial', 'ngMessages', 'ngMdIcons']);

app.config(function ($stateProvider,$urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider.state("home", {
        url: "/",
        controller: "homeController",
        templateUrl: "/app/views/home/index.html"
    })

    $stateProvider.state("user", {
        url: "/user", 
        controller: "userController",
        templateUrl: "/app/views/user/index.html"
    });

    $stateProvider.state("userEdit", {
        url: "/user/edit/:userId", 
        controller: "userController",
        templateUrl: "/app/views/user/edit.html"
    });

    $stateProvider.state("login", {
        url: "/login", 
        controller: "loginController",
        templateUrl: "/app/views/account/login.html"
    });

    $stateProvider.state("signup", {
        url: "/signup", 
        controller: "signupController",
        templateUrl: "/app/views/account/signup.html"
    });

    $stateProvider.state("values", {
        url: "/values",
        controller: "valuesIndexController",
        templateUrl: "/app/views/values/index.html"
    });

    $stateProvider.state("valuesAdd", {
        url: "/values/add",
        controller: "valuesAddController",
        templateUrl: "/app/views/values/add.html"
    });

    $stateProvider.state("valuesEdit", {
        url: "/values/edit/:valueId",
        controller: "valuesEditController",
        templateUrl: "/app/views/values/edit.html"
    });

    $stateProvider.state("valuesDetails", {
        url: "/values/details/:valueId",
        controller: "valuesDetailsController",
        templateUrl: "/app/views/values/details.html"
    });
    $stateProvider.state("projects", {
        url: "/projects",
        controller: "projectsIndexController",
        templateUrl: "/app/views/projects/index.html"
    });

    $stateProvider.state("projectsImageUpload", {
        url: "/projects/imageUpload",
        controller: "projectsImageUploadController",
        templateUrl: "/app/views/projects/imageUpload.html"
    });

    $stateProvider.state("projectsAdd", {
        url: "/projects/add",
        controller: "projectsAddController",
        templateUrl: "/app/views/projects/add.html"
    });

    $stateProvider.state("projectsEdit", {
        url: "/projects/edit/:projectId",
        controller: "projectsEditController",
        templateUrl: "/app/views/projects/edit.html"
    });

    $stateProvider.state("projectsDetails", {
        url: "/projects/details/:projectId",
        controller: "projectsDetailsController",
        templateUrl: "/app/views/projects/details.html"
    });

});

app.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
      .primaryPalette('brown')
      .accentPalette('indigo')
      .warnPalette('red')
    .backgroundPalette('grey');
});

//var serviceBase = 'http://localhost:49512/';
var serviceBase = 'http://projectdoneapidev.azurewebsites.net/';
var clientId = 'consoleApp';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: clientId
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


