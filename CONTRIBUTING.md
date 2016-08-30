# Contributing to Quiz-Engine
If you would like to contribute to this project, here are a few guidelines you should follow:

- [Coding guidelines](#coding-guidelines)
- [Commit Message Guidelines](#commit-message-guidelines)

## Coding guidelines
The following coding guide lines are going to help you write consistent code

###Global guide lines(these do not apply for Javascript)
 * Pascal case for class and method names
 * Camel case for local variables, parameters and private variables
 * Dependency injected variables should be declared as ***private readonly*** and the constructor parameter should have the same name as the variable. Use **this** for variable assignment inside the constructor

## Commit Message Guidelines
These have been imported from https://github.com/angular/angular/blob/master/CONTRIBUTING.md#commit and slightly modified.

### Type
Must be one of the following:

* **feat**: A new feature
* **fix**: A bug fix
* **docs**: Documentation only changes
* **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
* **refactor**: A code change that neither fixes a bug nor adds a feature
* **perf**: A code change that improves performance
* **test**: Adding missing tests
* **chore**: Changes to the build process or auxiliary tools and libraries such as documentation generation
* **ci**: Changes to our CI configuration files and scripts (example scopes: App-Veyor)
