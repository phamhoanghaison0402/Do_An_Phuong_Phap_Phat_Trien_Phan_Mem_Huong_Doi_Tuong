(function() {
  var $, banner, extend, gulp, karma, karmaConfig, name, paths, pkg, server, spawn, streamqueue;

  gulp = require('gulp');

  $ = require('gulp-load-plugins')({
    lazy: false
  });

  extend = require('util')._extend;

  streamqueue = require('streamqueue');

  spawn = require('child_process').spawn;

  karma = require('karma').server;

  karmaConfig = require('./karma.json');

  pkg = require('./package.json');

  name = pkg.name;

  paths = {
    src: './src',
    dist: './build',
    test: './test',
    docs: './docs'
  };

  server = {
    host: 'localhost',
    port: 3000
  };

  banner = '/* ========================================================================\n * <%= pkg.name %> - v<%= pkg.version %>\n * <%= pkg.homepage %>\n * ========================================================================\n * Copyright 2012-2013 <%= pkg.author.name %>\n *\n * ========================================================================\n * Licensed under the Apache License, Version 2.0 (the "License");\n * you may not use this file except in compliance with the License.\n * You may obtain a copy of the License at\n *\n *     http://www.apache.org/licenses/LICENSE-2.0\n *\n * Unless required by applicable law or agreed to in writing, software\n * distributed under the License is distributed on an "AS IS" BASIS,\n * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.\n * See the License for the specific language governing permissions and\n * limitations under the License.\n * ========================================================================\n */\n\n';

  gulp.task('coffee', function() {
    return gulp.src("" + paths.src + "/coffee/" + name + ".coffee").pipe($.changed("" + paths.dist + "/js")).pipe($.coffeelint('./coffeelint.json')).pipe($.coffeelint.reporter()).on('error', $.util.log).pipe($.coffee({
      bare: true
    })).on('error', $.util.log).pipe($.header(banner, {
      pkg: pkg
    })).pipe(gulp.dest("" + paths.dist + "/js")).pipe(gulp.dest("" + paths.src + "/docs/assets/js")).pipe(gulp.dest(paths.test)).pipe($.uglify()).pipe($.header(banner, {
      pkg: pkg
    })).pipe($.rename({
      suffix: '.min'
    })).pipe(gulp.dest("" + paths.dist + "/js"));
  });

  gulp.task('coffee-standalone', function() {
    return streamqueue({
      objectMode: true
    }, gulp.src(["./bower_components/bootstrap/js/tooltip.js", "./bower_components/bootstrap/js/popover.js"]), gulp.src("" + paths.src + "/coffee/" + name + ".coffee").pipe($.changed("" + paths.dist + "/js")).pipe($.coffeelint('./coffeelint.json')).pipe($.coffeelint.reporter()).on('error', $.util.log).pipe($.coffee({
      bare: true
    })).on('error', $.util.log)).pipe($.concat("" + name + "-standalone.js")).pipe($.header(banner, {
      pkg: pkg
    })).pipe(gulp.dest("" + paths.dist + "/js")).pipe($.uglify()).pipe($.header(banner, {
      pkg: pkg
    })).pipe($.rename({
      suffix: '.min'
    })).pipe(gulp.dest("" + paths.dist + "/js"));
  });

  gulp.task('less', function() {
    return gulp.src(["" + paths.src + "/less/" + name + ".less"]).pipe($.changed("" + paths.dist + "/css")).pipe($.less()).on('error', $.util.log).pipe($.header(banner, {
      pkg: pkg
    })).pipe(gulp.dest("" + paths.dist + "/css")).pipe(gulp.dest("" + paths.src + "/docs/assets/css")).pipe($.less({
      compress: true,
      cleancss: true
    })).pipe($.header(banner, {
      pkg: pkg
    })).pipe($.rename({
      suffix: '.min'
    })).pipe(gulp.dest("" + paths.dist + "/css"));
  });

  gulp.task('less-standalone', function() {
    return gulp.src("" + paths.src + "/less/" + name + "-standalone.less").pipe($.changed("" + paths.dist + "/css")).pipe($.less()).on('error', $.util.log).pipe($.header(banner, {
      pkg: pkg
    })).pipe(gulp.dest("" + paths.dist + "/css")).pipe($.less({
      compress: true,
      cleancss: true
    })).pipe($.header(banner, {
      pkg: pkg
    })).pipe($.rename({
      suffix: '.min'
    })).pipe(gulp.dest("" + paths.dist + "/css"));
  });

  gulp.task('test-coffee', ['coffee'], function() {
    return gulp.src("" + paths.src + "/coffee/" + name + ".spec.coffee").pipe($.changed(paths.test)).pipe($.coffeelint('./coffeelint.json')).pipe($.coffeelint.reporter()).on('error', $.util.log).pipe($.coffee()).on('error', $.util.log).pipe(gulp.dest(paths.test));
  });

  gulp.task('test-go', ['test-coffee'], function(done) {
    return karma.start(extend(karmaConfig, {
      singleRun: true
    }), done);
  });

  gulp.task('docs-build', ['coffee', 'less'], function(done) {
    return spawn('jekyll', ['build']).on('close', done);
  });

  gulp.task('docs-copy', ['docs-build'], function() {
    return gulp.src("./bower_components/**/*").pipe(gulp.dest("" + paths.docs + "/components"));
  });

  gulp.task('docs-coffee', ['docs-build'], function() {
    return gulp.src("" + paths.src + "/coffee/" + name + ".docs.coffee").pipe($.changed("" + paths.docs + "/assets/js")).pipe($.coffeelint.reporter()).on('error', $.util.log).pipe($.coffee()).on('error', $.util.log).pipe(gulp.dest("" + paths.docs + "/assets/js"));
  });

  gulp.task('clean-dist', function() {
    return gulp.src(paths.dist).pipe($.clean());
  });

  gulp.task('clean-test', function() {
    return gulp.src(paths.test).pipe($.clean());
  });

  gulp.task('clean-docs', function() {
    return gulp.src(paths.docs).pipe($.clean());
  });

  gulp.task('connect', ['docs'], function() {
    return $.connect.server({
      root: [paths.docs],
      host: server.host,
      port: server.port,
      livereload: true
    });
  });

  gulp.task('open', ['connect'], function() {
    return gulp.src("" + paths.docs + "/index.html").pipe($.open('', {
      url: "http://" + server.host + ":" + server.port
    }));
  });

  gulp.task('watch', ['connect'], function() {
    gulp.watch("" + paths.src + "/coffee/" + name + ".coffee", ['coffee', 'coffee-standalone']);
    gulp.watch("" + paths.src + "/less/" + name + ".less", ["less", "less-standalone"]);
    gulp.watch("" + paths.src + "/less/" + name + "-standalone.less", ['less-standalone']);
    gulp.watch("" + paths.src + "/coffee/" + name + ".spec.coffee", ['test']);
    gulp.watch(["" + paths.src + "/coffee/" + name + ".docs.coffee", "" + paths.src + "/docs/**/*"], ['docs']);
    return gulp.watch(["" + paths.dist + "/js/**/*.js", "" + paths.dist + "/css/**/*.css", "" + paths.docs + "/index.html"]).on('change', function(event) {
      return gulp.src(event.path).pipe($.connect.reload());
    });
  });

  gulp.task('bump', ['test'], function() {
    var bumpType;
    bumpType = $.util.env.type || 'patch';
    return gulp.src(['./bower.json', './package.json', './smart.json']).pipe($.bump({
      type: bumpType
    })).pipe(gulp.dest('./'));
  });

  gulp.task('clean', ['clean-dist', 'clean-test', 'clean-docs']);

  gulp.task('server', ['connect', 'open', 'watch']);

  gulp.task('dist', ['coffee', 'coffee-standalone', 'less', 'less-standalone']);

  gulp.task('test', ['coffee', 'test-coffee', 'test-go']);

  gulp.task('docs', ['coffee', 'less', 'docs-build', 'docs-copy', 'docs-coffee']);

  gulp.task('default', ['dist', 'docs', 'server']);

}).call(this);
