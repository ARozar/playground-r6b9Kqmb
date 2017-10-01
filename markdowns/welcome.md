# Hands-on Demo

@[Try for yourself! Update the code for the 'FullName' property to pass the test]({"stubs": ["Members/SalesPerson.cs", "Members/PropertyTests.cs"],"command": "dotnet.MemberTests.ExpressionBodiedPropertyTest"})

@[Try for yourself! Update the code for the 'CalculateCommission' method to pass the test]({"stubs": ["Members/SalesPerson.cs", "Members/MethodTests.cs"],"command": "dotnet.MemberTests.ExpressionBodiedMethodTest"})

@[Try for yourself! Update the code for the 'CalculateCommission' method to pass the test]({"stubs": ["NameOf/CustomerReferral.cs", "NameOf/NameOfTests.cs"],"command": "dotnet.MemberNameTests.NameOfTest"})

C# is a language that has really evolved over the years and is now the standard bearer for all the platforms that use the .Net runtime.   From its origins as a rival to Java in the area of enterprise software development .Net itself seems to be changing from its origins of one runtime with many languages to one language that runs on every platform.  Fortunately the last 2 versions of the language have seen some really nice additions to the syntax that make the code more expressive, terse and maintainable.

Here is a list of five features that I've found increasingly more useful as part of my regular workflow.

## String Interpolation (C# 6)
This is such a common sense addition to the language.  Instead of concatenating strings or using placeholders based on ordinal position such as:
```csharp
var title = "Mr";
var firstName = "Joe";
var lastName = "Bloggs";
            
var fullName = String.Format("{0} {1} {2}", title, firstName, lastName);
```
We can do:

```csharp
var fullName = $"{title} {firstName} {lastName}";
```
As well as creating readable code it is also performant. As all C# developers know it is best practice to use a StringBuilder when concatenating multiple strings due to string immutability.  Fortunately, this feature is merely syntactic sugar and the IL that is generated makes a call to System.String.Format which uses the StringBuilder class under the hood.

##Auto Property Initializers (C# 6)
Another feature that really makes for terse and readable code is Auto Property Initialization.  Quite often we need to initialize default values in the constructor of a class such as in the example below. 
```csharp
public class Customer
{
    public int Id { get; set; }
    public ICollection<Order> Orders { get; set; }
    public DateTime DateCreated { get; set; }

    public Customer()
    {
        Orders = new List<Order>();
        DateCreated = DateTime.UtcNow;
    }
}
```
We may want to initialize an empty collection to reduce the chance of null references and anyone who works with an ORM will have experienced the mismatch between the default date of C#'s DateTime type (00:00:00.0000000 UTC, January 1, 0001) and Sql Server's DateTime or DateTime2 types (January 1, 1753 and January 1, 1900 respectively).

Now we can write:
```csharp
public class Customer
{
    public int Id { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
```
Great when used appropriately both for the creator of the code as well as those reading it later.  And it's great when prototyping and trying things out.
##Expression Bodied Members (C# 6)
One of the most tedious tasks for any developer is to have to create a property getter just to offer a basic computed property such as the combination of a First and Last name.  Expression-bodied members offer us a nice new shorthand syntax for such situations.

```csharp
public string FullName => $"{FirstName} {LastName}";
```
I've found this particularly useful for functions that show any kind of mathmatical formula such as: 

```csharp
public decimal CalculateCommission (decimal percentage) => TotalSales *  percentage / 100; 
```

Which combined with the previous class would give us a class looking like:

```csharp
public class SalesPerson 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal TotalSales { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public decimal CalculateCommission (decimal percentage) => TotalSales * percentage / 100; 
}
```
Like so many other of the other recent features in the language, this really adds to the expressive power of the code that can be written.  Something that has been at the forefront of the languages' evolution since .Net 3.5 introduced LINQ, extension methods, and anonymous types. 
##Nameof (C# 6)
From template bindings in XAML, Razor Syntax that creates model-bound HTML or even logging, there are many situations where we want to record or express the name of a Type, Method or more commonly property.  Doing this as a string literal is both the most obvious and obviously worst way of doing this.  A more sophisticated approach might be to try the following:

```csharp
public static string GetNameOfProperty<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> lambdaOfProperty)
{
    var memberExpression = lambdaOfProperty.Body as MemberExpression;
    //in a production app we check for null as
    //the code might point to a public method
    var name = memberExpression.Member.Name;

    return name;
}
```
Great in certain situations (such as for forms in Razor, expressions a generally awesome when doing anything that goes from C# to another context, in this case, HTML) but it doesn't really scale well.
C# 5 gave us the "CallerMemberName" attribute.  This allows us to do the following

```csharp
public void LogError(string message, [CallerMemberName]string caller = null)
   {
            Logger.Error($"Error in {caller} - {message}");        
}
```
Calling our LogError method will automatically populate the caller variable with the name of the method or property that called the method if no string is provided for the caller parameter.  This is great as it covers us in a large number of situations but if we needed to log some kind of side-effect then we may need to resort to strings. 

Enter nameof:

```csharp
        public void LogError(string message, [CallerMemberName]string caller = null)
{
    Logger.Error($"Error in {caller} - {message}");

    Logger.Error($"Failed to call {nameof(Save)} due to error {message}");
}
```
This gives us a refactor safe way of naming members of a type.  Expressions are great for making the writer of the code feel really smart and knowledgeable but "nameof" provides something that is intuitive, terse, and easy to understand.

Jeremy Bytes has a [great post](https://jeremybytes.blogspot.co.uk/2016/02/callermembername-vs-nameof-in.html) on this subject in the context of change notification of properties and computed properties.

##Tuples (ValueTuples C# 7)
One big limitation of C# has always been the lack of an easy way to return multiple values from a method.  Whilst it's possible to use out parameters or create a class to hold the return values, the first solution creates a code flow that is in-elegant and the second can very quickly result in class explosion and creates a level of obstruction that simply doesn't exist in many other languages.  With the new ValueTuple, we finally have a great means (as well as expressive and intuitive syntax) by which we can do this:

```csharp
public (int one, int two) GetNumbers(int first, int second)
{
    return(first/2, second/2);
}
```

Which can then be used:

```csharp
public void NumberTest()
{
    var numbers = GetNumbers(4,4);
    Assert.Equal(Math.Pow(4,4), numbers.one + numbers.two);
}
```
NB: This test would fail.

It's also possible to destructure values from a returned tuple directly into local variables.  

```csharp
(var success, var message)  = Login("testUserName","testPassword");
```

To me, this is a key piece of what makes this a great feature.  Provided the elements of the Tuple hold real semantic meaning, we give are able to achieve a high level of expressivity about the intent of our code, as well as keeping down the code bloat.

There are a couple of things to note about this feature.

#### It's a NuGet package
If you want to take advantage of this feature you're going to have to install it.  You'll have to get the [System.ValueTuple Nuget package](https://www.nuget.org/packages/System.ValueTuple/).  Like most things in the new world of .Net this is a package that you opt-in to using.

#### It's a value type
There is a [System.Tuple](https://msdn.microsoft.com/en-us/library/system.tuple(v=vs.110).aspx) class already in the Framework.  When using this there are a few things to consider.

In instances where the assignment is equal to or greater than allocation then there is the possibility of performance considerations.  The following thread on [GitHub from the C# team](https://github.com/dotnet/roslyn/issues/347) provides some insight into this and much of the feature.

There are some issues to do with serialization to consider:
```csharp
public (bool success, string message) Login(string username, string password)
{
    //do some complex logic
    return (false, AccountLockedMessage);
}
```
The same lines of code compile to:

```csharp
[return: TupleElementNames(new string[] { "success", "message" })]
public ValueTuple<bool, string> Login(string username, string password)
{
    return new ValueTuple<bool, string>(false, AccountLockedMessage);
}
```
Our named elements are not more at runtime!  For example, when serializing our type we get:
```javascript
{ 
    "Item1": false, 
    "Item2": "Your account has been locked.  Please try again in 5 minutes" 
}
```
Serializers use reflection and obviously, our syntactic sugar doesn't exist at runtime.

Of course, it's pretty easy to map this to an anonymous type, assuming the tuple has only a few elements. (More than this and I'd start thinking a class definition is in order anyway)  I'm really enthused about this feature as its one of the things I feel developers who come from other languages that offer this kind of flexibility often lament and anything that makes the language more expressive and accessible is a good thing in my humble opinion.

##And an honorable mention for Pattern Matching(C# 7)
So this is one feature that I have somewhat conflicted views on.  It lets us perform matching on the shape of types so we can for instance do:
```csharp
public static int GetIntValue(object obj)
{
    if (obj is null)
        return default(int);

    if (obj is int i || obj is string s && int.TryParse(s, out i))
    {
        return i;
    }

    return default(int);
}
```
Allowing us to handle either an integer or fall back to trying to coalesce the value from a string in one line of conditional logic.  Yet a couple of times now when I've introduced this one to colleagues it has been met with a WTF moment. Now, not all developers are fond of change (no really honestly not all are ;) ) and in some cases, this one could be really useful.

But then also consider:

```csharp
public Reaction Confront(Animal adversary)
{
    switch (adversary)
    {
        case Lion L:
            return Reaction.Run;
        case Cat scaryCat when scaryCat.HasSharpClaws:
            return Reaction.Run;
        case Cat cat when !cat.HasSharpClaws:
            return Reaction.WagTail;
        case Dog D:
            return Reaction.Growl;
        default:
            return Reaction.WagTail;
    }
}
```
Again matching on the shape of an object is really cool and there are times such as handling events where this could be really useful as we are often passed an object in this scenario.

The thing that just vexes me slightly is that sometimes a switch statement can be a sign that the Open / Closed principle is being taken somewhat lightly.  If every new implementation results in our switch statement getting bigger, then perhaps something like the Strategy pattern might be in order that would see us inverting the responsibility of handling the implementation of our logic to some common abstraction.  Still, anyone that has worked with Redux can appreciate how effective the old switch statement can be in the right context.
##Final Thoughts
It really is a great time to be using both C# and the .Net Framework.  C# continues to evolve in its expressiveness and flexibility making it a great choice for server side processing as well as UI platforms.  That coupled with the increasing performance improvements found in [.Net Core](https://blogs.msdn.microsoft.com/dotnet/2017/06/07/performance-improvements-in-net-core/) and [most notably in Asp.Net Core](https://blogs.msdn.microsoft.com/dotnet/2017/06/07/performance-improvements-in-net-core/) (not the fastest of them all but OMG when compared to where recent versions were) and its ever expanding reach (now an option for the Linux world) the language and platform seem to be headed in a positive and upward trajectory.

PS: If you have a favorite feature that I've missed I'd love to hear about your favorite new aspect of the language.