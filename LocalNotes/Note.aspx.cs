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
    public partial class Note : System.Web.UI.Page
    {
        // Declare fields for class objects.
        private SqlNoteData _sqlNoteData;
        private TextLogging _textLogging;

        // Default constructor to instantiate classes.
        public Note()
        {
            _sqlNoteData = new SqlNoteData();
            _textLogging = new TextLogging();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Query the URI for a noteid parameter ...
                string strNoteID = Request.QueryString["noteid"];

                // Confirm the noteID value is present and numeric ...
                if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
                {
                    // Check for an existing record for the 'noteid' ...
                    NoteDataCheck(intNoteID);
                }
                else
                {
                    // Prepare this page for a fresh new note ...
                    NoteDataFresh();
                }
            }
            else
            {
                // Handle PostBack
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Obtain the noteID value from the hidden checked form field,
            // which is assigned when the note is loaded or saved.
            string strNoteID = hidNoteID.Value;

            // Obtain note details for further comparison.
            string oldNoteName = hidNoteName.Value;
            string oldNoteText = hidNoteText.Value;
            string newNoteName = txtNoteName.Text;
            string newNoteText = txtNoteText.Text;

            // Confirm the noteID value is present and numeric ...
            if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
            {
                try
                {
                    // Confirm there is note detail that needs to be updated ...
                    if (!(oldNoteName == newNoteName) || !(oldNoteText == newNoteText))
                    {
                        // Evaluate the note ID ...
                        if (intNoteID == 0)
                        {
                            // Create a brand new note for note ID 0 ...
                            intNoteID = _sqlNoteData.CreateNote(newNoteName, newNoteText);
                            hidNoteID.Value = intNoteID.ToString();
                            lblActionStatus.Text = String.Format("Note {0} was created.", intNoteID);
                            lblActionStatus.Visible = true;
                        }
                        else
                        {
                            // Update an existing note by the note ID ...
                            _sqlNoteData.UpdateNoteDetail(intNoteID, newNoteName, newNoteText);
                            lblActionStatus.Text = String.Format("Note {0} was updated.", intNoteID);
                            lblActionStatus.Visible = true;
                        }

                        // Update the hidden fields to hold onto the created/updated note detail ...
                        hidNoteName.Value = newNoteName;
                        hidNoteText.Value = newNoteText;

                        // Disable the save button ...
                        btnSave.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception ...
                    HandleAppError(ex);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Navigate to the home page ...
            GoToHomePage();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            // Obtain the noteID value from the hidden checked form field,
            // which is assigned when the note is loaded or saved.
            string strNoteID = hidNoteID.Value;

            try
            {
                // Confirm the noteID value is present and numeric ...
                if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
                {
                    // Restore the note by the note ID ...
                    _sqlNoteData.RemoveNote(intNoteID);

                    // Navigate to the home page ...
                    GoToHomePage();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception ...
                HandleAppError(ex);
            }
        }

        #region HELPER METHODS

        /// <summary>
        /// This helper method checks to see if the note ID exists for an active record,
        /// and further handles a found or empty record.
        /// </summary>
        /// <param name="noteID"></param>
        private void NoteDataCheck(int noteID)
        {
            try
            {
                // Select the note data into a DataTable ...
                DataTable noteTable = _sqlNoteData.SelectActiveNote(noteID).Tables[0];

                // Count the number of returned record rows ...
                int intRecordCount = noteTable.Rows.Count;

                // Count the number of returned record columns ...
                int intColumnCount = noteTable.Columns.Count;

                // We are expecting a single record returned based on the note ID with
                // three columns of data for that note (NoteName, NoteText, NoteTime).
                if ((intRecordCount == 1) && (intColumnCount == 3))
                {
                    // Pass the data table to the method for further processing ...
                    NoteDataFound(noteID, noteTable);
                }
                else
                {
                    // Handle an empty data scenario ...
                    NoteDataEmpty();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception ...
                HandleAppError(ex);
            }
        }

        /// <summary>
        /// This helper method prepares the page elements to create a fresh new note.
        /// </summary>
        private void NoteDataFresh()
        {
            // Assign a fresh new note with a noteID of 0 ...
            hidNoteID.Value = "0";
            hidNoteName.Value = String.Empty;
            hidNoteText.Value = String.Empty;

            txtNoteName.Text = String.Empty;
            txtNoteText.Text = String.Empty;

            UpdatePanelDataAbsent.Visible = false;
            UpdatePanelNoteFields.Visible = true;
            UpdatePanelNoteAction.Visible = true;

            // Place the cursor in the field ...
            txtNoteName.Focus();
        }

        /// <summary>
        /// After evaluating the DataSet for an active note, pass the data as a DataTable.
        /// This helper method gathers the note values, and assigns the values to their
        /// respective fields, and updates additional page elements.
        /// </summary>
        /// <param name="noteID">The note ID.</param>
        /// <param name="noteTable">The note as DataTable type.</param>
        private void NoteDataFound(int noteID, DataTable noteTable)
        {
            string strNoteName = String.Empty;
            string strNoteText = String.Empty;
            foreach (DataRow row in noteTable.Rows)
            {
                strNoteName = row["NoteName"].ToString();
                strNoteText = row["NoteText"].ToString();
            }

            hidNoteID.Value = noteID.ToString();
            hidNoteName.Value = strNoteName;
            hidNoteText.Value = strNoteText;

            txtNoteName.Text = strNoteName;
            txtNoteText.Text = strNoteText;

            UpdatePanelDataAbsent.Visible = false;
            UpdatePanelNoteFields.Visible = true;
            UpdatePanelNoteAction.Visible = true;
        }

        /// <summary>
        /// This helper method updates the page elements to show that there was no note
        /// record found.
        /// </summary>
        private void NoteDataEmpty()
        {
            lblDataAbsent.Text = "The note was not found.";
            UpdatePanelDataAbsent.Visible = true;
            UpdatePanelNoteFields.Visible = false;
            UpdatePanelNoteAction.Visible = false;
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