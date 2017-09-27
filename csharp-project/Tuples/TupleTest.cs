using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class TupleTests
    {
        public string AccountLockedMessage = "Your account has been locked.  Please try again in 5 minutes";

        [Fact]
        public void NumberTest()
        {
            var numbers = Numbers.GetNumbers(4,4);
            Assert.Equal(Math.Pow(4,4), numbers.one + numbers.two);
        }

        [Fact]
        public void LoginTest()
        {
            (var success, var message)  = Security.Login("testUserName","testPassword");

            Assert.False(success);
            Assert.True(message.CompareTo(Security.AccountLockedMessage) == 0);
        }

    }
}
