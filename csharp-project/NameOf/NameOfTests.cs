using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class MemberNameTests
    {       public class CustomerReferral
        {
            public string Email { get; set; }
            public string ReferrerId { get; set; }
            public string ReferralLink
                => $"https://mysite.com/referrals?{nameof(Email)}={Email}&{nameof(ReferrerId)}={ReferrerId}";

            public string ReferralLinkVerbose
                => $"https://mysite.com/referrals?{GetNameOfProperty(this, r => r.Email)}={Email}&{GetNameOfProperty(this, r => r.ReferrerId)}={ReferrerId}";
        }
        public static string GetNameOfProperty<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> lambdaOfProperty)
        {
            var memberExpression = lambdaOfProperty.Body as MemberExpression;
            //in a production app we check for null
            //the code might point to a public method
            var name = memberExpression.Member.Name;

            return name;
        }
        [Fact]
        public void ExpressionForNameTest()
        {
            var customerReferral = new CustomerReferral { };

            var propertyName = GetNameOfProperty(customerReferral, c => c.Email);
            Assert.Equal(propertyName, "Email");
        }
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
