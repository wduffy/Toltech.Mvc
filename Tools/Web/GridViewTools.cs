using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toltech.Tools
{

    public class GridViewTools
    {

        ///<summary>
        ///Removes any state maintaining objects that are used to track sort orders in the gridview CreateSortArrows method
        ///</summary>
        ///<param name="viewState"></param>
        ///<remarks></remarks>
        public static void ClearSortArrowState(StateBag viewState)
        {
            viewState.Remove("EntitySortProperty");
            viewState.Remove("EntitySortDirection");
        }

        ///<summary>
        ///Takes a GridView object and attaches a suitable sort artist to the headerrow cells of any sortable columns
        ///</summary>
        ///<param name="gridView">The <see cref="System.Web.UI.WebControls.GridView" /> object whose headerrow cells are to be managed</param>
        ///<param name="defaultSortExpression">The default sort expression of the GridView data as <see cref="System.String" /></param>
        ///<param name="defaultSortDirection">The default sort direction of the GridView data as <see cref="System.Web.UI.WebControls.SortDirection" /></param>
        ///<param name="sortImagesFolder">The folder where the sort images are stored as <see cref="System.String" />. The sort images must be named sort_ascending.gif, sort_descending.gif and sort_unsorted.gif.</param>
        ///<param name="viewState">The page ViewState object that will allow tracking of the current sortorder. Only used when the sort has been performed using <see cref="William.Model.Entity.EntitySorter(Of T)" /></param>
        ///<param name="throwEmtpyDataException">If true an error is thrown if the gridview contains no data</param>
        ///<remarks>Must be run from within the PreRender event of the GridView so that the HeaderRow cells are created</remarks>
        public static void CreateSortArrows(System.Web.UI.WebControls.GridView gridView, string defaultSortExpression, SortDirection defaultSortDirection, string sortImagesFolder, StateBag viewState, bool throwEmtpyDataException)
        {

            // Check to see if the GridView contains rows of data. If there is no data throw an exception or exit the subroutine
            if (gridView.Rows.Count == 0)
                if (throwEmtpyDataException)
                    throw new Exception(string.Format("The GridView \"{0}\" has no data.{1}Please ensure that you check the GridView has rows of data before running the CreateSortArrows shared method.", gridView.ID, Environment.NewLine));
                else
                    return;
            
            // Check to see that the GridView has been created before working with the HeaderRow cells
            // Although the RowCreated event fires after every row is created the actual GridView is
            // not created until after all rows have been created
            if (gridView.HeaderRow == null)
                throw new Exception(string.Format("The GridView \"{0}\" has not yet been created.{1}Please check that you are running the CreateSortArrows shared method from within the GridView's PreRender event handler.", gridView.ID, Environment.NewLine));
            
            // Enumerate the columns and create the arrows for each sortable column
            foreach(DataControlField dcf in gridView.Columns)
            {
                if (!string.IsNullOrEmpty(dcf.SortExpression))
                {

                    string sortDirection = string.Empty;

                    // Work out which arrow is to be used for this column. If viewstate is passed in
                    // use William.Model.Data.EntitySorter's custom tracking, otherwise use the properties of the gridview
                    if (viewState != null)
                    {

                        if (dcf.SortExpression == (string)viewState["EntitySortProperty"])
                            sortDirection = ((SortDirection)viewState["EntitySortDirection"]).ToString();
                        else if (string.IsNullOrEmpty((string)viewState["EntitySortProperty"]) && dcf.SortExpression == defaultSortExpression)
                        {
                            sortDirection = defaultSortDirection.ToString();
                            viewState["EntitySortProperty"] = defaultSortExpression;
                            viewState["EntitySortDirection"] = defaultSortDirection;
                        }
                        else
                            sortDirection = "Unsorted";

                    } 
                    else
                    {

                        if (dcf.SortExpression == gridView.SortExpression)
                            sortDirection = gridView.SortDirection.ToString();
                        else if (string.IsNullOrEmpty(gridView.SortExpression) && dcf.SortExpression == defaultSortExpression)
                            sortDirection = defaultSortDirection.ToString();
                        else
                            sortDirection = "Unsorted";

                    }

                    Image sortImage = new Image();
                    sortImage.ImageUrl = string.Format(sortImagesFolder + "sort_{0}.gif", sortDirection.ToLower());
                    sortImage.AlternateText = string.Format("Sort Order: {0}", sortDirection);
                    sortImage.ToolTip = string.Format("Sort Order: {0}", sortDirection);
                    sortImage.Style.Add("margin-left", "5px");

                    gridView.HeaderRow.Cells[gridView.Columns.IndexOf(dcf)].Controls.Add(sortImage);

                }
            }

        } // CreateSortArrows

    } // GridViewTools

}