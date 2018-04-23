using System.Collections.Generic;

namespace AnyClassLibrary
{
    public interface IAnyInterfaceDependency
    {
        IList<int> AnyGetOnlyProperty { get; }
        IList<int> AnyGetSetProperty { get; set; }
        void AnyMethodVoid();
        void AnyMethodVoidWithParams(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3);
        void AnyMethodWithSingleGenericType<T>();
        void AnyMethodWithMultipleGenericTypes<T1, T2>();
        IList<int> AnyMethodWithReturnValue();
        IList<int> AnyMethodWithReturnValueAndParams(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3);
    }

    public class AnyClassUnderTestWithInterfaceMembers
    {
        private readonly IAnyInterfaceDependency _anyInterfaceDependency1;
        private readonly IAnyInterfaceDependency _anyInterfaceDependency2;
        private readonly IAnyInterfaceDependency _anyInterfaceDependency3;

        public AnyClassUnderTestWithInterfaceMembers(IAnyInterfaceDependency anyInterfaceDependency1,
                                                     IAnyInterfaceDependency anyInterfaceDependency2,
                                                     IAnyInterfaceDependency anyInterfaceDependency3)
        {
            _anyInterfaceDependency1 = anyInterfaceDependency1;
            _anyInterfaceDependency2 = anyInterfaceDependency2;
            _anyInterfaceDependency3 = anyInterfaceDependency3;
        }

        public void AnyMethodUnderTest_With_Multiple_Dependency_Calls<T1, T2>()
        {
            var value01 = _anyInterfaceDependency1.AnyGetOnlyProperty;
            var value02 = _anyInterfaceDependency2.AnyGetOnlyProperty;
            var value03 = _anyInterfaceDependency3.AnyGetOnlyProperty;

            var value04 = _anyInterfaceDependency1.AnyGetSetProperty;
            var value05 = _anyInterfaceDependency2.AnyGetSetProperty;
            var value06 = _anyInterfaceDependency3.AnyGetSetProperty;

            _anyInterfaceDependency1.AnyMethodWithSingleGenericType<T1>();
            _anyInterfaceDependency2.AnyMethodWithSingleGenericType<T2>();
            _anyInterfaceDependency3.AnyMethodWithSingleGenericType<T1>();

            _anyInterfaceDependency1.AnyMethodWithMultipleGenericTypes<T1, T2>();
            _anyInterfaceDependency2.AnyMethodWithMultipleGenericTypes<T2, T1>();
            _anyInterfaceDependency3.AnyMethodWithMultipleGenericTypes<T1, T2>();

            var value07 = _anyInterfaceDependency1.AnyMethodWithReturnValue();
            var value08 = _anyInterfaceDependency2.AnyMethodWithReturnValue();
            var value09 = _anyInterfaceDependency3.AnyMethodWithReturnValue();

            var value10 = _anyInterfaceDependency1.AnyMethodWithReturnValueAndParams(value01, value04, value07);
            var value11 = _anyInterfaceDependency2.AnyMethodWithReturnValueAndParams(value02, value05, value08);
            var value12 = _anyInterfaceDependency3.AnyMethodWithReturnValueAndParams(value03, value06, value09);

            _anyInterfaceDependency1.AnyMethodVoid();
            _anyInterfaceDependency2.AnyMethodVoid();
            _anyInterfaceDependency3.AnyMethodVoid();

            _anyInterfaceDependency1.AnyMethodVoidWithParams(value10, value04, value07);
            _anyInterfaceDependency2.AnyMethodVoidWithParams(value02, value11, value08);
            _anyInterfaceDependency3.AnyMethodVoidWithParams(value03, value06, value12);
        }

        public void Any_Unarranged_Null_Invocation_Argument_Was_Provided(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            _anyInterfaceDependency1.AnyMethodVoidWithParams(anyParam1, null, anyParam3);
        }

        public void Any_Wrong_Argument_Was_Provided(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            _anyInterfaceDependency1.AnyMethodVoidWithParams(anyParam1, anyParam2, anyParam3);
        }

        public IList<int> AnyGetOnlyProperty_With_Call()
        {
            var value = _anyInterfaceDependency1.AnyGetOnlyProperty;

            return value;
        }

        public IList<int> AnyGetOnlyProperty_Without_Call()
        {
            return new List<int> { 7 };
        }

        public IList<int> AnyGetSetProperty_With_Call()
        {
            var value = _anyInterfaceDependency1.AnyGetSetProperty;

            return value;
        }

        public IList<int> AnyGetSetProperty_Without_Call()
        {
            return new List<int> { 7 };
        }

        public void AnyMethodVoid_Invoked_Once()
        {
            _anyInterfaceDependency1.AnyMethodVoid();
        }

        public void AnyMethodVoid_Invoked_Three_Times()
        {
            _anyInterfaceDependency1.AnyMethodVoid();
            _anyInterfaceDependency1.AnyMethodVoid();
            _anyInterfaceDependency1.AnyMethodVoid();
        }

        public void AnyMethodVoid_With_Call()
        {
            _anyInterfaceDependency1.AnyMethodVoid();
        }

        public void AnyMethodVoid_Without_Call()
        {
        }

        public void AnyMethodVoidWithParams_With_Call(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            _anyInterfaceDependency1.AnyMethodVoidWithParams(anyParam1, anyParam2, anyParam3);
        }

        public void AnyMethodVoidWithParams_Without_Call(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
        }

        public void AnyMethodWithMultipleGenericTypes_With_Call<T1, T2>()
        {
            _anyInterfaceDependency1.AnyMethodWithMultipleGenericTypes<T1, T2>();
        }

        public IList<int> AnyMethodWithReturnValue_With_Call()
        {
            var value = _anyInterfaceDependency1.AnyMethodWithReturnValue();

            return value;
        }

        public IList<int> AnyMethodWithReturnValue_Without_Call()
        {
            return new List<int> { 7 };
        }

        public IList<int> AnyMethodWithReturnValueAndParam_Without_Call(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            return new List<int> { 7 };
        }

        public IList<int> AnyMethodWithReturnValueAndParams_With_Call(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            var value = _anyInterfaceDependency1.AnyMethodWithReturnValueAndParams(anyParam1, anyParam2, anyParam3);

            return value;
        }

        public void AnyMethodWithSingleGenericType_With_Call<T>()
        {
            _anyInterfaceDependency1.AnyMethodWithSingleGenericType<T>();
        }

        public void AnyMethodWithSingleGenericType_Without_Call<T>()
        {
        }
    }
}