using System.ComponentModel;
using Xunit;

namespace dotnet
{
    public class TypePattenTests
    {
        public static int GetIntValue(object obj)
        {
            if (obj is null)
                return default(int);

            if (obj is int i || obj is string s && int.TryParse(s, out i))
            {
                return i;
            }

            return default(int);
        }

        [Fact]
        public void CanConvertIntFromString()
        {
            var initialValue = "1234";

            var result = GetIntValue(initialValue);

            Assert.True(result == 1234);
        }

    }
}
