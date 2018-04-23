using Castle.DynamicProxy;
using Mocksert.Extensions;
using System;
using System.Reflection;

namespace Mocksert.Arrangements
{
    internal interface IArrangementMethod : IArrangement
    {
        IArrangementArgument[] ArrangementArguments { get; }
        Type[] GenericArguments { get; }
        bool IsVoidMethod { get; }
        string MethodName { get; }
    }

    internal class ArrangementMethod<TProxy> : IArrangementMethod where TProxy : class
    {
        public IArrangementArgument[] ArrangementArguments { get; }
        public Type[] GenericArguments { get; }
        public bool IsVoidMethod { get; }
        public int MetadataToken { get; }
        public IProxyTargetAccessor Proxy { get; }
        public string MethodName { get; }
        public object ReturnValue { get; private set; }

        public ArrangementMethod(TProxy proxy, MethodBase methodBase, IArrangementArgument[] arrangeArguments, bool isVoidMethod)
        {
            ArrangementArguments = arrangeArguments;
            GenericArguments = methodBase.GetGenericArguments();
            IsVoidMethod = isVoidMethod;
            MetadataToken = methodBase.MetadataToken;
            MethodName = methodBase.Name;
            Proxy = (IProxyTargetAccessor)proxy;
        }

        public void SetReturnValue(object value)
        {
            ReturnValue = value;
        }
    }
}