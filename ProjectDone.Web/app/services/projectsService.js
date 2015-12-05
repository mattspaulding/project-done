'use strict';

angular.module('App.projectsServices', []).factory('Project', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(ngAuthSettings.apiServiceBaseUri + 'api/projects/:id', { id: '@projectId' }, {
        update: {
            method: 'PUT'
        }
    });
});
