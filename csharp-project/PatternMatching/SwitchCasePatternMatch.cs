using System.ComponentModel;
using Xunit;

namespace dotnet
{
    public enum Reaction
    {
        Run,
        Growl,
        WagTail,
        Bark
    }
    public class Animal
    {
    }

    public class Lion : Animal
    {
    }

    public class Cat : Animal
    {
        public bool HasSharpClaws { get; set; }
    }

    public class Dog : Animal
    {
        public Reaction Confront(Animal advesary)
        {
            switch (advesary)
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
    }


    public class TypePattenSwitchTests
    {
        [Fact]
        public void CanConvertIntFromString()
        {
            var dog = new Dog();

            var cat =  new Cat { HasSharpClaws = true };

            var reaction = dog.Confront(cat);

            Assert.True(reaction == Reaction.Run);
        }

    }

}
