using System;
using Castle.DynamicProxy;

namespace Mocksert
{
    [Serializable]
    internal class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.SetReturnValue();
        }
    }
}