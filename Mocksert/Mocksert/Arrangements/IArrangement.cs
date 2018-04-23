using Castle.DynamicProxy;

namespace Mocksert.Arrangements
{
    public interface IArrangement
    {
        int MetadataToken { get; }
        IProxyTargetAccessor Proxy { get; }
        object ReturnValue { get; }
        void SetReturnValue(object value);
    }
}