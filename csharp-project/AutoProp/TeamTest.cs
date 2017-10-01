using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace dotnet
{
    public class TeamTests
    {
        [Fact]
        public void TeamTest()
        {
            var team = new Team();
            
            
            Assert.NotEqual(default(DateTime), team.CreatedDate);

            Assert.NotNull(team.Players);
        }
    }
}