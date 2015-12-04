module.exports = function(grunt) {

  // Project configuration.
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    less: {
      development: {
        options: {
          compress: true,
          yuicompress: true,
          optimization: 2
        },
        files: {
          "static/css/main.css": "static/less/main.less"
        }
      }  
    },
    express: {
      dev: {
        options: {
          script: 'server.js'
        }
      }
    },
    autoprefixer: {
      options: {
        browsers: ['last 2 versions', 'ie 8', 'ie 9']
      },
      main: {
        src: 'static/css/main.css'
      }
    },
  /*  concat: {
      js: {
        src: ['js/modules/*.js', 'js/components/*.js'],
        dest: 'js/main.js',
      },
    },*/
    watch: {
      html: {
        files: ['views/*.html'],
        options: {
          livereload: true,
        }
      },
      less: {
        files: ['static/less/**/*'],
        tasks: ['less'],
        options: {
          livereload: true,
        }
      },
    /*  concat: {
        files: ['public/js/modules/*.js', 'js/components/*.js'],
        tasks: ['concat']
      },*/
      autoprefixer: {
        files: ['css/main.css'],
        tasks: ['autoprefixer']
      },
      express: {
        files:  [ '*' ],
        tasks:  [ 'express:dev' ],
        options: {
          spawn: false
        }
      }
    }
  });

  grunt.loadNpmTasks('grunt-express-server');
  grunt.loadNpmTasks('grunt-contrib-less');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-autoprefixer');

  // Default task(s).
  grunt.registerTask('default', ['express:dev','less', 'autoprefixer','watch']);

};