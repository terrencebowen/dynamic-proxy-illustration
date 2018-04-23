using Castle.DynamicProxy;
using Humanizer;
using KellermanSoftware.CompareNetObjects;
using Mocksert.Arrangements;
using Mocksert.Extensions;
using System.Linq;

namespace Mocksert.Exceptions
{
    internal interface IMockFailureMessageFactory
    {
        string GetNullProxyFailureMessage();
        string GetUnarrangedInvocationFailureMessage(string memberName, int arrangementCount, int invocationCount);
        string GetGenericTypeArgumentFailureMessage(IArrangement arrangement, IInvocation invocation);
        string GetInvalidArgumentValueFailureMessage(IArrangement arrangement, IInvocation invocation);
        string GetArrangementUninvokedFailureMessage(string memberName, int arrangementCount, int invocationCount);
    }

    internal class MockFailureMessageFactory : IMockFailureMessageFactory
    {
        public string GetNullProxyFailureMessage()
        {
            return "mock proxy instance cannot be null.";
        }

        public string GetUnarrangedInvocationFailureMessage(string memberName, int arrangementCount, int invocationCount)
        {
            var failureMessage = $"{memberName} was arranged {"times".ToQuantity(arrangementCount)}, " +
                                 $"but was called {"times".ToQuantity(invocationCount)}.";

            return failureMessage;
        }

        public string GetGenericTypeArgumentFailureMessage(IArrangement arrangement, IInvocation invocation)
        {
            var arrangementMethod = (IArrangementMethod)arrangement;
            var memberName = arrangementMethod.MethodName;
            var arrangementMethodGenericArguments = arrangementMethod.GenericArguments;
            var arrangementFriendlyArguments = arrangementMethodGenericArguments.Select(type => type.GetFriendlyTypeName());
            var arrangementFriendlyArgumentsCommaSeparated = $"<{string.Join(", ", arrangementFriendlyArguments)}>";
            var invocationGenericArguments = invocation.GenericArguments;
            var invocationFriendlyArguments = invocationGenericArguments.Select(type => type.GetFriendlyTypeName());
            var invocationFriendlyArgumentsSeparated = $"<{string.Join(", ", invocationFriendlyArguments)}>";

            var failureMessage = $"{memberName} was arranged with generic type(s) {arrangementFriendlyArgumentsCommaSeparated}, " +
                                 $"but was called with the generic type(s) {invocationFriendlyArgumentsSeparated}.";

            return failureMessage;
        }

        public string GetInvalidArgumentValueFailureMessage(IArrangement arrangement, IInvocation invocation)
        {
            var compareLogic = new CompareLogic();
            var arrangementMethod = (IArrangementMethod)arrangement;
            var arrangementArguments = arrangementMethod.ArrangementArguments;

            IArrangementArgument arrangementArgument = null;

            for (var index = 0; index < arrangementArguments.Length; index++)
            {
                var arrangementArgumentValue = arrangementArguments[index].ArgumentValue;
                var invocationArgumentValue = invocation.Arguments[index];
                var comparisonResult = compareLogic.Compare(arrangementArgumentValue, invocationArgumentValue);

                if (comparisonResult.AreEqual)
                {
                    continue;
                }

                arrangementArgument = arrangementArguments[index];
                break;
            }

            var ordinalPosition = arrangementArgument?.OrdinalPosition;
            var argumentName = arrangementArgument?.ArgumentName;
            var failureMessage = $"{arrangementMethod.MethodName} was not called with arranged argument " +
                                 $"for the {ordinalPosition} argument {{{argumentName}}}.";

            return failureMessage;
        }

        public string GetArrangementUninvokedFailureMessage(string memberName, int arrangementCount, int invocationCount)
        {
            var failureMessage = $"{memberName} was arranged {"times".ToQuantity(arrangementCount)}, " +
                                 $"but was called {"times".ToQuantity(invocationCount)}.";

            return failureMessage;
        }
    }
}