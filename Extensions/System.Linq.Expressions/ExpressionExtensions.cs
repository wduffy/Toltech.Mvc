using System;

namespace System.Linq.Expressions
{
    public static class ExpressionExtensions
    {

        public static string MemberAsString(this Expression expression)
        {
            if (expression is LambdaExpression)
                expression = (expression as LambdaExpression).Body;
            
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

                if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                    return string.Format("{0}.{1}", memberExpression.Expression.MemberAsString(), memberExpression.Member.Name);

                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format("Cannot interpret member from {0}", expression));

                return unaryExpression.Operand.MemberAsString();
            }

            throw new Exception(string.Format("Could not determine member from {0}", expression));
        }

    }
}
