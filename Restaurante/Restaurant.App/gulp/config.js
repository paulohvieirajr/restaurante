module.exports = {
    app: './',
    dist: 'target/www/',
    test: 'test/javascript/',
    scss: 'scss/',
    sassSrc: 'scss/**/*.{scss,sass}',
    sassVendor: 'scss/vendor.scss',
    cssDir: 'content',
    bower: 'app/libs/',
    tmp: 'target/tmp',
    revManifest: 'target/tmp/rev-manifest.json',
    port: 9000,
    apiPort: 80,
    liveReloadPort: 35729,
    uri: 'http://localhost:',
    constantTemplate:
        '(function () {\n' +
        '    \'use strict\';\n' +
        '    // Nao editar esse arquivo, ele e gerado automaticamente com a versao da aplicacao e o endereco da API respectiva ao ambiente\n' +
        '    angular\n' +
        '        .module(\'<%- moduleName %>\')\n' +
        '<% constants.forEach(function(constant) { %>        .constant(\'<%- constant.name %>\', <%= constant.value %>)\n<% }) %>;\n' +
        '})();\n'
};
