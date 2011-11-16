using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Toltech.Mvc
{

    public class Sorter<E> : IComparer<E>
    {        

#region " Private Members "

        public enum SortDirection
        {
            Ascending,
            Descending
        }

        private string _entitySortProperty = string.Empty;
        private SortDirection _entitySortDirection = SortDirection.Ascending;
        private IDictionary _viewState = null;

        private string EntitySortProperty
        {
            get
            {
                if (_viewState == null)
                    return _entitySortProperty;
                else
                {
                    object state = _viewState["EntitySortProperty"];

                    if (state == null)
                        return string.Empty;
                    else
                        return (string)state;
                }
            }
            set
            {
                if (_viewState == null)
                    _entitySortProperty = value;
                else
                    _viewState.Add("EntitySortProperty", value);
            }
        }

        private SortDirection EntitySortDirection
        {
            get
            {
                if (_viewState == null)
                    return _entitySortDirection;
                else
                {
                    object state = _viewState["EntitySortDirection"];

                    if (state == null)
                        return SortDirection.Ascending;
                    else
                        return (SortDirection)_viewState["EntitySortDirection"];
                }
            }
            set
            {
                if (_viewState == null)
                    _entitySortDirection = value;
                else
                    _viewState.Add("EntitySortDirection", value);
            }
        }

#endregion

#region " Constructors "

        /// <summary>
        /// Constructs a new EntityComparer object which tracks sorting in viewstate
        /// </summary>
        /// <param name="sortProperty">The id property that will be used in comparison operations</param>
        /// <param name="viewState">The page ViewState object that will allow tracking of the current sortorder</param>
        /// <remarks></remarks>
        public Sorter(string sortProperty, IDictionary viewState)
        {
            _viewState = viewState;

            if ((sortProperty != this.EntitySortProperty) || (_viewState["EntitySortDirection"] == null))
                this.EntitySortDirection = SortDirection.Ascending;
            else
                if (this.EntitySortDirection == SortDirection.Ascending)
                    this.EntitySortDirection = SortDirection.Descending;
                else
                    this.EntitySortDirection = SortDirection.Ascending;

            this.EntitySortProperty = sortProperty;
        }

        /// <summary>
        /// Constructs a new EntityCompaper object and specifies the sort direction without using viewstate as a tracking mechanism
        /// </summary>
        /// <param name="sortProperty">The id property that will be used in comparison operations</param>
        /// <param name="sortDirection">The direction in which this comparison will sort id properties</param>
        /// <remarks></remarks>
        public Sorter(string sortProperty, SortDirection sortDirection)
        {
            this.EntitySortDirection = sortDirection;
            this.EntitySortProperty = sortProperty;
        }

#endregion

#region " ICompaper Support "
        int IComparer<E>.Compare(E x, E y)
        {

            PropertyInfo propertyInfo = null;
            object property1 = null;
            object property2 = null;

            // IF: Subproperty has been requested drill into the object to retrieve its value
            // ELSE: retrieve the requested property from the object
            if (this.EntitySortProperty.Contains("."))
            {
                // At this point the benefits of generic t T are lost
                // because we do not know the t of subproperty objects
                string[] separatedProperties = this.EntitySortProperty.Split('.');
                object objX = null;
                object objY = null;
                
                propertyInfo = typeof(E).GetProperty(separatedProperties[0]);
                objX = propertyInfo.GetValue(x, null);
                objY = propertyInfo.GetValue(y, null);

                for (int i = 1; i <= (separatedProperties.Length - 1); i++)
                    propertyInfo = objX.GetType().GetProperty(separatedProperties[i]);

                property1 = propertyInfo.GetValue(objX, null);
                property2 = propertyInfo.GetValue(objY, null);
            }
            else
            {
                propertyInfo = typeof(E).GetProperty(this.EntitySortProperty);
                property1 = propertyInfo.GetValue(x, null);
                property2 = propertyInfo.GetValue(y, null);
            }


            // IF: Both properties contain an empty value then both are equal
            // ELSEIF: property 1 contains a value but property 2 does not - propery 1 is less
            // ELSEIF: property 2 contains a value but propety 1 does not - property 2 is less
            if (string.IsNullOrEmpty(property1.ToString()) && string.IsNullOrEmpty(property2.ToString()))
                return 0;
            else if (!string.IsNullOrEmpty(property1.ToString()) && string.IsNullOrEmpty(property2.ToString()))
                return -1;
            else if (string.IsNullOrEmpty(property1.ToString()) && !string.IsNullOrEmpty(property2.ToString()))
                return 1;

            // Return the comparison based on the current SortDirection
            if (this.EntitySortDirection == SortDirection.Ascending)
                return ((IComparable)property1).CompareTo(property2);
            else
                return ((IComparable)property2).CompareTo(property1);

        }

#endregion

    }

}