using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class Security
    {
        public const string AccountLockedMessage = "Your account has been locked.  Please try again in 5 minutes";

        public static (bool success, string message) Login(string username, string password)
        {
            //potentially do some complex logic
            return (true, "Login was fine");
        }
    }
}