angular.module('App.projectsControllers', [])

    .controller('projectsIndexController', function ($scope, $window, Project) {
        $scope.loading = true;
        $scope.projects = Project.query(function () {
            $scope.loading = false;

        });
    })

    .controller('projectsDetailsController', function ($scope, $state, $stateParams, Project) {
        $scope.project = Project.get({ id: $stateParams.projectId }); //Get a single project.Issues a GET to /api/projects/:id
        $scope.bids = [
            {
                bidId: 1,
                name: 'Bob',
                amount: 100,
                description: 'I\'m the guy you\'re looking for.'
            },
        {
            bidId: 2,
            name: 'Jill',
            amount: 200,
            description: 'I can do that!'
        },
        {
            bidId: 3,
            name: 'Jack',
            amount: 100,
            description: 'I can come over right now.'
        },
        {
            bidId: 4,
            name: 'Nancy',
            amount: 50,
            description: 'Best price always.'
        }
        ];
         $scope.deleteProject = function (project) { // Delete a project. Issues a DELETE to /api/projects/:id
             project.$delete(function () {
                 $state.go('projects');
             });
         };
    })

    .controller('projectsImageUploadController', function ($scope, $state, $stateParams, fileUpload,ngAuthSettings) {
        $scope.uploadFile = function () {
            debugger;
            var file = $scope.imageData;
            var uploadUrl = ngAuthSettings.apiServiceBaseUri + "/api/projects/Image";
            fileUpload.uploadFileToUrl(file, uploadUrl).then(function (result) {
                debugger;
                $scope.go('projects/add');
            });


          

            
        };
    }).controller('projectsAddController', function ($scope, $state, $stateParams, localStorageService, Project) {
        $scope.message = "";
        debugger;
        $scope.project = new Project();  //create new project instance. Properties will be set via ng-model on UI
        $scope.project.imagePath
            = localStorageService.get('imagePath');
        $scope.addProject = function () { //create a new project. Issues a POST to /api/projects
            debugger;
            $scope.project.$save(function () {
                $state.go('projects'); // on success go back to home i.e. projects state.
            });
        };
    }).controller('projectsEditController', function ($scope, $state, $stateParams, Project) {
        $scope.message = "";
        $scope.editProject = function () { //Update the edited project. Issues a PUT to /api/projects/:id
            $scope.project.$update(function () {
                $state.go('projects'); // on success go back to home i.e. projects state.
            });
        };

        $scope.loadProject = function () { //Issues a GET request to /api/projects/:id to get a project to update
            $scope.project = Project.get({ id: $stateParams.projectId });
        };

        $scope.loadProject(); // Load a project which can be edited on UI
    });