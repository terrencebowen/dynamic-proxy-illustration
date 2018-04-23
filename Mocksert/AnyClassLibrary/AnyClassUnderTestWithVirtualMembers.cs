using System;
using System.Collections.Generic;

namespace AnyClassLibrary
{
    public class AnyClassWithVirtualMembersDependency
    {
        public virtual IList<int> AnyGetOnlyProperty { get; }
        public virtual IList<int> AnyGetSetProperty { get; set; }

        public virtual void AnyMethodVoid()
        {
            throw new NotImplementedException();
        }

        public virtual void AnyMethodVoidWithParams(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            throw new NotImplementedException();
        }

        public virtual void AnyMethodWithSingleGenericType<T>()
        {
            throw new NotImplementedException();
        }

        public virtual void AnyMethodWithMultipleGenericTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public virtual IList<int> AnyMethodWithReturnValue()
        {
            throw new NotImplementedException();
        }

        public virtual IList<int> AnyMethodWithReturnValueAndParams(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            throw new NotImplementedException();
        }
    }

    public class AnyClassUnderTestWithVirtualMembers
    {
        private readonly AnyClassWithVirtualMembersDependency _anyVirtualMemberDependency1;
        private readonly AnyClassWithVirtualMembersDependency _anyVirtualMemberDependency2;
        private readonly AnyClassWithVirtualMembersDependency _anyVirtualMemberDependency3;

        public AnyClassUnderTestWithVirtualMembers(AnyClassWithVirtualMembersDependency anyVirtualMemberDependency1,
                                                   AnyClassWithVirtualMembersDependency anyVirtualMemberDependency2,
                                                   AnyClassWithVirtualMembersDependency anyVirtualMemberDependency3)
        {
            _anyVirtualMemberDependency1 = anyVirtualMemberDependency1;
            _anyVirtualMemberDependency2 = anyVirtualMemberDependency2;
            _anyVirtualMemberDependency3 = anyVirtualMemberDependency3;
        }

        public void AnyMethodUnderTest_With_Multiple_Dependency_Calls<T1, T2>()
        {
            var value01 = _anyVirtualMemberDependency1.AnyGetOnlyProperty;
            var value02 = _anyVirtualMemberDependency2.AnyGetOnlyProperty;
            var value03 = _anyVirtualMemberDependency3.AnyGetOnlyProperty;

            var value04 = _anyVirtualMemberDependency1.AnyGetSetProperty;
            var value05 = _anyVirtualMemberDependency2.AnyGetSetProperty;
            var value06 = _anyVirtualMemberDependency3.AnyGetSetProperty;

            _anyVirtualMemberDependency1.AnyMethodWithSingleGenericType<T1>();
            _anyVirtualMemberDependency2.AnyMethodWithSingleGenericType<T2>();
            _anyVirtualMemberDependency3.AnyMethodWithSingleGenericType<T1>();

            _anyVirtualMemberDependency1.AnyMethodWithMultipleGenericTypes<T1, T2>();
            _anyVirtualMemberDependency2.AnyMethodWithMultipleGenericTypes<T2, T1>();
            _anyVirtualMemberDependency3.AnyMethodWithMultipleGenericTypes<T1, T2>();

            var value07 = _anyVirtualMemberDependency1.AnyMethodWithReturnValue();
            var value08 = _anyVirtualMemberDependency2.AnyMethodWithReturnValue();
            var value09 = _anyVirtualMemberDependency3.AnyMethodWithReturnValue();

            var value10 = _anyVirtualMemberDependency1.AnyMethodWithReturnValueAndParams(value01, value04, value07);
            var value11 = _anyVirtualMemberDependency2.AnyMethodWithReturnValueAndParams(value02, value05, value08);
            var value12 = _anyVirtualMemberDependency3.AnyMethodWithReturnValueAndParams(value03, value06, value09);

            _anyVirtualMemberDependency1.AnyMethodVoid();
            _anyVirtualMemberDependency2.AnyMethodVoid();
            _anyVirtualMemberDependency3.AnyMethodVoid();

            _anyVirtualMemberDependency1.AnyMethodVoidWithParams(value10, value04, value07);
            _anyVirtualMemberDependency2.AnyMethodVoidWithParams(value02, value11, value08);
            _anyVirtualMemberDependency3.AnyMethodVoidWithParams(value03, value06, value12);
        }

        public void Any_Unarranged_Null_Invocation_Argument_Was_Provided(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            _anyVirtualMemberDependency1.AnyMethodVoidWithParams(anyParam1, null, anyParam3);
        }

        public void Any_Wrong_Argument_Was_Provided(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            _anyVirtualMemberDependency1.AnyMethodVoidWithParams(anyParam1, anyParam2, anyParam3);
        }

        public IList<int> AnyGetOnlyProperty_With_Call()
        {
            var value = _anyVirtualMemberDependency1.AnyGetOnlyProperty;

            return value;
        }

        public IList<int> AnyGetOnlyProperty_Without_Call()
        {
            return new List<int> { 7 };
        }

        public IList<int> AnyGetSetProperty_With_Call()
        {
            var value = _anyVirtualMemberDependency1.AnyGetSetProperty;

            return value;
        }

        public IList<int> AnyGetSetProperty_Without_Call()
        {
            return new List<int> { 7 };
        }

        public void AnyMethodVoid_Invoked_Once()
        {
            _anyVirtualMemberDependency1.AnyMethodVoid();
        }

        public void AnyMethodVoid_Invoked_Three_Times()
        {
            _anyVirtualMemberDependency1.AnyMethodVoid();
            _anyVirtualMemberDependency1.AnyMethodVoid();
            _anyVirtualMemberDependency1.AnyMethodVoid();
        }

        public void AnyMethodVoid_With_Call()
        {
            _anyVirtualMemberDependency1.AnyMethodVoid();
        }

        public void AnyMethodVoid_Without_Call()
        {
        }

        public void AnyMethodVoidWithParams_With_Call(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
            _anyVirtualMemberDependency1.AnyMethodVoidWithParams(anyParam1, anyParam2, anyParam3);
        }

        public void AnyMethodVoidWithParams_Without_Call(IList<int> anyParam1, IList<int> anyParam2, IList<int> anyParam3)
        {
        }

        public void AnyMethodWithMultipleGenericTypes_With_Call<T1, T2>()
        {
            _anyVirtualMemberDependency1.AnyMethodWithMultipleGenericTypes<T1, T2>();
        }

        public IList<int> AnyMethodWithReturnValue_With_Call()
        {
            var value = _anyVirtualMemberDependency1.AnyMethodWithReturnValue();

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
            var value = _anyVirtualMemberDependency1.AnyMethodWithReturnValueAndParams(anyParam1, anyParam2, anyParam3);

            return value;
        }

        public void AnyMethodWithSingleGenericType_With_Call<T>()
        {
            _anyVirtualMemberDependency1.AnyMethodWithSingleGenericType<T>();
        }

        public void AnyMethodWithSingleGenericType_Without_Call<T>()
        {
        }
    }
}