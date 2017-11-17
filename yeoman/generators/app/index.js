'use strict';

var Generator = require('yeoman-generator');

class MyGenerator extends Generator {
    constructor(args, opts) {
        // Calling the super constructor is important so our generator is correctly set up
        super(args, opts);  
    }

      // prompting() {
      //   return this.prompt([{
      //     type    : 'input',
      //     name    : 'name',
      //     message : 'Your project name',
      //     default : this.appname // Default to current folder name
      //   }, {
      //     type    : 'confirm',
      //     name    : 'cool',
      //     message : 'Would you like to enable the Cool feature?'
      //   }]).then((answers) => {
      //     this.log('app name', answers.name);
      //     this.log('cool feature', answers.cool);
      //   });
      // }

    writing() {
        this._copySolutionFile();
    }


    _copySolutionFile() {
        this.fs.copyTpl(
            this.templatePath('TemplateSolution.sln'),
            this.destinationPath('TemplateSolution.sln')
        );
    }
}

module.exports = MyGenerator;