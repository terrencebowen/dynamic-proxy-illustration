using Castle.DynamicProxy;
using Mocksert.Arrangements;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace Mocksert.Extensions
{
    internal static class FriendlyFormatter
    {
        internal static string GetFriendlyTypeName(this Type type)
        {
            var codeDomProvider = CodeDomProvider.CreateProvider(language: "C#");

            var isNullableType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            if (isNullableType)
            {
                type = Nullable.GetUnderlyingType(type);
            }

            var codeTypeReference = new CodeTypeReference(type);

            var codeTypeReferenceExpression = new CodeTypeReferenceExpression(codeTypeReference);

            using (var stringWriter = new StringWriter())
            {
                var codeGeneratorOptions = new CodeGeneratorOptions();

                codeDomProvider.GenerateCodeFromExpression(codeTypeReferenceExpression, stringWriter, codeGeneratorOptions);

                var stringBuilder = stringWriter.GetStringBuilder();
                var typeNamespace = $"{type.Namespace}.";
                var value = stringBuilder.ToString();
                var friendlyTypeName = value.Replace(typeNamespace, null);

                return friendlyTypeName;
            }
        }

        internal static string GetFriendlyMemberName(this IArrangement arrangement)
        {
            string memberName;

            switch (arrangement)
            {
                case IArrangementProperty arrangementProperty:
                    memberName = arrangementProperty.PropertyName;
                    break;

                case IArrangementMethod arrangementMethod:
                    memberName = arrangementMethod.MethodName;
                    break;

                default:
                    return null;
            }

            return memberName;
        }

        internal static string GetFriendlyMemberName(this IInvocation invocation)
        {
            string memberName;

            var methodInfo = invocation.Method;
            var methodName = methodInfo.Name;

            if (methodInfo.IsSpecialName)
            {
                const int getSetUnderscore = 4;

                memberName = methodName.Substring(getSetUnderscore);
            }
            else
            {
                memberName = methodInfo.Name;
            }

            return memberName;
        }
    }
}