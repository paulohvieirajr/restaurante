/// <vs AfterBuild='default' />
'use strict';

var gulp = require('gulp'),
    expect = require('gulp-expect-file'),
    sass = require('gulp-sass'),
    rev = require('gulp-rev'),
    templateCache = require('gulp-angular-templatecache'),
    htmlmin = require('gulp-htmlmin'),
    imagemin = require('gulp-imagemin'),
    ngConstant = require('gulp-ng-constant'),
    rename = require('gulp-rename'),
    eslint = require('gulp-eslint'),
    es = require('event-stream'),
    flatten = require('gulp-flatten'),
    del = require('del'),
    runSequence = require('run-sequence'),
    browserSync = require('browser-sync'),
    KarmaServer = require('karma').Server,
    plumber = require('gulp-plumber'),
    changed = require('gulp-changed'),
    gulpIf = require('gulp-if'),
    inject = require('gulp-inject'),
    angularFilesort = require('gulp-angular-filesort'),
    naturalSort = require('gulp-natural-sort'),
    bowerFiles = require('main-bower-files'),
    debug = require('gulp-debug'),
    parseString = require('xml2js').parseString,
    fs = require('fs'),
    exec = require('gulp-exec'),
    webserver = require('gulp-webserver'),
    spritesmith = require('gulp.spritesmith');

var handleErrors = require('./gulp/handleErrors'),
    serve = require('./gulp/serve'),
    util = require('./gulp/utils'),
    build = require('./gulp/build');

var config = require('./gulp/config');

gulp.task('clean', function () {
    return del([config.dist], { dot: true });
});

gulp.task('copy', function () {
    return es.merge(
        gulp.src(config.app + 'i18n/**')
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(changed(config.dist + 'i18n/'))
        .pipe(gulp.dest(config.dist + 'i18n/')),
        gulp.src(config.app + 'content/**/*.{woff,woff2,svg,ttf,eot,otf}')
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(changed(config.dist + 'content/fonts/'))
        .pipe(flatten())
        .pipe(rev())
        .pipe(gulp.dest(config.dist + 'content/fonts/'))
        .pipe(rev.manifest(config.revManifest, {
            base: config.dist,
            merge: true
        }))
        .pipe(gulp.dest(config.dist)),
        gulp.src([config.app + 'robots.txt', config.app + 'favicon.ico', config.app + '.htaccess'], { dot: true })
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(changed(config.dist))
        .pipe(gulp.dest(config.dist))
    );
});

gulp.task('images', function () {
    return gulp.src(config.app + 'content/images/**')
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(changed(config.dist + 'content/images'))
        .pipe(imagemin({ optimizationLevel: 5, progressive: true, interlaced: true }))
        .pipe(rev())
        .pipe(gulp.dest(config.dist + 'content/images'))
        .pipe(rev.manifest(config.revManifest, {
            base: config.dist,
            merge: true
        }))
        .pipe(gulp.dest(config.dist))
        .pipe(browserSync.reload({ stream: true }));
});

gulp.task('sprites', function () {
    var spriteData =
        gulp.src(config.app + 'images/*.*') // source path of the sprite images
            .pipe(spritesmith({
                imgName: 'sprite.png',
                cssName: 'sprite.css',
            }));

    spriteData.img.pipe(gulp.dest(config.app + 'content')); // output path for the sprite
    spriteData.css.pipe(gulp.dest(config.app + 'content')); // output path for the CSS
});

gulp.task('sprites-release', function () {
    var spriteData =
        gulp.src(config.app + 'images/*.*') // source path of the sprite images
            .pipe(spritesmith({
                imgName: 'sprite.png',
                cssName: 'sprite.css',
            }));

    spriteData.img.pipe(gulp.dest(config.dist + 'content/css')); // output path for the sprite
    spriteData.css.pipe(gulp.dest(config.dist + 'content/css')); // output path for the CSS
});

gulp.task('sass', function () {
    return es.merge(
        gulp.src(config.sassSrc)
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(expect(config.sassSrc))
        .pipe(changed(config.cssDir, { extension: '.css' }))
        .pipe(sass({ includePaths: config.bower }).on('error', sass.logError))
        .pipe(gulp.dest(config.cssDir)),

        gulp.src(config.bower + '**/fonts/**/*.{woff,woff2,svg,ttf,eot,otf}')
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(changed(config.app + 'content/fonts'))
        .pipe(flatten())
        .pipe(gulp.dest(config.app + 'content/fonts'))
    );
});

gulp.task('languages', function () {
    var locales = config.bower + 'angular-i18n/angular-locale_pt-br.js';

    return gulp.src(locales)
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(changed(config.app + 'i18n/'))
        .pipe(gulp.dest(config.app + 'i18n/'));
});

gulp.task('styles', ['sass'], function () {
    return gulp.src(config.app + 'content/css')
        .pipe(browserSync.reload({ stream: true }));
});

gulp.task('inject', ['inject:dep', 'inject:app']);

gulp.task('inject:dep', ['inject:vendor']);

gulp.task('ngconstant', function () {
    var webConfig = fs.readFileSync('web.config', 'utf8');
    var object = new Object;;

    parseString(webConfig, function (err, result) {
        if (result.configuration.appSettings) {
            for (var i = 0; i < result.configuration.appSettings[0].add.length; i++) {
                object[result.configuration.appSettings[0].add[i].$.key] = result.configuration.appSettings[0].add[i].$.value;
            }
        } else {
            console.log('web.config');
        }
    });

    return ngConstant({
        name: 'app',
        constants: object,
        template: config.constantTemplate,
        stream: true
    })
    .pipe(rename('app.constants.js'))
    .pipe(gulp.dest(config.app + 'app/'));
});

gulp.task('inject:app', function () {
    return gulp.src(config.app + 'index.html')
    .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(inject(gulp.src([config.app + 'app/**/*.js', '!' + config.app + 'app/libs/**/*.js'])
            .pipe(naturalSort())
            .pipe(angularFilesort()), { relative: true }))
        .pipe(gulp.dest(config.app));
});

gulp.task('inject:vendor', function () {
    var stream = gulp.src(config.app + 'index.html')
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(inject(gulp.src(bowerFiles(), { read: false }), {
            name: 'bower',
            relative: true
        }))
        .pipe(gulp.dest(config.app));

    return es.merge(stream, gulp.src(config.sassVendor)
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(inject(gulp.src(bowerFiles({ filter: ['**/*.{scss,sass}'] }), { read: false }), {
            name: 'bower',
            relative: true
        }))
        .pipe(gulp.dest(config.scss)));
});

gulp.task('inject:test', function () {
    return gulp.src(config.test + 'karma.conf.js')
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(inject(gulp.src(bowerFiles({ includeDev: true, filter: ['**/*.js'] }), { read: false }), {
            starttag: '// bower:js',
            endtag: '// endbower',
            transform: function (filepath) {
                return '\'' + filepath.substring(1, filepath.length) + '\',';
            }
        }))
        .pipe(gulp.dest(config.test));
});

gulp.task('inject:troubleshoot', function () {
    /* this task removes the troubleshooting content from index.html*/
    return gulp.src(config.app + 'index.html')
        .pipe(plumber({ errorHandler: handleErrors }))
        /* having empty src as we dont have to read any files*/
        .pipe(inject(gulp.src('', { read: false }), {
            starttag: '<!-- inject:troubleshoot -->',
            removeTags: true,
            transform: function () {
                return '<!-- Angular views -->';
            }
        }))
        .pipe(gulp.dest(config.app));
});

gulp.task('assets:prod', ['images', 'sprites-release', 'styles', 'html'], build);

gulp.task('html', function () {
    return gulp.src([config.app + 'app/**/*.html', '!' + config.bower + 'app/libs/**/*.html'])
        .pipe(htmlmin({ collapseWhitespace: true }))
        .pipe(templateCache({
            module: 'app',
            root: 'app/',
            moduleSystem: 'IIFE'
        }))
        .pipe(gulp.dest(config.tmp));
});

gulp.task('eslint', function () {
    return gulp.src(['gulpfile.js', config.app + 'app/**/*.js'])
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(eslint())
        .pipe(eslint.format())
        .pipe(eslint.failOnError());
});

gulp.task('eslint:fix', function () {
    return gulp.src(config.app + 'app/**/*.js')
        .pipe(plumber({ errorHandler: handleErrors }))
        .pipe(eslint({
            fix: true
        }))
        .pipe(eslint.format())
        .pipe(gulpIf(util.isLintFixed, gulp.dest(config.app + 'app')));
});

gulp.task('test', ['inject:test'], function (done) {
    new KarmaServer({
        configFile: __dirname + '/' + config.test + 'karma.conf.js',
        singleRun: true
    }, done).start();
});

gulp.task('watch', function () {
    gulp.watch('bower.json', ['install']);
    gulp.watch('Web.config', ['ngconstant']);
    gulp.watch(config.sassSrc, ['styles']);
    gulp.watch(config.app + 'content/images/**', ['images']);
    gulp.watch(config.app + 'app/**/*.js', ['inject:app']);
    gulp.watch([config.app + '*.html', config.app + 'app/**']).on('change', browserSync.reload);
});

gulp.task('watchvs', function () {
    gulp.watch('bower.json', ['install']);
    gulp.watch('Web.config', ['ngconstant']);
    gulp.watch(config.sassSrc, ['styles']);
    gulp.watch(config.app + 'content/images/**', ['images']);
    gulp.watch(config.app + 'app/**/*.js', ['inject:app']);
});

gulp.task('install', function () {
    runSequence(['inject:dep'], 'sass', 'sprites',
        'languages',
        'inject:app',
        'ngconstant'
        );
});

gulp.task('serve', function () {
    runSequence('install', serve);
});

gulp.task('webserver', function () {
    gulp.src('target/www')
      .pipe(webserver({
          open: true,
          fallback: 'index.html'
      }));
});

gulp.task('http-server', function () {
    gulp.src('target/www')
      .pipe(exec('http-server ./target/www -o'));
});
 
gulp.task('serve-release', function () {
    runSequence('build', 'http-server');
});

gulp.task('build', ['clean'], function (cb) {
    runSequence(['copy', 'inject:vendor', 'languages', 'ngconstant'], 'inject:app', 'inject:troubleshoot', 'assets:prod', cb);
});
