using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Toltech.Data
{

    public class EntityToCsvExporter : CsvExporter
    {

#region "Configuration Methods"

        ///<summary>
        ///Sets the object collection to be enumerated for construction of the .csv file
        ///</summary>
        ///<param name="objectsToExport">A <see cref="System.Collections.IEnumerable" /> collection</param>
        ///<remarks></remarks>
        public void SetObjectsToExport(IEnumerable objectsToExport)
        {
            _objectsToExport = objectsToExport;
        }
        private IEnumerable _objectsToExport = null;

        ///<summary>
        ///Sets the properties collection to be enumerated for construction of the .csv file
        ///</summary>
        ///<param name="propertiesToExport">A <see cref="System.Array" /> of type <see cref="System.String" /></param>
        ///<remarks></remarks>
        public void SetPropertiesToExport(params string[] propertiesToExport)
        {
            _propertiesToExport = propertiesToExport;
        }
        private string[] _propertiesToExport = null;

#endregion

        protected override void PerformErrorChecking()
        {

            if (_objectsToExport == null)
                throw new Exception("The CsvExporter has zero objects available for exporting.");

            if (_propertiesToExport == null)  // If there are no properties to export automatically get all properties using reflection
            {
                List<string> properties = new List<string>();

                IEnumerator ie = _objectsToExport.GetEnumerator();
                if (ie.MoveNext()) // We only need the first object to get the properties
                {
                    foreach (PropertyInfo pi in ie.Current.GetType().GetProperties())
                        if (pi.PropertyType == typeof(string) || pi.PropertyType.IsValueType)
                            properties.Add(pi.Name);

                    _propertiesToExport = properties.ToArray();
                }

                if (_propertiesToExport == null) // If there are still no properties to export throw an exception
                    throw new Exception("The CsvExporter has zero properties available for exporting.");
            } 
      
        }

        protected override void CreateCsv(StringBuilder csv)
        {
        
            // Create the header row
            foreach (string propertyName in _propertiesToExport)
            {
                csv.Append(propertyName);
                csv.Append(',');
            }
                
            csv.Remove(csv.Length - 1, 1);
            csv.AppendLine();

            // Create the data rows
            foreach (object itemToExport in _objectsToExport)
            {

                foreach (string propertyName in _propertiesToExport)
                {

                    PropertyInfo p = itemToExport.GetType().GetProperty(propertyName);
                    string propertyValue = string.Empty; // All values are converted to string for use in a .csv file

                    if (p == null)
                        throw new Exception("During export, a property '" + propertyName + "' could not be found in the '" + itemToExport.ToString() + "' object.");

                    // This is a hangover from the old webform data layers (WD - 18 Sept 11)
                    //if (!DataAdapter.IsDefaultValue(p.GetValue(itemToExport, null)))
                    propertyValue = p.GetValue(itemToExport, null).ToString();
                    
                    if (propertyValue.Contains(",") || propertyValue.Contains(Environment.NewLine))
                    {
                        if (propertyValue.ToString().Contains("\""))
                            propertyValue = propertyValue.ToString().Replace("\"", "\"\""); // Replace " with ""

                        propertyValue = "\"" + propertyValue.ToString() + "\"";
                    }

                    csv.Append(propertyValue);
                    csv.Append(',');

                }

                csv.Remove(csv.Length - 1, 1);
                csv.AppendLine();

            }

        }

    }

}