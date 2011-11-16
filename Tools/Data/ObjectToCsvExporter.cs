using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

// REMOVE F NOT USED
using System.Linq.Expressions;

namespace Toltech.Mvc.Tools
{

    public class ObjectToCsvExporter<T> : CsvBuilder
    {

        private IEnumerable<T> _objects = null;
        private Expression<Func<T, object>>[] _properties;

        public ObjectToCsvExporter(IEnumerable<T> objects, params Expression<Func<T, object>>[] properties)
        {
            _objects = objects;
            _properties = properties;
        }

        protected override void Validate()
        {

            if (_objects == null)
                throw new Exception("The CsvExporter has zero objects available for exporting.");

            if (_properties == null)
                throw new Exception("The CsvExporter has zero properties available for exporting.");
      
        }

        protected override void CreateCsv(System.IO.StreamWriter writer)
        {
            var first = true;
            
            // Create the header row
            foreach (var p in _properties)
            {
                if (first) first = false; else writer.Write(',');
                writer.Write(p.Body.MemberAsString());
            }

            // Create the data rows
            foreach (var o in _objects)
            {

                first = true;
                writer.WriteLine();

                foreach (var p in _properties)
                {
                    var value = p.Compile().Invoke(o).ToString();

                    if (value.Contains(",") || value.Contains(Environment.NewLine))
                    {
                        if (value.Contains("\""))
                            value = value.Replace("\"", "\"\""); // Replace " with ""

                        value = "\"" + value + "\"";
                    }

                    if (first) first = false; else writer.Write(',');
                    writer.Write(value);
                }

            }

        }

    }

}