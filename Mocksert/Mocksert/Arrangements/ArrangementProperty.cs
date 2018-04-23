using Castle.DynamicProxy;
using System.Reflection;

namespace Mocksert.Arrangements
{
    internal interface IArrangementProperty : IArrangement
    {
        bool IsReadOnlyProperty { get; }
        string PropertyName { get; }
    }

    internal class ArrangementProperty<TProxy> : IArrangementProperty where TProxy : class
    {
        public bool IsReadOnlyProperty { get; }
        public int MetadataToken { get; }
        public IProxyTargetAccessor Proxy { get; }
        public string PropertyName { get; }
        public object ReturnValue { get; private set; }

        public ArrangementProperty(TProxy proxy, PropertyInfo propertyInfo)
        {
            IsReadOnlyProperty = !(propertyInfo.CanRead && propertyInfo.CanWrite);
            MetadataToken = propertyInfo.GetMethod.MetadataToken;
            PropertyName = propertyInfo.Name;
            Proxy = (IProxyTargetAccessor)proxy;
        }

        public void SetReturnValue(object value)
        {
            ReturnValue = value;
        }
    }
}