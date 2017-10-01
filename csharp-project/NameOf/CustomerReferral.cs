// { autofold
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
// }
    public class CustomerReferral
    {
        public string Email { get; set; }
        public string ReferrerId { get; set; }
        public string ReferralLink
            => $"https://mysite.com/referrals?{GetNameOfProperty(this, r => r.Email)}={Email}&{GetNameOfProperty(this, r => r.ReferrerId)}={ReferrerId}";

        public static string GetNameOfProperty<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> lambdaOfProperty)
        {
            var memberExpression = lambdaOfProperty.Body as MemberExpression;
            //in a production app we check for null
            var name = memberExpression.Member.Name;

            return name;
        }
    }
// { autofold    
}
// }