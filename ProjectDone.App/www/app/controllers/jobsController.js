angular.module('App.jobsControllers', [])

    .controller('jobsIndexController', function ($scope,  $window, Job) {
        $scope.jobs = Job.query(); //fetch all jobs. Issues a GET to /api/jobs  
    })

    .controller('jobsDetailsController', function ($scope, $state, $stateParams, Job) {
         $scope.job = Job.get({ id: $stateParams.jobId }); //Get a single job.Issues a GET to /api/jobs/:id

         $scope.deleteJob = function (job) { // Delete a job. Issues a DELETE to /api/jobs/:id
             job.$delete(function () {
                 $state.go('jobs');
             });
         };
    })

    .controller('jobsAddController', function ($scope, $state, $stateParams, Job) {
        $scope.message = "";
        $scope.job = new Job();  //create new job instance. Properties will be set via ng-model on UI

        $scope.addJob = function () { //create a new job. Issues a POST to /api/jobs
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