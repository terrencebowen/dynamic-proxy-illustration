using Mocksert.Arrangements;
using Mocksert.Exceptions;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Mocksert.Tests
{
    public class MockTests
    {
        [Fact]
        public void TypeVisibility()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var definedTypes = assembly.DefinedTypes;
            var type = typeof(Mock);

            var permissibleVisibleTypes = new[]
            {
                typeof(IArrangement),
                typeof(Mock),
                typeof(MockException),
                typeof(MockTests)
            };

            foreach (var definedType in definedTypes)
            {
                var permissiblePublicType = definedType.Namespace == type.Namespace ||
                                            permissibleVisibleTypes.Contains(definedType);

                if (permissiblePublicType)
                {
                    continue;
                }

                Assert.False(definedType.IsVisible, userMessage: definedType.FullName);
            }
        }
    }
}