using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class TupleTest
    {
        public string AccountLockedMessage = "Your account has been locked.  Please try again in 5 minutes";

        [Fact]
        public void NumberTest()
        {
            var numbers = GetNumbers(4,4);
            Assert.Equal(Math.Pow(4,4), numbers.one + numbers.two);
        }

        [Fact]
        public void LoginTest()
        {
            (var success, var message)  = Login("testUserName","testPassword");

            Assert.False(success);
            Assert.True(message.CompareTo(AccountLockedMessage) == 0);
        }

        public (int one, int two) GetNumbers(int first, int second)
        {
            return(first/2, second/2);
        }

        public (bool success, string message) Login(string username, string password)
        {
            //do some complex logic
            return (false, AccountLockedMessage);
        }


    }
}
