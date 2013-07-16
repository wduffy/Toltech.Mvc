using System.Linq;
using System.Web.Mvc;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {

        /// <summary>
        /// Determines whether a sequence does not contain a specified element by using the default equality comparer.
        /// </summary>
        /// <returns>A boolean value indicating if the source does not contain an element</returns>
        /// <remarks></remarks>
        public static bool DoesNotContain<T>(this IEnumerable<T> source, T value)
        {
            return source == null ? true : !source.Contains(value);
        }

        /// <summary>
        /// Checks if the source contains an element that matches the query
        /// </summary>
        /// <returns>A boolean value indicating if the source contains an element that matched the query</returns>
        /// <remarks></remarks>
        public static bool IsEmpty<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source == null ? true : !source.Any();
        }

        /// <summary>
        /// Checks if the source is empty
        /// </summary>
        /// <returns>A boolean value indicating the if the source is empty</returns>
        /// <remarks></remarks>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null ? true : !source.Any();
        }

        /// <summary>
        /// Checks if the source is not empty
        /// </summary>
        /// <returns>A boolean value indicating if the source is not empty</returns>
        /// <remarks></remarks>
        public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        {
            return source == null ? false : source.Any();
        }

        /// <summary>
        /// Performs the specified action on each element of the source
        /// </summary>
        /// <remarks></remarks>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
                foreach (T element in source)
                    action(element);
        }

        /// <summary>
        /// Performs the specified action on each element of the source
        /// </summary>
        /// <remarks></remarks>
        public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Action<TKey, TValue> action)
        {
            if (source != null)
                foreach (var element in source)
                    action(element.Key, element.Value);
        }


        public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> something)
        {
            var result = new List<TResult>();

            source.ForEach(x => result.Add(something(x)));

            return result;
        }

        public static IEnumerable<SelectListItem> AsSelectList<T>(this IEnumerable<T> source, Func<T, string> text, Func<T, object> value)
        {
            return source.AsEnumerable().Select(x => new SelectListItem
            {
                Text = text.Invoke(x),
                Value = value.Invoke(x).ToString()
            });
        }

    }
}
