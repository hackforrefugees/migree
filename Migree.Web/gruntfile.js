module.exports = function(grunt) {
  var modRewrite = require('connect-modrewrite');
  var serveStatic = require('serve-static');
  var mountFolder = function (connect, dir) {
    return serveStatic(require('path').resolve(dir));
};
  // Project configuration.
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    s: {
      src: {
        /*m: "static/min/",*/
        js: "app/",
        assets: "assets/"
      },
      prod: {
        js: "prod/app/js/",
        sass: "prod/app/css/",
        assets: "prod/assets/",
        prod: "prod/"
      }
    },

    less: {
      development: {
        options: {
          compress: true,
          yuicompress: true,
          optimization: 2
        },
        files: {
          "assets/css/main.css": "assets/less/main.less"
        }
      }
    },

    jshint: {
      client: [ '<%= s.src.js %>'],
      options: {
          jshintrc: ".jshintrc"
      }
    },

    autoprefixer: {
      options: {
        browsers: ['last 2 versions', 'ie 8', 'ie 9']
      },
      main: {
        src: 'assets/css/main.css'
      }
    },

    concat: {
      angular: {
        src: ['bower_components/angular/angular.min.js',
              'bower_components/angular-ui-router/release/angular-ui-router.min.js',
              'bower_components/angular-route/angular-route.min.js',
              'bower_components/bootstrap-switch/dist/js/bootstrap-switch.min.js',
              'bower_components/angular-bootstrap-switch/dist/angular-bootstrap-switch.min.js',
              'bower_components/angular-resource/angular-resource.min.js',
              'bower_components/angular-q-spread/dist/q-spread.min.js'
              ],
        dest: 'static/vendor/angular-build.js',
      }
    },

    clean: {
      prod: ['prod']
    },
		copy: {
      prod: {
        expand: true, src: ['assets/**'], dest: 'prod/'
      }
		},
		// clean + copy = prod / test
    wiredep: {
      target: {
        src: 'index.html'
      }
    },

    connect: {
      options: {
        port: 9000,
        hostname: '0.0.0.0',//'migree.local',
        open: 'http://migree.local:9000',
        base: './',
        livereload: 35729
      },
      livereload: {
        options: {
          open: true,
          middleware: function (connect) {
              return [
                  modRewrite (['!\\.html|\\.js|\\.svg|\\.css|\\.png|\\.jpg|\\.woff|\\.ttf$ /index.html [L]']),
                  mountFolder(connect, './')
              ];
          }
        }
      }
    },

    watch: {
      html: {
        files: ['app/components/*.html'],
        options: {
          livereload: true,
        }
      },
      less: {
        files: ['assets/less/**/*'],
        tasks: ['less'],
        options: {
          livereload: true,
        }
      },
      js: {
        files: ['<%= s.src.js %>**/*.js', 'gruntfile.js'],
        tasks: ['jshint'],
        options: {
          spawn: false
          //livereload: true,
        }
      }
    },

    uglify: {
      scripts: {
        files: {
          '<%= s.src.m %>app.min.js': '<%= s.src.m %>app.js'
        }
      }
    },

    autoprefixer: {
      files: ['assets/css/main.css'],
      tasks: ['autoprefixer']
    }
  });

  grunt.loadNpmTasks('grunt-contrib-less');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-connect');
  grunt.loadNpmTasks('grunt-wiredep');
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-autoprefixer');

  // Default task(s).
  grunt.registerTask('default', ['clean','less','wiredep', 'autoprefixer', 'concat', /*'uglify',*/ 'connect','watch']); // postcss??
};
