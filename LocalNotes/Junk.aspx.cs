using LocalNotes.Site.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalNotes
{
    public partial class Junk : System.Web.UI.Page
    {
        // Declare fields for class objects.
        private SqlNoteData _sqlNoteData;
        private TextLogging _textLogging;

        // Default constructor to instantiate classes.
        public Junk()
        {
            _sqlNoteData = new SqlNoteData();
            _textLogging = new TextLogging();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Call the display logic for note item data ...
                DisplayJunkData();
            }
            else
            {
                // Handle PostBack
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            // Obtain the value checked items selected in the hidden checked form field ...
            string strChecked = hidChecked.Value;
            Console.WriteLine("strChecked: {0}", strChecked);

            // Determine if there are checked items ...
            if (!String.IsNullOrEmpty(strChecked))
            {
                // The checked item values are comma delimited...
                string[] arrParts = strChecked.Split(',');

                // Iterate through each item value ...
                foreach (var item in arrParts)
                {
                    // Each item value represents the numeric note ID ...
                    string strNoteID = item;

                    // Confirm the noteID value is present and numeric ...
                    if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
                    {
                        // Restore the note by the note ID ...
                        _sqlNoteData.RestoreNote(intNoteID);
                    }
                }

                // Clear the hidChecked value ...
                hidChecked.Value = String.Empty;

                // Navigate to the home page ...
                GoToHomePage();
            }
        }

        #region HELPER SECTION

        /// <summary>
        /// This method is used to select and bind junk item data to the form control. If there
        /// are no records, the panel indicating that there are no records is shown. Otherwise,
        /// the records are bound to the Repeater control to display the data.
        /// </summary>
        private void DisplayJunkData()
        {
            // Define the SelectNoteDataSet method input parameters
            bool isRemove = true;   // Set to false for note records, true for junk records marked as isRemove.
            int lenTextAbbr = 25;   // The number of characters of note text to return as abbreviated text.
            bool isSortDesc = true; // Set to true for returned records in descending order, false for ascending.

            try
            {
                // Obtain the DataSet and place into a DataTable ...
                DataTable junkTable = _sqlNoteData.SelectNoteDataSet(isRemove, lenTextAbbr, isSortDesc).Tables[0];

                // Count the number of returned records ...
                int intRecordCount = junkTable.Rows.Count;

                if (intRecordCount == 0)
                {
                    // Show the Update Panel ...
                    UpdatePanelDataAbsent.Visible = true;

                    // Assign the message ...
                    lblDataAbsent.Text = "There is no junk. :-)";

                    // Disable the Restore button ...
                    btnRestore.Enabled = false;
                }
                else
                {
                    // Hide the Update Panel ...
                    UpdatePanelDataAbsent.Visible = false;

                    // Assign records found data table to the form control ...
                    RepeaterItemDetail.DataSource = junkTable;
                    // Bind the data to the form control ...
                    RepeaterItemDetail.DataBind();

                    // Enable the Restore button ...
                    btnRestore.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception ...
                HandleAppError(ex);
            }
        }

        /// <summary>
        /// This helper method redirects to the Home page.
        /// </summary>
        private void GoToHomePage()
        {
            Response.Redirect("~/Home.aspx", false);
        }

        /// <summary>
        /// This method is for handling an exception by presenting information to the
        /// page for the user and logging details about the exception.
        /// </summary>
        /// <param name="ex">The exception</param>
        private void HandleAppError(Exception ex)
        {
            // Show the Update Panel ...
            UpdatePanelDataAbsent.Visible = true;

            // Assign the user message ...
            lblDataAbsent.Text = "Oh no, something went wrong :-(" + "<br />"
                + "<br />"
                + "We've logged system details about the issue for further investigation." + "<br />"
                + "[" + ex.HResult + "]<br />"
                + "<br />"
                + "Please notify your support team." + "<br />"
                + "<i>support@your.org</i>";

            // Log a message ...
            _textLogging.LogMessage("The following error occurred: " + ex.HResult);

            // Log the exception ...
            _textLogging.LogMessage(ex);
        }

        #endregion
    }
}