using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class MemberNameTests
    {       
        [Fact]
        public void NameOfTest()
        {
            var customerReferral = new CustomerReferral { Email = "test@test.com", ReferrerId = "CX-49" };

            var referralLink = customerReferral.ReferralLink;

            Assert.True(referralLink.Contains($"Email={customerReferral.Email}"));
            Assert.True(referralLink.Contains($"ReferrerId={customerReferral.ReferrerId}"));
        }

    }
}
