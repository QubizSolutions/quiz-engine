# Coding guidelines

## Global guide lines(these do not apply for Javascript)
 * Pascal case for class and method names
 * Camel case for local variables, parameters and private variables
 * Dependency injected variables should be declared as ***private readonly*** and the constructor parameter should have the same name as the variable. Use **this** for variable assignment inside the constructor
 * When checking if a **Nullable< T >** has a value assigned, use **Nullable< T >.HasValue**
 * Use the **AutoMapper** extensions,  **.DeepCopyTo< T >** and **Map(source, dest)**, whenever possible to streamline the flow. This implies having the same property names on both objects which in fact is a must where possible


## Layer guide lines(this applies to each layer of the application)
 * Each layer should **accept** and **return** its own ***data types***


## Repository guidelines
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
