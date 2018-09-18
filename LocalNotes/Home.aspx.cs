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
    public partial class Home : System.Web.UI.Page
    {
        // Declare fields for class objects.
        private SqlNoteData _sqlNoteData;
        private TextLogging _textLogging;

        // Default constructor to instantiate classes.
        public Home()
        {
            _sqlNoteData = new SqlNoteData();
            _textLogging = new TextLogging();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // The display logic for note item data ...
                DisplayNoteData();
            }
            else
            {
                // Handle PostBack
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            // Navigate to the note page ...
            Response.Redirect("~/Note.aspx", false);
        }

        #region HELPER SECTION

        /// <summary>
        /// This method is used to select and bind data about notes that are marked
        /// as junk to the form control. If there are no records, an update panel is
        /// shown to reflect that. Otherwise, the data is bound to a repeater control
        /// to display the data.
        /// </summary>
        private void DisplayNoteData()
        {
            // Define the SelectNoteDataSet method input parameters
            bool isRemove = false;  // Set to false for note records, true for junk records marked as isRemove.
            int lenTextAbbr = 25;   // The number of characters of note text to return as abbreviated text.
            bool isSortDesc = true; // Set to true for returned records in descending order, false for ascending.

            try
            {
                // Obtain the DataSet and place into a DataTable ...
                DataTable noteTable = _sqlNoteData.SelectNoteDataSet(isRemove, lenTextAbbr, isSortDesc).Tables[0];

                // Count the number of returned records ...
                int intRecordCount = noteTable.Rows.Count;

                // Update the hidden page field with the number of records ...
                hidRecords.Value = intRecordCount.ToString();

                if (intRecordCount == 0)
                {
                    // Show the Update Panel ...
                    UpdatePanelDataAbsent.Visible = true;

                    // Assign the message ...
                    lblDataAbsent.Text = "Click the '" + btnCreate.Text + "' button to begin writing your note. :-)";
                }
                else
                {
                    // Hide the Update Panel ...
                    UpdatePanelDataAbsent.Visible = false;

                    // Assign records found data table to the form control ...
                    RepeaterItemDetail.DataSource = noteTable;
                    // Bind the data to the form control ...
                    RepeaterItemDetail.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception ...
                HandleAppError(ex);
            }
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