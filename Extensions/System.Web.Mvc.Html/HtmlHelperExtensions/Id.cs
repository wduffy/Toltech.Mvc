using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {
        //public static string IdFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        //{
        //    //if (expression.Body.NodeType == ExpressionType.Call)
        //    //{
        //    //    var methodCallExpression = (MethodCallExpression)expression.Body;
        //    //    string name = GetHtmlIdNameFor(methodCallExpression);
        //    //    return name.Substring(expression.Parameters[0].Name.Length + 1).Replace('.', '_');
        //    //}
            
        //    return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1).Replace('.', '_');
        //}

        //public static string HtmlIdNameFor<TModel, TValue>(
        //    this HtmlHelper<TModel> htmlHelper,
        //    System.Linq.Expressions.Expression<Func<TModel, TValue>> expression)
        //{
        //    return (GetHtmlIdNameFor(expression));
        //}

        //private static string GetHtmlIdNameFor<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        //{
        //    if (expression.Body.NodeType == ExpressionType.Call)
        //    {
        //        var methodCallExpression = (MethodCallExpression)expression.Body;
        //        string name = GetHtmlIdNameFor(methodCallExpression);
        //        return name.Substring(expression.Parameters[0].Name.Length + 1).Replace('.', '_');

        //    }
        //    return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1).Replace('.', '_');
        //}

        //private static string GetHtmlIdNameFor(MethodCallExpression expression)
        //{
        //    var methodCallExpression = expression.Object as MethodCallExpression;
        //    if (methodCallExpression != null)
        //    {
        //        return GetHtmlIdNameFor(methodCallExpression);
        //    }
        //    return expression.Object.ToString();
        //}
    }
}
