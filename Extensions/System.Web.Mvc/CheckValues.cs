using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{

    public enum CheckValuesStatus
    {
        Checked,
        UnChecked,
        Both
    }

    public class CheckValues : SortedList<string, bool>
    {
        public CheckValues() { }
        public CheckValues(string[] values) : this(values, false) { }
        public CheckValues(string[] values, bool check) : this(values, x => check) { }
        public CheckValues(string[] values, Func<string, bool> func)
        {
            foreach (var value in values)
                this.Add(value, func(value));
        }

        public string[] ToArray(CheckValuesStatus status)
        {
            var output = new List<string>();

            foreach (var kvp in this)
                switch (status)
                {
                    case CheckValuesStatus.Checked:
                        if (kvp.Value) output.Add(kvp.Key);
                        break;
                    case CheckValuesStatus.UnChecked:
                        if (!kvp.Value) output.Add(kvp.Key);
                        break;
                    default:
                        output.Add(kvp.Key);
                        break;
                }

            return output.ToArray();
        }

    }

}
