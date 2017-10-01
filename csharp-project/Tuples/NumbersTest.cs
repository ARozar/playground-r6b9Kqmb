// { autofold
using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class NumbersTests
    {
// }        
        [Fact]
        public void NumberTest()
        {
            var inputOne = 4;
            var inputTwo = 5;

            var numbers = Numbers.GetNumbers(inputOne, inputTwo);
            
            Assert.Equal(Math.Pow(inputOne, inputOne), numbers.one);

            Assert.Equal(Math.Pow(inputTwo, inputTwo), numbers.two);
        }
// { autofold        
    }
}
//