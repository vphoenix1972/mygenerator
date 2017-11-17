'use strict';

var Generator = require('yeoman-generator');

class MyGenerator extends Generator {
    constructor(args, opts) {
        // Calling the super constructor is important so our generator is correctly set up
        super(args, opts);
    
        // Next, add your custom code
        this.option('babel'); // This method adds support for a `--babel` flag
      }

      prompting() {
        return this.prompt([{
          type    : 'input',
          name    : 'name',
          message : 'Your project name',
          default : this.appname // Default to current folder name
        }, {
          type    : 'confirm',
          name    : 'cool',
          message : 'Would you like to enable the Cool feature?'
        }]).then((answers) => {
          this.log('app name', answers.name);
          this.log('cool feature', answers.cool);
        });
      }

      method1() {
        this.log('method 1 just ran');
        
      }
    
      method2() {
        this.log('method 2 just ran');
      }
}

module.exports = MyGenerator;