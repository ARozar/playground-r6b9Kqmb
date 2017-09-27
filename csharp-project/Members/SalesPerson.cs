using System;
using Xunit;

namespace dotnet
{

    public class SalesPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalSales { get; set; }
        public string FullName => "";
        public decimal CalculateCommission(decimal percentage) => percentage;
    }

}
