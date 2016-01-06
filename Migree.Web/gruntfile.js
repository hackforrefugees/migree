// Generated on 2015-12-31 using generator-angular 0.15.1
'use strict';

// # Globbing
// for performance reasons we're only matching one level down:
// 'test/spec/{*/}*.js'
// use this if you want to recursively match all subfolders:
// 'test/spec/**/*.js'

module.exports = function (grunt) {

  var modRewrite = require('connect-modrewrite');
  var serveStatic = require('serve-static');
  var mountFolder = function (connect, dir) {
    return serveStatic(require('path').resolve(dir));
  };
  // Time how long tasks take. Can help when optimizing build times
  require('time-grunt')(grunt);

  // Automatically load required Grunt tasks
  require('jit-grunt')(grunt, {
    useminPrepare: 'grunt-usemin',
    ngtemplates: 'grunt-angular-templates',
    cdnify: 'grunt-google-cdn'
  });

  // Configurable paths for the application
  var appConfig = {
    root: '/',
    app: 'app',
    dist: 'dist',
    assets: 'assets'
  };

  // Define the configuration for all the tasks
  grunt.initConfig({

    // Project settings
    migree: appConfig,

    // Watches files for changes and runs tasks based on the changed files
    watch: {
      bower: {
        files: ['bower.json'],
        tasks: ['wiredep']
      },

      less: {
        files: ['<%= migree.assets %>/less/**/*.less'],
        tasks: ['less'],
        options: {
          livereload: '<%= connect.options.livereload %>'
        }
      },

      js: {
        files: ['<%= migree.app %>/**/*.js'],
        tasks: ['newer:jshint:all', 'newer:jscs:all'],
        options: {
          livereload: '<%= connect.options.livereload %>'
        }
      },
      jsTest: {
        files: ['test/spec/{*/}*.js'],
        tasks: ['newer:jshint:test', 'newer:jscs:test', 'karma']
      },
      styles: {
        files: ['<%= migree.assets %>/css/{*/}*.css'],
        tasks: ['newer:copy:styles', 'postcss']
      },

      gruntfile: {
        files: ['gruntfile.js']
      },
      livereload: {
        options: {
          livereload: '<%= connect.options.livereload %>'
        },
        files: [
          '<%= migree.app %>/index.html',
          '<%= migree.app %>/components/{*/}.html',
          '.tmp/styles/{*/}*.css',
          '<%= migree.assets %>/img/{*/}*.{png,jpg,jpeg,gif,webp,svg}'
        ]
      }
    },

    less: {
      development: {
        options: {
          compress: false,
          yuicompress: false,
          optimization: 2
        },
        files: {
          'assets/css/main.css': 'assets/less/main.less'
        }
      }
    },

    // The actual grunt server settings
    connect: {
      options: {
        port: 9000,
        // Change this to '0.0.0.0' to access the server from outside.
        hostname: 'localhost',
        livereload: 35729,
        //hostname: '0.0.0.0',//'migree.local',
      },
      livereload: {
        options: {
          open: true,

          middleware: function (connect) {
            return [
                modRewrite(['!\\.html|\\.js|\\.svg|\\.css|\\.png|\\.jpg|\\.woff|\\.ttf$ /index.html [L]']),
                connect().use(
                  '/bower_components',
                  connect.static('./bower_components')
                ),
                connect().use(
                  '/assets',
                  connect.static('./assets')
                ),
                mountFolder(connect, './app'),
            ];
          }
          /*
          middleware: function (connect) {
            return [
              connect.static('.tmp'),
              connect().use(
                '/bower_components',
                connect.static('./bower_components')
              ),
              connect().use(
                '/assets',
                connect.static('./assets')
              ),
              connect.static(appConfig.app)
            ];
          }*/
        }
      },
      test: {
        options: {
          port: 9001,
          middleware: function (connect) {
            return [
              connect.static('.tmp'),
              connect.static('test'),
              connect().use(
                '/bower_components',
                connect.static('./bower_components')
              ),
              connect.static(appConfig.app)
            ];
          }
        }
      },
      dist: {
        options: {
          open: true,
          base: '<%= migree.dist %>'
        }
      }
    },

    // Make sure there are no obvious mistakes
    jshint: {
      options: {
        jshintrc: '.jshintrc',
        reporter: require('jshint-stylish')
      },
      all: {
        src: [
          'Gruntfile.js',
          '<%= migree.app %>/**/*.js'
        ]
      },
      test: {
        options: {
          jshintrc: 'test/.jshintrc'
        },
        src: ['test/spec/{*/}*.js']
      }
    },

    // Make sure code styles are up to par
    jscs: {
      options: {
        config: '.jscsrc',
        verbose: true
      },
      all: {
        src: [
          'gruntfile.js',
          '<%= migree.app %>/{*/}*.js',
          '<%= migree.app %>/components/{*/}*.js',
        ]
      },
      test: {
        src: ['test/spec/{*/}*.js']
      }
    },

    // Empties folders to start fresh
    clean: {
      dist: {
        files: [{
          dot: true,
          src: [
            '.tmp',
            '<%= migree.dist %>/{*/}*',
            '!<%= migree.dist %>/.git{*/}*'
          ]
        }]
      },
      server: '.tmp'
    },

    // Add vendor prefixed styles
    postcss: {
      options: {
        processors: [
          require('autoprefixer-core')({browsers: ['last 1 version']})
        ]
      },
      server: {
        options: {
          map: true
        },
        files: [{
          expand: true,
          cwd: '.tmp/css/',
          src: '{*/}*.css',
          dest: '.tmp/css/'
        }]
      },
      dist: {
        files: [{
          expand: true,
          cwd: '.tmp/styles/',
          src: '{*/}*.css',
          //dest: '.tmp/styles/'
          dest: '<%= migree.dest %>/assets/css'
        }]
      }
    },

    // Automatically inject Bower components into the app
    wiredep: {
      app: {
        src: ['<%= migree.app %>/index.html'],
        ignorePath:  /\.\.\//
      },
      test: {
        devDependencies: true,
        src: '<%= karma.unit.configFile %>',
        ignorePath:  /\.\.\//,
        fileTypes:{
          js: {
            block: /(([\s\t]*)\/{2}\s*?bower:\s*?(\S*))(\n|\r|.)*?(\/{2}\s*endbower)/gi,
              detect: {
                js: /'(.*\.js)'/gi
              },
              replace: {
                js: '\'{{filePath}}\','
              }
            }
          }
      }
    },

    // Renames files for browser caching purposes
    filerev: {
      dist: {
        src: [
          '<%= migree.dist %>/scripts/{*/}*.js',
          '<%= migree.dist %>/assets/css/{*/}*.css',
          //'<%= migree.dist %>/assets/img/{*/}*.{png,jpg,jpeg,gif,webp,svg}',
          //'<%= migree.dist %>/assets/fonts/*'
        ]
      }
    },

    // Reads HTML for usemin blocks to enable smart builds that automatically
    // concat, minify and revision files. Creates configurations in memory so
    // additional tasks can operate on them
    useminPrepare: {
      html: '<%= migree.app %>/index.html',
      options: {
        dest: '<%= migree.dist %>',
        flow: {
          html: {
            steps: {
              js: ['concat', 'uglifyjs'],
              css: ['cssmin']
            },
            post: {}
          }
        }
      }
    },

    // Performs rewrites based on filerev and the useminPrepare configuration
    usemin: {
      html: ['<%= migree.dist %>/index.html'],
      css: ['<%= migree.dist %>/css/{*/}*.css'],
      js: ['<%= migree.dist %>/scripts/{*/}*.js'],
      options: {
        assetsDirs: [
          '<%= migree.dist %>',
          '<%= migree.dist %>/css',
          '<%= migree.dist %>/assets/img',
          '<%= migree.dist %>/assets/fonts'
        ],
        patterns: {
          js: [[/(assets\/img\/[^''""]*\.(png|jpg|jpeg|gif|webp|svg))/g, 'Replacing references to images']]
        }
      }
    },

    // The following *-min tasks will produce minified files in the dist folder
    // By default, your `index.html`'s <!-- Usemin block --> will take care of
    // minification. These next options are pre-configured if you do not wish
    // to use the Usemin blocks.
    // cssmin: {
    //   dist: {
    //     files: {
    //       '<%= migree.dist %>/styles/main.css': [
    //         '.tmp/styles/{*/}*.css'
    //       ]
    //     }
    //   }
    // },
    // uglify: {
    //   dist: {
    //     files: {
    //       '<%= migree.dist %>/scripts/scripts.js': [
    //         '<%= migree.dist %>/scripts/scripts.js'
    //       ]
    //     }
    //   }
    // },
    // concat: {
    //   dist: {}
    // },

    imagemin: {
      dist: {
        files: [{
          expand: true,
          cwd: '<%= migree.app %>/images',
          src: '{*/}*.{png,jpg,jpeg,gif}',
          dest: '<%= migree.dist %>/images'
        }]
      }
    },

    svgmin: {
      dist: {
        files: [{
          expand: true,
          cwd: '<%= migree.app %>/images',
          src: '{*/}*.svg',
          dest: '<%= migree.dist %>/images'
        }]
      }
    },

    htmlmin: {
      dist: {
        options: {
          collapseWhitespace: true,
          conservativeCollapse: true,
          collapseBooleanAttributes: true,
          removeCommentsFromCDATA: true
        },
        files: [{
          expand: true,
          cwd: '<%= migree.dist %>',
          src: ['*.html'],
          dest: '<%= migree.dist %>'
        }]
      }
    },

    ngtemplates: {
      dist: {
        options: {
          module: 'migreeApp',
          htmlmin: '<%= htmlmin.dist.options %>',
          usemin: 'scripts/migree.js'
        },
        cwd: '<%= migree.app %>',
        src: 'components/{*/}*.html',
        dest: '.tmp/templateCache.js'
      }
    },

    // ng-annotate tries to make the code safe for minification automatically
    // by using the Angular long form for dependency injection.
    ngAnnotate: {
      dist: {
        files: [{
          expand: true,
          cwd: '.tmp/concat/scripts',
          src: '*.js',
          dest: '.tmp/concat/scripts'
        }]
      }
    },

    // Replace Google CDN references
    cdnify: {
      dist: {
        html: ['<%= migree.dist %>/*.html']
      }
    },

    // Copies remaining files to places other tasks can use
    copy: {
      dist: {
        files: [
        {
          expand: true,
          cwd: '<%= migree.app %>',
          dest: '<%= migree.dist %>',
          src: 'index.html'
        },
        {
          expand: true,
          cwd: '<%= migree.assets %>',
          src: 'img/**/*',
          dest: '<%= migree.dist %>/assets/'
        },
        {
          expand: true,
          cwd: 'bower_components/bootstrap/dist',
          src: 'fonts/*',
          dest: '<%= migree.dist %>/assets/'
        },
        {
          expand: true,
          cwd: 'bower_components/bootstrap/dist',
          src: 'fonts/*',
          dest: '<%= migree.assets %>'
        },
        {
          expand: true,
          cwd: '<%= migree.assets %>',
          src: 'fonts/*',
          dest: '<%= migree.dist %>/assets/'
        }]
      },

      fonts: {
        files: [
          {
            expand: true,
            cwd: 'bower_components/bootstrap/dist',
            src: 'fonts/*',
            dest: '<%= migree.dist %>/assets/'
          },
          {
            expand: true,
            cwd: 'bower_components/bootstrap/dist',
            src: 'fonts/*',
            dest: '<%= migree.assets %>'
          },
        ]
      },
      styles: {
        expand: true,
        cwd: '<%= migree.assets %>/css',
        dest: '.tmp/css/',
        src: '{*/}*.css'
      }
    },

    // Run some tasks in parallel to speed up the build process
    concurrent: {
      server: [
        'copy:styles'
      ],
      test: [
        'copy:styles'
      ],
      dist: [
        'copy:styles',
        'imagemin',
        'svgmin'
      ],
      options: {
        limit: 10,
        logConcurrentOutput: true
      }
    },

    // Test settings
    karma: {
      unit: {
        configFile: 'test/karma.conf.js',
        singleRun: true
      }
    }
  });


  grunt.registerTask('serve', 'Compile then start a connect web server', function (target) {
    if (target === 'dist') {
      return grunt.task.run(['build', 'connect:dist:keepalive']);
    }

    grunt.task.run([
      'clean:server',
      'copy:fonts',
      'less',
      'wiredep',
      'concurrent:server',
      'postcss:server',
      'connect:livereload',
      'watch'
    ]);
  });

  grunt.registerTask('server', 'DEPRECATED TASK. Use the "serve" task instead', function (target) {
    grunt.log.warn('The `server` task has been deprecated. Use `grunt serve` to start a server.');
    grunt.task.run(['serve:' + target]);
  });

  grunt.registerTask('test', [
    'clean:server',
    'copy:fonts',
    'less',
    'wiredep',
    'concurrent:test',
    'postcss',
    'connect:test',
    'karma'
  ]);

  grunt.registerTask('build', [
    'clean:dist',
    'copy:fonts',
    'less',
    'wiredep',
    'useminPrepare',
    'concurrent:dist',
    'postcss',
    'ngtemplates',
    'concat',
    'ngAnnotate',
    'copy:dist',
    'cdnify',
    'cssmin',
    'uglify',
    'filerev',
    'usemin',
    'htmlmin'
  ]);

  grunt.registerTask('default', [
    'newer:jshint',
    'newer:jscs',
    'test',
    'build'
  ]);
};
