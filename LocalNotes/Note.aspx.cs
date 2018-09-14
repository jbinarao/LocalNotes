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
        // Declare object for the SQL note data.
        private SqlNoteData _sqlNoteData;

        // Default constructor to instantiate SqlNoteData.
        public Note()
        {
            _sqlNoteData = new SqlNoteData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Handle GET request ...

                // Check the URI for a noteid parameter ...
                string strNoteID = Request.QueryString["noteid"];
                if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
                {
                    // a check for an existing record ...
                    NoteDataCheck(intNoteID);
                }
                else
                {
                    // a fresh new note ...
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
            string strNoteID = hidNoteID.Value;

            string oldNoteName = hidNoteName.Value;
            string oldNoteText = hidNoteText.Value;
            string newNoteName = txtNoteName.Text;
            string newNoteText = txtNoteText.Text;

            if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
            {
                if (!(oldNoteName == newNoteName) || !(oldNoteText == newNoteText))
                {
                    if (intNoteID == 0)
                    {
                        intNoteID = _sqlNoteData.CreateNote(newNoteName, newNoteText);
                        hidNoteID.Value = intNoteID.ToString();
                        lblSaveStatus.Text = String.Format("Note {0} was created.", intNoteID);
                        lblSaveStatus.Visible = true;
                    }
                    else
                    {
                        _sqlNoteData.UpdateNoteDetail(intNoteID, newNoteName, newNoteText);
                        lblSaveStatus.Text = String.Format("Note {0} was updated.", intNoteID);
                        lblSaveStatus.Visible = true;
                    }
                    hidNoteName.Value = newNoteName;
                    hidNoteText.Value = newNoteText;
                    btnSave.Enabled = false;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GoToHomePage();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            string strNoteID = hidNoteID.Value;

            if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
            {
                _sqlNoteData.RemoveNote(intNoteID);
                GoToHomePage();
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
            DataTable noteTable = _sqlNoteData.SelectActiveNote(noteID).Tables["TestData"];
            int intRecordCount = noteTable.Rows.Count;
            int intColumnCount = noteTable.Columns.Count;
            if ((intRecordCount == 1) && (intColumnCount == 3))
            {
                NoteDataFound(noteID, noteTable);
            }
            else
            {
                NoteDataEmpty();
            }
        }

        /// <summary>
        /// This helper method prepares the page elements to create a fresh new note.
        /// </summary>
        private void NoteDataFresh()
        {
            hidNoteID.Value = "0";
            hidNoteName.Value = String.Empty;
            hidNoteText.Value = String.Empty;

            txtNoteName.Text = String.Empty;
            txtNoteText.Text = String.Empty;

            UpdatePanelNoteAbsent.Visible = false;
            UpdatePanelNoteFields.Visible = true;
            UpdatePanelNoteAction.Visible = true;
        }

        /// <summary>
        /// After evaluating the DataSet for an active note, pass the data as a pipe-
        /// delimited string where the [NoteName] is first index, then the [NoteText].
        /// This helper method gathers the note values, and assigns the values to their
        /// respective fields, and updates additional page elements.
        /// </summary>
        /// <param name="noteID">The note ID.</param>
        /// <param name="noteData">The note details as a pipe-delimited string.</param>
        private void NoteDataFound(int noteID, string noteData)
        {
            string[] arrParts = noteData.Split('|');
            string strNoteName = arrParts[0];
            string strNoteText = arrParts[1];

            hidNoteID.Value = noteID.ToString();
            hidNoteName.Value = strNoteName;
            hidNoteText.Value = strNoteText;

            txtNoteName.Text = strNoteName;
            txtNoteText.Text = strNoteText;

            UpdatePanelNoteAbsent.Visible = false;
            UpdatePanelNoteFields.Visible = true;
            UpdatePanelNoteAction.Visible = true;
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

            UpdatePanelNoteAbsent.Visible = false;
            UpdatePanelNoteFields.Visible = true;
            UpdatePanelNoteAction.Visible = true;
        }

        /// <summary>
        /// This helper method updates the page elements to show that there was no note
        /// record found.
        /// </summary>
        private void NoteDataEmpty()
        {
            lblNoteAbsent.Text = "The note was not found.";
            UpdatePanelNoteAbsent.Visible = true;
            UpdatePanelNoteFields.Visible = false;
            UpdatePanelNoteAction.Visible = false;
        }

        /// <summary>
        /// This helper method redirects to the Home page.
        /// </summary>
        private void GoToHomePage()
        {
            Response.Redirect("~/Home.aspx");
        }

        #endregion
    }
}