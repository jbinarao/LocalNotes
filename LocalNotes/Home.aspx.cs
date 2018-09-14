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
        // Declare object for the SQL note data.
        private SqlNoteData _sqlNoteData;

        // Default constructor to instantiate SqlNoteData.
        public Home()
        {
            _sqlNoteData = new SqlNoteData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable noteTable = _sqlNoteData.SelectNoteDataSet(false, 20, true).Tables["TestData"];

                int intRecordCount = noteTable.Rows.Count;

                // testing to simulate no records ...
                //intRecordCount = 0;

                hidRecords.Value = intRecordCount.ToString();

                if (intRecordCount == 0)
                {
                    // Show the UpdatePanelNoteAbsent ...
                    UpdatePanelNoteAbsent.Visible = true;

                    // Update the message for no notes ...
                    lblNoteAbsent.Text = "Click to create your first note. :-)";
                }
                else
                {
                    // Hide the UpdatePanelNoteAbsent ...
                    UpdatePanelNoteAbsent.Visible = false;

                    // Update the data source and data binding ...
                    RepeaterItemDetail.DataSource = noteTable;
                    RepeaterItemDetail.DataBind();
                }
            }
            else
            {
                // Handle PostBack
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Note.aspx");
        }
    }
}