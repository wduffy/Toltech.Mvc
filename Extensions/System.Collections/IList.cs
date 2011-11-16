using System.Linq;
using System.Collections.Generic;

namespace System.Collections
{

    public static class IListExtensions
    {

        public static string GetRowClass(this IList list, object item, string evenClass, string oddClass = "")
        {
            int index = list.IndexOf(item);
            return index % 2 == 0 ? evenClass : oddClass;
        }

    }
}
