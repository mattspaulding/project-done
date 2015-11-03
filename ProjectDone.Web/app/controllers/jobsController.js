angular.module('App.jobsControllers', [])

    .controller('jobsIndexController', function ($scope, $window, Job) {
        $scope.loading = true;
        $scope.jobs = Job.query(function () {
            $scope.loading = false;

        });
    })

    .controller('jobsDetailsController', function ($scope, $state, $stateParams, Job) {
        $scope.job = Job.get({ id: $stateParams.jobId }); //Get a single job.Issues a GET to /api/jobs/:id
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
         $scope.deleteJob = function (job) { // Delete a job. Issues a DELETE to /api/jobs/:id
             job.$delete(function () {
                 $state.go('jobs');
             });
         };
    })

    .controller('jobsImageUploadController', function ($scope, $state, $stateParams, fileUpload,ngAuthSettings) {
        $scope.uploadFile = function () {
            debugger;
            var file = $scope.imageData;
            var uploadUrl = ngAuthSettings.apiServiceBaseUri + "/api/jobs/Image";
            fileUpload.uploadFileToUrl(file, uploadUrl).then(function (result) {
                debugger;
                $scope.go('jobs/add');
            });


          

            
        };
    }).controller('jobsAddController', function ($scope, $state, $stateParams, localStorageService, Job) {
        $scope.message = "";
        debugger;
        $scope.job = new Job();  //create new job instance. Properties will be set via ng-model on UI
        $scope.job.imagePath
            = localStorageService.get('imagePath');
        $scope.addJob = function () { //create a new job. Issues a POST to /api/jobs
            debugger;
            $scope.job.$save(function () {
                $state.go('jobs'); // on success go back to home i.e. jobs state.
            });
        };
    }).controller('jobsEditController', function ($scope, $state, $stateParams, Job) {
        $scope.message = "";
        $scope.editJob = function () { //Update the edited job. Issues a PUT to /api/jobs/:id
            $scope.job.$update(function () {
                $state.go('jobs'); // on success go back to home i.e. jobs state.
            });
        };

        $scope.loadJob = function () { //Issues a GET request to /api/jobs/:id to get a job to update
            $scope.job = Job.get({ id: $stateParams.jobId });
        };

        $scope.loadJob(); // Load a job which can be edited on UI
    });