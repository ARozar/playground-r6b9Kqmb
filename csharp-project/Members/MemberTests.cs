using System;
using Xunit;

namespace dotnet
{
    public class MemberTests
    {
        public class SalesPerson 
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal TotalSales { get; set; }
            public string FullName => $"{FirstName} {LastName}";
            public decimal CalculateCommission (decimal percentage) => TotalSales *  percentage / 100; 
        }
        [Fact]
        public void ExpressionBodiedPropertyTest()
        {
            var salesPerson = new SalesPerson { FirstName = "Andrew", LastName = "Smith" };

            Assert.True(salesPerson.FullName.StartsWith(salesPerson.FirstName));

            Assert.True(salesPerson.FullName.EndsWith(salesPerson.LastName));
        }
        [Fact]
        public void ExpressionBodiedMethodTest()
        {
            var salesPerson = new SalesPerson { TotalSales = 10000 };

            var commision = salesPerson.CalculateCommission(10);

            Assert.True(commision == 1000);
        }
        
    }
}
