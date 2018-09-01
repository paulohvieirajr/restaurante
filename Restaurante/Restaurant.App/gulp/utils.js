'use strict';

var fs = require('fs');

module.exports =  {
    endsWith : endsWith,
    isLintFixed : isLintFixed
}

function endsWith(str, suffix) {
    return str.indexOf('/', str.length - suffix.length) !== -1;
}

function isLintFixed(file) {
	return file.eslint !== null && file.eslint.fixed;
}
