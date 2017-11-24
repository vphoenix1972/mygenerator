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
        this._makeEnv();

        this._copySolutionFile();
        this._copyReadme();
        this._copyGitignore();
        
        this._copySrcFolder();
    }

    _copySolutionFile() {
        var destPathOrig = this.destinationPath('TemplateSolution.sln');
        var destPathFinal = this.destinationPath(`${this._projectName}.sln`);

        this.fs.copyTpl(
            this.templatePath('TemplateSolution.sln'),
            destPathOrig,
            this._env
        );

        this.fs.move(destPathOrig, destPathFinal);
    }

    _copyReadme() {
        this.fs.copyTpl(
            this.templatePath('README.md'),
            this.destinationPath('README.md'),
            this._env
        );
    }

    _copyGitignore() {
        this.fs.copyTpl(
            this.templatePath('.gitignore'),
            this.destinationPath('.gitignore'),
            this._env
        );
    }

    _copySrcFolder() {
        var destSrcWebProjFolder = `src/${this._projectName}.Web`;

        this.fs.copyTpl(
            this.templatePath('src/TemplateProject.Web'),
            this.destinationPath(destSrcWebProjFolder),
            this._env
        );

        this.fs.move(
            this.destinationPath(`${destSrcWebProjFolder}/TemplateProject.Web.csproj`),
            this.destinationPath(`${destSrcWebProjFolder}/${this._projectName}.Web.csproj`)
        );
    }

    _makeEnv() {
        this._env = {
            projectName: this._projectName
        }
    }
}

module.exports = MyGenerator;