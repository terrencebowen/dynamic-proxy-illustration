using Castle.DynamicProxy;
using Mocksert.Arrangements;
using Mocksert.Extensions;
using System;
using System.Linq.Expressions;

namespace Mocksert
{
    public static class Mock
    {
        public static TProxy Create<TProxy>() where TProxy : class
        {
            var proxyType = typeof(TProxy);
            var interceptor = new Interceptor();
            var proxyGenerationHook = new ProxyGenerationHook();
            var interceptorSelector = new InterceptorSelector();
            var proxyGenerationOptions = new ProxyGenerationOptions(proxyGenerationHook) { Selector = interceptorSelector };
            var proxyGenerator = new ProxyGenerator();

            var proxy = proxyType.IsInterface
                 ? proxyGenerator.CreateInterfaceProxyWithoutTarget<TProxy>(proxyGenerationOptions, interceptor)
                 : proxyGenerator.CreateClassProxy<TProxy>(proxyGenerationOptions, interceptor);

            MockInvocationCache.Clear(proxy);

            return proxy;
        }

        public static void Arrange<TProxy>(this TProxy proxy, Expression<Action<TProxy>> expression) where TProxy : class
        {
            var arrangement = expression.CreateArrangementMethodVoid(proxy);

            MockInvocationCache.Add(arrangement);
        }

        public static IArrangement Arrange<TProxy, TReturn>(this TProxy proxy, Expression<Func<TProxy, TReturn>> expression) where TProxy : class
        {
            IArrangement arrangement;

            var isPropertyArrangement = expression.Body is MemberExpression;

            if (isPropertyArrangement)
            {
                arrangement = expression.CreateArrangementProperty(proxy);
            }
            else
            {
                arrangement = expression.CreateArrangementMethod(proxy);
            }

            return arrangement;
        }

        public static void Return(this IArrangement arrangement, object value)
        {
            arrangement.SetReturnValue(value);

            MockInvocationCache.Add(arrangement);
        }

        public static void Verify<TProxy>(this TProxy proxy) where TProxy : class
        {
            MockInvocationCache.Verify(proxy);
        }
    }
}