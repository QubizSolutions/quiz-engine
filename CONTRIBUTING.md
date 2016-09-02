# Contributing to Quiz-Engine
If you would like to contribute to this project, here are a few guidelines you should follow:

- [Coding guidelines](#coding-guidelines)
- [Commit Message Guidelines](#commit-message-guidelines)

## Coding guidelines
The following coding guide lines are going to help you write consistent code

### Global guide lines(these do not apply for Javascript)
 * Pascal case for class and method names
 * Camel case for local variables, parameters and private variables
 * Dependency injected variables should be declared as ***private readonly*** and the constructor parameter should have the same name as the variable. Use **this** for variable assignment inside the constructor
 * When checking if a **Nullable< T >** has a value assigned, use **Nullable< T >.HasValue**
 * Use the **AutoMapper** extensions,  **.DeepCopyTo< T >** and **Map(source, dest)**, whenever possible to streamline the flow. This implies having the same property names on both objects which in fact is a must where possible


### Layer guide lines(this applies to each layer of the application)
 * Each layer should **accept** and **return** its own ***data types***


#### Repository guidelines
 * Repository method naming conventions are as follows(try and follow them as much as possible, exceptions might occur):
    * **List**: When listing a collection of object. Append the filter name if applied(Ex: ***ListByAdmin***, ***ListByType***, ***ListByAdminAndType***)
  <pre><code class='language-cs'>
    public interface IFoo
    {
        Foo[] List();
        Foo[] ListByAdmin(Guid adminID);
        Foo[] ListByType(FooType type);
        Foo[] ListByAdminAndType(Guid adminID, FooType type);
    }
  </code></pre>
  * **Get**: When listing a single object. Append the filter name if applied(Ex: ***GetByID***, ***GetByAdmin***, ***GetByType***, ***GetByAdminAndType***)

  <pre><code class='language-cs'>
    public interface IFoo
    {
        Foo GetByID(Guid id);
        Foo GetByAdmin(Guid adminID);
        Foo GetByType(FooType type);
        Foo GetByAdminAndType(Guid adminID, FooType type);
    }
  </code></pre>

  * **Delete**: When deleting object(s). Append the filter name if applied(Ex: ***DeleteByID***, ***DeleteByAdmin***, ***DeleteByType***, ***DeleteByAdminAndType***)
  <pre><code class='language-cs'>
    public interface IFoo
    {
        void DeleteByID(Guid id);
        void DeleteByAdmin(Guid adminID);
        void DeleteByType(FooType type);
        void DeleteByAdminAndType(Guid adminID, FooType type);
    }
  </code></pre>


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
