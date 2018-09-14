using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LocalNotes.Site.Server
{
    public class SqlNoteData
    {
        // Fields for the development environment
        private readonly string _dataSource = "SANDBOX";
        private readonly string _initialCatalog = "LocalNotes";
        private readonly string _tableName = "TestData";

        /// <summary>
        /// Return a special DataSet of note details.
        /// NoteID: The note ID number.
        /// NoteName: The name of the note.
        /// NoteAbbr: Abbreviated part of the note text.
        /// NoteTime: The time the note was created or updated.
        /// Modified: A formulated and friendly string of when the note was created or updated from the time of the query.
        /// </summary>
        /// <param name="isRemove">Specify returning records marked as junk.</param>
        /// <param name="lenAbbrText">Specify the length of the abbreviated NoteText value to return.</param>
        /// <param name="isSortDesc">Specify the sort direction in descending order to return the the more recently modified or newer notes first.</param>
        /// <returns>DataSet of note details.</returns>
        public DataSet SelectNoteDataSet(bool isRemove, int lenAbbrText, bool isSortDesc)
        {
            string strConnString = GetConnStringMsSql();
            string strCmdText = "spSelectNoteData";
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@isRemove", isRemove);
                        command.Parameters.AddWithValue("@lenAbbrText", lenAbbrText);
                        command.Parameters.AddWithValue("@isSortDesc", isSortDesc);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(ds, _tableName);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }

        /// <summary>
        /// Update the detail for an existing note.
        /// </summary>
        /// <param name="noteID" type="int">The note ID.</param>
        /// <param name="noteName" type="string">The note name.</param>
        /// <param name="noteText" type="string">The note text.</param>
        public void UpdateNoteDetail(int noteID, string noteName, string noteText)
        {
            string strConnString = GetConnStringMsSql();
            string strCmdText = "spUpdateNoteDetail";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@noteID", noteID);
                        command.Parameters.AddWithValue("@noteName", noteName);
                        command.Parameters.AddWithValue("@noteText", noteText);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new note.
        /// </summary>
        /// <param name="noteName" type="string">The note name.</param>
        /// <param name="noteText" type="string">The note text.</param>
        /// <returns>The note ID for the new note record.</returns>
        public int CreateNote(string noteName, string noteText)
        {
            int intReturn = 0;

            string strConnString = GetConnStringMsSql();
            string strCmdText = "spInsertNoteData";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@noteName", noteName);
                        command.Parameters.AddWithValue("@noteText", noteText);

                        connection.Open();
                        intReturn = (int)command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
            return intReturn;
        }

        /// <summary>
        /// Mark the note for removal.
        /// </summary>
        /// <param name="noteID" type="int">The note ID.</param>
        public void RemoveNote(int noteID)
        {
            string strConnString = GetConnStringMsSql();
            string strCmdText = "spUpdateNoteRemove";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@noteID", noteID);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Mark the note to be restored.
        /// </summary>
        /// <param name="noteID" type="int">The note ID.</param>
        public void RestoreNote(int noteID)
        {
            string strConnString = GetConnStringMsSql();
            string strCmdText = "spUpdateNoteRestore";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@noteID", noteID);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Select active notes not marked for removal.
        /// </summary>
        /// <param name="noteID" type="int">The note ID.</param>
        /// <returns>DataSet of active notes.</returns>
        public DataSet SelectActiveNote(int noteID)
        {
            string strConnString = GetConnStringMsSql();
            string strCmdText = "spSelectActiveNote";
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@noteID", noteID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(ds, _tableName);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }

        #region HELPER METHODS

        /// <summary>
        /// Obtain the connection string for the SQL data source.
        /// </summary>
        /// <returns>The connection string.</returns>
        private string GetConnStringMsSql()
        {
            string strReturn = string.Empty;
            //strReturn = "Data Source=SANDBOX;Initial Catalog=LocalNotes;Integrated Security=True";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = _dataSource,
                InitialCatalog = _initialCatalog,
                IntegratedSecurity = true
            };

            strReturn = builder.ToString();
            return strReturn;
        }

        #endregion
    }
}