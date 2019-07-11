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
        this._copyNugetConfig();

        this._copySrcFolder();
        this._copyTestsFolder();
        this._copyDeployFolder();
    }

    install () {
        var cwd = this.destinationPath(`src/${this._csprojName}.Web`);

        this.spawnCommand("npm", ["install"], { cwd: cwd })
    }

    _prepare() {
        this._csprojName = this._jsUcfirst(this._projectName);
    }

    _makeEnv() {
        this._env = {
            projectName: this._projectName,
            npmPackageName: this._jsLcfirst(this._projectName),
            angularProjectName: this._jsUcfirst(this._projectName),
            angularModuleName: this._jsLcfirst(this._projectName),
            csprojName: this._csprojName,
            projectNamespace: this._jsUcfirst(this._projectName),
            jwtIssuer: this._projectName,
            jwtAudience: this._projectName,
            dockerImageName: this._projectName.toLowerCase()
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
            this.templatePath('._gitignore'),
            this.destinationPath('.gitignore'),
            this._env
        );
    }

    _copyNugetConfig() {
        this.fs.copyTpl(
            this.templatePath('NuGet.Config'),
            this.destinationPath('NuGet.Config'),
            this._env
        );
    }

    _copySrcFolder() {
        const projects = [
            { name: 'Web' },
            { name: 'Core' },
            { name: 'DataAccess.MongoDB' },
            { name: 'DataAccess.PostgreSQL' },
            { name: 'DataAccess.SQLite' },
            { name: 'DataAccess.SQLServer' },
            { name: 'Utils' },
            { name: 'Utils.EntityFrameworkCore' },
        ];

        for (const project of projects) {
            // Copy project's folder and apply template parameters
            this.fs.copyTpl(
                this.templatePath(`src/TemplateProject.${project.name}`),
                this.destinationPath(`src/${this._csprojName}.${project.name}`),
                this._env
            );

            // Rename csproj name
            this.fs.move(
                this.destinationPath(`src/${this._csprojName}.${project.name}/TemplateProject.${project.name}.csproj`),
                this.destinationPath(`src/${this._csprojName}.${project.name}/${this._csprojName}.${project.name}.csproj`)
            );
        }
    }

    _copyTestsFolder() {
        const projects = [
            { name: 'Web' },
            { name: 'Core' },
            { name: 'Utils' }
        ];

        for (const project of projects) {
            // Copy project's folder and apply template parameters
            this.fs.copyTpl(
                this.templatePath(`tests/TemplateProject.${project.name}.Tests`),
                this.destinationPath(`tests/${this._csprojName}.${project.name}.Tests`),
                this._env
            );

            // Rename csproj name
            this.fs.move(
                this.destinationPath(`tests/${this._csprojName}.${project.name}.Tests/TemplateProject.${project.name}.Tests.csproj`),
                this.destinationPath(`tests/${this._csprojName}.${project.name}.Tests/${this._csprojName}.${project.name}.Tests.csproj`)
            );
        }
    }

    _copyDeployFolder() {
        this.fs.copyTpl(
            this.templatePath('deploy'),
            this.destinationPath('deploy'),
            this._env
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