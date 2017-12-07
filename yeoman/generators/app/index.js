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
        this._prepare();

        this._makeEnv();

        this._copySolutionFile();
        this._copyReadme();
        this._copyGitignore();
        
        this._copySrcFolder();
    }

    install () {
        var cwd = this.destinationPath(this._env.destSrcWebProjFolder);

        this.spawnCommand("npm", ["install"], { cwd: cwd})
    }

    _prepare() {
        this._csprojName = this._jsUcfirst(this._projectName);
        this._destSrcWebProjFolder = `src/${this._csprojName}.Web`;
    }

    _makeEnv() {      
        this._env = {
            projectName: this._projectName,
            npmPackageName: this._jsLcfirst(this._projectName),
            angularModuleName: this._jsLcfirst(this._projectName),
            csprojName: this._csprojName,
            projectNamespace: this._jsUcfirst(this._projectName),
            destSrcWebProjFolder: this._destSrcWebProjFolder
        }
    }

    _copySolutionFile() {
        var destPathOrig = this.destinationPath('TemplateSolution.sln');
        var destPathFinal = this.destinationPath(`${this._csprojName}.sln`);

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
        this.fs.copyTpl(
            this.templatePath('src/TemplateProject.Web'),
            this.destinationPath(this._destSrcWebProjFolder),
            this._env
        );

        this.fs.move(
            this.destinationPath(`${this._destSrcWebProjFolder}/TemplateProject.Web.csproj`),
            this.destinationPath(`${this._destSrcWebProjFolder}/${this._csprojName}.Web.csproj`)
        );
    }

    _jsUcfirst(string) 
    {
        if (string.length < 1)
            return string;

        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    _jsLcfirst(string) 
    {
        if (string.length < 1)
            return string;

        return string.charAt(0).toLowerCase() + string.slice(1);
    }
}

module.exports = MyGenerator;