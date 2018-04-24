### Mocksert

Mocksert is a lightweight mocking framework illustration of Castle Project's dynamic proxy interception, and a brief overview of alternative implementations and philosophical approaches of other mocking libraries.  This project provides a basic mocking framework to illustrate the interception of proxy mocked calls.

##### Quick Peek
```csharp
public static IArrangement Arrange<TProxy, TReturn>(this TProxy proxy, Expression<Func<TProxy, TReturn>> expression)
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
```

##### What is Mocksert
What started as a spike curiosity turned into a neat example to show developers unfamiliar with mocking and the magic behind intercepting calls when testing.  It by no means is anything
I plan on publishing as an official framework.  It only mocks interfaces and classes with virtual members, and has none of the other advanced features other simple invocations and argument passing verification.

##### How it Works
1. First you create your proxy using an interface or a class with virtual members.
2. Next, you expect, set up, or arrange the property or method dependency.
3. Then, you immediately follow up with a Return extension method to specify the value that will be returned.
4. Finally, you verify that based on the arrangement you specified, that the actuall call made in the implementation matches.
    ```csharp
    public static class Mock
    {
        public static T Create<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public static T Arrange<T>(this T proxy, Expression<Func<T, dynamic>> expression)
        {
            throw new NotImplementedException();
        }

        public static void Return<T>(this T proxy, object value)
        {
            throw new NotImplementedException();
        }

        public static void Verify<T>(this T proxy) where T : class
        {
            throw new NotImplementedException();
        }
    }
    ```
##### What it Does?

The implementation of Castle Project's dynamic proxy was straight forward.

Set the start up project as Illustration for a simple illustration of intercepting a given call.

Without decompiling the sources and looking at other popular mocking frameworks:
  * Popular mocking frameworks
    * [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
    * [NSubstittue](http://nsubstitute.github.io)
    * [JustMock](https://www.telerik.com/products/mocking.aspx)
    * [RhinoMocks](https://hibernatingrhinos.com/oss/rhino-mocks) *(no longer maintained)*

After successfully intercepting a given call, I considered the fact that I ultimately needed to compare what was arranged using the following steps.
1. Create an arrangement object using the values from the arrangement lambda expression using a dictionary to store the following identifiers.
    *  Proxy Identifier: to store the unique proxy instance identifier using [ObjectIDGenerator](https://msdn.microsoft.com/en-us/library/system.runtime.serialization.objectidgenerator(v=vs.110).aspx)
    *  Property or Method Identifier: to store the idempotent member identifier using the [MetadataToken](https://msdn.microsoft.com/en-us/library/system.reflection.memberinfo.metadatatoken(v=vs.110).aspx) off of the [MemberInfo](https://msdn.microsoft.com/en-us/library/system.reflection.memberinfo(v=vs.110).aspx) class.
    ```csharp
        IDictionary<long, IDictionary<int, Queue<IArrangement>>> _arrangementQueue;
    ```
2. Assign the return value to the arrangement after the arrangement was partially created.
```csharp
    public static void Return(this IArrangement arrangement, object value)
    {
        arrangement.SetReturnValue(value);

        MockInvocationCache.Add(arrangement);
    }
```
3. Cache the arrangement in a queue after the arrangement has been completely established.
3. Cache the values from the invocation or actual call made.
```csharp
    [Serializable]
    internal class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.SetReturnValue();
        }
    }
```
4. Implement the comparison logic in the verification method which compares the arrangement to the invocation.
```csharp
    public static void Verify<TProxy>(this TProxy proxy) where TProxy : class
    {
        MockInvocationCache.Verify(proxy);
    }
```
5. Convey to the user is some meaningful way why an arrangement may have failed in the event an expectation was not met.
```csharp
    public string GetArrangementUninvokedFailureMessage(string memberName, int arrangementCount, int invocationCount)
    {
        var failureMessage = $"{memberName} was arranged {"times".ToQuantity(arrangementCount)}, but was called {"times".ToQuantity(invocationCount)}.";

        return failureMessage;
    }
```