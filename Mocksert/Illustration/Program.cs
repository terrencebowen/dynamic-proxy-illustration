using Castle.DynamicProxy;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace Illustration
{
    public interface ICalculator
    {
        int Divide(int dividend, int divisor);
    }

    public class ClassUnderTest
    {
        private readonly ICalculator _calculator;

        public ClassUnderTest(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public int MethodUnderTest(int dividend, int divisor)
        {
            var quotient = _calculator.Divide(dividend, divisor);

            return quotient;
        }
    }

    public class Program
    {
        public static void Main()
        {
            var calculator = Mock.Create<ICalculator>();
            var testObject = new ClassUnderTest(calculator);

            const int dividend = 100;
            const int divisor = 4;
            const int expected = 7;

            calculator.Arrange(x => x.Divide(dividend, divisor)).Return(expected);

            var actual = testObject.MethodUnderTest(dividend, divisor);

            Assert.Equal(expected, actual);

            calculator.Verify();
        }
    }

    public static class Mock
    {
        public static T Create<T>() where T : class
        {
            var interceptor = new Interceptor();
            var proxyGenerationHook = new ProxyGenerationHook();
            var interceptorSelector = new InterceptorSelector();
            var proxyGenerationOptions = new ProxyGenerationOptions(proxyGenerationHook) { Selector = interceptorSelector };
            var proxyGenerator = new ProxyGenerator();
            var proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget<T>(proxyGenerationOptions, interceptor);

            return proxy;
        }

        public static T Arrange<T>(this T proxy, Expression<Func<T, dynamic>> expression)
        {
            return proxy;
        }

        public static void Return<T>(this T proxy, object value)
        {
        }

        public static void Verify<T>(this T proxy) where T : class
        {
        }
    }

    [Serializable]
    public class ProxyGenerationHook : IProxyGenerationHook
    {
        // First method executed here, hard code true or provide logic to decide not to handle this method.
        public bool ShouldInterceptMethod(Type type, MethodInfo memberInfo)
        {
            return true;
        }

        // First method executed here if the member just can't be proxied at all.
        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        // Finally, this method is executed when all inspection is complete, place any needed clean up logic here.
        public void MethodsInspected()
        {
        }
    }

    [Serializable]
    public class InterceptorSelector : IInterceptorSelector
    {
        // First method called when the proxy object is invoked in some way.
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            return interceptors;
        }
    }

    [Serializable]
    public class Interceptor : IInterceptor
    {
        // This gets called after the InterceptSelector with the invocation.
        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = 7;
        }
    }
}