require.config({
    baseUrl: 'app',
    urlArgs: 'v=1.0'
});

require(
    [
        'animations/listAnimations',
        'app',
        'directives/wcUnique',
        'services/routeResolver',
        'services/config',
        'services/authService',
        'services/dataService',
        'services/modalService',
        'services/httpInterceptors',
        'controllers/navbarController'
    ],
    function () {
        angular.bootstrap(document, ['logvietApp']);
    });
