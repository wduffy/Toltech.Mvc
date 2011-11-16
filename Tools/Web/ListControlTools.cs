using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toltech.Tools.Globalization;

namespace Toltech.Tools.Web
{

    public class ListControlTools
    {

        public enum ListControlFilter
        {
            All,
            Selected,
            Unselected
        }

        ///<summary>
        ///Return all "texts" in a list control as a string array
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> that hold the values to be returned</param>
        ///<param name="returnFilter">Filters the values returned to [ All | Selected | Unselected ] as <see cref="Toltech.Tools.ListControlTools.ListControlFilter"/></param>
        ///<returns>A <see cref="System.Array" /> of type <see cref="System.String" /> containing all the texts in <paramref name="listControl" /></returns>
        ///<remarks></remarks>
        public static string[] GetTexts(ListControl listControl, ListControlFilter returnFilter)
        {
            List<string> output = new List<string>();

            foreach (ListItem li in listControl.Items)
            {
                if (returnFilter == ListControlFilter.All)
                    output.Add(li.Text);
                else if (returnFilter == ListControlFilter.Selected && li.Selected)
                    output.Add(li.Text);
                else if (returnFilter == ListControlFilter.Unselected && !li.Selected)
                    output.Add(li.Text);
            }
                
            return output.ToArray();
        }

        ///<summary>
        ///Return all "values" in a list control as a string array
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> that hold the values to be returned</param>
        ///<param name="returnFilter">Filters the values returned to [ All | Selected | Unselected ] as <see cref="Toltech.Tools.ListControlTools.ListControlFilter"/></param>
        ///<returns>A <see cref="System.Array" /> of type <see cref="System.String" /> containing all the values in <paramref name="listControl" /></returns>
        ///<remarks></remarks>
        public static string[] GetValues(ListControl listControl, ListControlFilter returnFilter)
        {
            List<string> output = new List<string>();

            foreach (ListItem li in listControl.Items)
            {
                if (returnFilter == ListControlFilter.All)
                    output.Add(li.Value);
                else if (returnFilter == ListControlFilter.Selected && li.Selected)
                    output.Add(li.Value);
                else if (returnFilter == ListControlFilter.Unselected && !li.Selected)
                    output.Add(li.Value);
            }
                
            return output.ToArray();
        }

        ///<summary>
        ///Sets the selected values of a ListControl based on a bitwise enum's value
        ///</summary>
        ///<param name="listControl">The <see cref="Web.UI.WebControls.ListControl" /> that will be enumerated to return a binary representation of all selected values</param>
        ///<param name="selectedValue">The bitwise enum value that will be selected in the <see cref="Web.UI.WebControls.ListControl" /> as <see cref="Integer" /></param>
        ///<param name="clearListControl">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the binary value(s) in <paramref name="selectedValue" /> are selected.</param>
        ///<remarks>William Duffy: 2008-03-03</remarks>
        public static void SetBinaryValueInList(System.Web.UI.WebControls.ListControl listControl, int selectedValue, bool clearListControl)
        {
            if (clearListControl)
                listControl.ClearSelection();

            foreach (ListItem li in listControl.Items)
                if ((int.Parse(li.Value) & selectedValue) > 0)
                    li.Selected = true;
        }

        ///<summary>
        ///Gets the binary representation of all selected values in a ListControl
        ///</summary>
        ///<param name="listControl">The <see cref="Web.UI.WebControls.ListControl" /> that will be enumerated to return a binary representation of all selected values</param>
        ///<returns>The binary representation of all selected values in the <see cref="Web.UI.WebControls.ListControl" /> as <see cref="Integer" /></returns>
        ///<remarks>William Duffy: 2008-03-03</remarks>
        public static int GetBinaryValueFromList(System.Web.UI.WebControls.ListControl listControl)
        {
            int binaryAdder = 0;

            foreach (ListItem li in listControl.Items)
                if (li.Selected)
                    binaryAdder += int.Parse(li.Value);

            return binaryAdder;
        }

        ///<summary>
        ///Return all selected ListItems in a ListControl as the derived type of the passed ListControl
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> that hold the selected values to be returned</param>
        ///<returns>A <see cref="System.Web.UI.ListControl" /> of <see cref="System.Web.UI.ListItem" />'s which are selected in <paramref name="listControl" /></returns>
        ///<remarks></remarks>
        public static ListControl GetSelected(ListControl listControl)
        {
            ListControl output = null;

            if (listControl is BulletedList)
                output = new BulletedList();
            else if (listControl is CheckBoxList)
                output = new CheckBoxList();
            else if (listControl is DropDownList)
                output = new DropDownList();
            else if (listControl is ListBox)
                output = new ListBox();
            else if (listControl is RadioButtonList)
                output = new RadioButtonList();
            else
                throw new ArgumentException("Type '" + listControl.GetType().FullName + "' is not a supported derived type of System.Web.UI.WebControls.ListControl.", "listControl");
            
            foreach (ListItem li in listControl.Items)
                if (li.Selected)
                {
                    ListItem listItem = new ListItem();
                    listItem.Text = li.Text;
                    listItem.Value = li.Value;
                    listItem.Enabled = li.Enabled;
                    listItem.Selected = li.Selected;
                    
                    foreach(string att in li.Attributes.Keys)
                        listItem.Attributes.Add(att, li.Attributes[att]);

                    foreach(string css in li.Attributes.CssStyle.Keys)
                        listItem.Attributes.CssStyle.Add(css, li.Attributes.CssStyle[css]);

                    output.Items.Add(listItem);
                }

            return output;
        }

        ///<summary>
        ///Select all items in a list control
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="values">The list of values to be selected; wildcard character * selects all, wildcard character ? deselects all</param>
        ///<remarks></remarks>
        public static void Select(ListControl listControl, params string[] values)
        { 
            if (values.Length > 0)
                foreach(ListItem li in listControl.Items)
                {
                    if (values[0] == "*")
                        li.Selected = true;
                    else if (values[0] == "?")
                        li.Selected = false;
                    else
                        if (Array.IndexOf(values, li.Value) > -1)
                            li.Selected = true;
                }
        }

        ///<summary>
        ///Populates a list control from values passed in a paramarray
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<param name="texts">The texts to be applied to the <paramref name="listControl" /></param>
        ///<remarks></remarks>
        public static void Populate(ListControl listControl, bool appendItems, params string[] texts)
        {

            if (!appendItems)
                listControl.Items.Clear();

            foreach(string text in texts)
            {
                ListItem li = new ListItem(text);
                listControl.Items.Add(li);
            }

        }

        ///<summary>
        ///Populates a list control from an enum type
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<param name="enumType">The <see cref="System.Enum" /> type used to populate the <see cref="System.Web.UI.LiteralControl" /></param>
        ///<param name="includeZeroKey">If true includes the zero based value of the enum in the new <see cref="System.Collections.Generic.Dictionary(Of Integer, string)" /></param>
        ///<remarks></remarks>
        public static void Populate(ListControl listControl, bool appendItems, Type enumType, bool includeZeroKey)
        {

            if (!appendItems)
                listControl.Items.Clear();

            Dictionary<int, string> values = EnumTools.GetEnumAsDictionary(enumType, includeZeroKey);
            foreach (KeyValuePair<int, string> item in values)
            {
                ListItem li = new ListItem(item.Value, item.Key.ToString());
                listControl.Items.Add(li);
            }

        }

        /// <summary>
        /// Populates a list control from an IEnumerable collection
        /// </summary>
        /// <param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        /// <param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        /// <param name="list">The <see cref="System.Collections.IEnumerable" /> collection to enumerate into the <paramref name="listControl" /></param>
        /// <param name="textProperty">The property to be used as the text field from each item in the collection</param>
        /// <param name="valueProperty">The property to be used as the value field from each item in the collection</param>
        /// <remarks></remarks>
        public static void Populate(ListControl listControl, bool appendItems, IEnumerable list, string textProperty, string valueProperty)
        {
            Populate(listControl, appendItems, list, textProperty, valueProperty, string.Empty);
        }

        /// <summary>
        /// Populates a list control from an IEnumerable collection
        /// </summary>
        /// <param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        /// <param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        /// <param name="list">The <see cref="System.Collections.IEnumerable" /> collection to enumerate into the <paramref name="listControl" /></param>
        /// <param name="textProperty">The property to be used as the text field from each item in the collection</param>
        /// <param name="valueProperty">The property to be used as the value field from each item in the collection</param>
        /// <param name="selectedValue">The value to be selected by default in the <see cref="System.Web.UI.WebControls.ListControl" /></param>
        /// <remarks></remarks>
        public static void Populate(ListControl listControl, bool appendItems, IEnumerable list, string textProperty, string valueProperty, string selectedValue)
        {
            listControl.DataSource = list;
            listControl.DataTextField = textProperty;
            listControl.DataValueField = valueProperty;
            listControl.AppendDataBoundItems = appendItems;
            listControl.DataBind();

            ListControlTools.SetSelectedValue(listControl, selectedValue);
        }

        /// <summary>
        /// Populates a list control from a Tree structure
        /// </summary>
        /// <param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        /// <param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        /// <param name="list">The <see cref="System.Collections.Generic.Tree" /> collection to populate into the <paramref name="listControl" /></param>
        /// <param name="textProperty">The property to be used as the text field from each item in the collection</param>
        /// <param name="valueProperty">The property to be used as the value field from each item in the collection</param>
        /// <remarks></remarks>
        public static void Populate<T>(ListControl listControl, bool appendItems, TreeNode<T> tree, string textProperty, string valueProperty)
        {
            Populate(listControl, appendItems, tree, textProperty, valueProperty, string.Empty);
        }

        /// <summary>
        /// Populates a list control from a Tree structure
        /// </summary>
        /// <param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        /// <param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        /// <param name="list">The <see cref="System.Collections.Generic.Tree" /> collection to populate into the <paramref name="listControl" /></param>
        /// <param name="textProperty">The property to be used as the text field from each item in the collection</param>
        /// <param name="valueProperty">The property to be used as the value field from each item in the collection</param>
        /// <param name="selectedValue">The value to be selected by default in the <see cref="System.Web.UI.WebControls.ListControl" /></param>
        /// <remarks></remarks>
        public static void Populate<T>(ListControl listControl, bool appendItems, TreeNode<T> tree, string textProperty, string valueProperty, string selectedValue)
        {
            bool isRoot = (tree == tree.Root);
            
            if (isRoot && !appendItems)
                listControl.Items.Clear();

            foreach (TreeNode<T> node in tree.Children)
            {
                ListItem li = new ListItem(new string('-', node.Depth * 2) + " " + DataBinder.GetPropertyValue(node.Item, textProperty).ToString(), DataBinder.GetPropertyValue(node.Item, valueProperty).ToString());
                listControl.Items.Add(li);
                Populate(listControl, appendItems, node, textProperty, valueProperty, selectedValue);
            }
            
            if (isRoot) // Only set selected value when finishing method on root
                ListControlTools.SetSelectedValue(listControl, selectedValue);
        }
        
        ///<summary>
        ///Populates a list control with a list of title abbreviations
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<remarks></remarks>
        public static void PopulateWithTitle(ListControl listControl, bool appendItems)
        {
            ListControlTools.PopulateWithTitle(listControl, appendItems, string.Empty);
        }

        ///<summary>
        ///Populates a list control with a list of title abbreviations
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<param name="selectedTitle">The default title abbreviation to be selected as <see cref="System.String" /></param>
        ///<remarks></remarks>
        public static void PopulateWithTitle(ListControl listControl, bool appendItems, string selectedTitle)
        {

            if (!appendItems)
                listControl.Items.Clear();

            listControl.Items.Add(new ListItem("Mr"));
            listControl.Items.Add(new ListItem("Mrs"));
            listControl.Items.Add(new ListItem("Miss"));
            listControl.Items.Add(new ListItem("Ms"));
            listControl.Items.Add(new ListItem("Dr"));
            listControl.Items.Add(new ListItem("Prof"));
            listControl.Items.Add(new ListItem("Hon"));
            listControl.Items.Add(new ListItem("Rev"));

            ListControlTools.SetSelectedValue(listControl, selectedTitle);

        }

        ///<summary>
        ///Populates a list control with a list of sexes
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<remarks></remarks>
        public static void PopulateWithSex(ListControl listControl, bool appendItems)
        {
            ListControlTools.PopulateWithSex(listControl, appendItems, string.Empty);
        }

        ///<summary>
        ///Populates a list control with a list of sexes
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<param name="selectedSex">The default sex to be selected as <see cref="System.String" /></param>
        ///<remarks></remarks>
        public static void PopulateWithSex(ListControl listControl, bool appendItems, string selectedSex)
        {

            if (!appendItems)
                listControl.Items.Clear();

            listControl.Items.Add(new ListItem("Male"));
            listControl.Items.Add(new ListItem("Female"));

            ListControlTools.SetSelectedValue(listControl, selectedSex);

        }

        ///<summary>
        ///Populates a list control with a list of the alphabet
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<remarks></remarks>
        public static void PopulateWithAlphabet(ListControl listControl, bool appendItems)
        {
            ListControlTools.PopulateWithAlphabet(listControl, appendItems, string.Empty);
        }

        ///<summary>
        ///Populates a list control with a list of the alphabet
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<param name="selectedLetter">The default letter to be selected as <see cref="System.String" /></param>
        ///<remarks></remarks>
        public static void PopulateWithAlphabet(ListControl listControl, bool appendItems, string selectedLetter)
        {

            if (!appendItems)
                listControl.Items.Clear();

            listControl.Items.Add(new ListItem("A"));
            listControl.Items.Add(new ListItem("B"));
            listControl.Items.Add(new ListItem("C"));
            listControl.Items.Add(new ListItem("D"));
            listControl.Items.Add(new ListItem("E"));
            listControl.Items.Add(new ListItem("F"));
            listControl.Items.Add(new ListItem("G"));
            listControl.Items.Add(new ListItem("H"));
            listControl.Items.Add(new ListItem("I"));
            listControl.Items.Add(new ListItem("J"));
            listControl.Items.Add(new ListItem("K"));
            listControl.Items.Add(new ListItem("L"));
            listControl.Items.Add(new ListItem("M"));
            listControl.Items.Add(new ListItem("N"));
            listControl.Items.Add(new ListItem("O"));
            listControl.Items.Add(new ListItem("P"));
            listControl.Items.Add(new ListItem("Q"));
            listControl.Items.Add(new ListItem("R"));
            listControl.Items.Add(new ListItem("S"));
            listControl.Items.Add(new ListItem("T"));
            listControl.Items.Add(new ListItem("U"));
            listControl.Items.Add(new ListItem("V"));
            listControl.Items.Add(new ListItem("W"));
            listControl.Items.Add(new ListItem("X"));
            listControl.Items.Add(new ListItem("Y"));
            listControl.Items.Add(new ListItem("Z"));

            ListControlTools.SetSelectedValue(listControl, selectedLetter);

        }

        ///<summary>
        ///Populates a list control with a list of numbers
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="startNumber">The first number in the list as <see cref="System.Integer" /></param>
        ///<param name="endNumber">The last number in the list as <see cref="System.Integer" /></param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<remarks></remarks>
        public static void PopulateWithNumbers(ListControl listControl, int startNumber, int endNumber, bool appendItems)
        {
            ListControlTools.PopulateWithNumbers(listControl, startNumber, endNumber, appendItems, string.Empty);
        }

        ///<summary>
        ///Populates a list control with a list of the alphabet
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<param name="selectedLetter">The default letter to be selected as <see cref="System.String" /></param>
        ///<remarks></remarks>
        public static void PopulateWithNumbers(ListControl listControl, int startNumber, int endNumber, bool appendItems, string selectedNumber)
        {

            if (!appendItems)
                listControl.Items.Clear();

            for (int i = startNumber; i <= endNumber; i++)
                listControl.Items.Add(new ListItem(i.ToString()));

            ListControlTools.SetSelectedValue(listControl, selectedNumber);

        }

        ///<summary>
        ///Populates a list control with a list of countries
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<remarks></remarks>
        public static void PopulateWithCountries(ListControl listControl, bool appendItems)
        {
            ListControlTools.PopulateWithCountries(listControl, appendItems, string.Empty);
        }
        
        ///<summary>
        ///Populates a list control with a list of countries (value is ISO 3166-1 Alpha-2 country code)
        ///</summary>
        ///<param name="listControl">The <see cref="System.Web.UI.WebControls.ListControl" /> to be populated</param>
        ///<param name="appendItems">A <see cref="System.Boolean" /> that specifies if the listcontrol is to be cleared before the new items are appended.</param>
        ///<param name="selectedCountry">The ISO 3166-1 Alpha-2 country code of the default country to be selected</param>
        ///<remarks></remarks>
        public static void PopulateWithCountries(ListControl listControl, bool appendItems, string selectedCountry)
        {

            if (!appendItems)
                listControl.Items.Clear();

            string[,] countries = Country.GetAllCountries();
            for (int i = 0; i < countries.GetUpperBound(0); i++)
                listControl.Items.Add(new ListItem(countries[i,0], countries[i,1]));

            ListControlTools.SetSelectedValue(listControl, selectedCountry);

        }

        /// <summary>
        /// Selects a value in a listcontrol if found; does not throw an exception if it is not
        /// </summary>
        ///<param name="listControl">The <see cref="Web.UI.WebControls.ListControl" /> that will be enumerated to return a binary representation of all selected values</param>
        ///<param name="selectedValue">The bitwise enum value that will be selected in the <see cref="Web.UI.WebControls.ListControl" /> as <see cref="Integer" /></param>
        /// <returns>A <see cref="System.Boolean" /> indicating if a value in the list was selected</returns>
        ///<remarks>William Duffy: 2009-01-19</remarks>
        public static bool SetSelectedValue(ListControl listControl, string selectedValue)
        {

            try
            {
                listControl.SelectedValue = selectedValue;
                return true;
            }
            catch
            {
                return false;
            }

        }

    }

}