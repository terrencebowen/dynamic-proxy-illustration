using AutoFixture;
using Mocksert;
using Mocksert.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace AnyClassLibrary.Tests
{
    public class ClassWithVirtualMembers
    {
        private readonly IList<int> _value01;
        private readonly IList<int> _value02;
        private readonly IList<int> _value03;
        private readonly IList<int> _value04;
        private readonly IList<int> _value05;
        private readonly IList<int> _value06;
        private readonly IList<int> _value07;
        private readonly IList<int> _value08;
        private readonly IList<int> _value09;
        private readonly IList<int> _value10;
        private readonly IList<int> _value11;
        private readonly IList<int> _value12;
        private readonly IList<int> _value13;
        private readonly Fixture _fixture;
        private readonly AnyClassWithVirtualMembersDependency _anyVirtualMemberDependency1;
        private readonly AnyClassWithVirtualMembersDependency _anyVirtualMemberDependency2;
        private readonly AnyClassWithVirtualMembersDependency _anyVirtualMemberDependency3;
        private readonly AnyClassUnderTestWithVirtualMembers _testObject;

        public ClassWithVirtualMembers()
        {
            _value01 = new List<int> { 01, 02, 03 };
            _value02 = new List<int> { 04, 05, 06 };
            _value03 = new List<int> { 07, 08, 09 };
            _value04 = new List<int> { 10, 11, 12 };
            _value05 = new List<int> { 13, 14, 15 };
            _value06 = new List<int> { 16, 17, 18 };
            _value07 = new List<int> { 19, 20, 21 };
            _value08 = new List<int> { 22, 23, 24 };
            _value09 = new List<int> { 25, 26, 27 };
            _value10 = new List<int> { 28, 29, 30 };
            _value11 = new List<int> { 31, 32, 33 };
            _value12 = new List<int> { 34, 35, 36 };
            _value13 = new List<int> { 37, 38, 39 };
            _fixture = new Fixture();

            _anyVirtualMemberDependency1 = Mock.Create<AnyClassWithVirtualMembersDependency>();
            _anyVirtualMemberDependency2 = Mock.Create<AnyClassWithVirtualMembersDependency>();
            _anyVirtualMemberDependency3 = Mock.Create<AnyClassWithVirtualMembersDependency>();

            _testObject = new AnyClassUnderTestWithVirtualMembers(_anyVirtualMemberDependency1,
                                                                  _anyVirtualMemberDependency2,
                                                                  _anyVirtualMemberDependency3);
        }

        [Fact]
        public void Passes_When_AnyGetOnlyProperty_Was_Arranged()
        {
            var expected = _fixture.Create<List<int>>();

            _anyVirtualMemberDependency1.Arrange(x => x.AnyGetOnlyProperty).Return(expected);

            var actual = _testObject.AnyGetOnlyProperty_With_Call();

            _anyVirtualMemberDependency1.Verify();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Fails_When_AnyGetOnlyProperty_Was_UnInvoked()
        {
            Assert.Throws<MockException>(() =>
            {
                var expected = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyGetOnlyProperty).Return(expected);

                var actual = _testObject.AnyGetOnlyProperty_Without_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_AnyGetOnlyProperty_Was_UnArranged()
        {
            Assert.Throws<MockException>(() =>
            {
                var actual = _testObject.AnyGetOnlyProperty_With_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Passes_When_AnyGetSetProperty_Was_Arranged()
        {
            var expected = _fixture.Create<List<int>>();

            _anyVirtualMemberDependency1.Arrange(x => x.AnyGetSetProperty).Return(expected);

            var actual = _testObject.AnyGetSetProperty_With_Call();

            _anyVirtualMemberDependency1.Verify();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Fails_When_AnyGetSetProperty_Was_UnInvoked()
        {
            Assert.Throws<MockException>(() =>
            {
                var expected = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyGetSetProperty).Return(expected);

                var actual = _testObject.AnyGetSetProperty_Without_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_AnyGetSetProperty_Was_UnArranged()
        {
            Assert.Throws<MockException>(() =>
            {
                var actual = _testObject.AnyGetSetProperty_With_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Passes_When_AnyMethodWithSingleGenericType_Was_Arranged()
        {
            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<bool>());

            _testObject.AnyMethodWithSingleGenericType_With_Call<bool>();

            _anyVirtualMemberDependency1.Verify();
        }

        [Fact]
        public void Fails_When_AnyMethodWithSingleGenericType_Was_UnInvoked()
        {
            Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<bool>());

                _testObject.AnyMethodWithSingleGenericType_Without_Call<bool>();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_AnyMethodWithSingleGenericType_Was_UnArranged()
        {
            Assert.Throws<MockException>(() =>
            {
                _testObject.AnyMethodWithSingleGenericType_With_Call<bool>();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Passes_When_AnyMethodWithReturnValue_Was_Arranged()
        {
            var expected = _fixture.Create<List<int>>();

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValue()).Return(expected);

            var actual = _testObject.AnyMethodWithReturnValue_With_Call();

            _anyVirtualMemberDependency1.Verify();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Fails_When_AnyMethodWithReturnValue_Was_UnInvoked()
        {
            Assert.Throws<MockException>(() =>
            {
                var expected = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValue()).Return(expected);

                var actual = _testObject.AnyMethodWithReturnValue_Without_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_AnyMethodWithReturnValue_Was_UnArranged()
        {
            Assert.Throws<MockException>(() =>
            {
                var actual = _testObject.AnyMethodWithReturnValue_With_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Passes_When_AnyMethodWithReturnValueAndParams_Was_Arranged()
        {
            var anyArgument1 = _fixture.Create<List<int>>();
            var anyArgument2 = _fixture.Create<List<int>>();
            var anyArgument3 = _fixture.Create<List<int>>();
            var expected = _fixture.Create<List<int>>();

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValueAndParams(anyArgument1, anyArgument2, anyArgument3)).Return(expected);

            var actual = _testObject.AnyMethodWithReturnValueAndParams_With_Call(anyArgument1, anyArgument2, anyArgument3);

            _anyVirtualMemberDependency1.Verify();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Fails_When_AnyMethodWithReturnValueAndParams_Was_UnInvoked()
        {
            Assert.Throws<MockException>(() =>
            {
                var anyArgument1 = _fixture.Create<List<int>>();
                var anyArgument2 = _fixture.Create<List<int>>();
                var anyArgument3 = _fixture.Create<List<int>>();
                var expected = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValueAndParams(anyArgument1, anyArgument2, anyArgument3)).Return(expected);

                var actual = _testObject.AnyMethodWithReturnValueAndParam_Without_Call(anyArgument1, anyArgument2, anyArgument3);

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_AnyMethodWithReturnValueAndParams_Was_UnArranged()
        {
            Assert.Throws<MockException>(() =>
            {
                var anyArgument1 = _fixture.Create<List<int>>();
                var anyArgument2 = _fixture.Create<List<int>>();
                var anyArgument3 = _fixture.Create<List<int>>();

                var actual = _testObject.AnyMethodWithReturnValueAndParams_With_Call(anyArgument1, anyArgument2, anyArgument3);

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Passes_When_AnyMethodVoid_Was_Arranged()
        {
            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());

            _testObject.AnyMethodVoid_With_Call();

            _anyVirtualMemberDependency1.Verify();
        }

        [Fact]
        public void Fails_When_AnyMethodVoid_Was_UnInvoked()
        {
            Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());

                _testObject.AnyMethodVoid_Without_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_AnyMethodVoid_Was_UnArranged()
        {
            Assert.Throws<MockException>(() =>
            {
                _testObject.AnyMethodVoid_With_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Passes_When_AnyMethodVoidWithParams_Was_Arranged()
        {
            var anyArgument1 = _fixture.Create<List<int>>();
            var anyArgument2 = _fixture.Create<List<int>>();
            var anyArgument3 = _fixture.Create<List<int>>();

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(anyArgument1, anyArgument2, anyArgument3));

            _testObject.AnyMethodVoidWithParams_With_Call(anyArgument1, anyArgument2, anyArgument3);

            _anyVirtualMemberDependency1.Verify();
        }

        [Fact]
        public void Fails_When_AnyMethodVoidWithParams_Was_UnInvoked()
        {
            Assert.Throws<MockException>(() =>
            {
                var anyArgument1 = _fixture.Create<List<int>>();
                var anyArgument2 = _fixture.Create<List<int>>();
                var anyArgument3 = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(anyArgument1, anyArgument2, anyArgument3));

                _testObject.AnyMethodVoidWithParams_Without_Call(anyArgument1, anyArgument2, anyArgument3);

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_AnyMethodVoidWithParams_Was_UnArranged()
        {
            Assert.Throws<MockException>(() =>
            {
                var anyArgument1 = _fixture.Create<List<int>>();
                var anyArgument2 = _fixture.Create<List<int>>();
                var anyArgument3 = _fixture.Create<List<int>>();

                _testObject.AnyMethodVoidWithParams_With_Call(anyArgument1, anyArgument2, anyArgument3);

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_Any_Unarranged_Null_Invocation_Argument_Was_Provided()
        {
            Assert.Throws<MockException>(() =>
            {
                var anyArgument1 = _fixture.Create<List<int>>();
                var anyArgument2 = _fixture.Create<List<int>>();
                var anyArgument3 = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(anyArgument1, anyArgument2, anyArgument3));

                _testObject.Any_Unarranged_Null_Invocation_Argument_Was_Provided(anyArgument1, anyArgument2, anyArgument3);

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Does_Not_Throw_Exception_When_Any_Arranged_Null_Argument_Was_Provided()
        {
            var anyArgument1 = _fixture.Create<List<int>>();
            List<int> anyArgument2 = null;
            var anyArgument3 = _fixture.Create<List<int>>();

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(anyArgument1, null, anyArgument3));

            _testObject.Any_Unarranged_Null_Invocation_Argument_Was_Provided(anyArgument1, anyArgument2, anyArgument3);

            _anyVirtualMemberDependency1.Verify();
        }

        [Fact]
        public void Fails_When_Arrange_Extension_Method_Was_Not_Used()
        {
            Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.AnyMethodVoid();

                _testObject.AnyMethodVoid_With_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_Return_Extension_Method_Was_Not_Used()
        {
            Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyGetOnlyProperty);

                var actual = _testObject.AnyGetOnlyProperty_With_Call();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_The_Wrong_Argument_Was_Provided()
        {
            Assert.Throws<MockException>(() =>
            {
                var anyArgument1 = _fixture.Create<List<int>>();
                var anyArgument2 = _fixture.Create<List<int>>();
                var anyArgument3 = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(anyArgument3, anyArgument2, anyArgument1));

                _testObject.Any_Wrong_Argument_Was_Provided(anyArgument1, anyArgument2, anyArgument3);

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_The_Wrong_Generic_Type_Argument_Was_Provided()
        {
            Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<char>());

                _testObject.AnyMethodWithSingleGenericType_With_Call<bool>();

                _anyVirtualMemberDependency1.Verify();
            });
        }

        [Fact]
        public void Fails_When_Verify_Is_Called_Before_Subject_Under_Test()
        {
            Assert.Throws<MockException>(() =>
            {
                var expected = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValue()).Return(expected);

                _anyVirtualMemberDependency1.Verify();

                var actual = _testObject.AnyMethodWithReturnValue_With_Call();

                Assert.Equal(expected, actual);
            });
        }

        [Fact]
        public void Returns_Correct_Exception_Counts_Upon_Failure_When_Arrangement_Count_Was_More_Than_Invocation_Count()
        {
            const string mockFailureMessage = "AnyMethodVoid was arranged 3 times, but was called 1 time.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());

                _testObject.AnyMethodVoid_Invoked_Once();

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void Returns_Correct_Exception_Counts_Upon_Failure_When_Invocation_Count_Was_More_Than_Arrangement_Count()
        {
            const string mockFailureMessage = "AnyMethodVoid was arranged 1 time, but was called 2 times.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());

                _testObject.AnyMethodVoid_Invoked_Three_Times();

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void Does_Not_Throw_Exception_When_Verify_Was_Not_Called()
        {
            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());

            _testObject.AnyMethodVoid_With_Call();
        }

        [Fact]
        public void GetUnarrangedInvocationFailureMessage_Returns_Correct_Mock_Failure_Message()
        {
            const string mockFailureMessage = "AnyMethodVoid was arranged 0 times, but was called 1 time.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _testObject.AnyMethodVoid_With_Call();

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void GetGenericTypeArgumentFailureMessage_Returns_Correct_Mock_Failure_Message_With_Single_Generic_Type_Argument()
        {
            const string mockFailureMessage = "AnyMethodWithSingleGenericType was arranged with generic type(s) <char>, but was called with the generic type(s) <bool>.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<char>());

                _testObject.AnyMethodWithSingleGenericType_With_Call<bool>();

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void GetGenericTypeArgumentFailureMessage_Returns_Correct_Mock_Failure_Message_With_Single_Namespace_Reduced_Generic_Type_Argument()
        {
            const string mockFailureMessage = "AnyMethodWithSingleGenericType was arranged with generic type(s) <IDictionary<int, IDictionary<IDictionary<IDictionary<IDictionary<bool, char>, string>, char>, int>>>, but was called with the generic type(s) <int>.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<IDictionary<int, IDictionary<IDictionary<IDictionary<IDictionary<bool, char>, string>, char>, int>>>());

                _testObject.AnyMethodWithSingleGenericType_With_Call<int>();

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void GetGenericTypeArgumentFailureMessage_Returns_Correct_Mock_Failure_Message_With_Multiple_Generic_Type_Arguments()
        {
            const string mockFailureMessage = "AnyMethodWithMultipleGenericTypes was arranged with generic type(s) <int, bool>, but was called with the generic type(s) <string, char>.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithMultipleGenericTypes<int, bool>());

                _testObject.AnyMethodWithMultipleGenericTypes_With_Call<string, char>();

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void GetInvalidArgumentValueFailureMessage_Returns_Correct_Mock_Failure_Message()
        {
            const string mockFailureMessage = "AnyMethodVoidWithParams was not called with arranged argument for the second argument {anyArgument2}.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                var anyArgument1 = _fixture.Create<List<int>>();
                var anyArgument2 = _fixture.Create<List<int>>();
                var anyArgument3 = _fixture.Create<List<int>>();

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(anyArgument1, anyArgument2, anyArgument3));

                _testObject.Any_Unarranged_Null_Invocation_Argument_Was_Provided(anyArgument1, anyArgument2, anyArgument3);

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void GetArrangementUninvokedFailureMessage_Returns_Correct_Mock_Failure_Message()
        {
            const string mockFailureMessage = "AnyMethodVoid was arranged 1 time, but was called 0 times.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());

                _testObject.AnyMethodVoid_Without_Call();

                _anyVirtualMemberDependency1.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void Passes_With_Multiple_Dependency_Arrangements()
        {
            _anyVirtualMemberDependency1.Arrange(x => x.AnyGetOnlyProperty).Return(_value01);
            _anyVirtualMemberDependency2.Arrange(x => x.AnyGetOnlyProperty).Return(_value02);
            _anyVirtualMemberDependency3.Arrange(x => x.AnyGetOnlyProperty).Return(_value03);

            _anyVirtualMemberDependency1.Arrange(x => x.AnyGetSetProperty).Return(_value04);
            _anyVirtualMemberDependency2.Arrange(x => x.AnyGetSetProperty).Return(_value05);
            _anyVirtualMemberDependency3.Arrange(x => x.AnyGetSetProperty).Return(_value06);

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<int>());
            _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithSingleGenericType<char>());
            _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithSingleGenericType<int>());

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithMultipleGenericTypes<int, char>());
            _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithMultipleGenericTypes<char, int>());
            _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithMultipleGenericTypes<int, char>());

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value07);
            _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
            _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value09);

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value01, _value04, _value07)).Return(_value10);
            _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value02, _value05, _value08)).Return(_value11);
            _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value03, _value06, _value09)).Return(_value12);

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());
            _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodVoid());
            _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodVoid());

            _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(_value10, _value04, _value07));
            _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodVoidWithParams(_value02, _value11, _value08));
            _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodVoidWithParams(_value03, _value06, _value12));

            _testObject.AnyMethodUnderTest_With_Multiple_Dependency_Calls<int, char>();

            _anyVirtualMemberDependency1.Verify();
            _anyVirtualMemberDependency2.Verify();
            _anyVirtualMemberDependency3.Verify();
        }

        [Fact]
        public void Fails_With_Multiple_Dependency_Arrangements_When_One_Or_Arrangements_Were_Not_Invoked()
        {
            const string mockFailureMessage = "AnyMethodWithReturnValue was arranged 7 times, but was called 1 time.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyGetOnlyProperty).Return(_value01);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyGetOnlyProperty).Return(_value02);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyGetOnlyProperty).Return(_value03);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyGetSetProperty).Return(_value04);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyGetSetProperty).Return(_value05);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyGetSetProperty).Return(_value06);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<int>());
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithSingleGenericType<char>());
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithSingleGenericType<int>());

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithMultipleGenericTypes<int, char>());
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithMultipleGenericTypes<char, int>());
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithMultipleGenericTypes<int, char>());

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value07);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value09);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value01, _value04, _value07)).Return(_value10);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value02, _value05, _value08)).Return(_value11);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value03, _value06, _value09)).Return(_value12);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodVoid());
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodVoid());

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(_value10, _value04, _value07));
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodVoidWithParams(_value02, _value11, _value08));
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodVoidWithParams(_value03, _value06, _value12));

                _testObject.AnyMethodUnderTest_With_Multiple_Dependency_Calls<int, char>();

                _anyVirtualMemberDependency1.Verify();
                _anyVirtualMemberDependency2.Verify();
                _anyVirtualMemberDependency3.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }

        [Fact]
        public void Fails_With_Multiple_Dependency_Arrangements_When_One_Or_Arrangements_Are_Provided_Invalid_Arguments()
        {
            const string mockFailureMessage = "AnyMethodWithReturnValueAndParams was not called with arranged argument for the second argument {_value13}.";

            var mockException = Assert.Throws<MockException>(() =>
            {
                _anyVirtualMemberDependency1.Arrange(x => x.AnyGetOnlyProperty).Return(_value01);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyGetOnlyProperty).Return(_value02);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyGetOnlyProperty).Return(_value03);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyGetSetProperty).Return(_value04);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyGetSetProperty).Return(_value05);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyGetSetProperty).Return(_value06);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithSingleGenericType<int>());
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithSingleGenericType<char>());
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithSingleGenericType<int>());

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithMultipleGenericTypes<int, char>());
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithMultipleGenericTypes<char, int>());
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithMultipleGenericTypes<int, char>());

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value07);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value08);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithReturnValue()).Return(_value09);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value01, _value04, _value07)).Return(_value10);
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value02, _value13, _value08)).Return(_value11);
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodWithReturnValueAndParams(_value03, _value06, _value09)).Return(_value12);

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoid());
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodVoid());
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodVoid());

                _anyVirtualMemberDependency1.Arrange(x => x.AnyMethodVoidWithParams(_value10, _value04, _value07));
                _anyVirtualMemberDependency2.Arrange(x => x.AnyMethodVoidWithParams(_value02, _value11, _value08));
                _anyVirtualMemberDependency3.Arrange(x => x.AnyMethodVoidWithParams(_value03, _value06, _value12));

                _testObject.AnyMethodUnderTest_With_Multiple_Dependency_Calls<int, char>();

                _anyVirtualMemberDependency1.Verify();
                _anyVirtualMemberDependency2.Verify();
                _anyVirtualMemberDependency3.Verify();
            });

            Assert.Equal(expected: mockFailureMessage, actual: mockException.Message);
        }
    }
}