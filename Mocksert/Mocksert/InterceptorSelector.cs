using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Mocksert
{
    [Serializable]
    internal class InterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo methodInfo, IInterceptor[] interceptors)
        {
            return interceptors;
        }
    }
}