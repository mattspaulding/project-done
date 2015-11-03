'use strict';
app.service('fileUpload', ['$http', 'localStorageService', 'ngAuthSettings',function ($http, localStorageService, ngAuthSettings) {
        this.uploadFileToUrl = function(file, uploadUrl){
            var fd = new FormData();
            fd.append('file', file);
            return $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: {'Content-Type': undefined}
            })
            .success(function (result) {
                localStorageService.set('imagePath', ngAuthSettings.apiServiceBaseUri+"JobImages/"+result);
                return result;
            })
            .error(function (data) {
                debugger;
            });
        }
    }]);