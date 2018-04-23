using Humanizer;
using Mocksert.Arrangements;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mocksert.Extensions
{
    internal static class ArrangementFactory
    {
        internal static IArrangementMethod CreateArrangementMethod<TProxy, TReturn>(this Expression<Func<TProxy, TReturn>> expression, TProxy proxy) where TProxy : class
        {
            var expressionBody = expression.Body;
            var arguments = expressionBody.GetArrangementArguments();
            var methodCallExpression = (MethodCallExpression)expressionBody;
            var methodInfo = methodCallExpression.Method;
            var arrangement = new ArrangementMethod<TProxy>(proxy, methodInfo, arguments, isVoidMethod: false);

            return arrangement;
        }

        internal static IArrangementMethod CreateArrangementMethodVoid<TProxy>(this Expression<Action<TProxy>> expression, TProxy proxy) where TProxy : class
        {
            var expressionBody = expression.Body;
            var arguments = GetArrangementArguments(expressionBody);
            var methodCallExpression = (MethodCallExpression)expressionBody;
            var methodInfo = methodCallExpression.Method;
            var arrangement = new ArrangementMethod<TProxy>(proxy, methodInfo, arguments, isVoidMethod: true);

            return arrangement;
        }

        internal static IArrangementProperty CreateArrangementProperty<TProxy, TReturn>(this Expression<Func<TProxy, TReturn>> expression, TProxy proxy) where TProxy : class
        {
            var expressionBody = expression.Body;
            var memberExpression = (MemberExpression)expressionBody;
            var memberInfo = memberExpression.Member;
            var propertyInfo = (PropertyInfo)memberInfo;
            var arrangement = new ArrangementProperty<TProxy>(proxy, propertyInfo);

            return arrangement;
        }

        private static IArrangementArgument[] GetArrangementArguments(this Expression expression)
        {
            var methodCallExpression = (MethodCallExpression)expression;
            var readonlyOnlyExpressions = methodCallExpression.Arguments;
            var readonlyExpressionCount = readonlyOnlyExpressions.Count;
            var arrangementArguments = new IArrangementArgument[readonlyExpressionCount];

            for (var index = 0; index < readonlyExpressionCount; index++)
            {
                var readonlyExpression = readonlyOnlyExpressions[index];
                var member = Expression.Convert(readonlyExpression, typeof(object));
                var lambda = Expression.Lambda<Func<object>>(member);
                var function = lambda.Compile();
                var argumentValue = function();
                var argumentType = argumentValue?.GetType();
                var ordinalPosition = (index + 1).ToOrdinalWords();

                string argumentName;
                string friendlyTypeName;

                if (argumentValue == null)
                {
                    argumentName = $"{ordinalPosition} parameter";
                    friendlyTypeName = "(null)";
                }
                else
                {
                    friendlyTypeName = argumentType.GetFriendlyTypeName();

                    var operand = ((UnaryExpression)lambda.Body).Operand;

                    argumentName = operand.NodeType == ExpressionType.MemberAccess
                        ? (string)((dynamic)operand).Member.Name
                        : $"{ordinalPosition} parameter";
                }

                arrangementArguments[index] = new ArrangementArgument(argumentName, friendlyTypeName, ordinalPosition, argumentValue);
            }

            return arrangementArguments;
        }
    }
}