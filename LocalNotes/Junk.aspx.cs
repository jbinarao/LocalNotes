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
        // Declare object for the SQL note data.
        private SqlNoteData _sqlNoteData;

        // Default constructor to instantiate SqlNoteData.
        public Junk()
        {
            _sqlNoteData = new SqlNoteData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string strSelect = hidChecked.Value;
            Console.WriteLine("select: {0}", strSelect);

            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(strSelect))
                {
                    // A checkbox was selected
                    Console.WriteLine("select: {0}", strSelect);
                }
                else
                {
                    // Get the junk data ...
                    JunkListDataBind();
                }
            }
            else
            {
                // Handle PostBack

                if (!String.IsNullOrEmpty(strSelect))
                {
                    // A checkbox was selected ...
                    Console.WriteLine("select: {0}", strSelect);
                }
                else
                {
                    // Get the junk data ...
                    JunkListDataBind();
                }

            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            string strSelect = hidChecked.Value;
            Console.WriteLine("select: {0}", strSelect);

            if (!String.IsNullOrEmpty(strSelect))
            {
                string[] arrParts = strSelect.Split(',');
                foreach (var item in arrParts)
                {
                    string strNoteID = item;
                    if (!String.IsNullOrEmpty(strNoteID) && Int32.TryParse((strNoteID), out int intNoteID))
                    {
                        _sqlNoteData.RestoreNote(intNoteID);
                    }
                    
                    DataTable junkTable = _sqlNoteData.SelectNoteDataSet(true, 50, true).Tables["TestData"];
                    int intRecordCount = junkTable.Rows.Count;
                    Console.WriteLine("record count: ", intRecordCount);
                }

                // Clear the hidChecked value ...
                hidChecked.Value = String.Empty;

                // Navigate to the home page ...
                GoToHomePage();
            }
        }

        #region HELPER SECTION

        /// <summary>
        /// 
        /// </summary>
        private void JunkListDataBind()
        {
            DataTable junkTable = _sqlNoteData.SelectNoteDataSet(true, 50, true).Tables["TestData"];
            int intRecordCount = junkTable.Rows.Count;

            if (intRecordCount == 0)
            {
                UpdatePanelJunkAbsent.Visible = true;

                lblJunkAbsent.Text = "There is no junk. :-)";
            }
            else
            {
                UpdatePanelJunkAbsent.Visible = false;

                RepeaterItemDetail.DataSource = junkTable;
                RepeaterItemDetail.DataBind();
            }
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