'use strict';

angular.module('App.jobsServices', []).factory('Job', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(ngAuthSettings.apiServiceBaseUri + 'api/jobs/:id', { id: '@jobId' }, {
        update: {
            method: 'PUT'
        }
    });
});
