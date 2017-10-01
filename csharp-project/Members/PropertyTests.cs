// { autofold
using System;
using Xunit;

namespace dotnet
{
    public partial class MemberTests
    {
// }
        [Fact]
        public void ExpressionBodiedPropertyTest()
        {
            var salesPerson = new SalesPerson { FirstName = "Andrew", LastName = "Smith" };

            Assert.True(salesPerson.FullName.StartsWith(salesPerson.FirstName));

            Assert.True(salesPerson.FullName.EndsWith(salesPerson.LastName));
        }        
// { autofold
    }
}
// }