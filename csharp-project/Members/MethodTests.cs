// { autofold
using System;
using Xunit;

namespace dotnet
{
    public partial class MemberTests
    {
// }
        [Fact]
        public void ExpressionBodiedMethodTest()
        {
            var salesPerson = new SalesPerson { TotalSales = 10000 };

            var commisionPercentage = 10;

            var commision = salesPerson.CalculateCommission(commisionPercentage);
            //Given commision percentage of 10 percent we should get 10 percent of 10000
            Assert.Equal(commision, 1000);
        }
// { autofold        
    }
}
// }