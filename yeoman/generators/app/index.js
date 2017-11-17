'use strict';

var Generator = require('yeoman-generator');

class MyGenerator extends Generator {
    constructor(args, opts) {
        // Calling the super constructor is important so our generator is correctly set up
        super(args, opts);  
    }

      prompting() {
        return this.prompt([{
            type    : 'input',
            name    : 'projectName',
            message : 'Your project name',
            default : 'Test'
        }]).then((answers) => {
            this._projectName = answers.projectName;
        });
    }

    writing() {
        this._copySolutionFile();
    }

    _copySolutionFile() {
        this.fs.copyTpl(
            this.templatePath('TemplateSolution.sln'),
            this.destinationPath(`${this._projectName}.sln`)
        );
    }
}

module.exports = MyGenerator;