(function () {
    'use strict';
    
    angular.module('app')
    .constant('MSGERROR', 'Problemas ao contatar o servidor. Por favor tente mais tarde.')
    .constant('LOADTEMPLATE', '<div class="mascara"><div class="cssload-container"><div class="cssload-speeding-wheel"></div><div class="margem-superior"><strong class="fonteBranca">Aguarde...</strong></div></div></div>');
})();