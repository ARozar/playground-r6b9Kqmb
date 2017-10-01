// { autofold
using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class SecurityTests
    {
// }
        [Fact]
        public void LoginTest()
        {
            (var success, var message)  = Security.Login("testUserName","testPassword");

            Assert.False(success);
            Assert.Equal(Security.AccountLockedMessage, message);
        }
// { autofold        
    }
}
//