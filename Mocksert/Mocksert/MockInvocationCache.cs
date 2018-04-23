using Castle.DynamicProxy;
using Mocksert.Arrangements;

namespace Mocksert
{
    internal static class MockInvocationCache
    {
        private static readonly IMockInvocationAuditor MockInvocationAuditor = new MockInvocationAuditor();

        public static void Add(IArrangement arrangement)
        {
            MockInvocationAuditor.AddArrangement(arrangement);
        }

        public static void SetReturnValue(this IInvocation invocation)
        {
            MockInvocationAuditor.UpdateInvocationCount(invocation);

            var arrangement = MockInvocationAuditor.GetArrangement(invocation);

            invocation.ReturnValue = arrangement.ReturnValue;

            MockInvocationAuditor.EnqueueInvocation(invocation);
            MockInvocationAuditor.VerifyGenericTypeArguments(arrangement, invocation);
            MockInvocationAuditor.VerifyArgumentValues(arrangement, invocation);
            MockInvocationAuditor.DequeueInvocation(invocation);
        }

        public static void Verify<TProxy>(this TProxy proxy) where TProxy : class
        {
            var proxyIdentifier = MockInvocationAuditor.GetProxyIdentifier(proxy);

            MockInvocationAuditor.VerifyArrangementInvocationCounts(proxyIdentifier);
            MockInvocationAuditor.Clear(proxyIdentifier);
        }

        public static void Clear<TProxy>(TProxy proxy) where TProxy : class
        {
            var proxyIdentifier = MockInvocationAuditor.GetProxyIdentifier(proxy);

            MockInvocationAuditor.Clear(proxyIdentifier);
        }
    }
}